using ExxoAvalonOrigins.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Banners;

public class GoldSlimeBanner : ModItem
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("Nearby players get a bonus against: Gold Slime");
        Item.ResearchUnlockCount = 1;
    }
    // The tooltip for this item is automatically assigned from .lang files
    public override void SetDefaults()
    {
        Item.width = 10;
        Item.height = 24;
        Item.maxStack = 99;
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.useAnimation = 15;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(0, 0, 10, 0);
        Item.createTile = ModContent.TileType<MonsterBanner>();
        Item.placeStyle = 29;
    }
}
