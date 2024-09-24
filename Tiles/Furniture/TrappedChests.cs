using Terraria.GameContent.ObjectInteractions;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.Audio;

namespace Avalon.Tiles.Furniture;

internal class TrappedChests : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSpelunker[Type] = true;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 1200;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileOreFinderPriority[Type] = 500;
		TileID.Sets.HasOutlines[Type] = true;
		TileID.Sets.IsAContainer[Type] = true;
		TileID.Sets.AvoidedByNPCs[Type] = true;
		TileID.Sets.InteractibleByNPCs[Type] = true;
		TileID.Sets.BasicChestFake[Type] = true;
		TileID.Sets.DisableSmartCursor[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile(Type);
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("Contagion"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("Underworld"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("BleachedEbony"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("Coughwood"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("Heartstone"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("OrangeDungeon"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("PurpleDungeon"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("ResistantWood"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("Tuhrtl"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("YellowDungeon"));
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("Platinum"));
		DustType = -1;
		AdjTiles = [441];
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}
	public override ushort GetMapOption(int i, int j)
	{
		return (ushort)(Main.tile[i, j].TileFrameX / 36);
	}
	public override void MouseOver(int i, int j)
	{
		Player player = Main.LocalPlayer;
		int style = Main.tile[i, j].TileFrameX / 36;
		int item = TileLoader.GetItemDropFromTypeAndStyle(base.Type, style);
		if (item > 0)
		{
			player.cursorItemIconID = item;
		}
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
	}
	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		Tile tile = Main.tile[i, j];
		int left = i;
		int top = j;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		if (Animation.GetTemporaryFrame(left, top, out var newFrameYOffset))
		{
			frameYOffset = 38 * newFrameYOffset;
		}
	}
	public override bool RightClick(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		Main.mouseRightRelease = false;
		int left = i;
		int top = j;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		Animation.NewTemporaryAnimation(2, tile.TileType, left, top);
		NetMessage.SendTemporaryAnimation(-1, 2, tile.TileType, left, top);
		Trigger(i, j);
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			Network.SyncWiring.SendPacket(Player.FindClosest(new Vector2(i * 16, j * 16), 40, 40), i, j, 2);
		}
		return true;
	}
	public static void Trigger(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int left = i;
		int top = j;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16, j * 16));
		Wiring.TripWire(left, top, 2, 2);
	}
}
