using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.PurpleDungeon;

public class PurpleDungeonBathtub : BathtubTemplate { }

public class PurpleDungeonBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonBed>();
}

public class PurpleDungeonBookcase : BookcaseTemplate { }

public class PurpleDungeonCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonCandelabra>();
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameX <= 36)
		{
			r = 0.9f;
			g = 0.45f;
			b = 0.6f;
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
		Color color = new Color(198, 171, 108, 0);
		int frameX = Main.tile[i, j].TileFrameX;
		int frameY = Main.tile[i, j].TileFrameY;
		int width = 18;
		int offsetY = 2;
		int height = 18;
		int offsetX = 1;
		Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
		{
			zero = Vector2.Zero;
		}
		for (int k = 0; k < 7; k++)
		{
			float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
			float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
			Main.spriteBatch.Draw(flameTexture.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}
	}
}

public class PurpleDungeonCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonCandle>();
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameX == 0)
		{
			r = 0.9f;
			g = 0.45f;
			b = 0.6f;
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
		Color color = new Color(198, 171, 108, 0);
		int frameX = Main.tile[i, j].TileFrameX;
		int frameY = Main.tile[i, j].TileFrameY;
		int width = 18;
		int offsetY = -4;
		int height = 20;
		int offsetX = 1;
		Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
		{
			zero = Vector2.Zero;
		}
		for (int k = 0; k < 7; k++)
		{
			float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
			float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
			Main.spriteBatch.Draw(flameTexture.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}
	}
}

public class PurpleDungeonChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonChair>();
}

public class PurpleDungeonChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonChandelier>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.9f, 0.45f, 0.6f);
}

public class PurpleDungeonChest : ChestTemplate
{
	protected override bool CanBeLocked => true;
	protected override int ChestKeyItemId => ItemID.GoldenKey;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry1"), MapChestName);
	}
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonChest>();
}

public class PurpleDungeonClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonClock>();
}

public class PurpleDungeonDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonDoor>();
}

public class PurpleDungeonDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonDoor>();
}

public class PurpleDungeonDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonDresser>();
}

public class PurpleDungeonLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonLamp>();
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameX == 0)
		{
			r = 0.9f;
			g = 0.45f;
			b = 0.6f;
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
		Color color = new Color(198, 171, 108, 0);
		int frameX = Main.tile[i, j].TileFrameX;
		int frameY = Main.tile[i, j].TileFrameY;
		int width = 18;
		int offsetY = 0;
		int height = 18;
		int offsetX = 1;
		Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
		{
			zero = Vector2.Zero;
		}
		for (int k = 0; k < 7; k++)
		{
			float x = (float)Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
			float y = (float)Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
			Main.spriteBatch.Draw(flameTexture.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}
	}
}

public class PurpleDungeonPiano : PianoTemplate { }

public class PurpleBrickPlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<Dusts.PurpleDungeonDust>();
}

public class PurpleDungeonSink : SinkTemplate { }

public class PurpleDungeonSofa : SofaTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonSofa>();
}

public class PurpleDungeonTable : TableTemplate { }

public class PurpleDungeonToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PurpleDungeon.PurpleDungeonToilet>();
}

public class PurpleDungeonWorkbench : WorkbenchTemplate { }
