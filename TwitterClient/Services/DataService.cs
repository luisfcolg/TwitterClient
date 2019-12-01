using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Controllers;
using TwitterClient.Models;

namespace TwitterClient.Services
{
    class DataService
    {
        private readonly DataServiceSingleton _dataService;

        public DataService(string connectionString)
        {
            _dataService = DataServiceSingleton.GetInstance(connectionString);
        }

        public Picture GetPicture(int idPicture)
        {
            try
            {
                return _dataService.GetPicture(idPicture);
            }
            catch
            {
                return null;
            }
        }

        public string SavePicture(Picture picture)
        {
            try
            {
                return _dataService.SavePicture(picture) ? "Picture saved successfully" : "Error in saving picture";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Post(Tweet tweet)
        {
            try
            {
                return _dataService.Post(tweet) ? "Tweet posted successfully" : "Error in posting tweet";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Comment(Comment comment)
        {
            try
            {
                return _dataService.Comment(comment) ? "Comment posted successfully" : "Error in posting comment";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Follow(int idFollower, int idFollowing)
        {
            try
            {
                return _dataService.Follow(idFollower, idFollowing) ? "Follow successful" : "Error in following";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Unfollow(int idFollower, int idFollowing)
        {
            try
            {
                return _dataService.Unfollow(idFollower, idFollowing) ? "Unfollow successful" : "Error in unfollowing";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string SavePost(int idUser, int idPost)
        {
            try
            {
                return _dataService.SavePost(idUser, idPost) ? "Post saved successfully" : "Error in saving post";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public Tweet GetPost(int idPost)
        {
            try
            {
                return _dataService.GetPost(idPost);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User GetUser(string username, string password)
        {
            try
            {
                return _dataService.GetUser(username, password);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User GetUserById(int idUser)
        {
            try
            {
                return _dataService.GetUserById(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Notification> GetNotifications(int idUser)
        {
            try
            {
                return _dataService.GetNotifications(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<User> GetFollowers(int idUser)
        {
            try
            {
                return _dataService.GetFollowers(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<User> GetFollowing(int idUser)
        {
            try
            {
                return _dataService.GetFollowing(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Comment> GetComments(int idPost)
        {
            try
            {
                return _dataService.GetComments(idPost);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Tweet> GetPosts(int idUser)
        {
            try
            {
                return _dataService.GetPosts(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Tweet> GetSavedPosts(int idUser)
        {
            try
            {
                return _dataService.GetSavedPosts(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool VerifyUser(string username, string email)
        {
            try
            {
                return _dataService.VerifyUser(username, email);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string AddUser(User user)
        {
            try
            {
                return _dataService.AddUser(user) ? "User registered successfully" : "Error in registering user";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
