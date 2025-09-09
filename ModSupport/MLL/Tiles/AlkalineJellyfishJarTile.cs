using Avalon.ModSupport.MLL.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.ModSupport.MLL.Tiles;

public class AlkalineJellyfishJarTile : ModTile
{
	private static readonly byte[] JellyfishCageMode = new byte[Main.cageFrames];
	private static readonly int[] JellyfishCageFrame = new int[Main.cageFrames];
	private static readonly int[] JellyfishCageFrameCounter = new int[Main.cageFrames];
	private static Asset<Texture2D>? glow;

	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");

		Main.tileLavaDeath[Type] = true;
		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(Type);

		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(12, 218, 168), CreateMapEntryName());
		RegisterItemDrop(ModContent.ItemType<AlkalineJellyfishJar>());

		DustType = DustID.Glass;
	}
	public override bool CreateDust(int i, int j, ref int type)
	{
		if (!WorldGen.genRand.NextBool(3))
		{
			return false;
		}
		return base.CreateDust(i, j, ref type);
	}
	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
		Main.critterCage = true;
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		Tile tile = Main.tile[i, j];
		int smallAnimalCageFrame = TileDrawing.GetSmallAnimalCageFrame(i, j, tile.TileFrameX, tile.TileFrameY);
		bool electrocuting = JellyfishCageMode[smallAnimalCageFrame] == 2;
		if (electrocuting)
		{
			r = 0.1f;
			g = 0.65f;
			b = 0.5f;
		}
		else
		{
			r = 0.025f;
			g = 0.4f;
			b = 0.3f;
		}
	}
	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		Tile tile = Main.tile[i, j];
		int smallAnimalCageFrame = TileDrawing.GetSmallAnimalCageFrame(i, j, tile.TileFrameX, tile.TileFrameY);
		frameYOffset = JellyfishCageFrame[smallAnimalCageFrame] * 36;
	}
	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		Tile tile = Main.tile[i, j];
		int smallAnimalCageFrame = TileDrawing.GetSmallAnimalCageFrame(i, j, tile.TileFrameX, tile.TileFrameY);
		Vector2 screenPosition = Main.Camera.UnscaledPosition;
		Vector2 screenOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
		Main.spriteBatch.Draw(glow.Value, new Vector2(i * 16, j * 16 + 2) - screenPosition + screenOffset, new Rectangle(tile.TileFrameX, tile.TileFrameY + JellyfishCageFrame[smallAnimalCageFrame] * 36, 16, 16), new Color(200, 200, 200, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		for (int i = 0; i < Main.cageFrames; i++)
		{
			JellyfishCageFrameCounter[i]++;
			if (JellyfishCageMode[i] == 0 && Main.rand.NextBool(1800))
			{
				JellyfishCageMode[i] = 1;
			}
			if (JellyfishCageMode[i] == 2 && Main.rand.NextBool(60))
			{
				JellyfishCageMode[i] = 3;
			}
			int changeFrameRand = 1;
			if (JellyfishCageMode[i] == 0)
			{
				changeFrameRand = Main.rand.Next(10, 20);
			}
			if (JellyfishCageMode[i] == 1)
			{
				changeFrameRand = Main.rand.Next(15, 25);
			}
			if (JellyfishCageMode[i] == 2)
			{
				changeFrameRand = Main.rand.Next(4, 9);
			}
			if (JellyfishCageMode[i] == 3)
			{
				changeFrameRand = Main.rand.Next(15, 25);
			}
			if (JellyfishCageMode[i] == 0 && JellyfishCageFrame[i] <= 3 && JellyfishCageFrameCounter[i] >= changeFrameRand)
			{
				JellyfishCageFrameCounter[i] = 0;
				JellyfishCageFrame[i]++;
				if (JellyfishCageFrame[i] >= 4)
				{
					JellyfishCageFrame[i] = 0;
				}
			}
			if (JellyfishCageMode[i] == 1 && JellyfishCageFrame[i] <= 7 && JellyfishCageFrameCounter[i] >= changeFrameRand)
			{
				JellyfishCageFrameCounter[i] = 0;
				JellyfishCageFrame[i]++;
				if (JellyfishCageFrame[i] >= 7)
				{
					JellyfishCageMode[i] = 2;
				}
			}
			if (JellyfishCageMode[i] == 2 && JellyfishCageFrame[i] <= 9 && JellyfishCageFrameCounter[i] >= changeFrameRand)
			{
				JellyfishCageFrameCounter[i] = 0;
				JellyfishCageFrame[i]++;
				if (JellyfishCageFrame[i] >= 9)
				{
					JellyfishCageFrame[i] = 7;
				}
			}
			if (JellyfishCageMode[i] == 3 && JellyfishCageFrame[i] <= 10 && JellyfishCageFrameCounter[i] >= changeFrameRand)
			{
				JellyfishCageFrameCounter[i] = 0;
				JellyfishCageFrame[i]++;
				if (JellyfishCageFrame[i] >= 10)
				{
					JellyfishCageFrame[i] = 3;
					JellyfishCageMode[i] = 0;
				}
			}
		}
	}
}
