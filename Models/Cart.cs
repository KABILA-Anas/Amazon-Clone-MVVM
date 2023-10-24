namespace Shoping.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem>? Items { get; set; } = new List<CartItem>();

        public void AddItem(int productId, int quantity = 1)
        {
            var existingItem = Items.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
        }

        /*public float TotalPrice()
        {
            float total = 0;
            foreach (var item in Items)
            {
                total += item.Product.Price * item.Quantity.GetValueOrDefault();
            }

            return total;
        }*/

        public int Size()
        {
            /*int size = 0;
            foreach (var item in Items)
            {
                size += item.Quantity.GetValueOrDefault();
            }

            return size;*/

            return Items.Sum(item => item.Quantity.GetValueOrDefault());

        }
    }
}
