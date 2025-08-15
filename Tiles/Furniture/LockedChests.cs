using Avalon.Common;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture
{
	public class LockedChests : ChestTemplate
	{
		public static int GetChestType(int type)
		{
			return type switch
			{
				0 => ItemID.Chest,
				7 => ItemID.EbonwoodChest,
				8 => ItemID.RichMahoganyChest,
				9 => ItemID.PearlwoodChest,
				10 => ItemID.IvyChest,
				11 => ItemID.IceChest,
				12 => ItemID.LivingWoodChest,
				13 => ItemID.SkywareChest,
				14 => ItemID.ShadewoodChest,
				15 => ItemID.WebCoveredChest,
				16 => ItemID.LihzahrdChest,
				17 => ItemID.WaterChest,
				28 => ItemID.DynastyChest,
				29 => ItemID.HoneyChest,
				30 => ItemID.SteampunkChest,
				31 => ItemID.PalmWoodChest,
				33 => ItemID.BorealWoodChest,
				34 => ItemID.SlimeChest,
				44 => ItemID.ObsidianChest,
				48 => ItemID.MartianChest,
				_ => ItemID.None,
			};
		}

		public static int GetDustType(int type)
		{
			return type switch
			{
				0 or 12 => DustID.WoodFurniture,
				7 => DustID.Ebonwood,
				8 => DustID.RichMahogany,
				9 => DustID.Pearlwood,
				10 => DustID.Gold,
				11 or 17 => DustID.Silver,
				13 => DustID.Skyware,
				14 => DustID.Shadewood,
				15 => DustID.Bone,
				16 => DustID.Lihzahrd,
				28 => DustID.DynastyWood,
				29 => DustID.t_Honey,
				30 => DustID.t_SteampunkMetal,
				31 => DustID.PalmWood,
				33 => DustID.t_BorealWood,
				34 => DustID.t_Slime,
				44 => DustID.Obsidian,
				_ => -1,
			};
		}

		protected override bool CanBeLocked => base.CanBeLocked;
		protected override int ChestKeyItemId => ItemID.GoldenKey;
		public override bool CanBeUnlockedNormally => false;
		private static Asset<Texture2D>? glowTexture;
		private static Asset<Texture2D>? glowTexture2;
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow");
			glowTexture2 = ModContent.Request<Texture2D>(Texture + "_Glow2");

			for (int i = 0; i < 49; i++) // End at 48 which is the martian chest's position
			{
				Color color = i switch
				{
					11 or 17 => new(106, 210, 255),
					10 or 13 or 15 => new(233, 207, 94),
					_ => new(174, 129, 92)
				};
				AddMapEntry(color, this.GetLocalization($"MapEntry{i}"), MapChestName);
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 3 : 10;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			type = GetDustType(TileObjectData.GetTileStyle(Main.tile[i, j]));
			return base.CreateDust(i, j, ref type);
		}

		public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			int type = GetChestType(TileObjectData.GetTileStyle(Main.tile[i, j]));
			if (type > 0)
			{
				yield return new Item(type);
			}
		}

		private int correctFrameY;
		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			if (GetChestType(drawData.tileFrameX / 36) == ItemID.MartianChest)
			{
				byte b = (byte)(100f + 150f * Main.martianLight);
				drawData.glowColor = new Color(b, b, b, 0);
				correctFrameY = drawData.tileFrameY + drawData.addFrY;
				drawData.glowSourceRect = new Rectangle(drawData.tileFrameX + drawData.addFrX, correctFrameY, drawData.tileWidth, drawData.tileHeight);
				drawData.glowTexture = glowTexture.Value;
			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			if (GetChestType(tile.TileFrameX / 36) == ItemID.MartianChest)
			{
				var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
				if (Main.drawToScreen)
				{
					zero = Vector2.Zero;
				}

				Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
				var frame = new Rectangle(tile.TileFrameX, correctFrameY, 16, 16);
				byte b = (byte)(100f + 150f * Main.martianLight);
				Color color = new(b, b, b, 100);
				spriteBatch.Draw(glowTexture2.Value, pos, frame, color);
			}
		}

		public static bool Lock(int X, int Y)
		{
			Tile tileSafely = Framing.GetTileSafely(X, Y);
			int style = TileObjectData.GetTileStyle(tileSafely);

			if (GetChestType(style) != ItemID.None)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
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

			if (GetChestType(style) != ItemID.None)
			{
				int dustType = style switch
				{
					16 => DustID.Gold,
					17 => DustID.SeaOatsOasis,
					48 => DustID.t_Martian,
					_ => DustID.Stone
				};
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = TileID.Containers;

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
			if (!(!tile.HasTile || item.type != ItemID.ChestLock || tile.TileType != TileID.Containers || !self.IsTargetTileInItemRange_AndPlayerBlockRange(item) || !self.ItemTimeIsZero || self.itemAnimation <= 0 || !self.controlUseItem))
			{
				Tile tileSafely = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
				int style = tileSafely.TileFrameX / 36;

				if (LockedChests.GetChestType(style) != ItemID.None)
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

