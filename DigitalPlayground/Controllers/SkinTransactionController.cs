using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTransactionController : ControllerBase
    {
        private readonly ISkinTransactionRepository _skinTransactionRepository;

        public SkinTransactionController(ISkinTransactionRepository skinTransactionRepository)
        {
            _skinTransactionRepository = skinTransactionRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] SkinTransaction skinTransaction)
        {
            try
            {
                var insertedId = _skinTransactionRepository.Insert(skinTransaction);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating skin transaction.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var skinTransaction = _skinTransactionRepository.GetById(id);
            if (skinTransaction == null)
            {
                return NotFound();
            }
            return Ok(skinTransaction);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SkinTransaction skinTransaction)
        {
            var existingSkinTransaction = _skinTransactionRepository.GetById(id);
            if (existingSkinTransaction == null)
            {
                return NotFound();
            }

            skinTransaction.Id = id;
            _skinTransactionRepository.Update(skinTransaction);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingSkinTransaction = _skinTransactionRepository.GetById(id);
            if (existingSkinTransaction == null)
            {
                return NotFound();
            }

            _skinTransactionRepository.Delete(id);

            return NoContent();
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var skinTransactions = _skinTransactionRepository.GetAll();
            return Ok(skinTransactions);
        }
        [HttpGet("top3byprice")]
        public IActionResult GetTop3ByPrice()
        {
            var top3Skins = _skinTransactionRepository.GetTop3SkinsByPrice();
            return Ok(top3Skins);
        }



    }
}
