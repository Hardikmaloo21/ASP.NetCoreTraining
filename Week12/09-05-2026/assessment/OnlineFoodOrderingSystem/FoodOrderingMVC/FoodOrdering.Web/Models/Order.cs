using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Web.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    }
}