using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using MovieSearch.iOS.Controllers;
using MovieSearch.iOS.Views;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieDetailsRows : UITableViewSource
    {
        private readonly List<MovieDetails> _movieList;

        public readonly NSString MovieListCellId = new NSString("MovieListCell");
        private readonly Action<MovieDetails> _onSelectedMovie;

        public MovieDetailsRows(List<MovieDetails> movieList, Action<MovieDetails> onSelectedMovie)
        {
            this._movieList = movieList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var movieCell = (MovieDetailCell)tableView.DequeueReusableCell((NSString)this.MovieListCellId);
            if (movieCell == null)
            {
                movieCell = new MovieDetailCell(this.MovieListCellId);
            }
            var movie = this._movieList[indexPath.Row];
            movieCell.UpdateCell(movie);
           
            return movieCell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovie(_movieList[indexPath.Row]);

        }

    }
}
