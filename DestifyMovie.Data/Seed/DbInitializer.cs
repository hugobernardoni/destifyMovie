using Microsoft.AspNetCore.Identity;
using DestifyMovie.Data.Entities;

namespace DestifyMovie.Data.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(UserManager<User> userManager, AppDbContext dbContext)
    {
        await SeedUsersAsync(userManager);
        await SeedActorsAndMoviesAsync(dbContext);
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@destify.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, "Admin@123");            
        }
    }

    private static async Task SeedActorsAndMoviesAsync(AppDbContext dbContext)
    {
        if (!dbContext.Movies.Any() && !dbContext.Actors.Any())
        {
            var actors = new List<Actor>
            {
                new Actor { Name = "Leonardo DiCaprio" },
                new Actor { Name = "Scarlett Johansson" },
                new Actor { Name = "Robert Downey Jr." },
                new Actor { Name = "Meryl Streep" },
                new Actor { Name = "Tom Hanks" }
            };

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Inception",
                    ReleaseYear = 2010,
                    Ratings = new List<MovieRating>
                    {
                        new MovieRating { Rating = 5, Comment = "Mind-blowing!" },
                        new MovieRating { Rating = 4, Comment = "A masterpiece!" }
                    },
                    Actors = new List<Actor> { actors[0], actors[4] }
                },
                new Movie
                {
                    Title = "Avengers: Endgame",
                    ReleaseYear = 2019,
                    Ratings = new List<MovieRating>
                    {
                        new MovieRating { Rating = 5, Comment = "Epic conclusion!" },
                        new MovieRating { Rating = 4, Comment = "Amazing cast and story." }
                    },
                    Actors = new List<Actor> { actors[1], actors[2] }
                },
                new Movie
                {
                    Title = "The Devil Wears Prada",
                    ReleaseYear = 2006,
                    Ratings = new List<MovieRating>
                    {
                        new MovieRating { Rating = 4, Comment = "Fantastic performances!" },
                        new MovieRating { Rating = 5, Comment = "Meryl Streep at her best!" }
                    },
                    Actors = new List<Actor> { actors[3] }
                }
            };

            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();
        }
    }
}
