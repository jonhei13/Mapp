using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MovieSearch.Models;
using Xamarin.Forms;
using MovieSearchForms.Pages;
using MovieSearch.MovieApiService;
using System.Windows.Input;
using System.Threading.Tasks;

namespace MovieSearchForms.ViewModels
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private List<MovieDetails> _movieList;
        private MovieDetails _selectedMovie;
        private MovieSearchService _service;
        public event PropertyChangedEventHandler PropertyChanged;
        //private bool _isRefreshing = false;

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
                OnPropertyChanged("Movies");
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
            await this._navigation.PushAsync(new MovieDetailsPage(this._selectedMovie), true);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void LoadActors()
        {
            var movies = await this._service.getActors(this._movieList);

        }

                /*
         * public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
         * 
         * public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await RefreshData();

                    IsRefreshing = false;
                });
            }
        }

        public async Task RefreshData()
        {
        }
        */
    }
}
