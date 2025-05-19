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

		public static bool Unlock(int X, int Y)
		{
			if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null)
			{
				return false;
			}
			int dustType = DustID.Stone;
			Tile tileSafely = Framing.GetTileSafely(X, Y);
			if (tileSafely.TileType != ModContent.TileType<LockedChests>()) return false;
			int type2 = tileSafely.TileType;
			int num2 = tileSafely.TileFrameX / 36;

			SoundEngine.PlaySound(SoundID.Unlock, new(X * 16, Y * 16));

			if (tileSafely.TileFrameX == 0)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						Main.tile[i, j].TileType = TileID.Containers;
						tileSafely2.TileFrameX += 11 * 36;
						for (int k = 0; k < 4; k++)
						{
							Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, dustType);
						}
					}
				}
			}
			else if (tileSafely.TileFrameX == 36)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						Main.tile[i, j].TileType = TileID.Containers;
						Main.tile[i, j].TileFrameX -= 36;
						for (int k = 0; k < 4; k++)
						{
							Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, dustType);
						}
					}
				}
			}
			else if (tileSafely.TileFrameX == 72)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = TileID.Containers;
						tileSafely2.TileFrameX += 10 * 36;
						for (int k = 0; k < 4; k++)
						{
							Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, dustType);
						}
					}
				}
			}
			else if (tileSafely.TileFrameX >= 108 && tileSafely.TileFrameX <= 216)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = TileID.Containers;
						tileSafely2.TileFrameX += 4 * 36;
						for (int k = 0; k < 4; k++)
						{
							Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, dustType);
						}
					}
				}
			}
			else if (tileSafely.TileFrameX >= 252 && tileSafely.TileFrameX <= 360)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = TileID.Containers;
						tileSafely2.TileFrameX += 6 * 36;
						for (int k = 0; k < 4; k++)
						{
							Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, dustType);
						}
					}
				}
			}
			else if (tileSafely.TileFrameX == 936)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = TileID.Containers;
						tileSafely2.TileFrameX += 22 * 36;
						for (int k = 0; k < 4; k++)
						{
							Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, dustType);
						}
					}
				}
			}

			return true;
		}

		public static bool Lock(int X, int Y)
		{
			if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null)
			{
				return false;
			}
			short num = 0;
			Tile tileSafely = Framing.GetTileSafely(X, Y);
			if (tileSafely.TileType != TileID.Containers) return false;
			int type = tileSafely.TileType;
			int num2 = tileSafely.TileFrameX / 36;


			SoundEngine.PlaySound(SoundID.Unlock, new(X * 16, Y * 16));
			if (tileSafely.TileFrameX == 0)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
						tileSafely2.TileFrameX += 36;
					}
				}
			}
			else if (tileSafely.TileFrameX == 396)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
						tileSafely2.TileFrameX -= 11 * 36;
					}
				}
			}
			else if (tileSafely.TileFrameX == 432)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
						tileSafely2.TileFrameX -= 10 * 36;
					}
				}
			}
			else if (tileSafely.TileFrameX >= 252 && tileSafely.TileFrameX <= 360)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
						tileSafely2.TileFrameX -= 4 * 36;
					}
				}
			}
			else if (tileSafely.TileFrameX >= 468 && tileSafely.TileFrameX <= 576)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
						tileSafely2.TileFrameX -= 6 * 36;
					}
				}
			}
			else if (tileSafely.TileFrameX == 1728)
			{
				for (int i = X; i <= X + 1; i++)
				{
					for (int j = Y; j <= Y + 1; j++)
					{
						Tile tileSafely2 = Framing.GetTileSafely(i, j);
						tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
						tileSafely2.TileFrameX -= 22 * 36;
					}
				}
			}
			return true;
		}
	}
}
