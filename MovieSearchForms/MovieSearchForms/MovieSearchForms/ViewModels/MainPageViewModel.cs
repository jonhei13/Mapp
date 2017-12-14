using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Xamarin.Forms;
using MovieSearchForms.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieSearchForms.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _service;
        private INavigation _navigation;
        private string _titleSearch;
        private Command _searchCommand;
        public event PropertyChangedEventHandler PropertyChanged;



        public MainPageViewModel(INavigation navigation)
        {
            _service = new MovieSearchService();
            this._navigation = navigation;
            _movieList = new List<MovieDetails>();
            _searchCommand = new Command(() => SearchCommandExecute());
        }


        public string titleSearch { 
            get{
                return this._titleSearch;
            } 
            set {
                _titleSearch = value;
                OnPropertyChanged();
            } 
        }
        public Command SearchCommmand
        {
            get; set;
        }

        private void SearchCommandExecute()
        {
            this.FetchMoviesByTitle(this._titleSearch);
        }

        public async void FetchMoviesByTitle(string title){
            this._movieList = await _service.GetMoviesByTitle(title);
            await _navigation.PushAsync(new MovieListPage(this._movieList));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
