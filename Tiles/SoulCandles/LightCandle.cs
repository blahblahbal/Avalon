using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.SoulCandles;

public class LightCandle : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
		TileObjectData.addTile(Type);
		Main.tileLighted[Type] = true;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(253, 221, 3));
		DustType = ModContent.DustType<Dusts.SoulofLight>();
	}
	public override void MouseOver(int i, int j)
	{
		Player player = Main.player[Main.myPlayer];
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<Items.Placeable.SoulCandles.LightCandle>();
	}

	public override bool RightClick(int i, int j)
	{
		WorldGen.KillTile(i, j);
		if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
		{
			NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
		}
		return true;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameX == 0)
		{
			r = 3f;
			g = 3f;
			b = 3f;
		}
	}

	public override void HitWire(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int topY = j - tile.TileFrameY / 18;
		short frameAdjustment = (short)(tile.TileFrameX > 0 ? -18 : 18);
		Main.tile[i, topY].TileFrameX += frameAdjustment;
		Wiring.SkipWire(i, topY);
		NetMessage.SendTileSquare(-1, i, topY + 1, 1, TileChangeType.None);
	}
	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (Main.rand.NextBool(25))
		{
			int num162 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.SoulofLight>(), 0f, 0f, 0, default, 1f);
			Main.dust[num162].noGravity = true;
		}

		if (!closer && BiomeTileCounts.NearbyEffectsRectangle(i, j, 30, 30))
		{
			ModContent.GetInstance<BiomeTileCounts>().LightCandleNearby = true;
		}
	}
	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
		var color = new Color(224, 104, 147, 0);
		int frameX = Main.tile[i, j].TileFrameX;
		int frameY = Main.tile[i, j].TileFrameY;
		int width = 18;
		int height = 18;
		int offsetX = 1;
		var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
		{
			zero = Vector2.Zero;
		}
		for (int k = 0; k < 7; k++)
		{
			float x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
			float y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
			Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/SoulCandles/LightCandle_Flame").Value, new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f + x, j * 16 - (int)Main.screenPosition.Y + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}
	}
}
