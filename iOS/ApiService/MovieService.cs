using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;

namespace MovieSearch.iOS.ApiService
{
    public class MovieService
    {
        private ImageDownloader _ImageDownloader;

        public MovieService(ImageDownloader downloader)
        {
            _ImageDownloader = downloader;
        }


        public async Task<string> getMovie(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Please Enter A Title";
            }
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
        public async Task<List<MovieDetails>> getMovies(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<MovieDetails>();
            }
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(name);

            var result = (from x in response.Results select x).ToList();
            var Movies = new List<MovieDetails>();
            var cancelToke = new CancellationTokenSource();
            CancellationToken token = cancelToke.Token;
            foreach (MovieInfo info in result)
            {
                var localPath = "";
                if (!string.IsNullOrEmpty(info.PosterPath))
                {
                    localPath = _ImageDownloader.LocalPathForFilename(info.PosterPath);
                    await _ImageDownloader.DownloadImage(info.PosterPath, localPath, token);
                }

                var MovieDetails = new MovieDetails()
                {
                    Title = info.Title,
                    Id = info.Id,
                    Genre = (from x in info.Genres select x.Name).ToList(),
                    ReleaseDate = info.ReleaseDate,
                    Description = info.Overview,
                    ImagePath = localPath
                };
                MovieDetails.actors = await getCredits(MovieDetails.Id);
                if (MovieDetails != null)
                {
                    Movies.Add(MovieDetails);
                }


            }
            return Movies;
        }
        public async Task<List<string>> getCredits(int movieId)
        {

            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiQueryResponse<MovieCredit> response = await movieApi.GetCreditsAsync(movieId);

            var actors = (from x in response.Item.CastMembers select x.Name).Take(3).ToList();

            return actors;

        }
    }
}
