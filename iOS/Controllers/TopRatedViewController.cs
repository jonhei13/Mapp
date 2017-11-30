using System;
using UIKit;
using MovieSearch.iOS.ApiService;

namespace MovieSearch.iOS.Controllers
{
    public class TopRatedViewController : UIViewController
    {
        MovieSettings _apiConnection;
        MovieService _apiService;
        public TopRatedViewController(MovieSettings ApiConnection, MovieService ApiService)
        {
            _apiConnection = ApiConnection;
            _apiService = ApiService;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           

        } 
    }
}
