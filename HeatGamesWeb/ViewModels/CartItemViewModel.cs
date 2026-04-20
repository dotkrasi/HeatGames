namespace HeatGamesWeb.ViewModels
{
    public class CartItemViewModel
    {
        public Guid GameId { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}