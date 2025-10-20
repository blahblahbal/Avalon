using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.Coughwood;
public class Coughwood : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Wood;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<CoughwoodTile>());
	}
}

[LegacyName("Coughwood")]
public class CoughwoodTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(116, 138, 106));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Coughwood>();
        DustType = ModContent.DustType<Dusts.CoughwoodDust>();
    }
}