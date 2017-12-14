﻿using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Xamarin.Forms;
using MovieSearchForms.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;

namespace MovieSearchForms.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private MovieDetails _selectedMovie;
        private INavigation _navigation;
        private string _titleSearch;
        private ICommand _searchCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel(INavigation navigation)
        {
            _service = new MovieSearchService();
            this._navigation = navigation;
            _movieList = new List<MovieDetails>();
            _searchCommand = new Command(() => SearchCommandExecute());
        }

        public string titleSearch 
        { 
            get{
                return this._titleSearch;
            } 
            set {
                _titleSearch = value;
                OnPropertyChanged();
            } 
        }

        public ICommand SearchCommand
        {
            get{
                return this._searchCommand;
            } 
            set {
                if(value != null){
                    OnPropertyChanged();
                    SearchCommandExecute();
                }
            }
        }

        private void SearchCommandExecute()
        {
            this.FetchMoviesByTitle(this._titleSearch);
        }

        public async void FetchMoviesByTitle(string title)
        {
            this.Movies = await _service.GetMoviesByTitle(title);
            this.Movies = await LoadActors();

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
        public async Task<List<MovieDetails>>LoadActors()
        {
            var movies = await this._service.getActors(this._movieList);
            return movies;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
