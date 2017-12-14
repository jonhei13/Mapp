using MovieSearch.Models;
using MovieSearch.MovieApiService;
using MovieSearchForms.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieSearchForms.ViewModels
{
    class UserControl : INotifyPropertyChanged
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private INavigation _navigation;
        private MovieDetails _selectedMovie;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserControl(INavigation navigation, List<MovieDetails> movies)
        {
            this._navigation = navigation;
            this._service = new MovieSearchService();
            this._movieList = movies;

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
    }
}
