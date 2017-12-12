using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Xamarin.Forms;

namespace MovieSearchForms
{
    public class MainPageViewModel
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private INavigation _navigation;

        public MainPageViewModel(INavigation navigation)
        {
            _service = new MovieSearchService();
            this._navigation = navigation;
            _movieList = new List<MovieDetails>();
        }

        public async void FetchMoviesByTitle(string title){
            this._movieList = await _service.GetMoviesByTitle(title);
            await _navigation.PushAsync(new MovieListPage(this._movieList));
        }
    }
}
