using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace MovieSearch.iOS
{
    public class MovieSettings : IMovieDbSettings
    {
        public string ApiKey => "0865a26248444f496c23cfd76c599a2c";


        public string ApiUrl => "http://api.themoviedb.org/3/";
        
        public MovieSettings()
        {
            MovieDbFactory.RegisterSettings(ApiKey,ApiUrl);
          
        }


        public async Task<string> getMovie(string name)
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(name);

            var result = (from x in response.Results select x.Title).FirstOrDefault();
            if (result == null)
            {
                return "Could Not Find Movie";
            }
            else
            {
                return result;
            }
        }
        public async Task<List<string>> getMovies(string name)
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(name);

            var result = (from x in response.Results select x.Title).ToList();
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

     

    }
}
