namespace EventPlanner.WebApiModels
{
    public class TableVM
    {
        public int Key { get; set; }
        public HeaderVM Header { get; set; }
        public UserRowVM[] UserRows { get; set; }

    }
}