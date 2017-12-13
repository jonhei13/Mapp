using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using MovieSearchForms.Pages;
using Xamarin.Forms;

namespace MovieSearchForms.ViewModels
{
    public class PopularPageViewModel
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private INavigation _navigation;

        public PopularPageViewModel(INavigation navigation)
        {
            _service = new MovieSearchService();
            this._navigation = navigation;
            _movieList = new List<MovieDetails>();
            FetchPopularMovies();
        }

        public async void FetchPopularMovies()
        {
            this._movieList = await _service.GetPopularMovies();
            await this._navigation.PushAsync(new MovieListPage(this._movieList));
        }
    }
}
