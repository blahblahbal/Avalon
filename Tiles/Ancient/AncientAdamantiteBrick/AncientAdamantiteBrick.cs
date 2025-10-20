using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient.AncientAdamantiteBrick;
public class AncientAdamantiteBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<AncientAdamantiteBrickTile>());
	}
}

[LegacyName("AncientAdamantiteBrick")]
public class AncientAdamantiteBrickTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(202, 66, 133));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1750;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        HitSound = SoundID.Tink;
        DustType = DustID.Adamantite;
    }
}