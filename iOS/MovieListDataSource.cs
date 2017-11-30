using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using MovieSearch.iOS.Controllers;
using MovieSearch.iOS.Views;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListDataSource : UITableViewSource
    {
        private readonly List<MovieDetails> _movieList;

        public readonly NSString MovieListCellId = new NSString("MovieListCell");
        private readonly Action<MovieDetails> _onSelectedMovie;

        public MovieListDataSource(List<MovieDetails> movieList, Action<MovieDetails> onSelectedMovie)
        {
            this._movieList = movieList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (Cell)tableView.DequeueReusableCell((NSString)this.MovieListCellId);
            if (cell == null)
            {
                cell = new Cell(this.MovieListCellId);
            }
            var movie = this._movieList[indexPath.Row];
            cell.UpdateCell(movie);
           
            return cell;
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
