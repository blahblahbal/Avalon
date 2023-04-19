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
    private readonly UIElement vanillaElement;

    public VanillaToExxoUIAdapter(UIElement vanillaElement) {
        this.vanillaElement = vanillaElement;
        UIElement lastElementTarget = this.vanillaElement;
        vanillaElement.Recalculate();

        OnInputEvent += (_, args) => {
            if (args is not MouseButtonEventArgs mouseClickEventArgs) {
                return;
            }

            UIElement? vanillaTarget = this.vanillaElement.GetElementAt(mouseClickEventArgs.MousePosition);
            var mouseEvent = new UIMouseEvent(this.vanillaElement, mouseClickEventArgs.MousePosition);
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
            Width = new UIDimension(vanillaElement.GetOuterDimensions().Width, ScreenUnit.Auto);
        }

        if (Height.Unit == ScreenUnit.Auto) {
            Height = new UIDimension(vanillaElement.GetOuterDimensions().Height, ScreenUnit.Auto);
        }

        base.RecalculateSelf();
        vanillaElement.Left = StyleDimension.FromPixels(InnerBounds.X);
        vanillaElement.Top = StyleDimension.FromPixels(InnerBounds.Y);
        vanillaElement.Width = StyleDimension.FromPixels(InnerBounds.Width);
        vanillaElement.Height = StyleDimension.FromPixels(InnerBounds.Height);
        vanillaElement.Recalculate();
    }

    /// <inheritdoc />
    protected override void PostRecalculate() {
        base.PostRecalculate();
        Outdated = true;
    }

    /// <inheritdoc />
    protected override void UpdateSelf(GameTime gameTime) {
        base.UpdateSelf(gameTime);
        vanillaElement.Update(gameTime);
    }

    /// <inheritdoc />
    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);
        vanillaElement.Draw(spriteBatch);
    }

    /// <inheritdoc />
    public override bool ContainsPoint(Point point) {
        return vanillaElement.ContainsPoint(point.ToVector2());
    }

    /// <inheritdoc />
    public override List<SnapPoint> GetSnapPoints() {
        return vanillaElement.GetSnapPoints();
    }
}
