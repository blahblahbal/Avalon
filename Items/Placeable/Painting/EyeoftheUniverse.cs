using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class EyeoftheUniverse : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 50;
        Item.height = 36;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<Rarities.MagentaRarity>();
        Item.createTile = ModContent.TileType<Tiles.EyeoftheUniverse>();
        Item.placeStyle = 0;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 10, 0);
        Item.useAnimation = 15;
    }
}
