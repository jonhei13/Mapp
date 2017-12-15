using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;
using MovieSearchForms.Pages;
using System.Windows.Input;
using System;

namespace MovieSearchForms.ViewModels
{
    public class TabsPageViewModel : INotifyPropertyChanged
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private MovieDetails _selectedMovie;
        private INavigation _navigation;
        private bool _isRefreshing;
        private bool _TopRatedIsRefreshing;

        public event PropertyChangedEventHandler PropertyChanged;

        public  TabsPageViewModel(MovieSearchService service)
        {
            _service = service;
            _movieList = new List<MovieDetails>();
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
                {
                    this._selectedMovie = value;
                    if(this._selectedMovie != null)
                    {
                        getDetailedMovie(this._selectedMovie);
                    }
                    OnPropertyChanged();
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
                    await RefreshPopular();
                    IsRefreshing = false;
                });
            }
        }

        public async Task RefreshPopular()
        {
             await this.FetchPopularMovies();
        }

        public bool TopRatedIsRefreshing
        {
            get { return _TopRatedIsRefreshing; }
            set
            {
                _TopRatedIsRefreshing = value;
                OnPropertyChanged(nameof(TopRatedIsRefreshing));
            }
        }

        public ICommand TopRatedRefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    TopRatedIsRefreshing = true;
                    await RefreshTopRated();
                    TopRatedIsRefreshing = false;
                });
            }
        }

        public async Task RefreshTopRated()
        {
            await this.FetchTopRatedMovies();
        }

        private async Task getDetailedMovie(MovieDetails movie)
        {
            try
            {
                this._selectedMovie = await this._service.GetDetailedMovie(movie);
                await this._navigation.PushAsync(new MovieDetailsPage(this._selectedMovie), true);

            }
            catch(NullReferenceException e)
            {
                this._selectedMovie = null;
            }
        }

        public async Task<List<MovieDetails>> LoadActors()
        {
            var movies = await this._service.getActors(this._movieList);
            return movies;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task FetchPopularMovies()
        {
             this.Movies = await _service.GetPopularMovies();
             this.Movies = await LoadActors();
        }
   
        public async Task FetchTopRatedMovies()
        {
            this.Movies = await _service.GetTopRatedMovies();
            this.Movies = await LoadActors();
        }
        public void setNavigation(INavigation pop)
        {
            this._navigation = pop;
        }
    }
}
