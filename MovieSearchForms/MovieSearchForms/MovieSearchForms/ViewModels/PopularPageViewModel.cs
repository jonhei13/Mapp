using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MovieSearchForms.ViewModels
{
    public class PopularPageViewModel
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private INavigation _navigation;
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
                if (value != null)
                {
                    this._movieList = value;
                    FetchPopularMovies();
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<List<MovieDetails>> FetchPopularMovies()
        {
            var movies = await _service.GetPopularMovies();
            return movies;
        }
    }
}
