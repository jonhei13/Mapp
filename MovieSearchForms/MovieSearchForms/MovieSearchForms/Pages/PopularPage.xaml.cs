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
            this.BindingContext = _viewModel;
            InitializeComponent();


        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this._viewModel.FetchPopularMovies();

        }

    }
}
