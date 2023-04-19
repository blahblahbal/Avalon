namespace Avalon.UI.Next.Structs;

public readonly struct EdgeInsets {
    public readonly int Left;
    public readonly int Top;
    public readonly int Right;
    public readonly int Bottom;

    public int Horizontal => Left + Right;
    public int Vertical => Bottom + Top;

    public EdgeInsets(int left, int top, int right, int bottom) {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public EdgeInsets(int vertical, int horizontal) : this(horizontal, vertical, horizontal, vertical) {
    }

    public EdgeInsets(int all) : this(all, all) {
    }
}
