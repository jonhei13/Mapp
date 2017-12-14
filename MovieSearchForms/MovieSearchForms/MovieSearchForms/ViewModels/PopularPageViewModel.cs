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
    public class PopularPageViewModel : INotifyPropertyChanged
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private MovieDetails _selectedMovie;
        private INavigation _navigation;
        private bool _isRefreshing;

        public event PropertyChangedEventHandler PropertyChanged;

        public  PopularPageViewModel(INavigation navigation)
        {
            _service = new MovieSearchService();
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
                    await RefreshData();
                    IsRefreshing = false;
                });
            }
        }

        public async Task RefreshData()
        {
            this.FetchPopularMovies();
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

        public async void FetchPopularMovies()
        {
             this.Movies = await _service.GetPopularMovies();
        }
    }
}
