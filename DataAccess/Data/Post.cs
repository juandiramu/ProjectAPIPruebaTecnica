using System;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Type { get; set; }
        public string Category { get; set; }
        public int CustomerId { get; set; }
        public Post()
        {

        }
        public Post(string title, string body, int type, int customerId)
        {
            Title = title;
            Body = body;
            Type = type;
            CustomerId = customerId;

        }
    }
}
