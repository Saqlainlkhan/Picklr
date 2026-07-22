namespace Picklr.Models
{
    // wraps ISession so controllers don't have to call GetString/SetString directly
    public class PicklrSession
    {
        private const string CartKey = "Cart";
        private const string ClubIdKey = "SelectedClubId";
        private const string DateKey = "SelectedDate";

        private readonly ISession session;

        public PicklrSession(ISession session) => this.session = session;

        // Filter state
        public void SetSelectedClubId(int? clubId)
        {
            if (clubId.HasValue)
                session.SetInt32(ClubIdKey, clubId.Value);
            else
                session.Remove(ClubIdKey);
        }

        public int? GetSelectedClubId() => session.GetInt32(ClubIdKey);

        public void SetSelectedDate(string? date)
        {
            if (string.IsNullOrEmpty(date))
                session.Remove(DateKey);
            else
                session.SetString(DateKey, date);
        }

        public string? GetSelectedDate() => session.GetString(DateKey);

        // Cart
        public List<CartItem> GetCart() =>
            session.GetObject<List<CartItem>>(CartKey) ?? new List<CartItem>();

        public void SetCart(List<CartItem> cart) => session.SetObject(CartKey, cart);

        public void AddToCart(CartItem item)
        {
            var cart = GetCart();
            cart.Add(item);
            SetCart(cart);
        }

        public int GetCartCount() => GetCart().Count;

        public void ClearCart() => session.Remove(CartKey);

        public void RemoveItem(string cartItemId)
        {
            var cart = GetCart();
            cart.RemoveAll(i => i.CartItemId == cartItemId);
            SetCart(cart);
        }
    }
}
