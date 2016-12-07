namespace EventPlanner.WebApiModels
{
    public class EventPageVM
    {
        public int SelectedPlaceId { get; set; }
        public MarkerVM[] Markers { get; set; }
        public TableVM[] Tables { get; set; }
    }
}