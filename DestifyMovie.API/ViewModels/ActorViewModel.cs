namespace DestifyMovie.API.ViewModels
{
    public class ActorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<MovieBasicViewModel> Movies { get; set; } = new();
    }
}
