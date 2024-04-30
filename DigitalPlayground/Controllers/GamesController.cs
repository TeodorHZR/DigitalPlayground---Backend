using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using DigitalPlayground.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        [HttpGet("{offset?}/{limit?}")]
        public List<GameModel> Get([FromRoute] int offset = 0, [FromRoute] int limit = 100, [FromQuery] string sortOrder = "ASC")
        {
            var validSortOrders = new[] { "ASC", "DESC" };
            if (!validSortOrders.Contains(sortOrder.ToUpper()))
            {
                throw new ArgumentException("Invalid sortOrder.");
            }

            var games = gameRepository.GetAll(offset, limit);
            return games.Select(x => new GameModel(x)).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<GameModel> GetById([FromRoute] int id)
        {
            var game = gameRepository.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            return new GameModel(game);
        }

        [HttpPost("")]
        public ActionResult<int> Insert([FromBody] GameModel game)
        {
            var newGame = new Game(game.Id, game.Name, game.Description, game.Price, game.Rating);
            var gameId = gameRepository.Insert(newGame);
            return gameId;
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GameModel gameModel)
        {
            var existingGame = gameRepository.GetById(id);
            if (existingGame == null)
            {
                return NotFound();
            }

            existingGame.Name = gameModel.Name;
            existingGame.Description = gameModel.Description;
            existingGame.Price = gameModel.Price;
            existingGame.Rating = gameModel.Rating;

            gameRepository.Update(existingGame);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var existingGame = gameRepository.GetById(id);
            if (existingGame == null)
            {
                return NotFound();
            }

            gameRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("paginated")]
        public ActionResult<IEnumerable<GameModel>> GetPaginated([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 100, [FromQuery] string sortOrder = "ASC")
        {
            try
            {
                var validSortOrders = new[] { "ASC", "DESC" };
                if (!validSortOrders.Contains(sortOrder.ToUpper()))
                {
                    return BadRequest("Invalid sortOrder.");
                }

                if (page <= 0 || itemsPerPage <= 0)
                {
                    return BadRequest("Invalid page or itemsPerPage values. They should be positive integers.");
                }

                var games = gameRepository.GetAll((page - 1) * itemsPerPage, itemsPerPage);
                return games.Select(x => new GameModel(x)).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving paginated games.");
            }
        }
    }
}
