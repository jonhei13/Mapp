using System;
using System.Collections.Generic;
using MovieSearchForms.ViewModels;
using Xamarin.Forms;
using MovieSearch.Models;

namespace MovieSearchForms.Pages
{
    public partial class PopularPage : ContentPage
    {
        private PopularPageViewModel _viewModel { get; set; }

        public PopularPage()
        {
            this._viewModel = new PopularPageViewModel(this.Navigation);
            fetchMovies();

  
        }
        public async void fetchMovies()
        {

            this._viewModel.Movies= await this._viewModel.FetchPopularMovies();
            this.BindingContext = _viewModel;
            InitializeComponent();
        }

    }
}
