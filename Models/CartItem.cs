namespace Picklr.Models
{
    public class CartItem
    {
        public string CartItemId { get; set; } = Guid.NewGuid().ToString();
        public int ProgramID { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string ClubName { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public string SelectedDate { get; set; } = string.Empty;
    }
}
