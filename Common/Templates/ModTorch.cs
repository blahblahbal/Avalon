using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace Avalon.Common.Templates;

public abstract class SpecialLight : ModTile
{
	public virtual int dustType => 0;
	public virtual int TorchItem => 0;
	public virtual Vector3 LightColor => Vector3.One;
	public virtual bool WaterDeath => true;
	public virtual bool NoDustGravity => true;
}

public abstract class ModTorch : SpecialLight
{
	public override void SetStaticDefaults()
	{
		RegisterItemDrop(TorchItem);

		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileSolid[Type] = false;
		Main.tileNoAttach[Type] = true;
		Main.tileNoFail[Type] = true;
		Main.tileWaterDeath[Type] = WaterDeath;
		TileID.Sets.Torch[Type] = true;
		TileID.Sets.DisableSmartCursor[Type] = true;
		TileID.Sets.FramesOnKillWall[Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newTile.WaterPlacement = (LiquidPlacement)System.Convert.ToInt32(WaterDeath);
		TileObjectData.newTile.WaterDeath = WaterDeath;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);

		TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newAlternate.WaterPlacement = (LiquidPlacement)System.Convert.ToInt32(WaterDeath);
		TileObjectData.newAlternate.WaterDeath = WaterDeath;
		TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
		TileObjectData.newAlternate.AnchorAlternateTiles = [TileID.WoodenBeam];
		TileObjectData.addAlternate(1);

		TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newAlternate.WaterPlacement = (LiquidPlacement)System.Convert.ToInt32(WaterDeath);
		TileObjectData.newAlternate.WaterDeath = WaterDeath;
		TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
		TileObjectData.newAlternate.AnchorAlternateTiles = [TileID.WoodenBeam];
		TileObjectData.addAlternate(2);

		TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newAlternate.WaterPlacement = (LiquidPlacement)System.Convert.ToInt32(WaterDeath);
		TileObjectData.newAlternate.WaterDeath = WaterDeath;
		TileObjectData.newAlternate.AnchorWall = true;
		TileObjectData.addAlternate(0);

		TileObjectData.addTile(Type);

		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Torch"));
		DustType = dustType;
		AdjTiles = [TileID.Torches];
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.rand.NextBool(40) && Main.tile[i, j].TileFrameX < 66)
		{
			Dust d = Dust.NewDustDirect(new Vector2(i * 16, j * 16) + new Vector2(6, -6), 0, 0, dustType, 0, 0, 100, default, Main.rand.NextFloat(0.5f, 1));
			if (!Main.rand.NextBool(3))
			{
				d.noGravity = NoDustGravity;
			}
			d.noLightEmittence = true;
			d.velocity *= 0.3f;
			d.velocity.Y -= 1.5f;
			d.noGravity = false;
		}
	}
	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = Main.rand.Next(10, 15);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		var tile = Main.tile[i, j];
		if (tile.TileFrameX < 66)
		{
			r = LightColor.X;
			g = LightColor.Y;
			b = LightColor.Z;
		}
	}
	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 0;
		if (WorldGen.InWorld(i, j - 1) && WorldGen.SolidTile(i, j - 1))
		{
			offsetY = 2;
			if (WorldGen.InWorld(i - 1, j + 1) && WorldGen.SolidTile(i - 1, j + 1) || WorldGen.InWorld(i + 1, j + 1) && WorldGen.SolidTile(i + 1, j + 1))
			{
				offsetY = 4;
			}
		}
	}
	public override void MouseOver(int i, int j)
	{
		Player player = Main.LocalPlayer;
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = TorchItem;
	}
	public override bool RightClick(int i, int j)
	{
		WorldGen.KillTile(i, j, false, false, true);
		if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
		{
			NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
		}
		return true;
	}
}

