using MovieSearch.Models;
using MovieSearchForms.ViewModels;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieSearchForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridView : ListView
    {
        private UserControl _userControl;
        private List<MovieDetails> _movieList = new List<MovieDetails>();
        public GridView()
        {
         //   this._userControl = new UserControl(this.Navigation);
            this.BindingContext = this._userControl;
            InitializeComponent();
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(MovieList), typeof(Binding), typeof(GridView));

        public Binding MovieList
        {
            get
            {
                var x = (Binding)GetValue(TextProperty);
                return x;
            }
            set
            {
                this.BindingContext = value;
            }
        }
        public List<MovieDetails> adaw
        {
            get => this._movieList;

            set
            {
                this._movieList = value;
                this.BindingContext = new UserControl(this.Navigation, this._movieList);
                OnPropertyChanged();
            }
        }

    }
}