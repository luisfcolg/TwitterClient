using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Picture ProfilePicture { get; set; }
        public DateTime MemberSince { get; set; }
        public List<User> Following { get; set; }
        public List<User> Followers { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Tweet> Posts { get; set; }
        public List<Tweet> SavedPosts { get; set; }
        public List<Notification> Notifications { get; set; }

        public bool Post(Tweet tweet)
        {
            return false;
        }

        public bool Comment(Tweet tweet1, Tweet tweet2)
        {
            return false;
        }

        public bool Follow(User user)
        {
            return false;
        }

        public bool Unfollow(User user)
        {
            return false;
        }

        public byte[] GetProfilePicture()
        {
            return null;
        }

        public List<Tweet> GetTimeline()
        {
            return null;
        }

        public List<Tweet> GetSavedPosts()
        {
            return null;
        }

        public bool SavePost(Tweet tweet)
        {
            return false;
        }

        public List<Notification> GetNotifications()
        {
            return null;
        }

        public bool Notify(User user1, User user2)
        {
            return false;
        }

        public List<User> GetFollowers()
        {
            return null;
        }

        public List<User> GetFollowing()
        {
            return null;
        }
    }
}
