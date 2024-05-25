using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentController(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        [HttpGet]
        public IActionResult GetAllTournaments()
        {
            try
            {
                var tournaments = _tournamentRepository.GetAll();
                return Ok(tournaments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving tournaments.");
            }
        }
        


        [HttpGet("{id}")]
        public IActionResult GetTournamentById(int id)
        {
            try
            {
                var tournament = _tournamentRepository.GetById(id);
                if (tournament == null)
                {
                    return NotFound();
                }
                return Ok(tournament);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving tournament.");
            }
        }

        [HttpPost]
        public IActionResult AddTournament([FromBody] Tournament tournament)
        {
            try
            {
                var insertedId = _tournamentRepository.Insert(tournament);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error adding tournament.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTournament(int id, [FromBody] Tournament tournament)
        {
            try
            {
                if (id != tournament.Id)
                {
                    return BadRequest("Tournament ID mismatch.");
                }
                _tournamentRepository.Update(tournament);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating tournament.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTournament(int id)
        {
            try
            {
                _tournamentRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting tournament.");
            }
        }

        [HttpGet("upcoming")]
        public IActionResult GetUpcomingTournament()
        {
            try
            {
                var tournament = _tournamentRepository.GetUpcomingTournament();
                if (tournament == null)
                {
                    return NotFound("No upcoming tournaments found.");
                }
                return Ok(tournament);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving upcoming tournament.");
            }
        }
    }
}
