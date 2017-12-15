using System;
using System.Collections.Generic;
using MovieSearchForms.ViewModels;
using Xamarin.Forms;
using MovieSearch.Models;

namespace MovieSearchForms.Pages
{
    public partial class PopularPage : ContentPage
    {
        private TabsPageViewModel _model;

        public PopularPage(TabsPageViewModel model)
        {
            this._model = model;
            this.BindingContext = model;
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this._model.SelectedMovie = null;
        }

    }
}
