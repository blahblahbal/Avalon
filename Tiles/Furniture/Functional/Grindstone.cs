using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Functional;

public class Grindstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(116, 116, 116), Language.GetText("Mods.Avalon.Tiles.Grindstone.MapEntry"));
        Main.tileFrameImportant[Type] = true;
		AnimationFrameHeight = 38;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
		TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        DustType = DustID.Stone;
    }
	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter > 4)
		{
			frameCounter = 0;
			frame++;
			if (frame >= 2)
			{
				frame = 0;
			}
		}
	}
}
