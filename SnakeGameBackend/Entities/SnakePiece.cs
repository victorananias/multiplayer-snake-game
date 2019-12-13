namespace SnakeGameBackend.Entities
{
    public class SnakePiece
    {
        public SnakePiece(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}