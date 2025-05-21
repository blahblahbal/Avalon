using Avalon.Common;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture
{
	public class LockedChests : ChestTemplate
	{
		private static int GetChestType(int type)
		{
			return type switch
			{
				0 => ItemID.IceChest,
				1 => ItemID.Chest,
				2 => ItemID.LivingWoodChest,
				3 => ItemID.EbonwoodChest,
				4 => ItemID.RichMahoganyChest,
				5 => ItemID.PearlwoodChest,
				6 => ItemID.IvyChest,
				7 => ItemID.SkywareChest,
				8 => ItemID.ShadewoodChest,
				9 => ItemID.WebCoveredChest,
				10 => ItemID.LihzahrdChest,
				11 => ItemID.WaterChest,
				26 => ItemID.MartianChest,
				_ => ItemID.None,
			};
		}

		protected override bool CanBeLocked => base.CanBeLocked;
		public override int Dust => DustID.Stone; // todo: give them each unique dusts (if the vanilla equivalent has them)
		protected override int ChestKeyItemId => ItemID.GoldenKey;
		public override bool CanBeUnlockedNormally => false;
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			for (int i = 0; i < 27; i++) // End at 26 which is the martian chest's position
			{
				// todo: set colour to the same as 
				// todo: give them each unique localisation entries
				AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
			}
		}
		public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			int type = GetChestType(TileObjectData.GetTileStyle(Main.tile[i, j]));
			if (type > 0)
			{
				yield return new Item(type);
			}
		}

		public static bool Lock(int X, int Y)
		{
			Tile tileSafely = Framing.GetTileSafely(X, Y);
			int style = TileObjectData.GetTileStyle(tileSafely);

			if (style is 0 or (>= 7 and <= 17) or 48)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
						tileSafely2.TileFrameX -= style switch
						{
							0 => -36,
							>= 7 and <= 10 => 4 * 36,
							11 => 11 * 36,
							12 => 10 * 36,
							>= 13 and <= 17 => 6 * 36,
							48 => 22 * 36,
							_ => throw new System.NotImplementedException(),
						};
					}
				}

				SoundEngine.PlaySound(SoundID.Unlock, new(X * 16, Y * 16));
				return true;
			}
			return false;
		}
		public static bool Unlock(int X, int Y)
		{
			Tile tileSafely = Framing.GetTileSafely(X, Y);
			int style = TileObjectData.GetTileStyle(tileSafely);

			if (style is (>= 0 and <= 11) or 26)
			{
				int dustType = DustID.Stone;
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = TileID.Containers;
						tileSafely2.TileFrameX += style switch
						{
							0 => 11 * 36,
							1 => -36,
							2 => 10 * 36,
							>= 3 and <= 6 => 4 * 36,
							>= 7 and <= 11 => 6 * 36,
							26 => 22 * 36,
							_ => throw new System.NotImplementedException(),
						};

						for (int k = 0; k < 4; k++)
						{
							Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, dustType);
						}
					}
				}

				SoundEngine.PlaySound(SoundID.Unlock, new(X * 16, Y * 16));
				return true;
			}
			return false;
		}
	}
	public class VanillaChestLocking : ModHook
	{
		protected override void Apply()
		{
			On_Chest.Lock += On_Chest_Lock;
			On_Chest.Unlock += On_Chest_Unlock;

			On_Player.PlaceThing_LockChest += On_Player_PlaceThing_LockChest;
		}

		private bool On_Chest_Lock(On_Chest.orig_Lock orig, int X, int Y)
		{
			if (!(Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null))
			{
				if (Main.tile[X, Y].TileType == TileID.Containers)
				{
					if (LockedChests.Lock(X, Y))
					{
						return true;
					}
				}
			}
			return orig(X, Y);
		}

		private bool On_Chest_Unlock(On_Chest.orig_Unlock orig, int X, int Y)
		{
			if (!(Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null))
			{
				if (Main.tile[X, Y].TileType == (ushort)ModContent.TileType<LockedChests>())
				{
					if (LockedChests.Unlock(X, Y))
					{
						return true;
					}
				}
			}
			return orig(X, Y);
		}

		private void On_Player_PlaceThing_LockChest(On_Player.orig_PlaceThing_LockChest orig, Player self)
		{
			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
			Item item = self.inventory[self.selectedItem];
			if (!(!tile.HasTile || item.type != ItemID.ChestLock || tile.TileType != TileID.Containers || !(self.position.X / 16f - (float)Player.tileRangeX - (float)item.tileBoost - (float)self.blockRange <= (float)Player.tileTargetX) || !((self.position.X + (float)self.width) / 16f + (float)Player.tileRangeX + (float)item.tileBoost - 1f + (float)self.blockRange >= (float)Player.tileTargetX) || !(self.position.Y / 16f - (float)Player.tileRangeY - (float)item.tileBoost - (float)self.blockRange <= (float)Player.tileTargetY) || !((self.position.Y + (float)self.height) / 16f + (float)Player.tileRangeY + (float)item.tileBoost - 2f + (float)self.blockRange >= (float)Player.tileTargetY) || !self.ItemTimeIsZero || self.itemAnimation <= 0 || !self.controlUseItem))
			{
				Tile tileSafely = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
				int style = tileSafely.TileFrameX / 36;

				if (style is 0 or (>= 7 and <= 17) or 48)
				{
					if (self.inventory[self.selectedItem].stack <= 0)
					{
						orig(self); // Probably redundant, just don't wanna return without calling orig
						return;
					}
					int xpos = Player.tileTargetX - (tile.TileFrameX / 18 % 2);
					int ypos = Player.tileTargetY - (tile.TileFrameY / 18);
					if (Chest.Lock(xpos, ypos))
					{
						self.inventory[self.selectedItem].stack--;
						if (self.inventory[self.selectedItem].stack <= 0)
						{
							self.inventory[self.selectedItem] = new Item();
						}
						if (Main.netMode == NetmodeID.MultiplayerClient)
						{
							NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, self.whoAmI, 3f, xpos, ypos);
						}
					}
				}
			}
			orig(self);
		}
	}
}

