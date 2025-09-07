using Avalon.Common;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class GemLocks : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.CoordinateHeights = new int[]
		{
			16,
			16,
			16
		};
		TileObjectData.newTile.AnchorWall = true;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.addTile(Type);
		TileID.Sets.DisableSmartCursor[Type] = true;
		AddMapEntry(new Color(11, 212, 13), this.GetLocalization("MapEntry0"));
		AddMapEntry(new Color(22, 212, 198), this.GetLocalization("MapEntry1"));
		AddMapEntry(new Color(174, 89, 46), this.GetLocalization("MapEntry2"));
	}

	public override ushort GetMapOption(int i, int j)
	{
		switch (Main.tile[i, j].TileFrameX / 54)
		{
			case 0:
				return 0;
			case 1:
				return 1;
			case 2:
				return 2;
		}
		return 0;
	}

	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		int item = 0;
		int num1 = (Main.tile[i, j].TileFrameX / 54);
		int num2 = (Main.tile[i, j].TileFrameY / 54);
		switch (num1)
		{
			case 0:
				item = ModContent.ItemType<Items.Placeable.Wiring.PeridotGemLock>();
				break;
			case 1:
				item = ModContent.ItemType<Items.Placeable.Wiring.TourmalineGemLock>();
				break;
			case 2:
				item = ModContent.ItemType<Items.Placeable.Wiring.ZirconGemLock>();
				break;
		}
		if (num2 > 0)
		{
			switch (num1)
			{
				case 0:
					item = ModContent.ItemType<Items.Placeable.Wiring.PeridotGemLock>();
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Other.LargePeridot>());
					break;
				case 1:
					item = ModContent.ItemType<Items.Placeable.Wiring.TourmalineGemLock>();
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Other.LargeTourmaline>());
					break;
				case 2:
					item = ModContent.ItemType<Items.Placeable.Wiring.ZirconGemLock>();
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Other.LargeZircon>());
					break;
			}
		}
		yield return new Item(item);
	}

	public override void MouseOver(int i, int j)
	{
		Player player = Main.LocalPlayer;
		player.noThrow = 2;
		player.cursorItemIconEnabled = false;
		int num28 = Main.tile[i, j].TileFrameX / 54;
		int num29 = Main.tile[i, j].TileFrameY / 54;
		int num30 = 0;
		switch (num28)
		{
			case 0:
				num30 = ModContent.ItemType<Items.Other.LargePeridot>();
				break;
			case 1:
				num30 = ModContent.ItemType<Items.Other.LargeTourmaline>();
				break;
			case 2:
				num30 = ModContent.ItemType<Items.Other.LargeZircon>();
				break;
		}
		if (num30 != 0)
		{
			if (num29 == 0 && player.HasItem(num30) && player.selectedItem != 58)
			{
				player.cursorItemIconEnabled = true;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					player.cursorItemIconID = num30;
				}
				else
				{
					player.cursorItemIconID = num30;
				}
			}
			else if (num29 == 1)
			{
				player.cursorItemIconEnabled = true;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					player.cursorItemIconID = num30;
				}
				else
				{
					player.cursorItemIconID = num30;
				}
			}
		}
	}
	public override bool RightClick(int i, int j)
	{
		Player player = Main.LocalPlayer;
		int num28 = Main.tile[i, j].TileFrameX / 54;
		int num29 = Main.tile[i, j].TileFrameY / 54;
		_ = Main.tile[i, j].TileFrameX % 54 / 18;
		_ = Main.tile[i, j].TileFrameY % 54 / 18;
		int num30 = -1;
		switch (num28)
		{
			case 0:
				num30 = ModContent.ItemType<Items.Other.LargePeridot>();
				break;
			case 1:
				num30 = ModContent.ItemType<Items.Other.LargeTourmaline>();
				break;
			case 2:
				num30 = ModContent.ItemType<Items.Other.LargeZircon>();
				break;
		}
		if (num30 != -1)
		{
			if (num29 == 0 && player.HasItem(num30) && player.selectedItem != 58)
			{
				player.GamepadEnableGrappleCooldown();
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					player.ConsumeItem(num30);
					WorldGen.ToggleGemLock(i, j, on: true);
				}
				else
				{
					player.ConsumeItem(num30);
					NetMessage.SendData(MessageID.GemLockToggle, -1, -1, null, i, j, 1f);
				}
			}
			else if (num29 == 1)
			{
				player.GamepadEnableGrappleCooldown();
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					WorldGen.ToggleGemLock(i, j, on: false);
				}
				else
				{
					NetMessage.SendData(MessageID.GemLockToggle, -1, -1, null, i, j);
				}
			}
		}

		return true;
	}
	public static void HitSwitch(int i, int j)
	{
		SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16 + 16, j * 16 + 16));
		Wiring.TripWire(i, j, 3, 3);
	}
	public override bool CreateDust(int i, int j, ref int type)
	{
		if (Main.tile[i, j].TileFrameY > 54)
		{
			switch (Main.tile[i, j].TileFrameX / 54)
			{
				case 0:
					var dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.PeridotDust>(), 0, 0, 100, default, 0.6f);
					Main.dust[dust].noLightEmittence = true;
					Main.dust[dust].noLight = true;
					return false;
				case 1:
					dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.TourmalineDust>(), 0, 0, 100, default, 0.6f);
					Main.dust[dust].noLightEmittence = true;
					Main.dust[dust].noLight = true;
					return false;
				case 2:
					dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.ZirconDust>(), 0, 0, 100, default, 0.6f);
					Main.dust[dust].noLightEmittence = true;
					Main.dust[dust].noLight = true;
					return false;
			}
		}
		return false;
	}
}
public class GemLocksToggle : ModHook
{
	protected override void Apply()
	{
		On_WorldGen.ToggleGemLock += On_WorldGen_ToggleGemLock;
	}

	private void On_WorldGen_ToggleGemLock(On_WorldGen.orig_ToggleGemLock orig, int i, int j, bool on)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j);
		if (!tileSafely.HasTile || tileSafely.TileType != ModContent.TileType<GemLocks>() || (tileSafely.TileFrameY < 54 && !on))
		{
			orig(i, j, on);
			return;
		}
		bool flag = false;
		int num = -1;
		if (tileSafely.TileFrameY >= 54)
		{
			flag = true;
		}
		int num2 = Main.tile[i, j].TileFrameX / 54;
		int num3 = Main.tile[i, j].TileFrameX % 54 / 18;
		int num4 = Main.tile[i, j].TileFrameY % 54 / 18;
		switch (num2)
		{
			case 0:
				num = ModContent.ItemType<Items.Other.LargePeridot>();
				break;
			case 1:
				num = ModContent.ItemType<Items.Other.LargeTourmaline>();
				break;
			case 2:
				num = ModContent.ItemType<Items.Other.LargeZircon>();
				break;
		}
		for (int k = i - num3; k < i - num3 + 3; k++)
		{
			for (int l = j - num4; l < j - num4 + 3; l++)
			{
				Main.tile[k, l].TileFrameY = (short)((on ? 54 : 0) + (l - j + num4) * 18);
			}
		}
		if (num != -1 && flag)
		{
			int item1 = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, num);
			NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item1, 1f);
		}
		WorldGen.SquareTileFrame(i, j);
		NetMessage.SendTileSquare(-1, i - num3, j - num4, 3, 3);
		GemLocks.HitSwitch(i - num3, j - num4);
		if (Main.netMode == NetmodeID.Server)
		{
			Network.SyncWiring.SendPacket(-1, i - num3, j - num4, Network.SyncWiring.WiringType.GemLocks);
		}
	}
}
