using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Text { get; set; }
        public Picture Picture { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
        public List<Comment> Comments { get; set; }

        public byte[] GetPostPicture()
        {
            return null;
        }

        public bool SavePostPicture(byte[] image)
        {
            return false;
        }

        public List<Comment> GetPostComments()
        {
            return null;
        }
    }
}
