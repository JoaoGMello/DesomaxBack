namespace DesomaxBack.ViewModels
{
    public class CarDetailsViewModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public string? Km { get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Like { get; set; }
    }
}
