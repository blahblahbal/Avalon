using Microsoft.Xna.Framework;

namespace Avalon.UI.Next;

public record SnapNode(string Name, int Id, Vector2 Anchor, Vector2 Offset) {
    public readonly string Name = Name;
    public readonly int Id = Id;
    public readonly Vector2 Anchor = Anchor;
    public readonly Vector2 Offset = Offset;
    public Vector2 Position { get; private set; }

    public SnapNode(string name, int id) : this(name, id, new Vector2(0.5f),
        Vector2.Zero) {
    }

    public void Recalculate(Rectangle bounds) {
        Position = new Vector2(bounds.X, bounds.Y) + Offset + Anchor * new Vector2(bounds.Width, bounds.Height);
    }
}
