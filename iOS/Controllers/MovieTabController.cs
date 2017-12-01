using System;
using UIKit;
namespace MovieSearch.iOS.Controllers
{
    public class MovieTabController : UITabBarController 
    {
        
        public override void ViewDidLoad(){
            base.ViewDidLoad();
            this.TabBar.BarTintColor = UIColor.Black;
            this.SelectedIndex = 0;
        }

    }
}
