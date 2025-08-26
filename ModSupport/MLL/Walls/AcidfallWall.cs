using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Walls;
public class AcidfallWall : ModWall
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(103, 181, 33));
		Main.wallHouse[Type] = true;
		HitSound = SoundID.Shatter;
		DustType = DustID.Glass;
	}
	public override void AnimateWall(ref byte frame, ref byte frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 7) // waterfalls set this value to 5, lava/honey/sand set this to 10
		{
			frameCounter = 0;
			frame++;
			if (frame > 7)
			{
				frame = 0;
			}
		}
	}
}
