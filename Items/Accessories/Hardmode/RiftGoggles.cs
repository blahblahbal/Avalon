using AltLibrary.Common.AltOres;
using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Tiles.Ores;
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
					if (!Data.Sets.TileSets.RiftOres[Main.tile[pt.X, pt.Y].TileType])
					{
						pposTile2 = Player.position + new Vector2(Main.rand.Next(-50 * 16, 50 * 16), Main.rand.Next(-35 * 16, 35 * 16));
						pt = pposTile2.ToTileCoordinates();
					}
					else break;
				}
				if (Data.Sets.TileSets.RiftOres[Main.tile[pt.X, pt.Y].TileType])
				{
					#region copper
					if (RiftGogglesData.CopperTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.CopperTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.CopperTier[index], RiftGogglesData.CopperTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region iron
					if (RiftGogglesData.IronTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.IronTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.IronTier[index], RiftGogglesData.IronTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region silver
					if (RiftGogglesData.SilverTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.SilverTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.SilverTier[index], RiftGogglesData.SilverTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region gold
					if (RiftGogglesData.GoldTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.GoldTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.GoldTier[index], RiftGogglesData.GoldTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region rhodium
					if (RiftGogglesData.RhodiumTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.RhodiumTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.RhodiumTier[index], RiftGogglesData.RhodiumTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region evil
					if (RiftGogglesData.EvilTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.EvilTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.EvilTier[index], RiftGogglesData.EvilTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region cobalt
					if (RiftGogglesData.CobaltTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.CobaltTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.CobaltTier[index], RiftGogglesData.CobaltTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region mythril
					if (RiftGogglesData.MythrilTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.MythrilTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.MythrilTier[index], RiftGogglesData.MythrilTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
						ModContent.GetInstance<RiftGogglesSystem>().ChangeTile = true;
					}
					#endregion
					#region adamantite
					if (RiftGogglesData.AdamantiteTier.Contains(Main.tile[pt.X, pt.Y].TileType))
					{
						int index = RiftGogglesData.AdamantiteTier.FindIndex(0, f => f == Main.tile[pt.X, pt.Y].TileType);
						ModContent.GetInstance<RiftGogglesSystem>().Positions2 = GetAllTiles(new Point(pt.X, pt.Y), RiftGogglesData.AdamantiteTier[index], RiftGogglesData.AdamantiteTier.NextInListWrapped(index), new Vector3(pt.X, pt.Y, 54));
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
		if (Main.tile[i, j].HasTile && Data.Sets.TileSets.RiftOres[Main.tile[i, j].TileType])
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
public static class RiftGogglesData
{
	public static int AddValueToList(List<int> list, int tileID)
	{
		list.Add(tileID);
		return 0;
	}

	public static int NextInListWrapped(this List<int> list, int index)
	{
		index++;
		if (index >= list.Count) index = 0;
		return list[index];
	}
	public static void AddAltLibAlternates()
	{
		foreach (AltOre ore in OreSlotLoader.GetOres<CopperOreSlot>())
		{
			if (ore.ore != ModContent.TileType<BronzeOre>())
				CopperTier.Add(ore.ore);
		}
		foreach (AltOre ore in OreSlotLoader.GetOres<IronOreSlot>())
		{
			if (ore.ore != ModContent.TileType<NickelOre>())
				IronTier.Add(ore.ore);
		}
		foreach (AltOre ore in OreSlotLoader.GetOres<SilverOreSlot>())
		{
			if (ore.ore != ModContent.TileType<ZincOre>())
				SilverTier.Add(ore.ore);
		}
		foreach (AltOre ore in OreSlotLoader.GetOres<GoldOreSlot>())
		{
			if (ore.ore != ModContent.TileType<BismuthOre>())
				GoldTier.Add(ore.ore);
		}
		foreach (AltOre ore in OreSlotLoader.GetOres<CobaltOreSlot>())
		{
			if (ore.ore != ModContent.TileType<DurataniumOre>())
				CobaltTier.Add(ore.ore);
		}
		foreach (AltOre ore in OreSlotLoader.GetOres<MythrilOreSlot>())
		{
			if (ore.ore != ModContent.TileType<NaquadahOre>())
				MythrilTier.Add(ore.ore);
		}
		foreach (AltOre ore in OreSlotLoader.GetOres<AdamantiteOreSlot>())
		{
			if (ore.ore != ModContent.TileType<TroxiniumOre>())
				AdamantiteTier.Add(ore.ore);
		}
	}
	public static List<int> CopperTier = new List<int>() { TileID.Copper, TileID.Tin, ModContent.TileType<BronzeOre>() };
	public static List<int> IronTier = new List<int>() { TileID.Iron, TileID.Lead, ModContent.TileType<NickelOre>() };
	public static List<int> SilverTier = new List<int>() { TileID.Silver, TileID.Tungsten, ModContent.TileType<ZincOre>() };
	public static List<int> GoldTier = new List<int>() { TileID.Gold, TileID.Platinum, ModContent.TileType<BismuthOre>() };
	public static List<int> RhodiumTier = new List<int>() { ModContent.TileType<RhodiumOre>(), ModContent.TileType<OsmiumOre>(), ModContent.TileType<IridiumOre>() };
	public static List<int> EvilTier = new List<int>() { TileID.Demonite, TileID.Crimtane, ModContent.TileType<BacciliteOre>() };
	public static List<int> CobaltTier = new List<int>() { TileID.Cobalt, TileID.Palladium, ModContent.TileType<DurataniumOre>() };
	public static List<int> MythrilTier = new List<int>() { TileID.Mythril, TileID.Orichalcum, ModContent.TileType<NaquadahOre>() };
	public static List<int> AdamantiteTier = new List<int>() { TileID.Adamantite, TileID.Titanium, ModContent.TileType<TroxiniumOre>() };
}
