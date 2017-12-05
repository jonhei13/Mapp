using System;
using Foundation;
using UIKit;
using CoreGraphics;
namespace MovieSearch.iOS.Views
{
    public class MovieDetailCell : UITableViewCell
    {
        private const double ImageHeight = 77;
        private UIImageView _imageView;
        private UILabel _headingLabel;
        private UILabel _subHeadingLabel;
        public MovieDetailCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;
            this.BackgroundColor = UIColor.Clear;
            this._imageView = new UIImageView()
            {
                Frame = new CGRect(this.ContentView.Bounds.Width - 80, 5, ImageHeight, ImageHeight),
            };
            this._headingLabel = new UILabel()
            {

                Frame = new CGRect(3, 5, 200, 25),
                Font = UIFont.FromName("Helvetica-Bold", 14f),
                TextColor = UIColor.Gray
            };
       
            this._subHeadingLabel = new UILabel()
            {
                Frame = new CGRect(5, 25, 200, 25),
                Font = UIFont.FromName("Helvetica", 10f),
                TextColor = UIColor.FromRGB(179, 166, 46)
            };
            this._subHeadingLabel.LineBreakMode = UILineBreakMode.WordWrap;
            this._subHeadingLabel.Lines = 0;
 
            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._headingLabel, this._subHeadingLabel });

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;

        }

        public void UpdateCell(MovieDetails Movie)
        {
            this._headingLabel.Text = Movie.Title + " (" + Movie.ReleaseDate.Year.ToString() + ")";
   
            this._imageView.Image = UIImage.FromFile(Movie.ImagePath);

            CheckActors(Movie);

        }

        private void CheckActors(MovieDetails Movie)
        {
            if (Movie.Actors == null)
            {
                this._subHeadingLabel.Text = "No Actors Listed";
            }
            else if (Movie.Actors.Count < 1)
            {
                this._subHeadingLabel.Text = "No Actors Listed";
            }
            else if (Movie.Actors.Count < 2)
            {

                this._subHeadingLabel.Text = Movie.Actors[0];
            }
            else if (Movie.Actors.Count < 3)
            {
                this._subHeadingLabel.Text = Movie.Actors[0] + ", " + Movie.Actors[1];
            }
            else
            {
                this._subHeadingLabel.Text = Movie.Actors[0] + ", " + Movie.Actors[1] + ", " + Movie.Actors[2];
            }
        }
    }
}
