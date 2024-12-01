namespace DestifyMovie.Data.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();
    public virtual ICollection<MovieRating> Ratings { get; set; } = new List<MovieRating>();
}
