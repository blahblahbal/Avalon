using System.Collections.Generic;
using Avalon.UI.Next.Enums;
using Avalon.UI.Next.Events;
using Avalon.UI.Next.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.Next;

public class ExxoToVanillaUIAdapter : UIElement {
    public readonly ExxoUIElement ExxoUIElement;
    private readonly ExxoUIElement parentElement;

    public ExxoToVanillaUIAdapter(ExxoUIElement exxoUIElement) {
        ExxoUIElement = exxoUIElement;
        ExxoUIElement.PositionMode = PositionMode.Absolute;
        MinWidth = StyleDimension.Fill;
        MinHeight = StyleDimension.Fill;
        parentElement = new ExxoUIElement();
        parentElement.Children.Add(exxoUIElement);

        OnMouseOver += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition, ButtonType.None, true, false));
            }
        };
        OnMouseOut += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.None,
                    false,
                    false));
            }
        };
        OnLeftMouseDown += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.LeftClick,
                    true,
                    false));
            }
        };
        OnLeftDoubleClick += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.LeftClick,
                    false,
                    true));
            }
        };
        OnLeftClick += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.LeftClick,
                    false,
                    false));
            }
        };
        OnLeftMouseUp += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.LeftClick,
                    false,
                    false));
            }
        };
        OnRightMouseDown += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.RightClick, true,
                    false));
            }
        };
        OnRightDoubleClick += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.RightClick,
                    false, true));
            }
        };
        OnRightClick += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.RightClick,
                    false, false));
            }
        };
        OnRightMouseUp += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.RightClick,
                    false, false));
            }
        };
        OnMiddleMouseDown += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.MiddleClick,
                    true, false));
            }
        };
        OnMiddleDoubleClick += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.MiddleClick,
                    false, true));
            }
        };
        OnMiddleClick += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.MiddleClick,
                    false, false));
            }
        };
        OnMiddleMouseUp += (evt, _) => {
            if (evt.Target is FakeEventTarget fakeEventTarget) {
                fakeEventTarget.LinkedElement.InputEvent(new MouseButtonEventArgs(fakeEventTarget.LinkedElement,
                    evt.MousePosition,
                    ButtonType.MiddleClick,
                    false, false));
            }
        };
    }

    /// <inheritdoc />
    public override void RecalculateChildren() {
        base.RecalculateChildren();
        parentElement.Position =
            new Point((int)(GetInnerDimensions().X + 0.5f), (int)(GetInnerDimensions().Y + 0.5f));
        parentElement.Width = new UIDimension((int)(GetInnerDimensions().Width + 0.5f), ScreenUnit.Pixels);
        parentElement.Height = new UIDimension((int)(GetInnerDimensions().Height + 0.5f), ScreenUnit.Pixels);
        parentElement.Recalculate();
    }

    /// <inheritdoc />
    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        ExxoUIElement.Update(gameTime);
    }

    /// <inheritdoc />
    protected override void DrawChildren(SpriteBatch spriteBatch) {
        base.DrawChildren(spriteBatch);
        ExxoUIElement.Draw(spriteBatch);
    }

    /// <inheritdoc />
    public override bool ContainsPoint(Vector2 point) {
        return ExxoUIElement.ContainsPoint(point.ToPoint());
    }

    /// <inheritdoc />
    public override List<SnapPoint> GetSnapPoints() {
        return ExxoUIElement.GetSnapPoints();
    }
}
