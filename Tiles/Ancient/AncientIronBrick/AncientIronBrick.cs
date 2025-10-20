using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.AncientIronBrick;

public class AncientIronBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<AncientIronBrickTile>());
	}
}
public class AncientIronBrickTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(147, 119, 96));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1800;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Iron;
    }
}
