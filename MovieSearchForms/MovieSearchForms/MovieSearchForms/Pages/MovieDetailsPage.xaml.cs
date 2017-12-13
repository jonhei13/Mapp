using System;
using MovieSearch.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieSearchForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsPage : ContentPage
    {
        public MovieDetailsPage(MovieDetails selectedMovie)
        {
            this.BindingContext = selectedMovie;
            InitializeComponent();
        }
    }
}
