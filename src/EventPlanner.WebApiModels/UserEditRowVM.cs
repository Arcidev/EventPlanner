namespace EventPlanner.WebApiModels
{
    public class UserEditRowVM
    {
        public string UserName { get; set; }
        public int TableKey { get; set; }
        public int[] Hours { get; set; } = { };
    }
}