using System;
using UIKit;
namespace MovieSearch.iOS.Controllers
{
    public class MovieTabController : UITabBarController 
    {
        
        public override void ViewDidLoad(){
            base.ViewDidLoad();

            //this.TabBar.BackgroundColor = UIColor.Black;
            //this.TabBar.BackgroundImage = UIImage.FromFile("Background");


            this.TabBar.TintColor = UIColor.Black;

            this.SelectedIndex = 0;
        }

    }
}
