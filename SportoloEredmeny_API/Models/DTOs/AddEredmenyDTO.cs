namespace SportoloEredmeny_API.Models.DTOs
{
    public class AddEredmenyDTO
    {
        public string Competition {  get; set; }
        public string Description { get; set; }
        public DateTime ResultTime { get; set; }
        public DateTime  UpdateTime { get; set; }
    }
}
