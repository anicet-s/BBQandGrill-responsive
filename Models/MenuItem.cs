using System;

namespace BBQandGrill.Models
{
    /// <summary>
    /// Represents a menu item
    /// </summary>
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MenuCategory Category { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsSpecial { get; set; }
        public int? CalorieCount { get; set; }

        public string FormattedPrice
        {
            get
            {
                return Price.ToString("C");
            }
        }

        public string CategoryName
        {
            get
            {
                return Category.ToString();
            }
        }
    }

    /// <summary>
    /// Menu categories
    /// </summary>
    public enum MenuCategory
    {
        Appetizer,
        Entree,
        Dessert,
        Beverage,
        Side
    }
}
