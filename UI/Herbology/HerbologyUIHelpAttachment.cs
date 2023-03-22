using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Avalon.UI.Herbology;

internal class HerbologyUIHelpAttachment : ExxoUIAttachment<ExxoUIElement, ExxoUITextPanel>
{
    private bool attachedThisUpdate;
    private bool enabled;

    public HerbologyUIHelpAttachment() : base(new ExxoUITextPanel(LocalizedText.Empty))
    {
        Color newColor = AttachmentElement.BackgroundColor;
        newColor.A = byte.MaxValue;
        AttachmentElement.BackgroundColor = newColor;
    }

    public bool Enabled
    {
        get => enabled;
        set
        {
            enabled = value;
            if (!enabled)
            {
                AttachTo(null);
            }
        }
    }

    public override void AttachTo(ExxoUIElement? attachmentHolder)
    {
        if (attachmentHolder != AttachmentHolder)
        {
            if (AttachmentHolder is ExxoUIPanel oldHolder)
            {
                oldHolder.ResetColor();
            }
        }

        base.AttachTo(attachmentHolder);
        if (AttachmentHolder is ExxoUIPanel newHolder)
        {
            newHolder.BorderColor = Color.Gold;
        }
    }

    public void Register(ExxoUIElement element, string description)
    {
        element.OnMouseOver += (evt, _) =>
        {
            if (!attachedThisUpdate && Enabled && element.ContainsPoint(evt.MousePosition))
            {
                attachedThisUpdate = true;
                AttachTo(element);
                AttachmentElement.TextElement.SetText(description);
            }
        };
        element.OnLastMouseOut += delegate
        {
            if (AttachmentHolder == element)
            {
                AttachTo(null);
            }
        };
    }

    /// <inheritdoc />
    protected override void PositionAttachment(ref Vector2 position)
    {
        base.PositionAttachment(ref position);
        position.Y -= AttachmentElement.GetOuterDimensions().Height;
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        attachedThisUpdate = false;
    }
}
