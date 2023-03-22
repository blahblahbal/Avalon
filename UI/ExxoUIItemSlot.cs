using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUIItemSlot : ExxoUIImageButton
{
    private Item item;

    public ExxoUIItemSlot(Asset<Texture2D> backgroundTexture, int itemType) : base(backgroundTexture)
    {
        item = new Item();
        item.netDefaults(itemType);
        item.stack = 1;

        InnerImage = new ExxoUIImage(TextureAssets.Item[item.type])
        {
            HAlign = UIAlign.Center, VAlign = UIAlign.Center,
        };
        Append(InnerImage);
    }

    public ExxoUIImage InnerImage { get; }

    public Item Item
    {
        get => item;
        set
        {
            item = value;
            InnerImage.SetImage(TextureAssets.Item[Item.type]);
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        if (IsMouseHovering)
        {
            Main.hoverItemName = " ";
            Main.HoverItem = Item;
        }
    }

    protected override void DrawChildren(SpriteBatch spriteBatch)
    {
        if (Item.stack > 0)
        {
            base.DrawChildren(spriteBatch);
        }
    }
}
