using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieSearch.MovieApiService;
using Xamarin.Forms;

namespace MovieSearchForms.Pages
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage());
            var MovieSearchPage = new MainPage();
            var MovieSearchNavigationPage = new NavigationPage(MovieSearchPage);
            MovieSearchNavigationPage.Title = "Movie Search";

            var TopRatedPage = new TopRatedPage();
            var TopRatedNavigationPage = new NavigationPage(TopRatedPage);
            TopRatedNavigationPage.Title = "Top Rated";

            /*var PopularPage = new PopularPage();
            var PopularNavigationPage = new NavigationPage(PopularPage);
            PopularNavigationPage.Title = "Popular";*/

            var tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(MovieSearchNavigationPage);
            tabbedPage.Children.Add(TopRatedNavigationPage);

            MainPage = tabbedPage;
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
