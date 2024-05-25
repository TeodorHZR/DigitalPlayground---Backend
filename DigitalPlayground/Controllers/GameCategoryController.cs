using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using DigitalPlayground.Data.Repositories;
using DigitalPlayground.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class GameCategoryController : Controller
    {
        private readonly IGameCategoryRepository _gameCategoryRepository;
        public GameCategoryController(IGameCategoryRepository gameCategoryRepository)
        {
            _gameCategoryRepository = gameCategoryRepository;
        }
        [HttpPost("add")]
        public void AddGameToCategory([FromBody] GameCategoryModel model)
        {
            var cat = new GameCategory(model.GameId, model.CategoryId);

            _gameCategoryRepository.AddGameToCategory(cat);
        }
        [HttpDelete("remove")]
        public void RemoveGameToCategory([FromBody] GameCategoryModel model)
        {
            var cat = new GameCategory(model.GameId, model.CategoryId);

            _gameCategoryRepository.RemoveGameFromCategory(cat);
        }
        [HttpGet("categories/{gameId}")]
        public IActionResult GetCategoriesForGame(int gameId)
        {
            try
            {
                var categories = _gameCategoryRepository.GetCategoriesForGame(gameId);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving categories for game.");
            }
        }

        [HttpGet("games/{categoryId}")]
        public IActionResult GetGamesForCategory(int categoryId)
        {
            try
            {
                var games = _gameCategoryRepository.GetGamesForCategory(categoryId);
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving games for category.");
            }
        }
    }
}
