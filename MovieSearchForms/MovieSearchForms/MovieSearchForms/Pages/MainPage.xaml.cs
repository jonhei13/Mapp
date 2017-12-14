using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MovieSearch.MovieApiService;
using MovieSearch.Models;
using MovieSearchForms.ViewModels;

namespace MovieSearchForms.Pages
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _mainViewModel;

        public MainPage()
        {
            InitializeComponent();
            this._mainViewModel = new MainPageViewModel(this.Navigation);
            this.BindingContext = _mainViewModel;
            var x = this.BindingContext;
        }
    }
}
