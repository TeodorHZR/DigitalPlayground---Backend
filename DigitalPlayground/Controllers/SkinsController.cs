using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System;
using DigitalPlayground.Models;
using DigitalPlayground.Data;
using DigitalPlayground.Data.Repositories;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinsController : ControllerBase
    {
        private readonly ISkinRepository _skinRepository;

        public SkinsController(ISkinRepository skinRepository)
        {
            _skinRepository = skinRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] SkinModel model)
        {
            try
            {
                var skin = new Skin(0, model.Name, model.Description, model.UserId, model.ImagePath, model.IsForSale, model.Price, model.GameId);
                var insertedId = _skinRepository.Insert(skin);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating skin.");
            }
        }

        [HttpGet("available/{userId}")]
        public ActionResult<IEnumerable<Skin>> GetAvailableForUser(int userId)
        {
            try
            {
                var availableSkins = _skinRepository.GetAllAvailableForUser(userId);
                return Ok(availableSkins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving skins.");
            }
        }
        [HttpGet("availableGame/{gameId}/{excludeUserId}")]
        public ActionResult<IEnumerable<Skin>> GetAvailableForGame(int gameId, int excludeUserId)
        {
            try
            {
                var availableSkins = _skinRepository.GetAllAvailableForGame(gameId, excludeUserId);
                return Ok(availableSkins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving skins.");
            }
        }

        [HttpPut("{skinId}/user/{userId}")]
        public IActionResult UpdateUser(int skinId, int userId)
        {
            try
            {
                _skinRepository.UpdateUser(skinId, userId);
                return Ok(new { message = "UserId updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the UserId.", error = ex.Message });
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<Skin>> GetAllSkins()
        {
            try
            {
                var skins = _skinRepository.GetAll();
                return Ok(skins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving skins.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var skin = _skinRepository.GetById(id);
                if (skin == null)
                {
                    return NotFound("Skin not found.");
                }
                return Ok(skin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving skin.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SkinModel model)
        {
            try
            {
                var existingSkin = _skinRepository.GetById(id);
                if (existingSkin == null)
                {
                    return NotFound("Skin not found.");
                }
                existingSkin.Name = model.Name;
                existingSkin.Description = model.Description;
                existingSkin.UserId = model.UserId;
                existingSkin.ImagePath = model.ImagePath;

                _skinRepository.Update(existingSkin);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating skin.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingSkin = _skinRepository.GetById(id);
                if (existingSkin == null)
                {
                    return NotFound("Skin not found.");
                }

                _skinRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting skin.");
            }
        }
        [HttpGet("maxPrice/{maxPrice}/game/{gameId}/excludeUser/{excludeUserId}")]
        public ActionResult<IEnumerable<Skin>> GetSkinsByMaxPrice(float maxPrice, int gameId, int excludeUserId)
        {
            var skins = _skinRepository.GetSkinsByMaxPrice(maxPrice, gameId, excludeUserId);
            return Ok(skins);
        }
        [HttpGet("orderByPrice")]
        public ActionResult<IEnumerable<Skin>> GetSkinsOrderedByPrice([FromQuery] bool ascending, [FromQuery] int gameId, [FromQuery] int excludeUserId)
        {
            var skins = _skinRepository.GetSkinsOrderedByPrice(ascending, gameId, excludeUserId);
            return Ok(skins);
        }
    }
}
