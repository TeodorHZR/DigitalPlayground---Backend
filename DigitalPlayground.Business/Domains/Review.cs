using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class Review
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }

        public Review() { }
        public Review(int reviewId, string message, int rating, int gameId, int userId)
        {
            Id = reviewId;
            Message = message;
            Rating = rating;
            GameId = gameId;
            UserId = userId;
        }
    }
}
