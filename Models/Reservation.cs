using System.ComponentModel.DataAnnotations;

namespace Picklr.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        public int ProgramID { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string ClubName { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public string SelectedDate { get; set; } = string.Empty;
        public DateTime BookedOn { get; set; } = DateTime.Now;
    }
}
