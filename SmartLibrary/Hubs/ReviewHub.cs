using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Hubs
{
    public class ReviewHub : Hub
    {
        public async Task SendReview(int reviewId, string userName, int rating, string comment)
        {
            string createdAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            await Clients.All.newReview(reviewId, userName, rating, comment, createdAt);
        }

        public void SendReviewUpdate(int reviewId, string userName, int rating, string review, string createdAt)
        {
            Clients.All.updateReview(reviewId, userName, rating, review, createdAt);
        }

        public void SendReviewDelete(int reviewId)
        {
            Clients.All.deleteReview(reviewId);
        }
    }
}