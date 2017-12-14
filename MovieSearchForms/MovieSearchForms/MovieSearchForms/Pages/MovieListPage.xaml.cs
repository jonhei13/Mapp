using System;
using System.Collections.Generic;
using MovieSearch.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovieSearchForms.ViewModels;

namespace MovieSearchForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListPage : ContentPage
    {
        MovieListViewModel _model { get; set; }
        public MovieListPage(List<MovieDetails> movieList)
        {
            this._model = new MovieListViewModel(this.Navigation, movieList);
    
            this.BindingContext = _model;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this._model.LoadActors();
            this.ListView.SelectedItem = null;
        }
    }
}
