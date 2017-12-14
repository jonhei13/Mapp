using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearchForms.ViewModels;
using Xamarin.Forms;

namespace MovieSearchForms.Pages
{
    public partial class TopRatedPage : ContentPage
    {
        private TopRatedPageViewModel _viewModel;

        public TopRatedPage()
        {
            
            this._viewModel = new TopRatedPageViewModel(this.Navigation);
            this.BindingContext = this._viewModel;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this._viewModel.FetchTopRatedMovies();

        }
    }
}
