namespace DBAccessLib
{
    public interface ICartControl
    {
        public void CartSetup();
        public void AddToCart(int product, int quantity);
        public void SaveCartOnExit();
        public void ChangeCartQuantity(int product, int quantity);
        public void PlaceOrder();
    }
}