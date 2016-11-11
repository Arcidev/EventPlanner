
namespace EventPlanner.DAL.Entities
{
    public class Place
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Place(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
