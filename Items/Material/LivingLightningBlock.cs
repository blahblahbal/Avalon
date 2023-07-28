using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class LivingLightningBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.LivingLightning>();
        Item.useTime = 10;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.value = 50;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.Size = new Vector2(16);
    }
}
