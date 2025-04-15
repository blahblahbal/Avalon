using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class RiftGoggles : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 1);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().RiftGoggles = true;
	}

	/*public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ItemID.CursedFlame, 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ItemID.Ichor, 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ModContent.ItemType<Pathogen>(), 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ItemID.CursedFlame, 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ItemID.Ichor, 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ModContent.ItemType<Pathogen>(), 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
    }*/
}
class RiftGogglesPlayer : ModPlayer
{
	public override void PostUpdate()
	{
		#region rift goggles
		// mobs
		if (Player.ZoneCrimson || Player.ZoneCorrupt || Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion)
		{
			if (Main.rand.NextBool(5500) && Player.GetModPlayer<AvalonPlayer>().RiftGoggles)
			{
				Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-20 * 16, 21 * 16), Main.rand.Next(-20 * 16, 21 * 16));
				Point pt = pposTile2.ToTileCoordinates();
				if (!Main.tile[pt.X, pt.Y].HasTile)
				{
					int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.MobRift>(), 0);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
					}

					for (int i = 0; i < 20; i++)
					{
						int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
						Main.dust[num893].velocity *= 2f;
						Main.dust[num893].scale = 0.9f;
						Main.dust[num893].noGravity = true;
						Main.dust[num893].fadeIn = 3f;
					}
				}
			}
		}
		if (ExxoAvalonOrigins.Confection != null)
		{
			if (Player.ZoneHallow || Player.InModBiome(ExxoAvalonOrigins.Confection.Find<ModBiome>("ConfectionBiome")))
			{
				if (Main.rand.NextBool(5500) && Player.GetModPlayer<AvalonPlayer>().RiftGoggles)
				{
					Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-20 * 16, 21 * 16), Main.rand.Next(-20 * 16, 21 * 16));
					Point pt = pposTile2.ToTileCoordinates();
					if (!Main.tile[pt.X, pt.Y].HasTile)
					{
						int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.MobRift>(), ai1: 1);
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
						}

						for (int i = 0; i < 20; i++)
						{
							int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
							Main.dust[num893].velocity *= 2f;
							Main.dust[num893].scale = 0.9f;
							Main.dust[num893].noGravity = true;
							Main.dust[num893].fadeIn = 3f;
						}
					}
				}
			}
		}
		// ores
		if (Player.GetModPlayer<AvalonPlayer>().RiftGoggles && Main.rand.NextBool(3200))
		{
			if (Player.ZoneRockLayerHeight)
			{
				Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-50 * 16, 50 * 16), Main.rand.Next(-35 * 16, 35 * 16));
				Point pt = pposTile2.ToTileCoordinates();
				for (int q = 0; q < 50; q++)
				{
					if (!Data.Sets.Tile.RiftOres[Main.tile[pt.X, pt.Y].TileType])
					{
						pposTile2 = Player.position + new Vector2(Main.rand.Next(-50 * 16, 50 * 16), Main.rand.Next(-35 * 16, 35 * 16));
						pt = pposTile2.ToTileCoordinates();
					}
					else break;
				}
				if (Data.Sets.Tile.RiftOres[Main.tile[pt.X, pt.Y].TileType])
				{
					#region copper
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Copper)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Copper, TileID.Tin, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Tin)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Tin, ModContent.TileType<Tiles.Ores.BronzeOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.BronzeOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.BronzeOre>(), TileID.Copper, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region iron
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Iron)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Iron, TileID.Lead, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Lead)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Lead, ModContent.TileType<Tiles.Ores.NickelOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.NickelOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.NickelOre>(), TileID.Iron, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region silver
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Silver)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Silver, TileID.Tungsten, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Tungsten)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Tungsten, ModContent.TileType<Tiles.Ores.ZincOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.ZincOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.ZincOre>(), TileID.Silver, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region gold
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Gold)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Gold, TileID.Platinum, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Platinum)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Platinum, ModContent.TileType<Tiles.Ores.BismuthOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.BismuthOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.BismuthOre>(), TileID.Gold, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region rhodium
					if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.RhodiumOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.RhodiumOre>(), ModContent.TileType<Tiles.Ores.OsmiumOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.OsmiumOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.OsmiumOre>(), ModContent.TileType<Tiles.Ores.IridiumOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.IridiumOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.IridiumOre>(), ModContent.TileType<Tiles.Ores.RhodiumOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region evil
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Demonite)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Demonite, TileID.Crimtane, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Crimtane)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Crimtane, ModContent.TileType<Tiles.Ores.BacciliteOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.BacciliteOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.BacciliteOre>(), TileID.Demonite, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region cobalt
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Cobalt)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Cobalt, TileID.Palladium, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Palladium)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Palladium, ModContent.TileType<Tiles.Ores.DurataniumOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.DurataniumOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.DurataniumOre>(), TileID.Cobalt, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region mythril
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Mythril)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Mythril, TileID.Orichalcum, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Orichalcum)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Orichalcum, ModContent.TileType<Tiles.Ores.NaquadahOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.NaquadahOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.NaquadahOre>(), TileID.Mythril, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region adamantite
					if (Main.tile[pt.X, pt.Y].TileType == TileID.Adamantite)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Adamantite, TileID.Titanium, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == TileID.Titanium)
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), TileID.Titanium, ModContent.TileType<Tiles.Ores.TroxiniumOre>(), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					else if (Main.tile[pt.X, pt.Y].TileType == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
					{
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), ModContent.TileType<Tiles.Ores.TroxiniumOre>(), TileID.Adamantite, new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
				}
			}
		}
		// fishing
		/*if (Player.ZoneCrimson || Player.ZoneCorrupt || Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion)
        {
            if (Main.rand.NextBool(15) && RiftGoggles)
            {
                Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-30 * 16, 21 * 16), Main.rand.Next(-30 * 16, 21 * 16));
                Point pt = pposTile2.ToTileCoordinates();
                //can spawn underwater if there's an overhang, needs to be fixed
                if (Main.tile[pt.X, pt.Y].LiquidType == LiquidID.Water && Main.tile[pt.X, pt.Y].LiquidAmount > 100 &&
                    Main.tile[pt.X, pt.Y - 3].LiquidAmount == 0 && Main.tile[pt.X, pt.Y - 2].LiquidAmount > 1) //  && (!Main.tile[pt.X, pt.Y - 3].HasTile || Main.tile[pt.X, pt.Y - 3].HasUnactuatedTile)
                {
                    if (ClassExtensions.CanSpawnFishingRift(new Vector2(pt.X * 16, pt.Y * 16), ModContent.NPCType<NPCs.FishingRift>(), 16 * 20))
                    {
                        int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.FishingRift>(), 0);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                            Main.dust[num893].velocity *= 2f;
                            Main.dust[num893].scale = 0.9f;
                            Main.dust[num893].noGravity = true;
                            Main.dust[num893].fadeIn = 3f;
                        }
                    }
                }
            }
        }*/
		#endregion rift goggles
	}
	public static void AddValidNeighbors(Queue<Point> toDo, List<Point> done, Point pos, int type, List<Vector4> positions)
	{
		TryAddPoint(toDo, done, pos + new Point(-1, 0), type, positions);
		TryAddPoint(toDo, done, pos + new Point(1, 0), type, positions);
		TryAddPoint(toDo, done, pos + new Point(0, -1), type, positions);
		TryAddPoint(toDo, done, pos + new Point(0, 1), type, positions);
	}
	public static List<Vector4> GetAllTiles(Point pos, int type, int replace, Vector3 position)
	{
		List<Vector4> positions = new List<Vector4>();

		Queue<Point> toDo = new Queue<Point>();
		List<Point> done = new List<Point>();

		// add the first point
		TryAddPoint(toDo, done, pos, type, new List<Vector4>() { new Vector4(position.X, position.Y, position.Z, replace) });

		// repeatedly take the next point to do, add it to the done list, then add all of its neighbors to the to-do list
		Point nextPoint;
		while (toDo.TryDequeue(out nextPoint))
		{
			// dequeue automatically removes it from the list
			done.Add(nextPoint);
			positions.Add(new Vector4(nextPoint.X, nextPoint.Y, // position
				position.Z, // timer
				replace)); // tile to draw
			AddValidNeighbors(toDo, done, nextPoint, type, positions);
		}

		return positions;
	}
	public static void TryAddPoint(Queue<Point> toDo, List<Point> done, Point point, int type, List<Vector4> positions)
	{
		if (!toDo.Contains(point) && !done.Contains(point))
		{
			Tile t = Framing.GetTileSafely(point.X, point.Y);
			if (t.HasTile && t.TileType == type)
				toDo.Enqueue(point);
		}
	}
}
class RiftGogglesGlobalTile : GlobalTile
{
	public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
	{
		if (!Main.tileSolid[Main.tile[i, j].TileType])
		{
			return;
		}
		if (Main.tile[i, j].HasTile && Data.Sets.Tile.RiftOres[Main.tile[i, j].TileType])
		{
			Tile tile = Main.tile[i, j];
			var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}

			if (ModContent.GetInstance<RiftGogglesSystem>().Positions2.Count == 0) return;
			int index = ModContent.GetInstance<RiftGogglesSystem>().Positions2.FindVector2InVector4List(new Vector2(i, j));
			if (index >= ModContent.GetInstance<RiftGogglesSystem>().Positions2.Count || index == -1) return;

			// 654 to 600
			int v = (int)ModContent.GetInstance<RiftGogglesSystem>().Positions2[index].Z; // - 600;
																						  //if (v < 0) return;
			int frameY = (int)(54 - ModContent.GetInstance<RiftGogglesSystem>().Positions2[index].Z) / 3;
			int frameX = 0;
			if (frameY > 6 && frameY <= 13)
			{
				frameY -= 7;
				frameX = 1;
			}
			else if (frameY > 13)
			{
				frameY -= 14;
				frameX = 2;
			}

			Vector2 pos = new Vector2(i, j) * 16 + zero - Main.screenPosition;
			var frame = new Rectangle(tile.TileFrameX + frameX * 288, tile.TileFrameY + frameY * 270, 16, 16);
			var halfFrame = new Rectangle(tile.TileFrameX + frameX * 288, tile.TileFrameY + frameY * 270, 16, 8);

			Texture2D tex = ModContent.Request<Texture2D>("Avalon/Assets/Textures/OreRiftAnimation").Value;
			if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
			{
				spriteBatch.Draw(tex, pos, frame, Color.White);
			}
			else if (tile.IsHalfBlock)
			{
				pos = new Vector2(i * 16, (j * 16) + 8) + zero - Main.screenPosition;
				spriteBatch.Draw(tex, pos, halfFrame, Color.White);
			}
			else
			{
				Vector2 screenOffset = new Vector2(Main.offScreenRange, Main.offScreenRange);
				if (Main.drawToScreen)
				{
					screenOffset = Vector2.Zero;
				}
				Vector2 vector = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + screenOffset;
				int slopeType = (int)tile.Slope;
				int num5 = 2;
				int addFrY = Main.tileFrame[type] * 90;
				int addFrX = 0;
				for (int q = 0; q < 8; q++)
				{
					int num6 = q * -2;
					int num7 = 16 - q * 2;
					int num8 = 16 - num7;
					int num9;
					switch (slopeType)
					{
						case 1:
							num6 = 0;
							num9 = q * 2;
							num7 = 14 - q * 2;
							num8 = 0;
							break;
						case 2:
							num6 = 0;
							num9 = 16 - q * 2 - 2;
							num7 = 14 - q * 2;
							num8 = 0;
							break;
						case 3:
							num9 = q * 2;
							break;
						default:
							num9 = 16 - q * 2 - 2;
							break;
					}
					Main.spriteBatch.Draw(tex, vector + new Vector2(num9, q * num5 + num6), (Rectangle?)new Rectangle(tile.TileFrameX + frameX * 288 + addFrX + num9, tile.TileFrameY + frameY * 270 + addFrY + num8, num5, num7), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
				int num10 = ((slopeType <= 2) ? 14 : 0);
				Main.spriteBatch.Draw(tex, vector + new Vector2(0f, num10), (Rectangle?)new Rectangle(tile.TileFrameX + frameX * 288 + addFrX, tile.TileFrameY + frameY * 270 + addFrY + num10, 16, 2), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
		}
	}
}
public class OreRift : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemNoGravity[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 28;
		Item.alpha = 120;
	}
	public override bool CanPickup(Player player)
	{
		return false;
	}
	public override void PostUpdate()
	{
		Point tile = Item.Center.ToTileCoordinates();
		for (int q = 0; q < Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftLocations.Count; q++)
		{
			Vector3 pos = Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftLocations[q];
			if (pos.Z > 260)
			{
				Item.alpha -= 3;
			}
			if (pos.Z < 150)
			{
				Item.alpha += 5;
			}
			if (pos.Z == 150)
			{
				#region copper
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Copper)
				{
					RiftReplace(tile, TileID.Copper, TileID.Tin, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tin)
				{
					RiftReplace(tile, TileID.Tin, ModContent.TileType<Tiles.Ores.BronzeOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BronzeOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.BronzeOre>(), TileID.Copper, pos);
				}
				#endregion
				#region iron
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Iron)
				{
					RiftReplace(tile, TileID.Iron, TileID.Lead, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Lead)
				{
					RiftReplace(tile, TileID.Lead, ModContent.TileType<Tiles.Ores.NickelOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NickelOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.NickelOre>(), TileID.Iron, pos);
				}
				#endregion
				#region silver
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Silver)
				{
					RiftReplace(tile, TileID.Silver, TileID.Tungsten, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tungsten)
				{
					RiftReplace(tile, TileID.Tungsten, ModContent.TileType<Tiles.Ores.ZincOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.ZincOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.ZincOre>(), TileID.Silver, pos);
				}
				#endregion
				#region gold
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Gold)
				{
					RiftReplace(tile, TileID.Gold, TileID.Platinum, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Platinum)
				{
					RiftReplace(tile, TileID.Platinum, ModContent.TileType<Tiles.Ores.BismuthOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BismuthOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.BismuthOre>(), TileID.Gold, pos);
				}
				#endregion
				#region rhodium
				if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.RhodiumOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.RhodiumOre>(), ModContent.TileType<Tiles.Ores.OsmiumOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.OsmiumOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.OsmiumOre>(), ModContent.TileType<Tiles.Ores.IridiumOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.IridiumOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.IridiumOre>(), ModContent.TileType<Tiles.Ores.RhodiumOre>(), pos);
				}
				#endregion
				#region evil
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Demonite)
				{
					RiftReplace(tile, TileID.Demonite, TileID.Crimtane, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Crimtane)
				{
					RiftReplace(tile, TileID.Crimtane, ModContent.TileType<Tiles.Ores.BacciliteOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BacciliteOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.BacciliteOre>(), TileID.Demonite, pos);
				}
				#endregion
				#region cobalt
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Cobalt)
				{
					RiftReplace(tile, TileID.Cobalt, TileID.Palladium, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Palladium)
				{
					RiftReplace(tile, TileID.Palladium, ModContent.TileType<Tiles.Ores.DurataniumOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.DurataniumOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.DurataniumOre>(), TileID.Cobalt, pos);
				}
				#endregion
				#region mythril
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Mythril)
				{
					RiftReplace(tile, TileID.Mythril, TileID.Orichalcum, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Orichalcum)
				{
					RiftReplace(tile, TileID.Orichalcum, ModContent.TileType<Tiles.Ores.NaquadahOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NaquadahOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.NaquadahOre>(), TileID.Mythril, pos);
				}
				#endregion
				#region adamantite
				if (Main.tile[tile.X, tile.Y].TileType == TileID.Adamantite)
				{
					RiftReplace(tile, TileID.Adamantite, TileID.Titanium, pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == TileID.Titanium)
				{
					RiftReplace(tile, TileID.Titanium, ModContent.TileType<Tiles.Ores.TroxiniumOre>(), pos);
				}
				else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
				{
					RiftReplace(tile, ModContent.TileType<Tiles.Ores.TroxiniumOre>(), TileID.Adamantite, pos);
				}
				#endregion
			}
			pos.Z--;
			if (pos.Z == 0)
			{
				Item.active = false;
			}
		}
	}
	public static List<List<Point>> AddValidNeighbors(List<List<Point>> p, Point start)
	{
		p.Add(new List<Point>()
		{
			start + new Point(0, -1), start + new Point(0, 1), start + new Point(-1, 0), start + new Point(1, 0)
		});

		return p;
	}

	public static void Honeyify(Point p, int type, int maxTiles = 600)
	{
		int tiles = 0;

		Tile tile = Framing.GetTileSafely(p);
		if (!tile.HasTile || tile.TileType != type)
		{
			return;
		}

		List<List<Point>> points = new List<List<Point>>();
		points = AddValidNeighbors(points, p);

		int index = 0;

		while (points.Count > 0 && tiles < maxTiles && index < points.Count)
		{
			List<Point> tilePos = points[index];
			foreach (Point a in tilePos)
			{
				Tile t = Framing.GetTileSafely(a.X, a.Y);
				if (t.HasTile && t.TileType == type && t.LiquidAmount == 0)
				{
					t.LiquidAmount = 54;
					t.LiquidType = LiquidID.Honey;
					tiles++;
					AddValidNeighbors(points, a);
				}
			}
			index++;
		}
	}

	public static void RiftReplace(Point p, int type, int replace, Vector3 position, int maxTiles = 600)
	{
		List<Vector4> positions = new List<Vector4>();
		int tiles = 0;

		Tile tile = Framing.GetTileSafely(p);
		if (!tile.HasTile || tile.TileType != type)
		{
			return;
		}

		List<List<Point>> points = new List<List<Point>>();
		points = AddValidNeighbors(points, p);

		int index = 0;
		while (points.Count > 0 && tiles < maxTiles && index < points.Count)
		{
			List<Point> tilePos = points[index];

			foreach (Point a in tilePos)
			{
				Tile t = Framing.GetTileSafely(a.X, a.Y);
				if (t.HasTile && t.TileType == type && t.LiquidType == LiquidID.Honey)
				{
					Tile q = Framing.GetTileSafely(a.X, a.Y);
					positions.Add(new Vector4(a.X, a.Y, // position
												position.Z, // timer
												replace)); // tile to draw
														   //q.TileType = (ushort)replace;
														   //WorldGen.SquareTileFrame(a.X, a.Y);
														   //if (Main.netMode != NetmodeID.SinglePlayer)
														   //{
														   //    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 21, a.X, a.Y, replace);
														   //}
					tiles++;
					AddValidNeighbors(points, a);
				}
			}
			index++;
		}
	}
}
public class RiftGogglesSystem : ModSystem
{
	public List<Vector4> Positions2 = new List<Vector4>();
	public bool ChangeTile = true;

	public override void PostUpdateEverything()
	{

		for (int i = 0; i < Positions2.Count; i++)
		{
			{
				Vector4 test = Positions2[i];
				test.Z--;
				if (test.Z < 0) test.Z = 0;
				Positions2[i] = test;
			}

			// change the tile
			if (ModContent.GetInstance<RiftGogglesSystem>().Positions2[i].Z == 27)
			{
				Point tilePos = new Point((int)ModContent.GetInstance<RiftGogglesSystem>().Positions2[i].X, (int)ModContent.GetInstance<RiftGogglesSystem>().Positions2[i].Y);
				Main.tile[tilePos].TileType = (ushort)ModContent.GetInstance<RiftGogglesSystem>().Positions2[i].W;
				//WorldGen.SquareTileFrame(tilePos.X, tilePos.Y);
			}
		}
	}
}
