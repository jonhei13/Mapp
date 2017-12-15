using System;
using Xamarin.Forms;
using MovieSearchForms.ViewModels;
namespace MovieSearchForms.Pages
{
    public partial class TabPage : TabbedPage
    {
        private TabsPageViewModel _topViewModel;
        private TabsPageViewModel _popularViewModel;

        private Page _top;
        private Page _popular;
        private Page _topNav;
        private Page _popularNav;

        public TabPage(Page top, Page topNavigation, Page popular, Page popNavigation)
        {
            this._top = top;
            this._topNav = topNavigation;

            this._popular = popular;
            this._popularNav = popNavigation;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _topViewModel = new TabsPageViewModel(this._topNav.Navigation);
            _popularViewModel = new TabsPageViewModel(this._popularNav.Navigation);
            _popular.BindingContext = _popularViewModel;
            _top.BindingContext = _topViewModel;

            this._popularViewModel.FetchPopularMovies();
            this._topViewModel.FetchTopRatedMovies();
        }
    }
}
