using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trap;

public class CannonballTrap : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.DemoniteBrick);
        Item.mech = true;
        Item.createTile = ModContent.TileType<Tiles.Savanna.CannonballTrap>();
    }
}
