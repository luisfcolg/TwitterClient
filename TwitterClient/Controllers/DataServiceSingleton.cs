using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using TwitterClient.Models;
using System.Data;

namespace TwitterClient.Controllers
{
    public class DataServiceSingleton
    {
        private readonly SQLClient _client;
        private static DataServiceSingleton _instance;

        private DataServiceSingleton(string connectionString)
        {
            _client = new SQLClient(connectionString);
        }

        public static DataServiceSingleton GetInstance(string connectionString)
        {
            if (_instance == null)
                _instance = new DataServiceSingleton(connectionString);

            return _instance;
        }

        public Picture GetPicture(int idPicture)
        {
            Picture result = null;

            try
            {
                if(_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getPicture",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idProfilePicture", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPicture
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while(dataReader.Read())
                    {
                        result = new Picture
                        {
                            Id = (int)dataReader["idPicture"],
                            FileName = dataReader["fileNames"].ToString(),
                            Data = (byte[])dataReader["datas"]
                        };

                        break;
                    }
                }
            }
            catch(Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool SavePicture(Picture picture)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "savePicture",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@fileName", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = picture.FileName
                    };

                    var par2 = new SqlParameter("@data", SqlDbType.Image)
                    {
                        Direction = ParameterDirection.Input,
                        Value = picture.Data
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool Post(Tweet tweet)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "post",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.User.Id,
                    };

                    var par2 = new SqlParameter("@text", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.Text
                    };

                    var par3 = new SqlParameter("@idPicture", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.Picture.Id
                    };

                    var par4 = new SqlParameter("@likes", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.Likes
                    };

                    var par5 = new SqlParameter("@date", SqlDbType.DateTime)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.Date
                    };

                    var par6 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool Comment(Comment comment)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "comment",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPost", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = comment.IdPost
                    };

