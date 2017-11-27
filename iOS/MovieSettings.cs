using System;
using DM.MovieApi;
namespace MovieSearch.iOS
{
    public class MovieSettings : IMovieDbSettings
    {
        public MovieSettings()
        {
        }

        public string ApiKey => "0865a26248444f496c23cfd76c599a2c";


        public string ApiUrl => "http://api.themoviedb.org/3/";
    }
}
