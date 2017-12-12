using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieSearch.MovieApiService;
using Xamarin.Forms;

namespace MovieSearchForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MovieSearchForms.MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
