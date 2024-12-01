namespace DestifyMovie.Data.Entities;

public class MovieRating
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int MovieId { get; set; }
    public virtual Movie? Movie { get; set; }
}
