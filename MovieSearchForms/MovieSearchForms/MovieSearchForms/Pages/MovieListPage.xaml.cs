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
        public MovieListPage(List<MovieDetails> movieList)
        {
            this.BindingContext = new MovieListViewModel(this.Navigation, movieList);
            InitializeComponent();
        }
    }
}
