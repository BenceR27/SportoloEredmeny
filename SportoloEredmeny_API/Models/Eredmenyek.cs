namespace SportoloEredmeny_API.Models
{
    public class Eredmenyek
    {
        public int Id { get; set; }
        public string Competition { get; set; }
        public string Description { get; set; }
        public DateTime ResultTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int SportoloId { get; set; }
    }
}
