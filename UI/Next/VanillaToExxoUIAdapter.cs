using System;
using System.Collections.Generic;
using Avalon.UI.Next.Enums;
using Avalon.UI.Next.Events;
using Avalon.UI.Next.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.Next;

public class VanillaToExxoUIAdapter : ExxoUIElement {
    public readonly UIElement VanillaElement;

    public VanillaToExxoUIAdapter(UIElement vanillaElement) {
        VanillaElement = vanillaElement;
        UIElement lastElementTarget = VanillaElement;
        vanillaElement.Recalculate();

        OnInputEvent += (_, args) => {
            if (args is not MouseButtonEventArgs mouseClickEventArgs) {
                return;
            }

            UIElement? vanillaTarget = VanillaElement.GetElementAt(mouseClickEventArgs.MousePosition);
            var mouseEvent = new UIMouseEvent(VanillaElement, mouseClickEventArgs.MousePosition);
            switch (mouseClickEventArgs.ButtonType) {
                case ButtonType.LeftClick when mouseClickEventArgs.Pressed:
                    vanillaTarget.LeftMouseDown(mouseEvent);
                    break;
                case ButtonType.LeftClick: {
                    if (mouseClickEventArgs.DoubleClick) {
                        vanillaTarget.LeftDoubleClick(mouseEvent);
                    }
                    else {
                        vanillaTarget.LeftClick(mouseEvent);
                    }

                    vanillaTarget.LeftMouseUp(mouseEvent);
                    break;
                }
                case ButtonType.RightClick when mouseClickEventArgs.Pressed:
                    vanillaTarget.RightMouseDown(mouseEvent);
                    break;
                case ButtonType.RightClick: {
                    if (mouseClickEventArgs.DoubleClick) {
                        vanillaTarget.RightDoubleClick(mouseEvent);
                    }
                    else {
                        vanillaTarget.RightClick(mouseEvent);
                    }

                    vanillaTarget.RightMouseUp(mouseEvent);
                    break;
                }
                case ButtonType.MiddleClick when mouseClickEventArgs.Pressed:
                    vanillaTarget.MiddleMouseDown(mouseEvent);
                    break;
                case ButtonType.MiddleClick: {
                    if (mouseClickEventArgs.Factor != null) {
                        vanillaTarget.ScrollWheel(new UIScrollWheelEvent(mouseEvent.Target, mouseEvent.MousePosition,
                            (int)(mouseClickEventArgs.Factor + 0.5f)));
                        break;
                    }

                    if (mouseClickEventArgs.DoubleClick) {
                        vanillaTarget.MiddleDoubleClick(mouseEvent);
                    }
                    else {
                        vanillaTarget.MiddleClick(mouseEvent);
                    }

                    vanillaTarget.MiddleMouseUp(mouseEvent);
                    break;
                }
                case ButtonType.None when mouseClickEventArgs.Pressed:
                    vanillaTarget.MouseOver(mouseEvent);
                    lastElementTarget = vanillaTarget;
                    break;
                case ButtonType.None:
                    lastElementTarget.MouseOut(mouseEvent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
    }

    /// <inheritdoc />
    protected override void RecalculateSelf() {
        if (Width.Unit == ScreenUnit.Auto) {
            Width = new UIDimension(VanillaElement.GetOuterDimensions().Width, ScreenUnit.Auto);
        }

        if (Height.Unit == ScreenUnit.Auto) {
            Height = new UIDimension(VanillaElement.GetOuterDimensions().Height, ScreenUnit.Auto);
        }

        base.RecalculateSelf();
        VanillaElement.Left = StyleDimension.FromPixels(InnerBounds.X);
        VanillaElement.Top = StyleDimension.FromPixels(InnerBounds.Y);
        VanillaElement.Width = StyleDimension.FromPixels(InnerBounds.Width);
        VanillaElement.Height = StyleDimension.FromPixels(InnerBounds.Height);
        VanillaElement.Recalculate();
    }

    /// <inheritdoc />
    protected override void PostRecalculate() {
        base.PostRecalculate();
        Outdated = true;
    }

    /// <inheritdoc />
    protected override void UpdateSelf(GameTime gameTime) {
        base.UpdateSelf(gameTime);
        VanillaElement.Update(gameTime);
    }

    /// <inheritdoc />
    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);
        VanillaElement.Draw(spriteBatch);
    }

    /// <inheritdoc />
    public override bool ContainsPoint(Point point) {
        return VanillaElement.ContainsPoint(point.ToVector2());
    }

    /// <inheritdoc />
    public override List<SnapPoint> GetSnapPoints() {
        return VanillaElement.GetSnapPoints();
    }
}
