using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MovieSearch.MovieApiService;

namespace MovieSearchForms
{
    public partial class MainPage : ContentPage
    {
        private MovieSearchService _service;
        public MainPage()
        {
            InitializeComponent();
            _service = new MovieSearchService();
        }
        private async void TitleSearchButton_OnClicked(object sender, EventArgs e)
        {
            this.SearchTitleProgressBar.IsRunning = true;
            var Movies =  await _service.GetMoviesByTitle(this.TitleSearch.Text);
            this.MovieLabel.Text = (from x in Movies select x.Title).FirstOrDefault();
            this.SearchTitleProgressBar.IsRunning = false;
        }
    }
}
