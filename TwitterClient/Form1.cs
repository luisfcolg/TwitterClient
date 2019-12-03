using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterClient.Models;
using TwitterClient.Services;
using TwitterClient.Controllers;
using TwitterClient.Strategy;
using System.Configuration;
using TwitterClient.PasswordHelper;

namespace TwitterClient
{
    public partial class Form1 : Form
    {
        private static DataService _service;
        User _user;
        UserMemento _userMemento;

        List<string> countries = new List<string>();

        public Form1()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ToString();
            _service = new DataService(connectionString);
            _user = _service.GetUserById(3);

            InitializeComponent();

            LoadCities();
            registerLocation.DataSource = new BindingSource { DataSource = countries };
            registerLocation.SelectedIndex = 0;
        }

        // Load cities from proxy
        private void LoadCities()
        {
            IProxyCountry proxy = new ProxyCountry();
            var proxyCountries = proxy.Countries();

            foreach (var i in proxyCountries)
                if (i.name != "")
                    countries.Add(i.name);
        }

        // Go to registration page
        private void signupButton_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = false;
            registerPanel.Visible = true;
        }

        // Register new user
        private void registerButton_Click(object sender, EventArgs e)
        {
            if (registerName.Text == "" || registerUsername.Text == "" || registerPassword.Text == "" || registerEmail.Text == "")
            {
                MessageBox.Show("Please, fill all the required fealds.\nRequired fields: name, username, password, email.", "Error");
                RestartRegisterFields();
                return;
            }

            // Validate email
            string m = registerEmail.Text;

            var email = new ValidateEmailClass(new ValidateEmailAt(), m);
            if(email.Validate())
            {
                email = new ValidateEmailClass(new ValidateEmailDomain(), m);
                if(!email.Validate())
                {
                    MessageBox.Show("Invalid email adress.", "Error");
                    RestartRegisterFields();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid email adress.", "Error");
                RestartRegisterFields();
                return;
            }

            // Validate username and email
            if(_service.VerifyUser(registerUsername.Text, registerEmail.Text))
            {
                MessageBox.Show("Username or email are already taken.", "Error");
                RestartRegisterFields();
                return;
            }

            // Validate phone number
            if(registerPhone.Text.Length > 0 && registerPhone.Text.Length != 10)
            {
                MessageBox.Show("Invalid phone number.", "Error");
                RestartRegisterFields();
                return;
            }
            else if(registerPhone.Text.Length == 10)
            {
                foreach(char i in registerPhone.Text)
                    if(i < 48 || i > 57)
                    {
                        MessageBox.Show("Invalid phone number.", "Error");
                        RestartRegisterFields();
                        return;
                    }
            }

            // Password encriptation
            PasswordBuilder pass = new SHA256Builder(registerPassword.Text);

            pass.GenerateBytes();
            pass.GenerateHashBytes();
            pass.GenerateHashString();

            // Save user
            User u = new User
            {
                Username = registerUsername.Text,
                Password = pass.GetPassword().HashString,
                Name = registerName.Text,
                Phone = "",
                Email = registerEmail.Text,
                MemberSince = DateTime.Now,
                Bio = "",
                Location = countries[registerLocation.SelectedIndex],
                BirthDate = registerBirthDate.Value
            };

            // If user has phone, decorate it
            if (registerPhone.Text != "")
                u = new PhoneDecorator(u, registerPhone.Text).GetDecoratedUser();

            // If user has bio, decorate it
            if (registerBio.Text != "")
                u = new BioDecorator(u, registerBio.Text).GetDecoratedUser();

            string result = _service.AddUser(u);
            MessageBox.Show(result, "Result");

            RestartRegisterFields();

            registerPanel.Visible = false;
            loginPanel.Visible = true;
        }

        // Restarts all register page fields
        private void RestartRegisterFields()
        {
            registerName.Text = "";
            registerUsername.Text = "";
            registerPassword.Text = "";
            registerPhone.Text = "";
            registerEmail.Text = "";
            registerBio.Text = "";
            registerLocation.SelectedIndex = 0;
        }

        // Restarts all login page fields
        private void RestartLoginFields()
        {
            loginUsername.Text = "";
            loginPassword.Text = "";
        }

        // Restarts all tweet page fields
        private void RestartTweetFields()
        {
            tweetText.Text = "";
        }

        // User login
        private void loginButton_Click(object sender, EventArgs e)
        {
            PasswordBuilder pass = new SHA256Builder(loginPassword.Text);

            pass.GenerateBytes();
            pass.GenerateHashBytes();
            pass.GenerateHashString();

            _user = _service.GetUser(loginUsername.Text, pass.GetPassword().HashString);

            if (_user == null)
                MessageBox.Show("Wrong username or password", "Error");
            else
                MessageBox.Show("Successful login", "Welcome");

            RestartLoginFields();

            if(_user != null)
            {
                _user.Followers = _service.GetFollowers(_user.Id);
                _user.Following = _service.GetFollowing(_user.Id);
                _user.Posts = _service.GetPosts(_user.Id);
                _user.SavedPosts = _service.GetSavedPosts(_user.Id);
                _user.Notifications = _service.GetNotifications(_user.Id);

                loginPanel.Visible = false;
                menuPanel.Visible = true;
            }
        }

        // User logout
        private void menuLogoutButton_Click(object sender, EventArgs e)
        {
            menuHomeButton.ForeColor = Color.White;
            menuNotificationsButton.ForeColor = Color.White;
            menuProfileButton.ForeColor = Color.White;
            menuLogoutButton.ForeColor = Color.White;
            menuSearchButton.ForeColor = Color.White;
            menuSavedButton.ForeColor = Color.White;

            _user = null;
            menuPanel.Visible = false;
            registerPanel.Visible = false;
            loginPanel.Visible = false;
        }

        // Post a Tweet
        private void tweetPostButton_Click(object sender, EventArgs e)
        {
            if(tweetText.Text == "")
            {
                MessageBox.Show("Write something to tweet", "Error");
                RestartTweetFields();
                return;
            }

            string t = tweetText.Text;

            Tweet tweet = new Tweet
            {
                User = _user,
                Text = t,
                Likes = 0,
                Date = DateTime.Now
            };

            var result = _service.Post(tweet);
            MessageBox.Show(result, "Result");

            RestartTweetFields();
        }

        // Show profile
        private void menuProfileButton_Click(object sender, EventArgs e)
        {
            menuProfileButton.ForeColor = Color.FromArgb(29, 161, 242);

            profileName.Text = _user.Name;
            profileUsername.Text = '@' + _user.Username;
            profileMemberSince.Text = "Joined " + _user.MemberSince.Month.ToString() + " " + _user.MemberSince.Year.ToString();
            profileBio.Text = _user.Bio;

            if (_user.Following != null)
                profileFollowing.Text = "" + _user.Following.Count;
            else
                profileFollowing.Text = "" + 0;

            if (_user.Followers != null)
                profileFollowers.Text = "" + _user.Followers.Count;
            else
                profileFollowers.Text = "" + 0;

            profilePanel.Visible = true;
        }

        // Show profile edit page
        private void profileEditButton_Click(object sender, EventArgs e)
        {
            editName.Text = _user.Name;
            editPhone.Text = _user.Phone;
            editBio.Text = _user.Bio;

            profilePanel.Visible = false;
            editPanel.Visible = true;
        }

        // Modify profile info
        private void editSaveButton_Click(object sender, EventArgs e)
        {
            SaveToUserMemento();

            // Validate name
            if (editName.Text == "")
            {
                MessageBox.Show("Please, select a name.", "Error");
                return;
            }

            _user.Name = editName.Text;

            // Validate phone number
            if (editPhone.Text.Length > 0 && editPhone.Text.Length != 10)
            {
                MessageBox.Show("Invalid phone number.", "Error");
                return;
            }
            else if (editPhone.Text.Length == 10)
            {
                foreach (char i in editPhone.Text)
                    if (i < 48 || i > 57)
                    {
                        MessageBox.Show("Invalid phone number.", "Error");
                        return;
                    }
            }

            // If user has phone, decorate it
            if (editPhone.Text != "")
                _user = new PhoneDecorator(_user, editPhone.Text).GetDecoratedUser();

            // Validate password
            if (editPassword.Text != "")
            {
                PasswordBuilder pass = new SHA256Builder(editPassword.Text);

                pass.GenerateBytes();
                pass.GenerateHashBytes();
                pass.GenerateHashString();

                _user.Password = pass.GetPassword().HashString;
            }

            // If user has bio, decorate it
            if (editBio.Text != "")
                _user = new BioDecorator(_user, editBio.Text).GetDecoratedUser();

            var result = _service.UpdateUser(_user);
            MessageBox.Show(result, "Result");

            menuProfileButton_Click(sender, e);
            profileRestoreEdit.Visible = true;

            editPanel.Visible = false;
            profilePanel.Visible = true;
        }

        // Save actual user to memento
        private UserMemento SaveToUserMemento()
        {
            _userMemento = new UserMemento(_user);
            return _userMemento;
        }

        // Restor user from memento
        private void RestoreFromUserMemento()
        {
            _user = _userMemento.GetSavedUser();
        }

        private void profileRestoreEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RestoreFromUserMemento();

            menuProfileButton_Click(sender, e);
            profileRestoreEdit.Visible = false;
        }
    }
}