public abstract class ModCampfire : SpecialLight
{
	public override void SetStaticDefaults()
	{
		RegisterItemDrop(TorchItem);

		// Properties
		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileWaterDeath[Type] = WaterDeath;
		Main.tileLavaDeath[Type] = true;
		TileID.Sets.HasOutlines[Type] = true;
		TileID.Sets.InteractibleByNPCs[Type] = true;
		TileID.Sets.Campfire[Type] = true;

		DustType = -1; // No dust when mined.
		AdjTiles = [TileID.Campfire];

		// Placement
		TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Campfire, 0));
		//  This is what is copied from the Campfire tile
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.StyleWrapLimit = 16;
		TileObjectData.newTile.WaterPlacement = (LiquidPlacement)System.Convert.ToInt32(WaterDeath);
		TileObjectData.newTile.WaterDeath = WaterDeath;
		TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.DrawYOffset = 2;

		TileObjectData.newTile.StyleLineSkip = 9; // This needs to be added to work for modded tiles.
		TileObjectData.addTile(Type);

		// Etc
		AddMapEntry(new Color(254, 121, 2), Language.GetText("ItemName.Campfire"));
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer) return;

		Tile tile = Main.tile[i, j];
		if (tile.TileFrameY < 36)
		{
			Main.SceneMetrics.HasCampfire = true;
		}
	}

	public override void MouseOver(int i, int j)
	{
		Player player = Main.LocalPlayer;
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;

		int style = TileObjectData.GetTileStyle(Main.tile[i, j]);
		player.cursorItemIconID = TileLoader.GetItemDropFromTypeAndStyle(Type, style);
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16, j * 16));
		ToggleTile(i, j);
		return true;
	}

	public override void HitWire(int i, int j)
	{
		ToggleTile(i, j);
	}

	// ToggleTile is a method that contains code shared by HitWire and RightClick, since they both toggle the state of the tile.
	// Note that TileFrameY doesn't necessarily match up with the image that is drawn, AnimateTile and AnimateIndividualTile contribute to the drawing decisions.
	public static void ToggleTile(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int topX = i - tile.TileFrameX % 54 / 18;
		int topY = j - tile.TileFrameY % 36 / 18;

		short frameAdjustment = (short)(tile.TileFrameY >= 36 ? -36 : 36);

		for (int x = topX; x < topX + 3; x++)
		{
			for (int y = topY; y < topY + 2; y++)
			{
				Main.tile[x, y].TileFrameY += frameAdjustment;

				if (Wiring.running)
				{
					Wiring.SkipWire(x, y);
				}
			}
		}

		if (Main.netMode != NetmodeID.SinglePlayer)
		{
			NetMessage.SendTileSquare(-1, topX, topY, 3, 2);
		}
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (++frameCounter >= 4)
		{
			frameCounter = 0;
			frame = ++frame % 8;
		}
	}
	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		var tile = Main.tile[i, j];
		if (tile.TileFrameY < 36)
		{
			frameYOffset = Main.tileFrame[type] * 36;
		}
		else
		{
			frameYOffset = 252;
		}
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.gamePaused || !Main.instance.IsActive)
		{
			return;
		}
		if (!Lighting.UpdateEveryFrame || new FastRandom(Main.TileFrameSeed).WithModifier(i, j).Next(4) == 0)
		{
			Tile tile = Main.tile[i, j];
			// Only emit dust from the top tiles, and only if toggled on. This logic limits dust spawning under different conditions.
			if (tile.TileFrameY == 0 && Main.rand.NextBool(3) && ((Main.drawToScreen && Main.rand.NextBool(4)) || !Main.drawToScreen))
			{
				Dust dust = Dust.NewDustDirect(new Vector2(i * 16 + 2, j * 16 - 4), 4, 8, DustID.Smoke, 0f, 0f, 100);
				if (tile.TileFrameX == 0)
					dust.position.X += Main.rand.Next(8);

				if (tile.TileFrameX == 36)
					dust.position.X -= Main.rand.Next(8);

				dust.alpha += Main.rand.Next(100);
				dust.velocity *= 0.2f;
				dust.velocity.Y -= 0.5f + Main.rand.Next(10) * 0.1f;
				dust.fadeIn = 0.5f + Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameY < 36)
		{
			float pulse = Main.rand.Next(28, 42) * 0.005f;
			pulse += (270 - Main.mouseTextColor) / 700f;
			r = LightColor.X + pulse;
			g = LightColor.Y + pulse;
			b = LightColor.Z + pulse;
		}
	}
}
