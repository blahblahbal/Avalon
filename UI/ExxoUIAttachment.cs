using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Avalon.UI;

public class ExxoUIAttachment<THolder, TAttachment> : ExxoUIElement
    where THolder : ExxoUIElement where TAttachment : ExxoUIElement
{
    public ExxoUIAttachment(TAttachment uiElement)
    {
        Active = false;
        Width = StyleDimension.Fill;
        Height = StyleDimension.Fill;
        AttachmentElement = uiElement;
        Append(AttachmentElement);
    }

    public event EventHandler<AttachToEventArgs>? OnAttachTo;
    public event EventHandler<PositionAttachmentEventArgs>? OnPositionAttachment;

    /// <inheritdoc />
    public override bool IsDynamicallySized => false;

    public TAttachment AttachmentElement { get; }

    public THolder? AttachmentHolder { get; private set; }

    public override bool ContainsPoint(Vector2 point) => IsVisible && AttachmentElement.ContainsPoint(point);

    public virtual void AttachTo(THolder? attachmentHolder)
    {
        AttachmentHolder = attachmentHolder;
        Active = AttachmentHolder != null;
        OnAttachTo?.Invoke(this, new AttachToEventArgs(attachmentHolder));
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (AttachmentHolder == null)
        {
            return;
        }

        Vector2 position = AttachmentHolder.GetDimensions().Position() - Parent.GetOuterDimensions().Position();
        PositionAttachment(ref position);
        AttachmentElement.Left.Set(position.X, 0);
        AttachmentElement.Top.Set(position.Y, 0);
        RecalculateChildrenSelf();
        base.DrawSelf(spriteBatch);
    }

    protected virtual void PositionAttachment(ref Vector2 position)
    {
        var args = new PositionAttachmentEventArgs(position);
        OnPositionAttachment?.Invoke(this, args);
        position = args.Position;
    }

    public class PositionAttachmentEventArgs : EventArgs
    {
        public PositionAttachmentEventArgs(Vector2 position) => Position = position;
        public Vector2 Position { get; set; }
    }

    public class AttachToEventArgs : EventArgs
    {
        public AttachToEventArgs(ExxoUIElement? attachmentHolder) => AttachmentHolder = attachmentHolder;
        public ExxoUIElement? AttachmentHolder { get; }
    }
}
