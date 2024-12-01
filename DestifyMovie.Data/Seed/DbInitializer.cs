using Microsoft.AspNetCore.Identity;
using DestifyMovie.Data.Entities;

namespace DestifyMovie.Data.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(UserManager<User> userManager, AppDbContext context)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "admin",
                Email = "admin@example.com"                
            };

            await userManager.CreateAsync(user, "Admin@123");
        }

        if (!context.Movies.Any())
        {
            var actor1 = new Actor { Name = "Actor 1" };
            var actor2 = new Actor { Name = "Actor 2" };

            var movie1 = new Movie
            {
                Title = "Movie 1",
                ReleaseYear = 2020,
                Actors = new List<Actor> { actor1, actor2 },
                Ratings = new List<MovieRating>
                {
                    new MovieRating { Rating = 5, Comment = "Great movie!" }
                }
            };

            var movie2 = new Movie
            {
                Title = "Movie 2",
                ReleaseYear = 2021,
                Actors = new List<Actor> { actor1 }
            };

            context.Movies.AddRange(movie1, movie2);
            await context.SaveChangesAsync();
        }
    }
}
