using System;
using System.Collections.Generic;
using MovieSearchForms.ViewModels;
using Xamarin.Forms;

namespace MovieSearchForms.Pages
{
    public partial class PopularPage : ContentPage
    {
        private PopularPageViewModel _viewModel;

        public PopularPage()
        {
            InitializeComponent();
            this._viewModel = new PopularPageViewModel(this.Navigation);
            this.BindingContext = _viewModel;
        }

    }
}
