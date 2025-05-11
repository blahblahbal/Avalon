using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;
public class RhodiumBrickWall : ModWall
{
	public override void SetStaticDefaults()
	{
		Main.wallHouse[Type] = true;
		//ItemDrop = ModContent.ItemType<Items.Placeable.Wall.RhodiumBrickWallItem>();
		AddMapEntry(new Color(79, 40, 61));
		DustType = ModContent.DustType<Dusts.RhodiumDust>();
	}
}
