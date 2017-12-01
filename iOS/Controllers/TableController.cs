using System;
using System.Collections.Generic;
using UIKit;
namespace MovieSearch.iOS.Controllers
{
    public class TableController : UITableViewController
    {
        private List<MovieDetails> _movieList;

        public TableController(List<MovieDetails> movieList)
        {
            this._movieList = movieList;
          
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie List";

            this.TableView.Source = new MovieDetailsRows(_movieList, _onSelected);
        }
        public void _onSelected(MovieDetails mov)
        {
            this.NavigationController.PushViewController(new MovieDetailsController(mov), true);


        }
    }

}
