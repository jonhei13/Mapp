using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Android.Content;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme")]
    public class MainActivity : FragmentActivity
    {
        const string TOPRATEDTAB = "Top Rated";
        const string SEARCHBARTAB = "Search";
        const string TABTITLE = "Search Movies";
        private List<MovieDetails> _movieListTitleInput;
        private List<MovieDetails> _movieListTopRated;
        public static MovieSearchService MovieService { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this._movieListTitleInput = new List<MovieDetails>();
            this._movieListTopRated = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var topRatedFragment = new TopRatedFragment(MovieService, _movieListTopRated);
            var titleInputFragment = new TitleInputFragment(MovieService, _movieListTitleInput);
            var fragments = new Fragment[]
            {
                titleInputFragment,
                topRatedFragment
            };
            var titles = CharSequence.ArrayFromStringArray(new[] { SEARCHBARTAB, TOPRATEDTAB });

            
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);
            
            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            tabLayout.TabSelected += (object sender, TabLayout.TabSelectedEventArgs e) =>
            {
                if (e.Tab.Text.Equals(TOPRATEDTAB))
                {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(toolbar.WindowToken, 0);
                    topRatedFragment.getMovies();
                }
            };
            this.ActionBar.Title = TABTITLE;

        
        }

 
    } 
}


