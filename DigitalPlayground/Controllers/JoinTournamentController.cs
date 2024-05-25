using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System;
using DigitalPlayground.Models;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinTournamentController : ControllerBase
    {
        private readonly IJoinTournamentRepository _joinTournamentRepository;

        public JoinTournamentController(IJoinTournamentRepository joinTournamentRepository)
        {
            _joinTournamentRepository = joinTournamentRepository;
        }

        [HttpPost("join")]
        public IActionResult JoinTournament([FromBody] JoinTournamentModel model)
        {
            try
            {
                var joinTournament = new JoinTournament(model.Id, model.TournamentId, model.UserId);
                var insertedId = _joinTournamentRepository.InsertOrUpdate(joinTournament);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error joining tournament.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var joinTournament = _joinTournamentRepository.GetById(id);
                if (joinTournament == null)
                {
                    return NotFound("Join tournament not found.");
                }
                return Ok(joinTournament);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving join tournament.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] JoinTournamentModel model)
        {
            try
            {
                var existingJoinTournament = _joinTournamentRepository.GetById(id);
                if (existingJoinTournament == null)
                {
                    return NotFound("Join tournament not found.");
                }

                // Update the existing join tournament
                existingJoinTournament.TournamentId = model.TournamentId;
                existingJoinTournament.UserId = model.UserId;

                _joinTournamentRepository.Update(existingJoinTournament);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating join tournament.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingJoinTournament = _joinTournamentRepository.GetById(id);
                if (existingJoinTournament == null)
                {
                    return NotFound("Join tournament not found.");
                }

                _joinTournamentRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting join tournament.");
            }
        }
    }
}
