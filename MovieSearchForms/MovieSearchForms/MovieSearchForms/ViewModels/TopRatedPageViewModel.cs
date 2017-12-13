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
            _movieList = new List<MovieDetails>();
            this._navigation = navigation;
            FetchTopRatedMovies();
        }

        public List<MovieDetails> getTopRatedList(){
            return this._movieList;
        }

        public async void FetchTopRatedMovies()
        {
            this._movieList = await _service.GetTopRatedMovies();
            await this._navigation.PushAsync(new MovieListPage(this._movieList));
        }
    }
}

