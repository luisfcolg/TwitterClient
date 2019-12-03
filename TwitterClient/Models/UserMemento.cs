using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    class UserMemento
    {
        private User user = new User();

        public UserMemento(User user)
        {
            this.user.Id = user.Id;
            this.user.Name = user.Name;
            this.user.Username = user.Username;
            this.user.Password = user.Password;
            this.user.Name = user.Name;
            this.user.Phone = user.Phone;
            this.user.Email = user.Email;
            this.user.ProfilePicture = user.ProfilePicture;
            this.user.MemberSince = user.MemberSince;
            this.user.Following= user.Following;
            this.user.Followers = user.Followers;
            this.user.Bio= user.Bio;
            this.user.Location = user.Location;
            this.user.BirthDate= user.BirthDate;
            this.user.Posts= user.Posts;
            this.user.SavedPosts = user.SavedPosts;
            this.user.Notifications= user.Notifications;
        }

        public User GetSavedUser()
        {
            return user;
        }
    }
}
