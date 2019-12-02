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

        List<string> countries = new List<string>();

        public Form1()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ToString();
            _service = new DataService(connectionString);

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
                Phone = registerPhone.Text,
                Email = registerEmail.Text,
                MemberSince = DateTime.Now,
                Bio = registerBio.Text,
                Location = countries[registerLocation.SelectedIndex],
                BirthDate = registerBirthDate.Value
            };

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
            _user = null;
            menuPanel.Visible = false;
            registerPanel.Visible = false;
            loginPanel.Visible = false;
        }
    }
}
