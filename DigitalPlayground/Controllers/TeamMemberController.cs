using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberRepository _teamMemberRepository;

        public TeamMemberController(ITeamMemberRepository teamMemberRepository)
        {
            _teamMemberRepository = teamMemberRepository;
        }

        [HttpPost]
        public IActionResult AddTeamMember([FromBody] TeamMember teamMember)
        {
            try
            {
                _teamMemberRepository.Insert(teamMember);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error adding team member.");
            }
        }

        [HttpDelete("{teamId}/{userId}")]
        public IActionResult RemoveTeamMember(int teamId, int userId)
        {
            try
            {
                _teamMemberRepository.Delete(teamId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error removing team member.");
            }
        }

        [HttpGet("{teamId}/member/{userId}/exists")]
        public IActionResult Exists(int teamId, int userId)
        {
            try
            {
                bool exists = _teamMemberRepository.GetById(teamId, userId);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error checking team member existence.");
            }
        }

        [HttpGet("{teamId}/players")]
        public IActionResult GetPlayersByTeam(int teamId)
        {
            try
            {
                var players = _teamMemberRepository.GetPlayersByTeam(teamId);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving players for team.");
            }
        }
    }
}
