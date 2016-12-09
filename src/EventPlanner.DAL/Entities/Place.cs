
namespace EventPlanner.DAL.Entities
{
    public class Place
    {
        public double X { get; set; }

        public double Y { get; set; }

        public string Title { get; set; }

        public Place(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