                    var par2 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = comment.User.Id
                    };

                    var par3 = new SqlParameter("@text", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = comment.Text
                    };

                    var par4 = new SqlParameter("@likes", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = comment.Likes
                    };

                    var par5 = new SqlParameter("@date", SqlDbType.DateTime)
                    {
                        Direction = ParameterDirection.Input,
                        Value = comment.Date
                    };

                    var par6 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool Follow(int idFollower, int idFollowing)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "follow",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idFollower", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idFollower
                    };

                    var par2 = new SqlParameter("@idFollowing", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idFollowing
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool Unfollow(int idFollower, int idFollowing)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "unfollow",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idFollower", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idFollower
                    };

                    var par2 = new SqlParameter("@idFollowing", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idFollowing
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool SavePost(int idUser, int idPost)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "savePost",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@idPost", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public Tweet GetPost(int idPost)
        {
            Tweet result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getPost",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPost", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        List<Comment> comments = GetComments(idPost);

                        result = new Tweet
                        {
                            Id = (int)dataReader["idTweet"],
                            User = GetUserById((int)dataReader["idUser"]),
                            Text = dataReader["texts"].ToString(),
                            Picture = GetPicture((int)dataReader["idPicture"]),
                            Likes = (int)dataReader["likes"],
                            Date = (DateTime)dataReader["dates"],
                            Comments = comments
                        };

                        break;
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public User GetUser(string username, string password)
        {
            User result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getUser",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    var par2 = new SqlParameter("@password", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = password
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        List<User> followers = GetFollowers((int)dataReader["idUser"]);
                        List<User> following = GetFollowing((int)dataReader["idUser"]);
                        List<Tweet> posts = GetPosts((int)dataReader["idUser"]);
                        List<Tweet> savedPosts = GetSavedPosts((int)dataReader["idUser"]);
                        List<Notification> notifications = GetNotifications((int)dataReader["idUser"]);

                        result = new User
                        {
                            Id = (int)dataReader["idUser"],
                            Username = dataReader["username"].ToString(),
                            Password = dataReader["pass"].ToString(),
                            Name = dataReader["names"].ToString(),
                            Phone = dataReader["phone"].ToString(),
                            Email = dataReader["email"].ToString(),
                            ProfilePicture = GetPicture((int)dataReader["idProfilePicture"]),
                            MemberSince = (DateTime)dataReader["memberSince"],
                            Followers = followers,
                            Following = following,
                            Bio = dataReader["bio"].ToString(),
                            Location = dataReader["locations"].ToString(),
                            BirthDate = (DateTime)dataReader["birthDate"],
                            Posts = posts,
                            SavedPosts = savedPosts,
                            Notifications = notifications
                        };

                        break;
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public User GetUserById(int idUser)
        {
            User result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getUserById",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        result = new User
                        {
                            Id = (int)dataReader["idUser"],
                            Username = dataReader["username"].ToString(),
                            Password = dataReader["pass"].ToString(),
                            Name = dataReader["names"].ToString(),
                            Phone = dataReader["phone"].ToString(),
                            Email = dataReader["email"].ToString(),
                            ProfilePicture = GetPicture((int)dataReader["idProfilePicture"]),
                            MemberSince = (DateTime)dataReader["memberSince"],
                            Bio = dataReader["bio"].ToString(),
                            Location = dataReader["locations"].ToString(),
                            BirthDate = (DateTime)dataReader["birthDate"]
                        };

                        break;
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<Notification> GetNotifications(int idUser)
        {
            List<Notification> result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getNotifications",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Notification n = new Notification
                        {
                            Id = (int)dataReader["idNotification"],
                            User = GetUserById(idUser),
                            Title = dataReader["title"].ToString(),
                            Post = GetPost((int)dataReader["idPost"])
                        };

                        result.Add(n);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<User> GetFollowers(int idUser)
        {
            List<User> result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getFollowers",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        User u = new User
                        {
                            Id = (int)dataReader["idUser"],
                            Username = dataReader["username"].ToString(),
                            Password = dataReader["pass"].ToString(),
                            Name = dataReader["names"].ToString(),
                            Phone = dataReader["phone"].ToString(),
                            Email = dataReader["email"].ToString(),
                            ProfilePicture = GetPicture((int)dataReader["idProfilePicture"]),
                            MemberSince = (DateTime)dataReader["memberSince"],
                            Bio = dataReader["bio"].ToString(),
                            Location = dataReader["locations"].ToString(),
                            BirthDate = (DateTime)dataReader["birthDate"]
                        };

                        result.Add(u);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<User> GetFollowing(int idUser)
        {
            List<User> result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getFollowing",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        User u = new User
                        {
                            Id = (int)dataReader["idUser"],
                            Username = dataReader["username"].ToString(),
                            Password = dataReader["pass"].ToString(),
                            Name = dataReader["names"].ToString(),
                            Phone = dataReader["phone"].ToString(),
                            Email = dataReader["email"].ToString(),
                            ProfilePicture = GetPicture((int)dataReader["idProfilePicture"]),
                            MemberSince = (DateTime)dataReader["memberSince"],
                            Bio = dataReader["bio"].ToString(),
                            Location = dataReader["locations"].ToString(),
                            BirthDate = (DateTime)dataReader["birthDate"]
                        };

                        result.Add(u);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<Comment> GetComments(int idPost)
        {
            List<Comment> result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getComments",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPost", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Comment c = new Comment
                        {
                            Id = (int)dataReader["idComment"],
                            IdPost = (int)dataReader["idPost"],
                            User = GetUserById((int)dataReader["idUser"]),
                            Text = dataReader["texts"].ToString(),
                            Likes = (int)dataReader["likes"],
                            Date = (DateTime)dataReader["dates"]
                        };

                        result.Add(c);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<Tweet> GetPosts(int idUser)
        {
            List<Tweet> result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getPosts",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        List<Comment> comments = GetComments((int)dataReader["idTweet"]);

                        Tweet t = new Tweet
                        {
                            Id = (int)dataReader["idTweet"],
                            User = GetUserById((int)dataReader["idUser"]),
                            Text = dataReader["texts"].ToString(),
                            Picture = GetPicture((int)dataReader["idPicture"]),
                            Likes = (int)dataReader["likes"],
                            Date = (DateTime)dataReader["dates"],
                            Comments = comments
                        };

                        result.Add(t);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<Tweet> GetSavedPosts(int idUser)
        {
            List<Tweet> result = null;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getSavedPosts",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        List<Comment> comments = GetComments((int)dataReader["idTweet"]);

                        Tweet t = new Tweet
                        {
                            Id = (int)dataReader["idTweet"],
                            User = GetUserById((int)dataReader["idUser"]),
                            Text = dataReader["texts"].ToString(),
                            Picture = GetPicture((int)dataReader["idPicture"]),
                            Likes = (int)dataReader["likes"],
                            Date = (DateTime)dataReader["dates"],
                            Comments = comments
                        };

                        result.Add(t);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool VerifyUser(string username, string email)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "verifyUser",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    var par2 = new SqlParameter("@email", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = email
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool AddUser(User user)
        {
            var result = false;

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "addUser",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Username
                    };

                    var par2 = new SqlParameter("@password", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Password
                    };

                    var par3 = new SqlParameter("@names", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Name
                    };

                    var par4 = new SqlParameter("@phone", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Email
                    };

                    var par5 = new SqlParameter("@email", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Email
                    };

                    var par6 = new SqlParameter("@memberSince", SqlDbType.DateTime)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Email
                    };

                    var par7 = new SqlParameter("@bio", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Bio
                    };

                    var par8 = new SqlParameter("@locations", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.Location
                    };

                    var par9 = new SqlParameter("@birthDate", SqlDbType.DateTime)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.BirthDate
                    };

                    var par10 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);
                    command.Parameters.Add(par7);
                    command.Parameters.Add(par8);
                    command.Parameters.Add(par9);
                    command.Parameters.Add(par10);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }
    }
}