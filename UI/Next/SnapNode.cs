using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.Next;

public record SnapNode(string Name, int Id, Vector2 Anchor, Vector2 Offset) {
    public readonly string Name = Name;
    public readonly int Id = Id;
    public readonly Vector2 Anchor = Anchor;
    public readonly Vector2 Offset = Offset;

    public SnapNode(string name, int id) : this(name, id, new Vector2(0.5f), Vector2.Zero) {
    }

    public SnapPoint ToSnapPoint(ExxoUIElement exxoUIElement) {
        var point = new SnapPoint(Name, Id, Vector2.Zero,
            exxoUIElement.InnerBounds.TopLeft() + Offset +
            Anchor * new Vector2(exxoUIElement.InnerBounds.Width, exxoUIElement.InnerBounds.Height));
        point.Calculate(new UIElement());
        return point;
    }
}
