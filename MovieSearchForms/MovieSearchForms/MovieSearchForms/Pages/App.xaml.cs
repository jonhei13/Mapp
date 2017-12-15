﻿using System;
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

            var MovieSearchPage = new MainPage();
            var MovieSearchNavigationPage = new NavigationPage(MovieSearchPage);
            MovieSearchNavigationPage.Title = "Movie Search";

            var TopRatedPage = new TopRatedPage();
            var TopRatedNavigationPage = new NavigationPage(TopRatedPage);
            TopRatedNavigationPage.Title = "Top Rated";

            var PopularPage = new PopularPage();
            var PopularNavigationPage = new NavigationPage(PopularPage);
            PopularNavigationPage.Title = "Popular";

            var tabbedPage = new TabPage(TopRatedPage, TopRatedNavigationPage, PopularPage, PopularNavigationPage);
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
