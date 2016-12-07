namespace EventPlanner.WebApiModels
{
    public class MarkerVM
    {
        public string Title { get; set; }
        public int Key { get; set; }
        public PositionVM Position { get; set; }
    }
}