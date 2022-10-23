namespace SquaresApp.Models.Geometry
{
    public struct SquareIdentifier
    {
        public int AX { get; }
        public int AY { get; }
        public int BX { get; }
        public int BY { get; }
        public int CX { get; }
        public int CY { get; }

        public SquareIdentifier(int ax, int ay, int bx, int by, int cx, int cy)
        {
            AX = ax;
            AY = ay;
            BX = bx;
            BY = by;
            CX = cx;
            CY = cy;
        }
    }
}