using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using MovieSearchForms.Pages;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieSearchForms.ViewModels
{
    public class TopRatedPageViewModel : INotifyPropertyChanged
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private MovieDetails _selectedMovie;
        private INavigation _navigation;
        private bool _isRefreshing = false;
        public event PropertyChangedEventHandler PropertyChanged;

        public TopRatedPageViewModel(INavigation navigation)
        {
            _service = new MovieSearchService();
            _movieList = new List<MovieDetails>();
            this._navigation = navigation;
        }

        public List<MovieDetails> Movies
        {
            get => this._movieList;

            set
            {
                if (value != null)
                {
                    this._movieList = value;
                    getTopRatedList();
                    OnPropertyChanged();
                }
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

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
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
            this.FetchTopRatedMovies();
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
        public List<MovieDetails> getTopRatedList()
        {
            return this._movieList;
        }

        public async void FetchTopRatedMovies()
        {
            this.Movies = await _service.GetTopRatedMovies();
        }
    }
}

