using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Models;

namespace MovieSearch.MovieApiService
{
    public class MovieSearchService
    {
        private IApiMovieRequest _movieApi;

        public MovieSearchService()
        {
            MovieSettings ApiConnection = new MovieSettings();
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
         
        }

        public async Task<List<MovieDetails>> GetMoviesByTitle(string name)
        {
            List<MovieDetails> _movieList = new List<MovieDetails>();
            if (string.IsNullOrEmpty(name))
            {
                return new List<MovieDetails>();
            }
            ApiSearchResponse<MovieInfo> response = await _movieApi.SearchByTitleAsync(name);
            _movieList = await GetMovies(response.Results);
            return _movieList;
        }

        public async Task<List<MovieDetails>> GetTopRatedMovies()
        {
            List<MovieDetails> _movieList = new List<MovieDetails>();
            ApiSearchResponse<MovieInfo> response = await _movieApi.GetTopRatedAsync();
            _movieList = await GetMovies(response.Results);
            return _movieList;
        }

        private async Task<List<MovieDetails>> GetMovies(IReadOnlyList<MovieInfo> response)
        {
            List<MovieDetails> _movieList = new List<MovieDetails>();
            var result = (from x in response select x).ToList();
            var Movies = new List<MovieDetails>();

            foreach (MovieInfo info in result)
            {

                var MovieDetails = new MovieDetails()
                {
                    Title = info.Title,
                    Id = info.Id,
                    Genre = (from x in info.Genres select x.Name).ToList(),
                    ReleaseDate = info.ReleaseDate.Year,
                    Description = info.Overview,
                    ImagePath = "http://image.tmdb.org/t/p/original/" + info.PosterPath
                };

                MovieDetails.Actors = await getCredits(MovieDetails.Id);
                if (MovieDetails != null)
                {
                    _movieList.Add(MovieDetails);
                }
            }
            return _movieList;
        }
        private async Task<List<string>> getCredits(int movieId)
        {
            ApiQueryResponse<MovieCredit> response = await _movieApi.GetCreditsAsync(movieId);
            try
            {
                var actors = (from x in response.Item.CastMembers select x.Name).Take(3).ToList();
                return actors;

            }
            catch (NullReferenceException e)
            {
                //WriteLine(e);
                return null;
            }

        }
    }
}
