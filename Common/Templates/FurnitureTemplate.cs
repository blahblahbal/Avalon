using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Common.Templates
{
	public abstract class FurnitureTemplate : ModTile
	{
		public virtual int Dust => -1;
		public virtual int DropItem => 0;
		public virtual bool LavaDeath => true;
		public virtual bool WaterDeath => true;
	}
	public abstract class BathtubTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2); //this style already takes care of direction for us
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			AddMapEntry(new Color(144, 148, 144), Language.GetText("ItemName.Bathtub"));
			DustType = Dust;
		}
	}
	public abstract class BedTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.CanBeSleptIn[Type] = true;
			TileID.Sets.IsValidSpawnPoint[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Bed"));
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[] { TileID.Beds };
			DustType = Dust;
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int spawnX = i - (tile.TileFrameX / 18) + (tile.TileFrameX >= 72 ? 5 : 2);
			int spawnY = j + 2;

			if (tile.TileFrameY % 38 != 0)
			{
				spawnY--;
			}

			if (!Player.IsHoveringOverABottomSideOfABed(i, j))
			{
				if (player.IsWithinSnappngRangeToTile(i, j, PlayerSleepingHelper.BedSleepingMaxDistance))
				{
					player.GamepadEnableGrappleCooldown();
					player.sleeping.StartSleeping(player, i, j);
				}
			}
			else
			{
				player.FindSpawn();

				if (player.SpawnX == spawnX && player.SpawnY == spawnY)
				{
					player.RemoveSpawn();
					Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, 240, 20);
				}
				else if (Player.CheckSpawn(spawnX, spawnY))
				{
					player.ChangeSpawn(spawnX, spawnY);
					Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, 240, 20);
				}
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (!Player.IsHoveringOverABottomSideOfABed(i, j))
			{
				if (player.IsWithinSnappngRangeToTile(i, j, PlayerSleepingHelper.BedSleepingMaxDistance))
				{ // Match condition in RightClick. Interaction should only show if clicking it does something
					player.noThrow = 2;
					player.cursorItemIconEnabled = true;
					player.cursorItemIconID = ItemID.SleepingIcon;
				}
			}
			else
			{
				player.noThrow = 2;
				player.cursorItemIconEnabled = true;
				player.cursorItemIconID = DropItem;
			}
		}
	}
	public abstract class DresserTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.BasicDresser[Type] = true;
			TileID.Sets.AvoidedByNPCs[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true;
			TileID.Sets.IsAContainer[Type] = true;
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

			AdjTiles = new int[] { TileID.Dressers };
			DustType = Dust;

			// Names
			AddMapEntry(new Color(191, 142, 111), CreateMapEntryName(), MapChestName);

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[] {
			TileID.MagicalIceBlock,
			TileID.Boulder,
			TileID.BouncyBoulder,
			TileID.LifeCrystalBoulder,
			TileID.RollingCactus
		};
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
		}

		public override LocalizedText DefaultContainerName(int frameX, int frameY)
		{
			return CreateMapEntryName();
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override void ModifySmartInteractCoords(ref int width, ref int height, ref int frameWidth, ref int frameHeight, ref int extraY)
		{
			width = 3;
			height = 1;
			extraY = 0;
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			int left = Main.tile[i, j].TileFrameX / 18;
			left %= 3;
			left = i - left;
			int top = j - Main.tile[i, j].TileFrameY / 18;
			if (Main.tile[i, j].TileFrameY == 0)
			{
				Main.CancelClothesWindow(true);
				Main.mouseRightRelease = false;
				player.CloseSign();
				player.SetTalkNPC(-1);
				Main.npcChatCornerItem = 0;
				Main.npcChatText = "";
				if (Main.editChest)
				{
					SoundEngine.PlaySound(SoundID.MenuTick);
					Main.editChest = false;
					Main.npcChatText = string.Empty;
				}
				if (player.editedChestName)
				{
					NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
					player.editedChestName = false;
				}
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					if (left == player.chestX && top == player.chestY && player.chest != -1)
					{
						player.chest = -1;
						Recipe.FindRecipes();
						SoundEngine.PlaySound(SoundID.MenuClose);
					}
					else
					{
						NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, top);
						Main.stackSplit = 600;
					}
				}
				else
				{
					player.piggyBankProjTracker.Clear();
					player.voidLensChest.Clear();
					int chestIndex = Chest.FindChest(left, top);
					if (chestIndex != -1)
					{
						Main.stackSplit = 600;
						if (chestIndex == player.chest)
						{
							player.chest = -1;
							Recipe.FindRecipes();
							SoundEngine.PlaySound(SoundID.MenuClose);
						}
						else if (chestIndex != player.chest && player.chest == -1)
						{
							player.OpenChest(left, top, chestIndex);
							SoundEngine.PlaySound(SoundID.MenuOpen);
						}
						else
						{
							player.OpenChest(left, top, chestIndex);
							SoundEngine.PlaySound(SoundID.MenuTick);
						}
						Recipe.FindRecipes();
					}
				}
			}
			else
			{
				Main.playerInventory = false;
				player.chest = -1;
				Recipe.FindRecipes();
				player.SetTalkNPC(-1);
				Main.npcChatCornerItem = 0;
				Main.npcChatText = "";
				Main.interactedDresserTopLeftX = left;
				Main.interactedDresserTopLeftY = top;
				Main.OpenClothesWindow();
			}
			return true;
		}

		// This is not a hook, this is just a normal method used by the MouseOver and MouseOverFar hooks to avoid repeating code.
		public void MouseOverNearAndFarSharedLogic(Player player, int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			left -= tile.TileFrameX % 54 / 18;
			if (tile.TileFrameY % 36 != 0)
			{
				top--;
			}
			int chestIndex = Chest.FindChest(left, top);
			player.cursorItemIconID = -1;
			if (chestIndex < 0)
			{
				player.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
			}
			else
			{
				string defaultName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY); // This gets the ContainerName text for the currently selected language

				if (Main.chest[chestIndex].name != "")
				{
					player.cursorItemIconText = Main.chest[chestIndex].name;
				}
				else
				{
					player.cursorItemIconText = defaultName;
				}
				if (player.cursorItemIconText == defaultName)
				{
					player.cursorItemIconID = DropItem;
					player.cursorItemIconText = "";
				}
			}
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			Player player = Main.LocalPlayer;
			MouseOverNearAndFarSharedLogic(player, i, j);
			if (player.cursorItemIconText == "")
			{
				player.cursorItemIconEnabled = false;
				player.cursorItemIconID = ItemID.None;
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			MouseOverNearAndFarSharedLogic(player, i, j);
			if (Main.tile[i, j].TileFrameY > 0)
			{
				player.cursorItemIconID = ItemID.FamiliarShirt;
				player.cursorItemIconText = "";
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Chest.DestroyChest(i, j);
		}

		public static string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}

			if (tile.TileFrameY != 0)
			{
				top--;
			}

			int chest = Chest.FindChest(left, top);
			if (chest < 0)
			{
				return Language.GetTextValue("LegacyDresserType.0");
			}

			if (Main.chest[chest].name == "")
			{
				return name;
			}

			return name + ": " + Main.chest[chest].name;
		}
	}
	public abstract class OpenDoorTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolid[Type] = false;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileNoSunLight[Type] = true;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 1);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 2);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			TileID.Sets.HousingWalls[Type] = true; //needed for non-solid blocks to count as walls
			TileID.Sets.HasOutlines[Type] = true;
			AddMapEntry(new Color(119, 105, 79));
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[] { TileID.OpenDoor };
			TileID.Sets.CloseDoorID[Type] = Type - 1;
			RegisterItemDrop(DropItem);
			DustType = Dust;
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}
		public override bool Slope(int i, int j)
		{
			return false;
		}
		public override void MouseOver(int i, int j)
		{
			var player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = DropItem;
		}
	}
	public abstract class ClosedDoorTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.NotReallySolid[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			AddMapEntry(new Color(119, 105, 79));
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[] { TileID.ClosedDoor };
			TileID.Sets.OpenDoorID[Type] = Type + 1;
			RegisterItemDrop(DropItem);
			DustType = Dust;
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}
		public override bool Slope(int i, int j)
		{
			return false;
		}
		public override void MouseOver(int i, int j)
		{
			var player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = DropItem;
		}
	}
	public abstract class ChestTemplate : FurnitureTemplate
	{
		public virtual bool Shiny => false;
		public virtual int ShinyFrequency => 1200; //Doubt it'll ever need to be changed, just in case tbh
		protected virtual bool CanBeLocked => false;
		protected virtual int ChestKeyItemId => ItemID.None;
		public virtual bool CanBeUnlockedNormally => true;
		public override void SetStaticDefaults()
		{
			if (Shiny)
			{
				Main.tileShine2[Type] = true;
				Main.tileShine[Type] = ShinyFrequency;
			}
			// Properties
			Main.tileSpelunker[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileOreFinderPriority[Type] = 500;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.BasicChest[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.AvoidedByNPCs[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true;
			TileID.Sets.IsAContainer[Type] = true;
			TileID.Sets.FriendlyFairyCanLureTo[Type] = true;

			DustType = Dust;
			AdjTiles = [TileID.Containers];

			// Other tiles with just one map entry use CreateMapEntryName() to use the default translationkey, "MapEntry"
			// Since ExampleChest needs multiple, we register our own MapEntry keys
			if (CanBeUnlockedNormally)
			{
				AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
				if (CanBeLocked)
				{
					AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry1"), MapChestName);
				}
			}

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = [16, 18];
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = [
				TileID.MagicalIceBlock,
				TileID.Boulder,
				TileID.BouncyBoulder,
				TileID.LifeCrystalBoulder,
				TileID.RollingCactus
			];
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
		}

		public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			int type = 0;
			switch (TileObjectData.GetTileStyle(Main.tile[i, j]))
			{
				case 0: // Unlocked
				case 1: // Locked
					type = DropItem;
					break;
			}
			if (type > 0)
			{
				yield return new Item(type);
			}
		}

		public override ushort GetMapOption(int i, int j)
		{
			return (ushort)(Main.tile[i, j].TileFrameX / 36);
		}

		public override LocalizedText DefaultContainerName(int frameX, int frameY)
		{
			int option = frameX / 36;
			return this.GetLocalization("MapEntry" + option);
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override bool IsLockedChest(int i, int j)
		{
			return ((CanBeLocked && Main.tile[i, j].TileFrameX / 36 == 1) || !CanBeUnlockedNormally);
		}

		public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
		{
			if (!CanBeUnlockedNormally)
			{
				frameXAdjustment = 0;
			}
			return IsLockedChest(i, j);
		}

		public override bool LockChest(int i, int j, ref short frameXAdjustment, ref bool manual)
		{
			int style = TileObjectData.GetTileStyle(Main.tile[i, j]);
			// We need to return true only if the tile style is the unlocked variant of a chest that supports locking. 
			if (style == 0 && CanBeLocked && CanBeUnlockedNormally)
			{
				// We can check other conditions as well, such as how biome chests can't be locked until Plantera is defeated
				return true;
			}
			return false;
		}

		public static string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}

			if (tile.TileFrameY != 0)
			{
				top--;
			}

			int chest = Chest.FindChest(left, top);
			if (chest < 0)
			{
				return Language.GetTextValue("LegacyChestType.0");
			}

			if (Main.chest[chest].name == "")
			{
				return name;
			}

			return name + ": " + Main.chest[chest].name;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			// We override KillMultiTile to handle additional logic other than the item drop. In this case, unregistering the Chest from the world
			Chest.DestroyChest(i, j);
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
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

			player.CloseSign();
			player.SetTalkNPC(-1);
			Main.npcChatCornerItem = 0;
			Main.npcChatText = "";
			if (Main.editChest)
			{
				SoundEngine.PlaySound(SoundID.MenuTick);
				Main.editChest = false;
				Main.npcChatText = string.Empty;
			}

			if (player.editedChestName)
			{
				NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
				player.editedChestName = false;
			}

			bool isLocked = Chest.IsLocked(left, top);


			if (isLocked)
			{
				if (player.HasItemInInventoryOrOpenVoidBag(ChestKeyItemId) && Chest.Unlock(left, top) && player.ConsumeItem(ChestKeyItemId))
				{
					if (Main.netMode == NetmodeID.MultiplayerClient)
					{
						NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 1f, left, top);
					}
				}
			}
			else
			{
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					if (left == player.chestX && top == player.chestY && player.chest != -1)
					{
						player.chest = -1;
						Recipe.FindRecipes();
						SoundEngine.PlaySound(SoundID.MenuClose);
					}
					else
					{
						NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, top);
						Main.stackSplit = 600;
					}
				}
				else
				{
					int chest = Chest.FindChest(left, top);
					if (chest != -1)
					{
						Main.stackSplit = 600;
						if (chest == player.chest)
						{
							player.chest = -1;
							SoundEngine.PlaySound(SoundID.MenuClose);
						}
						else
						{
							SoundEngine.PlaySound(player.chest < 0 ? SoundID.MenuOpen : SoundID.MenuTick);
							player.OpenChest(left, top, chest);
						}

						Recipe.FindRecipes();
					}
				}
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
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

			int chest = Chest.FindChest(left, top);
			player.cursorItemIconID = -1;
			if (chest < 0)
			{
				player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
			}
			else
			{
				string defaultName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY); // This gets the ContainerName text for the currently selected language
				player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : defaultName;
				if (player.cursorItemIconText == defaultName)
				{
					player.cursorItemIconID = DropItem;
					if (Main.tile[left, top].TileFrameX / 36 == 1 || !CanBeUnlockedNormally)
					{
						player.cursorItemIconID = ChestKeyItemId;
					}
					player.cursorItemIconText = "";
				}
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			MouseOver(i, j);
			Player player = Main.LocalPlayer;
			if (player.cursorItemIconText == "")
			{
				player.cursorItemIconEnabled = false;
				player.cursorItemIconID = ItemID.None;
			}
		}
	}
	public abstract class ChandelierTemplate : FurnitureTemplate
	{
		public virtual Vector3 LightColor => new(1f, 0.95f, 0.65f);

		// Flame texture fields
		public virtual bool HasFlameTexture => true;
		private Asset<Texture2D>? FlameTexture;
		public virtual Color FlameColor => new(100, 100, 100, 0);
		public virtual float FlameJitterMultX => 0.15f;
		public virtual float FlameJitterMultY => 0.35f;

		// Flame dust fields
		public virtual int FlameDust => -1;
		public enum FlameDustPlacements : ushort
		{
			None = 0,
			TopLeft = 1,
			TopMiddle = 2,
			TopRight = 4,
			MiddleLeft = 8,
			Middle = 16,
			MiddleRight = 32,
			BottomLeft = 64,
			BottomMiddle = 128,
			BottomRight = 256,
			All = 511
		}
		public virtual FlameDustPlacements FlameDustPositions => FlameDustPlacements.MiddleLeft | FlameDustPlacements.MiddleRight;

		// Glow texture fields
		public virtual bool HasGlowTexture => false;
		private Asset<Texture2D>? GlowTexture;
		public virtual Color GlowColor => Color.White;

		public override void SetStaticDefaults()
		{
			FlameTexture = HasFlameTexture ? ModContent.Request<Texture2D>(Texture + "_Flame") : null;
			GlowTexture = HasGlowTexture ? ModContent.Request<Texture2D>(Texture + "_Glow") : null;

			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileLighted[Type] = true;

			TileID.Sets.MultiTileSway[Type] = true;
			TileID.Sets.IsAMechanism[Type] = true;

			DustType = Dust;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.newTile.StyleLineSkip = 2;
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			AddMapEntry(new Color(235, 166, 135), Language.GetText("MapObject.Chandelier"));
			RegisterItemDrop(DropItem);
		}
		public override void HitWire(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int topX = i - tile.TileFrameX / 18 % 3;
			int topY = j - tile.TileFrameY / 18 % 3;

			short frameAdjustment = (short)(tile.TileFrameX >= 54 ? -54 : 54);

			for (int x = topX; x < topX + 3; x++)
			{
				for (int y = topY; y < topY + 3; y++)
				{
					Main.tile[x, y].TileFrameX += frameAdjustment;
					Wiring.SkipWire(x, y);
				}
			}

			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendTileSquare(-1, topX, topY, 3, 3);
			}
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].TileFrameX / 54 != 0)
			{
				return;
			}
			r = LightColor.X;
			g = LightColor.Y;
			b = LightColor.Z;
		}
		public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
		{
			if (FlameDust != -1)
			{
				if (Main.rand.NextBool(40) && tileFrameX < 54)
				{
					int tileColumn = tileFrameX / 18 % 3;
					int tileRow = tileFrameY / 18 % 3;
					// Check if the current segment has been set to spawn dust in the chandelier's file
					for (int k = 0; k < 9; k++)
					{
						int X = k % 3;
						int Y = k / 3;
						if (X == tileColumn && Y == tileRow)
						{
							if ((Enum.GetValues<FlameDustPlacements>()[k + 1] & FlameDustPositions) != 0)
							{
								Dust dust = Terraria.Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 2), 14, 6, FlameDust, 0f, 0f, 100);
								if (Main.rand.NextBool(3))
								{
									dust.noGravity = true;
								}

								dust.velocity *= 0.3f;
								dust.velocity.Y -= 1.5f;
								break;
							}
						}
					}
				}
			}
		}
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			offsetY = 0;
		}
		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (TileObjectData.IsTopLeft(Main.tile[i, j]))
			{
				Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
			}
			return false;
		}
		public override void AdjustMultiTileVineParameters(int i, int j, ref float? overrideWindCycle, ref float windPushPowerX, ref float windPushPowerY, ref bool dontRotateTopTiles, ref float totalWindMultiplier, ref Texture2D glowTexture, ref Color glowColor)
		{
			// Vanilla chandeliers all share these parameters.
			overrideWindCycle = 1f;
			windPushPowerY = 0;
			if (GlowTexture != null)
			{
				glowTexture = GlowTexture.Value;
				glowColor = GlowColor;
			}
		}
		public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
		{
			if (FlameTexture == null)
			{
				return;
			}

			ulong flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

			tileFlameData.flameTexture = FlameTexture.Value;
			tileFlameData.flameSeed = flameSeed;
			tileFlameData.flameCount = 7;
			tileFlameData.flameColor = FlameColor;
			tileFlameData.flameRangeXMin = -10;
			tileFlameData.flameRangeXMax = 11;
			tileFlameData.flameRangeYMin = -10;
			tileFlameData.flameRangeYMax = 1;
			tileFlameData.flameRangeMultX = FlameJitterMultX;
			tileFlameData.flameRangeMultY = FlameJitterMultY;
		}
	}
	public abstract class ChairTemplate : FurnitureTemplate
	{
		public const int NextStyleHeight = 40; // Calculated by adding all CoordinateHeights + CoordinatePaddingFix.Y applied to all of them + 2
		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.CanBeSatOnForNPCs[Type] = true; // Facilitates calling ModifySittingTargetInfo for NPCs
			TileID.Sets.CanBeSatOnForPlayers[Type] = true; // Facilitates calling ModifySittingTargetInfo for Players
			TileID.Sets.DisableSmartCursor[Type] = true;

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);

			DustType = Dust;
			AdjTiles = new int[] { TileID.Chairs };

			// Names
			AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Chair"));

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			// The following 3 lines are needed if you decide to add more styles and stack them vertically
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;

			TileObjectData.newTile.LavaDeath = LavaDeath;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1); // Facing right will use the second texture style
			TileObjectData.addTile(Type);
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance); // Avoid being able to trigger it from long range
		}

		public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
		{
			// It is very important to know that this is called on both players and NPCs, so do not use Main.LocalPlayer for example, use info.restingEntity
			Tile tile = Framing.GetTileSafely(i, j);

			//info.directionOffset = info.restingEntity is Player ? 6 : 2; // Default to 6 for players, 2 for NPCs
			//info.visualOffset = Vector2.Zero; // Defaults to (0,0)

			info.TargetDirection = -1;
			if (tile.TileFrameX != 0)
			{
				info.TargetDirection = 1; // Facing right if sat down on the right alternate (added through addAlternate in SetStaticDefaults earlier)
			}

			// The anchor represents the bottom-most tile of the chair. This is used to align the entity hitbox
			// Since i and j may be from any coordinate of the chair, we need to adjust the anchor based on that
			info.AnchorTilePosition.X = i; // Our chair is only 1 wide, so nothing special required
			info.AnchorTilePosition.Y = j;

			if (tile.TileFrameY % NextStyleHeight == 0)
			{
				info.AnchorTilePosition.Y++; // Here, since our chair is only 2 tiles high, we can just check if the tile is the top-most one, then move it 1 down
			}
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{ // Avoid being able to trigger it from long range
				player.GamepadEnableGrappleCooldown();
				player.sitting.SitDown(player, i, j);
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{ // Match condition in RightClick. Interaction should only show if clicking it does something
				return;
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = DropItem;

			if (Main.tile[i, j].TileFrameX / 18 < 1)
			{
				player.cursorItemIconReversed = true;
			}
		}
	}
	public abstract class CandleTemplate : FurnitureTemplate
	{
		public virtual Vector3 LightColor => new(1f, 0.95f, 0.65f);

		// Flame texture fields
		public virtual bool HasFlameTexture => true;
		private Asset<Texture2D>? FlameTexture;
		public virtual Color FlameColor => new(100, 100, 100, 0);
		public virtual float FlameJitterMultX => 0.15f;
		public virtual float FlameJitterMultY => 0.35f;

		// Flame dust fields (unused by candles)
		//public virtual int FlameDust => -1;

		// Glow texture fields
		public virtual bool HasGlowTexture => false;
		private Asset<Texture2D>? GlowTexture;
		public virtual Color GlowColor => Color.White;
		public override void SetStaticDefaults()
		{
			FlameTexture = HasFlameTexture ? ModContent.Request<Texture2D>(Texture + "_Flame") : null;
			GlowTexture = HasGlowTexture ? ModContent.Request<Texture2D>(Texture + "_Glow") : null;

			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileLighted[Type] = true;

			DustType = Dust;

			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = [20];
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Candle"));
			RegisterItemDrop(DropItem);
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = DropItem;
		}
		public override bool RightClick(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			short frameAdjustment = (short)(tile.TileFrameX >= 18 ? -18 : 18);
			Main.tile[i, j].TileFrameX += frameAdjustment;
			NetMessage.SendTileSquare(-1, i, j, 1);
			return true;
		}
		public override void HitWire(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			short frameAdjustment = (short)(tile.TileFrameX >= 18 ? -18 : 18);
			Main.tile[i, j].TileFrameX += frameAdjustment;
			NetMessage.SendTileSquare(-1, i, j, 1);
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].TileFrameX / 18 != 0)
			{
				return;
			}
			r = LightColor.X;
			g = LightColor.Y;
			b = LightColor.Z;
		}
		//public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
		//{
		//	if (FlameDust != -1)
		//	{
		//		if (Main.rand.NextBool(40) && tileFrameX < 18)
		//		{
		//			Dust dust = Terraria.Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 2), 14, 6, FlameDust, 0f, 0f, 100);
		//			if (Main.rand.NextBool(3))
		//			{
		//				dust.noGravity = true;
		//			}

		//			dust.velocity *= 0.3f;
		//			dust.velocity.Y -= 1.5f;
		//		}
		//	}
		//}
		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			if (GlowTexture != null)
			{
				drawData.glowTexture = GlowTexture.Value;
				drawData.glowColor = GlowColor;
			}
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (FlameTexture == null)
			{
				return;
			}

			Tile tile = Main.tile[i, j];

			if (!TileDrawing.IsVisible(tile))
			{
				return;
			}

			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

			int width = 18;
			int offsetY = -4;
			int height = 20;
			short frameX = tile.TileFrameX;
			short frameY = tile.TileFrameY;

			TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY);

			ulong randSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

			for (int k = 0; k < 7; k++)
			{
				float shakeX = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
				float shakeY = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;

				spriteBatch.Draw(FlameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + shakeX, j * 16 - (int)Main.screenPosition.Y + offsetY + shakeY) + zero, new Rectangle(frameX, frameY, width, height), FlameColor, 0f, default, 1f, SpriteEffects.None, 0f);
			}
		}
	}
	public abstract class CandelabraTemplate : FurnitureTemplate
	{
		public virtual Vector3 LightColor => new(1f, 0.95f, 0.65f);

		// Flame texture fields
		public virtual bool HasFlameTexture => true;
		private Asset<Texture2D>? FlameTexture;
		public virtual Color FlameColor => new(100, 100, 100, 0);
		public virtual float FlameJitterMultX => 0.15f;
		public virtual float FlameJitterMultY => 0.35f;

		// Flame dust fields
		public virtual int FlameDust => -1;

		// Glow texture fields
		public virtual bool HasGlowTexture => false;
		private Asset<Texture2D>? GlowTexture;
		public virtual Color GlowColor => Color.White;
		public override void SetStaticDefaults()
		{
			FlameTexture = HasFlameTexture ? ModContent.Request<Texture2D>(Texture + "_Flame") : null;
			GlowTexture = HasGlowTexture ? ModContent.Request<Texture2D>(Texture + "_Glow") : null;

			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileLighted[Type] = true;

			DustType = Dust;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			//TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.CoordinateHeights = [16, 18];
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Candelabra"));
			RegisterItemDrop(DropItem);
		}
		public override void HitWire(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int topX = i - tile.TileFrameX / 18 % 2;
			int topY = j - tile.TileFrameY / 18 % 2;

			short frameAdjustment = (short)(tile.TileFrameX >= 36 ? -36 : 36);

			for (int x = topX; x < topX + 2; x++)
			{
				for (int y = topY; y < topY + 2; y++)
				{
					Main.tile[x, y].TileFrameX += frameAdjustment;
					Wiring.SkipWire(x, y);
				}
			}

			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendTileSquare(-1, topX, topY, 2, 2);
			}
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].TileFrameX / 36 != 0)
			{
				return;
			}
			r = LightColor.X;
			g = LightColor.Y;
			b = LightColor.Z;
		}
		public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
		{
			// The following math makes dust only spawn at the tile coordinates of the flames:
			// OO
			// --
			if (FlameDust != -1)
			{
				if (Main.rand.NextBool(40) && tileFrameX < 36 && tileFrameY < 18)
				{
					Dust dust = Terraria.Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 2), 14, 6, FlameDust, 0f, 0f, 100);
					if (Main.rand.NextBool(3))
					{
						dust.noGravity = true;
					}

					dust.velocity *= 0.3f;
					dust.velocity.Y -= 1.5f;
				}
			}
		}
		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			if (GlowTexture != null)
			{
				drawData.glowTexture = GlowTexture.Value;
				drawData.glowColor = GlowColor;
			}
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (FlameTexture == null)
			{
				return;
			}

			Tile tile = Main.tile[i, j];

			if (!TileDrawing.IsVisible(tile))
			{
				return;
			}

			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

			int width = 18;
			int offsetY = 2;
			int height = 18;
			short frameX = tile.TileFrameX;
			short frameY = tile.TileFrameY;

			TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY);

			ulong randSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

			for (int k = 0; k < 7; k++)
			{
				float shakeX = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
				float shakeY = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;

				spriteBatch.Draw(FlameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + shakeX, j * 16 - (int)Main.screenPosition.Y + offsetY + shakeY) + zero, new Rectangle(frameX, frameY, width, height), FlameColor, 0f, default, 1f, SpriteEffects.None, 0f);
			}
		}
	}
	public abstract class LampTemplate : FurnitureTemplate
	{
		public virtual Vector3 LightColor => new(1f, 0.95f, 0.65f);

		// Flame texture fields
		public virtual bool HasFlameTexture => true;
		private Asset<Texture2D>? FlameTexture;
		public virtual Color FlameColor => new(100, 100, 100, 0);
		public virtual float FlameJitterMultX => 0.15f;
		public virtual float FlameJitterMultY => 0.35f;

		// Flame dust fields
		public virtual int FlameDust => -1;

		// Glow texture fields
		public virtual bool HasGlowTexture => false;
		private Asset<Texture2D>? GlowTexture;
		public virtual Color GlowColor => Color.White;
		public override void SetStaticDefaults()
		{
			FlameTexture = HasFlameTexture ? ModContent.Request<Texture2D>(Texture + "_Flame") : null;
			GlowTexture = HasGlowTexture ? ModContent.Request<Texture2D>(Texture + "_Glow") : null;

			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileWaterDeath[Type] = WaterDeath;
			Main.tileLighted[Type] = true;

			DustType = Dust;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.newTile.WaterDeath = WaterDeath;
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			AddMapEntry(new Color(253, 221, 3), Language.GetText("MapObject.FloorLamp"));
			RegisterItemDrop(DropItem);
		}
		public override void HitWire(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int topY = j - tile.TileFrameY / 18 % 3;

			short frameAdjustment = (short)(tile.TileFrameX >= 18 ? -18 : 18);

			for (int y = topY; y < topY + 3; y++)
			{
				Main.tile[i, y].TileFrameX += frameAdjustment;
				Wiring.SkipWire(i, y);
			}

			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendTileSquare(-1, i, topY, 1, 3);
			}
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].TileFrameX / 18 != 0)
			{
				return;
			}
			r = LightColor.X;
			g = LightColor.Y;
			b = LightColor.Z;
		}
		public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
		{
			// The following math makes dust only spawn at the tile coordinates of the flames:
			// O
			// -
			// -
			if (FlameDust != -1)
			{
				if (Main.rand.NextBool(40) && tileFrameX < 18 && tileFrameY < 18)
				{
					Dust dust = Terraria.Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 2), 14, 6, FlameDust, 0f, 0f, 100);
					if (Main.rand.NextBool(3))
					{
						dust.noGravity = true;
					}

					dust.velocity *= 0.3f;
					dust.velocity.Y -= 1.5f;
				}
			}
		}
		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			if (GlowTexture != null)
			{
				drawData.glowTexture = GlowTexture.Value;
				drawData.glowColor = GlowColor;
			}
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (FlameTexture == null)
			{
				return;
			}

			Tile tile = Main.tile[i, j];

			if (!TileDrawing.IsVisible(tile))
			{
				return;
			}

			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

			int width = 18;
			int offsetY = 0;
			int height = 18;
			short frameX = tile.TileFrameX;
			short frameY = tile.TileFrameY;

			TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY);

			ulong randSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

			for (int k = 0; k < 7; k++)
			{
				float shakeX = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
				float shakeY = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;

				spriteBatch.Draw(FlameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + shakeX, j * 16 - (int)Main.screenPosition.Y + offsetY + shakeY) + zero, new Rectangle(frameX, frameY, width, height), FlameColor, 0f, default, 1f, SpriteEffects.None, 0f);
			}
		}
	}
	public abstract class LanternTemplate : FurnitureTemplate
	{
		public virtual Vector3 LightColor => new(1f, 0.95f, 0.65f);

		// Flame texture fields
		public virtual bool HasFlameTexture => true;
		private Asset<Texture2D>? FlameTexture;
		public virtual Color FlameColor => new(100, 100, 100, 0);
		public virtual float FlameJitterMultX => 0.15f;
		public virtual float FlameJitterMultY => 0.35f;

		// Flame dust fields
		public virtual int FlameDust => -1;

		// Glow texture fields
		public virtual bool HasGlowTexture => false;
		private Asset<Texture2D>? GlowTexture;
		public virtual Color GlowColor => Color.White;
		public override void SetStaticDefaults()
		{
			FlameTexture = HasFlameTexture ? ModContent.Request<Texture2D>(Texture + "_Flame") : null;
			GlowTexture = HasGlowTexture ? ModContent.Request<Texture2D>(Texture + "_Glow") : null;

			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileLighted[Type] = true;

			TileID.Sets.MultiTileSway[Type] = true;
			TileID.Sets.IsAMechanism[Type] = true;

			DustType = Dust;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = [16, 16];
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			AddMapEntry(new Color(251, 235, 127), Language.GetText("MapObject.Lantern"));
			RegisterItemDrop(DropItem);
		}
		public override void HitWire(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int topY = j - tile.TileFrameY / 18 % 2;

			short frameAdjustment = (short)(tile.TileFrameX >= 18 ? -18 : 18);

			for (int y = topY; y < topY + 2; y++)
			{
				Main.tile[i, y].TileFrameX += frameAdjustment;
				Wiring.SkipWire(i, y);
			}

			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendTileSquare(-1, i, topY, 1, 2);
			}
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].TileFrameX / 18 != 0)
			{
				return;
			}
			r = LightColor.X;
			g = LightColor.Y;
			b = LightColor.Z;
		}
		public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
		{
			// The following math makes dust only spawn at the tile coordinates of the flames:
			// -
			// O
			if (FlameDust != -1)
			{
				if (Main.rand.NextBool(40) && tileFrameX < 18 && tileFrameY >= 18)
				{
					Dust dust = Terraria.Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 2), 14, 6, FlameDust, 0f, 0f, 100);
					if (Main.rand.NextBool(3))
					{
						dust.noGravity = true;
					}

					dust.velocity *= 0.3f;
					dust.velocity.Y -= 1.5f;
				}
			}
		}
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			offsetY = 0;
		}
		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (TileObjectData.IsTopLeft(Main.tile[i, j]))
			{
				Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
			}
			return false;
		}
		public override void AdjustMultiTileVineParameters(int i, int j, ref float? overrideWindCycle, ref float windPushPowerX, ref float windPushPowerY, ref bool dontRotateTopTiles, ref float totalWindMultiplier, ref Texture2D glowTexture, ref Color glowColor)
		{
			// Vanilla lanterns all share these parameters.
			overrideWindCycle = 1f;
			windPushPowerY = 0;
			if (GlowTexture != null)
			{
				glowTexture = GlowTexture.Value;
				glowColor = GlowColor;
			}
		}
		public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
		{
			if (FlameTexture == null)
			{
				return;
			}

			ulong flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

			tileFlameData.flameTexture = FlameTexture.Value;
			tileFlameData.flameSeed = flameSeed;
			tileFlameData.flameCount = 7;
			tileFlameData.flameColor = FlameColor;
			tileFlameData.flameRangeXMin = -10;
			tileFlameData.flameRangeXMax = 11;
			tileFlameData.flameRangeYMin = -10;
			tileFlameData.flameRangeYMax = 1;
			tileFlameData.flameRangeMultX = FlameJitterMultX;
			tileFlameData.flameRangeMultY = FlameJitterMultY;
		}
	}
	public abstract class BookcaseTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileNoAttach[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.IgnoredByNpcStepUp[Type] = true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

			DustType = Dust;
			AdjTiles = new int[] { TileID.Bookcases };

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Bookcase"));
		}
	}
	public abstract class WorkbenchTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileTable[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.IgnoredByNpcStepUp[Type] = true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

			DustType = Dust;
			AdjTiles = new int[] { TileID.WorkBenches };

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new[] { 18 };
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

			AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.WorkBench"));
		}
	}
	public abstract class ToiletTemplate : FurnitureTemplate
	{
		public const int NextStyleHeight = 40; // Calculated by adding all CoordinateHeights + CoordinatePaddingFix.Y applied to all of them + 2

		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.CanBeSatOnForNPCs[Type] = true; // Facilitates calling ModifySittingTargetInfo for NPCs
			TileID.Sets.CanBeSatOnForPlayers[Type] = true; // Facilitates calling ModifySittingTargetInfo for Players
			TileID.Sets.DisableSmartCursor[Type] = true;

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);

			DustType = Dust;
			AdjTiles = new int[] { TileID.Toilets }; // Condider adding TileID.Chairs to AdjTiles to mirror "(regular) Toilet" and "Golden Toilet" behavior for crafting stations

			// Names
			AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Toilet"));

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			// The following 3 lines are needed if you decide to add more styles and stack them vertically
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;

			TileObjectData.newTile.LavaDeath = LavaDeath;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1); // Facing right will use the second texture style
			TileObjectData.addTile(Type);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance); // Avoid being able to trigger it from long range
		}

		public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
		{
			// It is very important to know that this is called on both players and NPCs, so do not use Main.LocalPlayer for example, use info.restingEntity
			Tile tile = Framing.GetTileSafely(i, j);

			//info.directionOffset = info.restingEntity is Player ? 6 : 2; // Default to 6 for players, 2 for NPCs
			//info.visualOffset = Vector2.Zero; // Defaults to (0,0)

			if (info.RestingEntity is not null and Player)
			{
				Player p = (Player)info.RestingEntity;
				typeof(Player).GetMethod("TryToPoop", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(p, null);
			}

			info.TargetDirection = -1;

			if (tile.TileFrameX != 0)
			{
				info.TargetDirection = 1; // Facing right if sat down on the right alternate (added through addAlternate in SetStaticDefaults earlier)
			}

			// The anchor represents the bottom-most tile of the chair. This is used to align the entity hitbox
			// Since i and j may be from any coordinate of the chair, we need to adjust the anchor based on that
			info.AnchorTilePosition.X = i; // Our chair is only 1 wide, so nothing special required
			info.AnchorTilePosition.Y = j;

			if (tile.TileFrameY % NextStyleHeight == 0)
			{
				info.AnchorTilePosition.Y++; // Here, since our chair is only 2 tiles high, we can just check if the tile is the top-most one, then move it 1 down
			}

			// Here we add a custom fun effect to this tile that vanilla toilets do not have. This shows how you can type cast the restingEntity to Player and use visualOffset as well.
			if (info.RestingEntity is Player player && player.HasBuff(BuffID.Stinky))
			{
				info.VisualOffset = Main.rand.NextVector2Circular(2, 2);
			}
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{ // Avoid being able to trigger it from long range
				player.GamepadEnableGrappleCooldown();
				player.sitting.SitDown(player, i, j);
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{ // Match condition in RightClick. Interaction should only show if clicking it does something
				return;
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = DropItem;

			if (Main.tile[i, j].TileFrameX / 18 < 1)
			{
				player.cursorItemIconReversed = true;
			}
		}

		public override void HitWire(int i, int j)
		{
			// Spawn the toilet effect here when triggered by a signal
			Tile tile = Main.tile[i, j];

			int spawnX = i;
			int spawnY = j - (tile.TileFrameY % NextStyleHeight) / 18;

			Wiring.SkipWire(spawnX, spawnY);
			Wiring.SkipWire(spawnX, spawnY + 1);

			if (Wiring.CheckMech(spawnX, spawnY, 60))
			{
				Projectile.NewProjectile(Wiring.GetProjectileSource(spawnX, spawnY), spawnX * 16 + 8, spawnY * 16 + 12, 0f, 0f, ProjectileID.ToiletEffect, 0, 0f, Main.myPlayer);
			}
		}
	}
	public abstract class TableTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileTable[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.IgnoredByNpcStepUp[Type] = true;

			AdjTiles = new int[] { TileID.Tables };

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

			AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Table"));
			DustType = Dust;
		}
	}
	public abstract class SofaTemplate : FurnitureTemplate
	{
		public const int NextStyleHeight = 38; // Calculated by adding all CoordinateHeights + CoordinatePaddingFix.Y applied to all of them + 2
		public virtual float SittingHeight => 0;
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.CanBeSatOnForNPCs[Type] = false; // Facilitates calling ModifySittingTargetInfo for NPCs
			TileID.Sets.CanBeSatOnForPlayers[Type] = true; // Facilitates calling ModifySittingTargetInfo for Players

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Sofa"));
			DustType = Dust;
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance); // Avoid being able to trigger it from long range
		}

		public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
		{
			// It is very important to know that this is called on both players and NPCs, so do not use Main.LocalPlayer for example, use info.restingEntity
			Tile tile = Framing.GetTileSafely(i, j);

			if (info.RestingEntity.direction == 1) //Facing right
			{
				if (tile.TileFrameX == 0)
				{
					info.DirectionOffset = info.RestingEntity is Player ? -1 : 4; // Default to 6 for players, 2 for NPCs
					info.VisualOffset = new Vector2(5, SittingHeight); // Defaults to (0,0)
				}
				if (tile.TileFrameX == 18)
				{
					info.DirectionOffset = info.RestingEntity is Player ? -1 : 0;
					info.VisualOffset = new Vector2(1, SittingHeight);
				}
				if (tile.TileFrameX == 36)
				{
					info.DirectionOffset = info.RestingEntity is Player ? -1 : -6;
					info.VisualOffset = new Vector2(-3, SittingHeight);
				}
				info.TargetDirection = 1; // Facing right if sat down on while facing right, left otherwise
			}
			else //Facing left
			{
				if (tile.TileFrameX == 0)
				{
					info.DirectionOffset = info.RestingEntity is Player ? 1 : -4;
					info.VisualOffset = new Vector2(-3, SittingHeight);
				}
				if (tile.TileFrameX == 18)
				{
					info.DirectionOffset = info.RestingEntity is Player ? 1 : 2;
					info.VisualOffset = new Vector2(1, SittingHeight);
				}
				if (tile.TileFrameX == 36)
				{
					info.DirectionOffset = info.RestingEntity is Player ? 1 : 6;
					info.VisualOffset = new Vector2(5, SittingHeight);
				}
				info.TargetDirection = -1;
			}

			// The anchor represents the bottom-most tile of the chair. This is used to align the entity hitbox
			// Since i and j may be from any coordinate of the chair, we need to adjust the anchor based on that
			info.AnchorTilePosition.X = i; // Our chair is only 1 wide, so nothing special required
			info.AnchorTilePosition.Y = j;

			if (tile.TileFrameY % NextStyleHeight == 0)
			{
				info.AnchorTilePosition.Y++; // Here, since our chair is only 2 tiles high, we can just check if the tile is the top-most one, then move it 1 down
			}
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{ // Avoid being able to trigger it from long range
				player.GamepadEnableGrappleCooldown();
				player.sitting.SitDown(player, i, j);
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{ // Match condition in RightClick. Interaction should only show if clicking it does something
				return;
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = DropItem;
		}
	}
	public abstract class SinkTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Sink"));
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[] { TileID.Sinks };
			DustType = Dust;
		}
	}
	public abstract class PianoTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Piano"));
			DustType = Dust;
		}
	}
	public abstract class ClockTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.Clock[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
			16,
			16,
			16,
			16,
			16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.Origin = new Point16(0, 4);
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.GrandfatherClock"));
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[] { TileID.GrandfatherClocks };
			DustType = Dust;
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			var player = Main.player[Main.myPlayer];
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = DropItem;
		}

		public override bool RightClick(int x, int y)
		{
			{
				var text = "AM";
				var time = Main.time;
				if (!Main.dayTime)
				{
					time += 54000.0;
				}
				time = time / 86400.0 * 24.0;
				time = time - 7.5 - 12.0;
				if (time < 0.0)
				{
					time += 24.0;
				}
				if (time >= 12.0)
				{
					text = "PM";
				}
				var intTime = (int)time;
				var deltaTime = time - intTime;
				deltaTime = ((int)(deltaTime * 60.0));
				var text2 = string.Concat(deltaTime);
				if (deltaTime < 10.0)
				{
					text2 = "0" + text2;
				}
				if (intTime > 12)
				{
					intTime -= 12;
				}
				if (intTime == 0)
				{
					intTime = 12;
				}
				var newText = string.Concat("Time: ", intTime, ":", text2, " ", text);
				Main.NewText(newText, 255, 240, 20);
			}
			return true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
	public abstract class PlatformTemplate : FurnitureTemplate
	{
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = LavaDeath;
			TileID.Sets.Platforms[Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = LavaDeath;
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			AddMapEntry(new Color(191, 142, 111));
			//ItemDrop = ModContent.ItemType<Items.Placeable.Tile.OrangeBrickPlatform>();
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[] { TileID.Platforms };
			DustType = Dust;
		}

		public override void PostSetDefaults()
		{
			Main.tileNoSunLight[Type] = false;
		}
	}
}
