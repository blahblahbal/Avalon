using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

public class IconItem : ModItem
{
    public override string Texture => "Avalon/icon_small";
    public override void SetDefaults()
    {
        Item.width = Item.height = 28;
    }
}
