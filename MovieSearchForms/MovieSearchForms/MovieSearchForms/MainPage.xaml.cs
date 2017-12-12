using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MovieSearch.MovieApiService;
using MovieSearch.Models;

namespace MovieSearchForms
{
    public partial class MainPage : ContentPage
    {
        private MovieSearchService _service;
        private List<MovieDetails> _movieList;

        public MainPage()
        {
            InitializeComponent();
            _service = new MovieSearchService();
        }
        private async void TitleSearchButton_OnClicked(object sender, EventArgs e)
        {
            this.SearchTitleProgressBar.IsRunning = true;
            this._movieList =  await _service.GetMoviesByTitle(this.TitleSearch.Text);

            await this.Navigation.PushAsync(new MovieListPage(this._movieList));
            this.SearchTitleProgressBar.IsRunning = false;
        }
    }
}
