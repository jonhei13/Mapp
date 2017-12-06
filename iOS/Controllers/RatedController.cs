using System;
using System.Collections.Generic;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using System.Threading.Tasks;
using MovieSearch.iOS.ApiService;
using UIKit;
using MovieSearch.MovieApiService;
namespace MovieSearch.iOS.Controllers
{
    public class RatedController : UITableViewController
    {
        private bool DidAppear = false;
        private MovieSettings _apiConnection;
        private MovieService _apiService;
        private List<MovieDetails> _topRated;
        private const double startX = 20;
        private const double startY = 80;
        private const double height = 50;

        public RatedController(MovieSettings ApiConnection, MovieService ApiService)
        {
            _apiConnection = ApiConnection;
            _apiService = ApiService;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
           
            base.ViewDidLoad();
            this.Title = "Top Rated";
            this.TableView.Source = new MovieDetailsRows(new List<MovieDetails>(), _onSelected);
            PopulateTopRated();

        }

        public override void ViewDidAppear(bool animated)
        {
            
            if (this.DidAppear){
                base.ViewDidAppear(true);
                this.Title = "Top Rated";
                this.TableView.Source = new MovieDetailsRows(new List<MovieDetails>(), _onSelected);
                PopulateTopRated();  
            }
            this.DidAppear = true;
            this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Background"));
            this.TableView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Background"));

        }

        UIActivityIndicatorView CreateLoading()
        {
            var i = new UIActivityIndicatorView();
            i.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge;
            i.Frame = new System.Drawing.RectangleF((float)startX, (float)(startY + 100), (float)(this.View.Bounds.Width - 2 * startX), (float)height);
            i.Color = UIColor.Gray;
            return i;
        }
        private async void PopulateTopRated()
        {
            var loading = CreateLoading();
            this.View.AddSubview(loading);
            loading.StartAnimating();
            _topRated = await getTopRatedMovies();
            loading.StopAnimating();

            this.TableView.Source = new MovieDetailsRows(_topRated, _onSelected);
            this.TableView.ReloadData();


        }
        public async Task<List<MovieDetails>> getTopRatedMovies()
        {
            var TopRatedMovies = await _apiService.GetTopRatedMovies();
            return TopRatedMovies;
        }
        public void _onSelected(MovieDetails mov)
        {
            this.NavigationController.PushViewController(new MovieDetailsController(mov), true);
            this.DidAppear = false;


        }
    }
}
