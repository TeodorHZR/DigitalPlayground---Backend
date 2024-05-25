using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System;
using DigitalPlayground.Models;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameTransactionController : ControllerBase
    {
        private readonly IGameTransactionRepository _gameTransactionRepository;

        public GameTransactionController(IGameTransactionRepository gameTransactionRepository)
        {
            _gameTransactionRepository = gameTransactionRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] GameTransactionModel model)
        {
            try
            {
                var gameTransaction = new GameTransaction(model.UserId, model.Amount, model.GameId);
                var insertedId = _gameTransactionRepository.Insert(gameTransaction);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating game transaction.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var gameTransaction = _gameTransactionRepository.GetById(id);
                if (gameTransaction == null)
                {
                    return NotFound("Game transaction not found.");
                }
                return Ok(gameTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving game transaction.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GameTransactionModel model)
        {
            try
            {
                var existingTransaction = _gameTransactionRepository.GetById(id);
                if (existingTransaction == null)
                {
                    return NotFound("Game transaction not found.");
                }
                existingTransaction.UserId = model.UserId;
                existingTransaction.Amount = model.Amount;
                existingTransaction.GameId = model.GameId;
                existingTransaction.Date = DateTime.Now;

                _gameTransactionRepository.Update(existingTransaction);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating game transaction.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingTransaction = _gameTransactionRepository.GetById(id);
                if (existingTransaction == null)
                {
                    return NotFound("Game transaction not found.");
                }

                _gameTransactionRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting game transaction.");
            }
        }
    }
}
