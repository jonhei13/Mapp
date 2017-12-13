using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MovieSearch.Models;
using Xamarin.Forms;
using MovieSearch.MovieApiService;

namespace MovieSearchForms
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private List<MovieDetails> _movieList;
        private MovieDetails _selectedMovie;
        private MovieSearchService _service;


        public MovieListViewModel(INavigation navigation, List<MovieDetails> movieList)
        {
            this._service = new MovieSearchService();
            this._navigation = navigation;
            this._movieList = movieList;
        }

        public List<MovieDetails> Movies
        {
            get => this._movieList;

            set
            {
                this._movieList = value;
                OnPropertyChanged();
            }
        }

        public MovieDetails SelectedMovie
        {
            get => this._selectedMovie;

            set
            {
                if (value != null)
                {
                    var movie = value;
                    getDetailedMovie(movie);
                
                }
            }
        }

        private async void getDetailedMovie(MovieDetails movie)
        {
            this._selectedMovie = await this._service.GetDetailedMovie(movie);
            this._navigation.PushAsync(new MovieDetailsPage(this._selectedMovie), true);
        }
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
