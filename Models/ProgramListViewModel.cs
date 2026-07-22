namespace Picklr.Models
{
    public class DateOption
    {
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }

    public class ProgramListViewModel
    {
        // Filter parameters echoed back to the view
        public int? SelectedClubId { get; set; }
        public string? SelectedDate { get; set; }

        // Weekday name derived from SelectedDate, e.g. "Friday"
        public string? DayOfWeek { get; set; }

        // Full "Wednesday, July 15" label derived from SelectedDate, for the page heading
        public string? FriendlyDate { get; set; }

        // Dropdown sources
        public List<Club> Clubs { get; set; } = new List<Club>();
        public List<DateOption> DateOptions { get; set; } = new List<DateOption>();

        // Query results
        public List<PicklrProgram> Programs { get; set; } = new List<PicklrProgram>();

        public int CartCount { get; set; }
    }
}
