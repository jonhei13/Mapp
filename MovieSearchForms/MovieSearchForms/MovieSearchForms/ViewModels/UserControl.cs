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

    }
}
