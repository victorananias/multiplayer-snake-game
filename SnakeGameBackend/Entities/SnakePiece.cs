namespace SnakeGameBackend.Entities
{
    public class SnakePiece
    {
        public SnakePiece(int x, int y)
        {
            X = x;
            Y = y;
            Size = 20;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
    }
}