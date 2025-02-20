using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Wall;

public class UnsafeAncientOrangeBrickWall : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
        ItemID.Sets.DrawUnsafeIndicator[Type] = true;
		ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<AncientOrangeBrickWall>()] = Type;
    }

    public override void SetDefaults()
    {
        Item.Size = new Vector2(12);
        Item.autoReuse = true;
        Item.consumable = true;
        Item.useTurn = true;
        Item.useTime = 7;
        Item.createWall = ModContent.WallType<Walls.UnsafeAncientOrangeBrickWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
}
