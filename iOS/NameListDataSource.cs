using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using MovieSearch.iOS.Controllers;
using MovieSearch.iOS.Views;
using UIKit;

namespace MovieSearch.iOS
{
    public class NameListDataSource : UITableViewSource
    {
        private readonly List<MovieDetails> _nameList;

        public readonly NSString NameListCellId = new NSString("NameListCell");
        private readonly Action<MovieDetails> _onSelectedMovie;

        public NameListDataSource(List<MovieDetails> nameList, Action<MovieDetails> onSelectedMovie)
        {
            this._nameList = nameList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (Cell)tableView.DequeueReusableCell((NSString)this.NameListCellId);
            if (cell == null)
            {
                cell = new Cell(this.NameListCellId);
            }
            var movie = this._nameList[indexPath.Row];
            cell.UpdateCell(movie);
           
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._nameList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovie(_nameList[indexPath.Row]);

        }

    }
}
