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
            InitializeComponent();
            this._viewModel = new TopRatedPageViewModel(this.Navigation);
        }
    }
}
