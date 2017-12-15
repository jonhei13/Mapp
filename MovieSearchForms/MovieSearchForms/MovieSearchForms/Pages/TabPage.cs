using System;
using Xamarin.Forms;
using MovieSearchForms.ViewModels;
using MovieSearch.MovieApiService;
namespace MovieSearchForms.Pages
{
    public partial class TabPage : TabbedPage
    {
        private TabsPageViewModel _topViewModel;
        private TabsPageViewModel _popularViewModel;
        private MovieSearchService _service;

        public TabPage(TabsPageViewModel top, TabsPageViewModel pop, MovieSearchService service)
        {
            this._topViewModel = top;
            this._popularViewModel = pop;
            this._service = service;
<<<<<<< HEAD
=======

            this._popular = popular;
            this._popularNav = popNavigation;

>>>>>>> 09da07ee3ae6d79f0b2320f1075a28da671e9b4c
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this._popularViewModel.FetchPopularMovies();
            this._topViewModel.FetchTopRatedMovies();
        }

    }
}
