using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Banners;

public class OreSlimeBanner : ModItem
{
    // The tooltip for this item is automatically assigned from .lang files
    public override void SetDefaults()
    {
        Item.width = 10;
        Item.height = 24;
        Item.maxStack = 9999;
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.useAnimation = 15;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(0, 0, 10);
        Item.createTile = ModContent.TileType<MonsterBanner>();
        Item.placeStyle = 23;
    }
}
