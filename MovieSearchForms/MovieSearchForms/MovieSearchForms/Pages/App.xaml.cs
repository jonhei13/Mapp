using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieSearch.MovieApiService;
using MovieSearchForms.ViewModels;
using Xamarin.Forms;
using MovieSearch.Models;

namespace MovieSearchForms.Pages
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();




            var service = new MovieSearchService();

            var topViewModel = new TabsPageViewModel(service);
            var popularViewModel = new TabsPageViewModel(service);

            var MovieSearchPage = new MainPage();
            var MovieSearchNavigationPage = new NavigationPage(MovieSearchPage);
            MovieSearchNavigationPage.Title = "Movie Search";

            var TopRatedPage = new TopRatedPage(topViewModel);
            var TopRatedNavigationPage = new NavigationPage(TopRatedPage);
            TopRatedNavigationPage.Title = "Top Rated";

            var PopularPage = new PopularPage(popularViewModel);
            var PopularNavigationPage = new NavigationPage(PopularPage);
            PopularNavigationPage.Title = "Popular";

            topViewModel.setNavigation(TopRatedNavigationPage.Navigation);
            popularViewModel.setNavigation(PopularNavigationPage.Navigation);


            var tabbedPage = new TabPage(topViewModel, popularViewModel, service);
            tabbedPage.Children.Add(MovieSearchNavigationPage);
            tabbedPage.Children.Add(TopRatedNavigationPage);
            tabbedPage.Children.Add(PopularNavigationPage);
            tabbedPage.BackgroundColor = Color.FromHex("#00ffffff");

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
