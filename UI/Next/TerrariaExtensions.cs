using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Avalon.UI.Next;

public static class TerrariaExtensions {
    public static SnapPoint ToSnapPoint(this SnapNode snapNode) {
        var snapPoint = new SnapPoint(snapNode.Name, snapNode.Id, Vector2.Zero, snapNode.Position);
        snapPoint.Calculate(new UIElement());
        return snapPoint;
    }

    public static SnapNode ToSnapNode(this SnapPoint snapPoint) {
        var snapNode = new SnapNode(snapPoint.Name, snapPoint.Id, Vector2.Zero, snapPoint.Position);
        snapNode.Recalculate(new Rectangle());
        return snapNode;
    }
}
