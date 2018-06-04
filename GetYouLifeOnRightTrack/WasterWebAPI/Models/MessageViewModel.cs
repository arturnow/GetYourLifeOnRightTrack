using System;

namespace WasterWebAPI.Models
{
    public class MessageViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }
        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public bool RequireConfirmation { get; set; }

        public DateTime DueDateTime { get; set; }
    }
}