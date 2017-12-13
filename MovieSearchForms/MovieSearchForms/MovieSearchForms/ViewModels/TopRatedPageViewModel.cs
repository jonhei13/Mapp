using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using MovieSearchForms.Pages;
using Xamarin.Forms;

namespace MovieSearchForms.ViewModels
{
    public class TopRatedPageViewModel
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private INavigation _navigation;

        public TopRatedPageViewModel(INavigation navigation)
        {
            _service = new MovieSearchService();
            this._navigation = navigation;
            _movieList = new List<MovieDetails>();
            FetchTopRatedMovies();
        }

        public async void FetchTopRatedMovies()
        {
            this._movieList = await _service.GetTopRatedMovies();
            await _navigation.PushAsync(new MovieListPage(this._movieList));
        }
    }
}

