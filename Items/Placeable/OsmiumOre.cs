using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Placeable
{
    public class OsmiumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Ores.OsmiumOre>();
            Item.rare = ItemRarityID.Orange;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 9, 0);
            Item.useAnimation = 15;
            Item.Size = new Vector2(16);
        }
    }
}
