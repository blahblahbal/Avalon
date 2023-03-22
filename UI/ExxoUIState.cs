using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace Avalon.UI;

public abstract class ExxoUIState : UIState
{
    protected virtual bool DisableRecipeScrolling => true;
    protected virtual bool FocusInteractionsToUI => true;
    protected virtual bool HideItemHoverIcon => true;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (IsMouseHovering)
        {
            Main.LocalPlayer.cursorItemIconEnabled = Main.LocalPlayer.cursorItemIconEnabled && !HideItemHoverIcon;
            Main.LocalPlayer.mouseInterface = FocusInteractionsToUI;
            if (DisableRecipeScrolling)
            {
                PlayerInput.LockVanillaMouseScroll("Avalon/ExxoUIState");
            }
        }
    }

    public override void OnInitialize()
    {
        base.OnInitialize();
        RemoveAllChildren();
    }

    public override void MiddleDoubleClick(UIMouseEvent evt)
    {
        base.MiddleDoubleClick(evt);
        OnInitialize(); //TODO: REMOVE
    }

    /// <inheritdoc />
    public override bool ContainsPoint(Vector2 point) => ChildrenContainsPoint(point);

    public bool ChildrenContainsPoint(Vector2 point) => Elements.Any(element => element.ContainsPoint(point));
}
