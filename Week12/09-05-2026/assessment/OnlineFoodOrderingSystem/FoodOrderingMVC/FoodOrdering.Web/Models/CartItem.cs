namespace FoodOrdering.Web.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int FoodItemId { get; set; }

        public FoodItem FoodItem { get; set; }

        public int Quantity { get; set; }
    }
}