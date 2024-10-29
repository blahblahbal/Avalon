using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class LivingPathogen : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;
		Main.tileLighted[Type] = true;
		DustType = ModContent.DustType<PathogenFlameDust>();
		AnimationFrameHeight = 90;
		AddMapEntry(new Color(166, 116, 236));
	}
	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}
	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frame = Main.tileFrame[TileID.LivingFire];
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.5f;
		g = 0f;
		b = 2f;
	}
}
