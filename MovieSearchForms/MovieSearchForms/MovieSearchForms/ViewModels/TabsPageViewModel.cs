using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;
using MovieSearchForms.Pages;
using System.Windows.Input;

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

        public  TabsPageViewModel(INavigation navigation, MovieSearchService service)
        {
            _service = service;
            this._navigation = navigation;
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
                    await RefreshPopular();
                    IsRefreshing = false;
                });
            }
        }

        public async Task RefreshPopular()
        {
            this.FetchPopularMovies();
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
            this.FetchTopRatedMovies();
        }


        private async void getDetailedMovie(MovieDetails movie)
        {
            this._selectedMovie = await this._service.GetDetailedMovie(movie);
            await this._navigation.PushAsync(new MovieDetailsPage(this._selectedMovie), true);
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

        public async void FetchPopularMovies()
        {
             this.Movies = await _service.GetPopularMovies();
             this.Movies = await LoadActors();
        }
   
        public async void FetchTopRatedMovies()
        {
            this.Movies = await _service.GetTopRatedMovies();
            this.Movies = await LoadActors();
        }
    }
}
