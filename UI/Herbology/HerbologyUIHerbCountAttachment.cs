using System.Globalization;
using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Data;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace ExxoAvalonOrigins.UI.Herbology;

internal class HerbologyUIHerbCountAttachment : ExxoUIAttachment<ExxoUIItemSlot, ExxoUIPanelWrapper<ExxoUIList>>
{
    public HerbologyUIHerbCountAttachment() : base(new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList()))
    {
        Color newColor = AttachmentElement.BackgroundColor;
        newColor.A = byte.MaxValue;
        AttachmentElement.BackgroundColor = newColor;

        AttachmentElement.InnerElement.FitHeightToContent = true;
        AttachmentElement.InnerElement.FitWidthToContent = true;
        AttachmentElement.InnerElement.Direction = Direction.Horizontal;

        Image = new ExxoUIImage();
        AttachmentElement.InnerElement.Append(Image);

        Text = new ExxoUIText(LocalizedText.Empty) { VAlign = UIAlign.Center };
        AttachmentElement.InnerElement.Append(Text);
    }

    public ExxoUIImage Image { get; }
    public ExxoUIText Text { get; }

    /// <inheritdoc />
    protected override void PositionAttachment(ref Vector2 position)
    {
        base.PositionAttachment(ref position);
        position.Y -= AttachmentElement.MinHeight.Pixels;
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);

        if (AttachmentHolder == null)
        {
            return;
        }

        AvalonHerbologyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>();

        int herbType = HerbologyData.GetBaseHerbType(AttachmentHolder.Item);

        if (herbType == -1)
        {
            return;
        }

        Text.SetText(modPlayer.HerbCounts.ContainsKey(herbType)
            ? modPlayer.HerbCounts[herbType].ToString(CultureInfo.CurrentCulture)
            : "0");

        Image.SetImage(TextureAssets.Item[herbType]);
    }
}
