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
                if (model.Date == default(DateTime))
                {
                    model.Date = DateTime.UtcNow;
                }

                var gameTransaction = new GameTransaction(model.Id, model.UserId, model.Amount, model.GameId, model.Date);
                var insertedId = _gameTransactionRepository.Insert(gameTransaction);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating game transaction: {ex.Message}");
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
                    return NotFound("Tranzacția de joc nu a fost găsită.");
                }
                return Ok(gameTransaction);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Eroare la preluarea tranzacției de joc: {ex.Message}");
                return StatusCode(500, "Eroare la preluarea tranzacției de joc.");
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

        [HttpGet("user/{userId}/purchasedGames")]
        public IActionResult GetUserPurchasedGames(int userId, int offset = 0, int limit = 5)
        {
            try
            {
                var games = _gameTransactionRepository.GetUserPurchasedGames(userId, offset, limit);
                var totalGames = _gameTransactionRepository.GetTotalUserPurchasedGames(userId);
                var result = new
                {
                    Games = games,
                    TotalCount = totalGames
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving purchased games.");
            }
        }
        [HttpGet("all-transactions")]
        public IActionResult GetAllTransactions()
        {
            try
            {
                var transactions = _gameTransactionRepository.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("sales-statistics")]
        public IActionResult GetGameSalesStatistics()
        {
            var statistics = _gameTransactionRepository.GetGameSalesStatistics();
            return Ok(statistics);
        }

    }
}
