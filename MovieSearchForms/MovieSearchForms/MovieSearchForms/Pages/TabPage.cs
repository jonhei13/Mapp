using System;
using Xamarin.Forms;
using MovieSearchForms.ViewModels;

namespace MovieSearchForms.Pages
{
    public partial class TabPage : TabbedPage
    {
        private PopularPageViewModel _popularViewModel;
        private TopRatedPageViewModel _topRatedViewModel;

        public TabPage()
        {
            _popularViewModel = new PopularPageViewModel(this.Navigation);
            _topRatedViewModel = new TopRatedPageViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this._popularViewModel.FetchPopularMovies();
            this._topRatedViewModel.FetchTopRatedMovies();
        }
    }
}
