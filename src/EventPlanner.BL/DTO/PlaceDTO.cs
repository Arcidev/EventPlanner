

namespace EventPlanner.BL.DTO
{
    public class PlaceDTO
    {
        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            PlaceDTO fooItem = obj as PlaceDTO;
            if (fooItem == null)
            {
                return false;
            }
            

            return fooItem.X == this.X && fooItem.Y == this.Y;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.X.GetHashCode();
            hash = (hash * 7) + this.Y.GetHashCode();
            return hash;
        }
    }
}
