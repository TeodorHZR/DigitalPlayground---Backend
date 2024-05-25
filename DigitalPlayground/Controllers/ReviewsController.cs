using Microsoft.AspNetCore.Mvc;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System;
using DigitalPlayground.Models;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ReviewModel model)
        {
            try
            {
                var review = new Review(model.Id,model.Message, model.Rating, model.GameId, model.UserId);
                var insertedId = _reviewRepository.Insert(review);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating review.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var review = _reviewRepository.GetById(id);
                if (review == null)
                {
                    return NotFound("Review not found.");
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving review.");
            }
        }
        [HttpGet("reviews/{gameName}")]
        public IActionResult GetReviewMessagesByGame(string gameName)
        {
            try
            {
                var reviewMessages = _reviewRepository.GetReviewMessagesByGame(gameName);
                return Ok(reviewMessages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReviewModel model)
        {
            try
            {
                var existingReview = _reviewRepository.GetById(id);
                if (existingReview == null)
                {
                    return NotFound("Review not found.");
                }
                existingReview.Message = model.Message;
                existingReview.Rating = model.Rating;
                existingReview.GameId = model.GameId;
                existingReview.UserId = model.UserId;

                _reviewRepository.Update(existingReview);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating review.");
            }
        }
        [HttpPost("createOrUpdate")]
        public IActionResult CreateOrUpdate([FromBody] ReviewModel model)
        {
            try
            {
                var review = new Review(model.Id, model.Message, model.Rating, model.GameId, model.UserId);
                var insertedId = _reviewRepository.InsertOrUpdate(review);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating or updating review.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingReview = _reviewRepository.GetById(id);
                if (existingReview == null)
                {
                    return NotFound("Review not found.");
                }

                _reviewRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting review.");
            }
        }
        [HttpGet("messages")]
        public IActionResult GetReviewMessages(string gameName, int page = 1, int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and PageSize must be positive integers.");
            }

            int offset = (page - 1) * pageSize;

            var messages = _reviewRepository.GetReviewMessages(gameName, offset, pageSize);

            return Ok(messages);
        }
    }
}
