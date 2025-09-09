using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Gem;

public class PeridotSquirrelCage : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.CritterCageLidStyle[Type] = 2;
		Main.tileFrameImportant[Type] = true;
		Main.tileTable[Type] = true;
		Main.tileLavaDeath[Type] = true;
		Main.tileSolidTop[Type] = true;


		// The larger cage uses Style6x3.
		TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(Type);

		AddMapEntry(new Color(122, 217, 232), CreateMapEntryName());
		RegisterItemDrop(ModContent.ItemType<Items.Placeable.Furniture.Gem.PeridotSquirrelCage>());

		DustType = DustID.Glass;
	}

	private readonly int animationFrameHeight = 54;

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		// This code utilizes some math to stagger each individual tile. First the top left tile is found, then those coordinates are passed into some math to stagger an index into Main.snail2CageFrame
		// Main.snail2CageFrame is used since we want the same animation, but if we wanted a different frame count or a different animation timing, we could write our own by adapting vanilla code and placing the code in AnimateTile
		Tile tile = Main.tile[i, j];
		Main.critterCage = true;
		int left = i - tile.TileFrameX / 18;
		int top = j - tile.TileFrameY / 18;
		int offset = left / 3 * (top / 3);
		offset %= Main.cageFrames;
		frameYOffset = Main.squirrelCageFrame[offset] * animationFrameHeight;
	}
}
public class TourmalineSquirrelCage : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.CritterCageLidStyle[Type] = 2;
		Main.tileFrameImportant[Type] = true;
		Main.tileTable[Type] = true;
		Main.tileLavaDeath[Type] = true;
		Main.tileSolidTop[Type] = true;

		// The larger cage uses Style6x3.
		TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(Type);

		AddMapEntry(new Color(122, 217, 232), CreateMapEntryName());
		RegisterItemDrop(ModContent.ItemType<Items.Placeable.Furniture.Gem.TourmalineSquirrelCage>());

		DustType = DustID.Glass;
	}

	private readonly int animationFrameHeight = 54;

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		// This code utilizes some math to stagger each individual tile. First the top left tile is found, then those coordinates are passed into some math to stagger an index into Main.snail2CageFrame
		// Main.snail2CageFrame is used since we want the same animation, but if we wanted a different frame count or a different animation timing, we could write our own by adapting vanilla code and placing the code in AnimateTile
		Tile tile = Main.tile[i, j];
		Main.critterCage = true;
		int left = i - tile.TileFrameX / 18;
		int top = j - tile.TileFrameY / 18;
		int offset = left / 3 * (top / 3);
		offset %= Main.cageFrames;
		frameYOffset = Main.squirrelCageFrame[offset] * animationFrameHeight;
	}
}
public class ZirconSquirrelCage : ModTile
{
	public override void SetStaticDefaults()
	{
		TileID.Sets.CritterCageLidStyle[Type] = 2;
		Main.tileFrameImportant[Type] = true;
		Main.tileTable[Type] = true;
		Main.tileLavaDeath[Type] = true;
		Main.tileSolidTop[Type] = true;

		// The larger cage uses Style6x3.
		TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(Type);

		AddMapEntry(new Color(122, 217, 232), CreateMapEntryName());
		RegisterItemDrop(ModContent.ItemType<Items.Placeable.Furniture.Gem.ZirconSquirrelCage>());

		DustType = DustID.Glass;
	}

	private readonly int animationFrameHeight = 54;

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		// This code utilizes some math to stagger each individual tile. First the top left tile is found, then those coordinates are passed into some math to stagger an index into Main.snail2CageFrame
		// Main.snail2CageFrame is used since we want the same animation, but if we wanted a different frame count or a different animation timing, we could write our own by adapting vanilla code and placing the code in AnimateTile
		Tile tile = Main.tile[i, j];
		Main.critterCage = true;
		int left = i - tile.TileFrameX / 18;
		int top = j - tile.TileFrameY / 18;
		int offset = left / 3 * (top / 3);
		offset %= Main.cageFrames;
		frameYOffset = Main.squirrelCageFrame[offset] * animationFrameHeight;
	}
}
