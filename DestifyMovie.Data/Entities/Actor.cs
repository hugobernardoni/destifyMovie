namespace DestifyMovie.Data.Entities;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
