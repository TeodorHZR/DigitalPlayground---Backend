using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            try
            {
                var teams = _teamRepository.GetAll();
                return Ok(teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving teams.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamById(int id)
        {
            var team = _teamRepository.GetById(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost("create")]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            try
            {
                _teamRepository.Insert(team);
                return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating team.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, [FromBody] Team team)
        {
            var existingTeam = _teamRepository.GetById(id);
            if (existingTeam == null)
            {
                return NotFound();
            }

            team.Id = id;
            _teamRepository.Update(team);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var existingTeam = _teamRepository.GetById(id);
            if (existingTeam == null)
            {
                return NotFound();
            }

            _teamRepository.Delete(id);

            return NoContent();
        }
    }
}
