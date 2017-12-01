using System;
using System.Collections.Generic;

namespace MovieSearch.iOS
{
    public class MovieDetails
    {
        public int Id
        {
            get;
            set;
        }
        public MovieDetails()
        {

        }
        public string Title
        {
            get;
            set;
        }

        public DateTime ReleaseDate
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
        public List<string> Genre
        {
            get;
            set;
        }
        public List<string> actors
        {
            get;
            set;
        }
        public string ImagePath
        {
            get;
            set;
        }

        //public int Runtime { get; set; }
    }
}
