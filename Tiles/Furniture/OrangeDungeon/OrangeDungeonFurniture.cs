using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.OrangeDungeon;

public class OrangeDungeonBathtub : BathtubTemplate { }

public class OrangeDungeonBed : BedTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonBed>();
}

public class OrangeDungeonBookcase : BookcaseTemplate { }

public class OrangeDungeonCandelabra : CandelabraTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonCandelabra>();
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
			r = 0.69f;
			g = 0.32f;
			b = 0.77f;
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

public class OrangeDungeonCandle : CandleTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonCandle>();
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
			r = 0.69f;
			g = 0.32f;
			b = 0.77f;
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

public class OrangeDungeonChair : ChairTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonChair>();
}

public class OrangeDungeonChandelier : ChandelierTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonChandelier>();
	public override Color FlameColor => new(198, 171, 108, 0);
	public override Vector3 LightColor => new(0.69f, 0.32f, 0.77f);
}

public class OrangeDungeonChest : ChestTemplate
{
	protected override bool CanBeLocked => true;
	protected override int ChestKeyItemId => ItemID.GoldenKey;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry1"), MapChestName);
	}
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonChest>();
}

public class OrangeDungeonClock : ClockTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonClock>();
}

public class OrangeDungeonDoorClosed : ClosedDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonDoor>();
}

public class OrangeDungeonDoorOpen : OpenDoorTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonDoor>();
}

public class OrangeDungeonDresser : DresserTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonDresser>();
}

public class OrangeDungeonLamp : LampTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonLamp>();
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
			r = 0.69f;
			g = 0.32f;
			b = 0.77f;
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

public class OrangeDungeonPiano : PianoTemplate { }

public class OrangeBrickPlatform : PlatformTemplate
{
	public override int Dust => ModContent.DustType<Dusts.OrangeDungeonDust>();
}

public class OrangeDungeonSink : SinkTemplate { }

public class OrangeDungeonSofa : SofaTemplate
{
	public override float SittingHeight => 0;
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonSofa>();
}

public class OrangeDungeonTable : TableTemplate { }

public class OrangeDungeonToilet : ToiletTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeon.OrangeDungeonToilet>();
}

public class OrangeDungeonWorkbench : WorkbenchTemplate { }
