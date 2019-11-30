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

namespace TwitterClient
{
    public partial class Form1 : Form
    {
        List<string> countries = new List<string>();

        public Form1()
        {
            InitializeComponent();

            LoadCities();
            BindingSource source = new BindingSource();
            source.DataSource = countries;
            registerLocation.DataSource = source;
            registerLocation.SelectedIndex = 0;
        }

        private void LoadCities()
        {
            IProxyCountry proxy = new ProxyCountry();
            var proxyCountries = proxy.Countries();

            foreach (var i in proxyCountries)
                if (i.name != "")
                    countries.Add(i.name);

        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = false;
            registerPanel.Visible = true;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {

        }
    }
}
