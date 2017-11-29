﻿using System;
using Foundation;
using UIKit;
using CoreGraphics;
namespace MovieSearch.iOS.Views
{
    public class Cell : UITableViewCell
    {
        private const double ImageHeight = 33;
        private UIImageView _imageView;
        private UILabel _headingLabel;
        private UILabel _subHeadingLabel;
        public Cell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;

            this._imageView = new UIImageView()
            {

                Frame = new CGRect(this.ContentView.Bounds.Width - 60, 5, ImageHeight, ImageHeight),


            };
            this._headingLabel = new UILabel()
            {

                Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 60, 25)
                //Font = UIFont.FromName()
                //TextColor = UIColor.FromRGB(127,0,51þ)
                //BackgroundColor = UIColor.Clear

            };

            this._subHeadingLabel = new UILabel()
            {

                Frame = new CGRect(100, 25, 100, 25),
                //Font = UIFont.FromName()
                //TextColor = UIColor.FromRGB(127,0,51þ)
                //BackgroundColor = UIColor.Clear
                TextAlignment = UITextAlignment.Center

            };

            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._headingLabel, this._subHeadingLabel });

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;

        }
        public void UpdateCell(MovieDetails Movie)
        {
            this._imageView.Image = UIImage.FromFile(Movie.ImagePath);
            this._headingLabel.Text = Movie.Title + "( " + Movie.ReleaseDate.Year.ToString() + " )";
            if (Movie.actors.Count < 1)
            {
                this._subHeadingLabel.Text = "No Actors Listed";
            }
            else if (Movie.actors.Count < 2)
            {
                
                this._subHeadingLabel.Text = Movie.actors[0];   
            }
            else if (Movie.actors.Count < 3)
            {
                this._subHeadingLabel.Text = Movie.actors[0] + Movie.actors[1];   
            }
            else 
            {
                this._subHeadingLabel.Text = Movie.actors[0] + Movie.actors[1] + Movie.actors[2];    
            }
          
        }
    }
}
