using System;
using System.Collections.Generic;
using MovieSearch.Models;
using MovieSearchForms.ViewModels;
using Xamarin.Forms;

namespace MovieSearchForms.Pages
{
    public partial class TopRatedPage : ContentPage
    {
        private TabsPageViewModel _model;
        public TopRatedPage(TabsPageViewModel model)
        {
            this._model = model;
            this.BindingContext = model;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
           
            base.OnAppearing();
            this._model.SelectedMovie = null;

        }
    }
}
