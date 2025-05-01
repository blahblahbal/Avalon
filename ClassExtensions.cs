using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Ammo;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon;

public static class ClassExtensions
{
	public static Rectangle Expand(this Rectangle r, int xDist, int yDist)
	{
		r.X -= xDist;
		r.Y -= yDist;
		r.Width += xDist * 2;
		r.Height += yDist * 2;
		return r;
	}

	public static Dictionary<TKey, TValue> TorchLauncherAdding<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
		where TKey : notnull where TValue : notnull
	{
		dict.Add(key, value);
		return dict;
	}
	public static List<List<int>> AddToListAndReturnIt(this List<List<int>> list, List<int> list2)
	{
		list.Add(list2);
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[0] == ItemID.HallowedKey)
			{
				Data.Sets.ItemSets.HallowedChest.Add(list2[1]);
			}
			if (list2[0] == ItemID.CorruptionKey)
			{
				Data.Sets.ItemSets.CorruptionChest.Add(list2[1]);
			}
			if (list2[0] == ItemID.CrimsonKey)
			{
				Data.Sets.ItemSets.CrimsonChest.Add(list2[1]);
			}
			if (list2[0] == ItemID.DungeonDesertKey)
			{
				Data.Sets.ItemSets.DesertChest.Add(list2[1]);
			}
			if (list2[0] == ItemID.JungleKey)
			{
				Data.Sets.ItemSets.JungleChest.Add(list2[1]);
			}
			if (list2[0] == ItemID.FrozenKey)
			{
				Data.Sets.ItemSets.FrozenChest.Add(list2[1]);
			}
		}
		return list;
	}
	public static int FindVector2InVector4List(this List<Vector4> v4, Vector2 v2)
	{
		for (int i = 0; i < v4.Count; i++)
		{
			if (v4[i].X == v2.X && v4[i].Y == v2.Y)
			{
				return i;
			}
		}
		return -1;
	}

	public static int FindVector2InVector3List(this List<Vector3> v3, Vector2 v2)
	{
		for (int i = 0; i < v3.Count; i++)
		{
			if (v3[i].X == v2.X && v3[i].Y == v2.Y)
			{
				return i;
			}
		}
		return -1;
	}
	public static int BannerPlaceStyleToItemID(int placeStyle)
	{
		placeStyle += 21;
		for (int i = 0; i < ItemLoader.ItemCount; i++)
		{
			Item item = new Item();
			item.SetDefaults(i);
			if (item.createTile == TileID.Banners && item.placeStyle == placeStyle)
			{
				return i;
			}
		}
		return -1;
	}

	public static int GetPickaxePower(int tileType, int yPos)
	{
		if (!TileID.Sets.Ore[tileType]) return -1;

		int power = 0;
		if (TileLoader.GetTile(tileType) is ModTile modTile)
		{
			power = modTile.MinPick;
		}
		else
		{
			if (tileType == TileID.Chlorophyte) power = 200;
			if (tileType == TileID.Meteorite) power = 50;
			if ((tileType == TileID.Demonite || tileType == TileID.Crimtane) && yPos > Main.worldSurface) power = 55;
			if (tileType == TileID.Hellstone) power = 70;
			if (tileType == TileID.Cobalt || tileType == TileID.Palladium) power = 100;
			if (tileType == TileID.Mythril || tileType == TileID.Orichalcum) power = 110;
			if (tileType == TileID.Adamantite || tileType == TileID.Titanium) power = 150;
		}

		return power;
	}

	/// <summary>
	/// Return the <see cref="Player"/> instance of this NPC's target.
	/// </summary>
	/// <param name="npc"></param>
	/// <returns></returns>
	public static Player PlayerTarget(this NPC npc) => Main.player[npc.target];

	/// <summary>
	/// Return the <see cref="Player"/> instance of this Projectile's owner.
	/// </summary>
	/// <param name="proj"></param>
	/// <returns></returns>
	public static Player Owner(this Projectile proj) => Main.player[proj.owner];

	public static int OwnerProjCounts(this Projectile proj, int type)
	{
		return Main.player[proj.owner].ownedProjectileCounts[type];
	}

	public static bool DoesTileExistInBoxAroundPlayer(this Player p, int boxRadius, int tileType, bool silenceCandle = false)
	{
		Point pos = p.Center.ToTileCoordinates();
		for (int x = pos.X - boxRadius; x <= pos.X + boxRadius; x++)
		{
			for (int y = pos.Y - boxRadius; y <= pos.Y + boxRadius; y++)
			{
				if (!WorldGen.InWorld(x, y)) continue;
				if (silenceCandle)
				{
					if (Main.tile[x, y].TileType == tileType && Main.tile[x, y].TileFrameX == 0)
					{
						return true;
					}
				}
				else
				{
					if (Main.tile[x, y].TileType == tileType)
					{
						return true;
					}
				}
			}
		}
		return false;
	}
	public static bool HasHeadThatShouldntBeReplaced(this Player p)
	{
		return (p.merman && !p.hideMerman) ||
			(p.wereWolf && !p.hideWolf) ||
			(p.GetModPlayer<AvalonPlayer>().lavaMerman && !p.GetModPlayer<AvalonPlayer>().HideVarefolk) || p.face == 19;
	}
	public static bool IsPotion(this Item i)
	{
		return i.healLife > 0 || i.healMana > 0 || i.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina > 0;
	}
	public static void SkipWireMulti(int xpos, int ypos, int xLength, int yLength)
	{
		for (int i = xpos; i < xpos + xLength - 1; i++)
		{
			for (int j = ypos; j < ypos + yLength - 1; j++)
			{
				Wiring.SkipWire(i, j);
			}
		}
	}

	/// <summary>
	///     Finds a type of NPC.
	/// </summary>
	/// <param name="type">The type of NPC to find.</param>
	/// <returns>The index of the found NPC in the Main.npc[] array. If not found, returns -1.</returns>
	public static int FindATypeOfNPC(int type)
	{
		foreach (var npc in Main.ActiveNPCs)
		{
			if (type == npc.type && npc.active)
			{
				return npc.whoAmI;
			}
		}

		return -1;
	}

	public static void DropCoinsProperly(float num7, int i, int j)
	{
		while ((int)num7 > 0)
		{
			if (num7 > 1000000f)
			{
				int num8 = (int)(num7 / 1000000f);
				if (num8 > 50 && Main.rand.NextBool(2))
				{
					num8 /= Main.rand.Next(3) + 1;
				}
				if (Main.rand.NextBool(2))
				{
					num8 /= Main.rand.Next(3) + 1;
				}
				num7 -= 1000000 * num8;
				int platinum = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i, j, 16, 16, ItemID.PlatinumCoin, num8);
				if (Main.netMode == NetmodeID.MultiplayerClient && platinum >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, platinum, 1f);
				}
				continue;
			}
			if (num7 > 10000f)
			{
				int num9 = (int)(num7 / 10000f);
				if (num9 > 50 && Main.rand.NextBool(2))
				{
					num9 /= Main.rand.Next(3) + 1;
				}
				if (Main.rand.NextBool(2))
				{
					num9 /= Main.rand.Next(3) + 1;
				}
				num7 -= 10000 * num9;
				int gold = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i, j, 16, 16, ItemID.GoldCoin, num9);
				if (Main.netMode == NetmodeID.MultiplayerClient && gold >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, gold, 1f);
				}
				continue;
			}
			if (num7 > 100f)
			{
				int num10 = (int)(num7 / 100f);
				if (num10 > 50 && Main.rand.NextBool(2))
				{
					num10 /= Main.rand.Next(3) + 1;
				}
				if (Main.rand.NextBool(2))
				{
					num10 /= Main.rand.Next(3) + 1;
				}
				num7 -= 100 * num10;
				int silver = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i, j, 16, 16, ItemID.SilverCoin, num10);
				if (Main.netMode == NetmodeID.MultiplayerClient && silver >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, silver, 1f);
				}
				continue;
			}
			int num11 = (int)num7;
			if (num11 > 50 && Main.rand.NextBool(2))
			{
				num11 /= Main.rand.Next(3) + 1;
			}
			if (Main.rand.NextBool(2))
			{
				num11 /= Main.rand.Next(4) + 1;
			}
			if (num11 < 1)
			{
				num11 = 1;
			}
			num7 -= num11;
			int money = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i, j, 16, 16, ItemID.CopperCoin, num11);
			if (Main.netMode == NetmodeID.MultiplayerClient && money >= 0)
			{
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, money, 1f);
			}
		}
	}
	public static Player GetPlayerForTile(int x, int y)
	{
		return Main.player[Player.FindClosest(new Vector2(x, y) * 16f, 16, 16)];
	}
	public static bool DownedAllButOneMechBoss()
	{
		return NPC.downedMechBoss1 && NPC.downedMechBoss2 && !NPC.downedMechBoss3 ||
			NPC.downedMechBoss1 && !NPC.downedMechBoss2 && NPC.downedMechBoss3 ||
			!NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
	}
	public static Vector2 LengthClamp(this Vector2 vector, float max, float min = 0)
	{
		if (vector.Length() > max) return Vector2.Normalize(vector) * max;
		else if (vector.Length() < min) return Vector2.Normalize(vector) * min;
		else return vector;
	}
	//public static bool AnyOreRiftsInRange(int x, int y, Item )
	//{
	//    for (int i = 0; i < 400; i++)
	//    {
	//        if (Main.item[i].type == ModContent.ItemType<Items.OreRift>() && Vector2.Distance(Main.item[i].position, 
	//    }
	//}
	public static List<List<Point>> AddValidNeighbors(List<List<Point>> p, Point start)
	{
		p.Add(new List<Point>()
		{
			start + new Point(0, -1), start + new Point(0, 1), start + new Point(-1, 0), start + new Point(1, 0)
		});

		return p;
	}

	/// <summary>
	/// Harvests an area using a veinminer algorithm.
	/// </summary>
	/// <param name="p">The tile coordinates as a Point.</param>
	/// <param name="type">The tile type to veinmine.</param>
	public static void VeinMine(Point p, int type, int maxTiles = 500)
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
				if (t.HasTile && t.TileType == type)
				{
					WorldGen.KillTile(a.X, a.Y);
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 20, a.X, a.Y);
					}
					tiles++;
					AddValidNeighbors(points, a);
				}
			}
			index++;
		}
	}

	public static void ReplaceVein(Point p, int type, int replace, int maxTiles = 500)
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
				if (t.HasTile && t.TileType == type)
				{
					Tile q = Framing.GetTileSafely(a.X, a.Y);
					if (replace == ushort.MaxValue)
					{
						q.HasTile = false;
					}
					else
					{
						q.TileType = (ushort)replace;
						WorldGen.SquareTileFrame(a.X, a.Y);
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 21, a.X, a.Y, replace);
						}
					}
					tiles++;
					AddValidNeighbors(points, a);
				}
			}
			index++;
		}
	}

	public static int ReturnEquippedDyeInSlot(this Player p, int type)
	{
		int slot = -1;
		for (int i = 3; i < p.armor.Length; i++)
		{
			if (i >= 10 && i <= 12) continue;
			int slot2 = i;
			if (p.armor[i].type == type)
			{
				if (i > 10)
					slot2 -= 10;
				slot = slot2;

			}
		}
		return slot;
	}
	public static int GetTileMinPick(Tile tile)
	{
		int type = tile.TileType;
		if (type > 692)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null)
			{
				return modTile.MinPick;
			}
		}
		else
		{
			if (type == TileID.Traps)
			{
				if (tile.TileFrameY / 18 - 1 <= 3)
				{
					return 210;
				}
			}
			switch (type)
			{
				case TileID.LihzahrdBrick:
				case TileID.LihzahrdAltar:
					return 210;
				case TileID.Chlorophyte:
					return 200;
				case TileID.Adamantite:
				case TileID.Titanium:
					return 150;
				case TileID.Mythril:
				case TileID.Orichalcum:
					return 110;
				case TileID.Cobalt:
				case TileID.Palladium:
					return 100;
				case TileID.Hellstone:
					return 70;
				case TileID.Ebonstone:
				case TileID.Crimstone:
				case TileID.Pearlstone:
				case TileID.Hellforge:
					return 65;
				case TileID.Demonite:
				case TileID.Crimtane:
				case TileID.Obsidian:
					return 55;
				case TileID.Meteorite:
					return 50;
			}
		}
		return 0;
	}
	/// <summary>
	///     Helper method for checking if the current item is an armor piece - used for armor prefixes.
	/// </summary>
	/// <param name="item">The item.</param>
	/// <returns>Whether or not the item is an armor piece.</returns>
	public static bool IsArmor(this Item item) =>
		(item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1) && !item.vanity;

	public static Rectangle NewRectVector2(Vector2 v, Vector2 wH) => new((int)v.X, (int)v.Y, (int)wH.X, (int)wH.Y);

	/// <summary>
	/// Helper method to check if the current item is a tool - used for tool prefixes.
	/// </summary>
	/// <param name="item">The item.</param>
	/// <returns>Whether or not the item is a tool.</returns>
	public static bool IsTool(this Item item)
	{
		return item.pick > 0 || item.axe > 0 || item.hammer > 0;
	}

	public static void Active(this Tile t, bool a) => t.HasTile = a;

	public static Vector2 ClampToCircle(Vector2 center, float radius, Vector2 pos)
	{
		// Calculate the offset vector from the center of the circle to our position
		Vector2 offset = pos - center;
		// Calculate the linear distance of this offset vector
		float distance = offset.Length();
		if (radius < distance)
		{
			// If the distance is more than our radius we need to clamp
			// Calculate the direction to our position
			Vector2 direction = offset / distance;
			// Calculate our new position using the direction to our old position and our radius
			pos = center + direction * radius;
			return pos;
		}
		return pos;
	}
	public static void ConsumeStamina(this Player p, int amt)
	{
		if (p.GetModPlayer<AvalonStaminaPlayer>().StaminaDrain)
		{
			amt *= (int)(p.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks * p.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainMult);
		}

		if (p.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
		{
			p.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
		}
		else if (p.GetModPlayer<AvalonStaminaPlayer>().StamFlower)
		{
			p.GetModPlayer<AvalonStaminaPlayer>().QuickStamina();
			if (p.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
			{
				p.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
			}
		}
	}
	public static void AttemptToConvertNPCToContagion(this NPC n)
	{
		if (n.type == NPCID.Bunny)
		{
			n.Transform(ModContent.NPCType<NPCs.Critters.ContaminatedBunny>());
		}
		if (n.type == NPCID.Goldfish)
		{
			n.Transform(ModContent.NPCType<NPCs.Critters.ContaminatedGoldfish>());
		}
		if (n.type == NPCID.Penguin)
		{
			n.Transform(ModContent.NPCType<NPCs.Critters.ContaminatedPenguin>());
		}
	}

	public static Item? HasItemInArmorFindIt(this Player p, int type)
	{
		for (int i = 0; i < p.armor.Length; i++)
		{
			if (p.armor[i].type == type) return p.armor[i];
		}
		return null;
	}
	public static int HasItemInArmorReturnIndex(this Player p, int type)
	{
		for (int i = 0; i < p.armor.Length; i++)
		{
			if (p.armor[i].type == type) return i;
		}
		return -1;
	}
	public static bool HasItemInFunctionalAccessories(this Player p, int type)
	{
		int max = 7;
		if (Main.expertMode) max = 8;
		if (Main.masterMode) max = 9;
		for (int i = 3; i <= max; i++)
		{
			if (p.armor[i].type == type) return true;
		}
		return false;
	}

	public static void SendPacket(this Player p, ModPacket packet, bool server)
	{
		if (!server)
		{
			packet.Send();
		}
		else
		{
			packet.Send(-1, p.whoAmI);
		}
	}

	/// <summary>
	///     Checks if the current player has an item in their armor/accessory slots.
	/// </summary>
	/// <param name="p">The player.</param>
	/// <param name="type">The item ID to check.</param>
	/// <returns>Whether or not the item is found.</returns>
	public static bool HasItemInArmor(this Player p, int type) => p.armor.Any(t => type == t.type);
	public static bool InPillarZone(this Player p)
	{
		if (!p.ZoneTowerStardust && !p.ZoneTowerVortex && !p.ZoneTowerSolar)
		{
			return p.ZoneTowerNebula;
		}

		return true;
	}

	/// <summary>
	///     Used to draw float coordinates to rounded coordinates to avoid blurry rendering of textures.
	/// </summary>
	/// <param name="vector">The vector to convert.</param>
	/// <returns>The rounded vector.</returns>
	public static Vector2 ToNearestPixel(this Vector2 vector) => new((int)(vector.X + 0.5f), (int)(vector.Y + 0.5f));

	/// <summary>
	///     Helper method for Vampire Teeth and Blah's Knives life steal.
	/// </summary>
	/// <param name="p">The player.</param>
	/// <param name="dmg">The damage to use in the life steal calculation.</param>
	/// <param name="position">The position to spawn the life steal projectile at.</param>
	public static void VampireHeal(this Player p, int dmg, Vector2 position)
	{
		float num = dmg * 0.075f;
		if ((int)num == 0)
		{
			return;
		}

		if (p.lifeSteal <= 0f)
		{
			return;
		}

		p.lifeSteal -= num;
		int num2 = p.whoAmI;
		Projectile.NewProjectile(
			p.GetSource_Accessory(new Item(ModContent.ItemType<VampireTeeth>())),
			position.X, position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, p.whoAmI, num2, num);
	}
	public static Asset<T> VanillaLoad<T>(this Asset<T> asset) where T : class
	{
		try
		{
			if (asset.State == AssetState.NotLoaded)
			{
				Main.Assets.Request<Texture2D>(asset.Name, AssetRequestMode.ImmediateLoad);
			}
		}
		catch (AssetLoadException)
		{
		}

		return asset;
	}
	public static int GetRhodiumVariantItemOre(this AvalonWorld.RhodiumVariant? rhodiumVariant)
	{
		return rhodiumVariant switch
		{
			AvalonWorld.RhodiumVariant.Osmium => ModContent.ItemType<OsmiumOre>(),
			AvalonWorld.RhodiumVariant.Rhodium => ModContent.ItemType<RhodiumOre>(),
			AvalonWorld.RhodiumVariant.Iridium => ModContent.ItemType<IridiumOre>(),
			_ => -1,
		};
	}
	/// <summary>
	/// Extension to hide something from displaying in the Bestiary.
	/// </summary>
	/// <param name="itemDropRule"></param>
	/// <returns></returns>
	public static LeadingConditionRule HideFromBestiary(this IItemDropRule itemDropRule)
	{
		var conditionRule = new LeadingConditionRule(new Conditions.NeverTrue());
		conditionRule.OnFailedConditions(itemDropRule, true);
		return conditionRule;
	}
	/// <summary>
	///     A helper method to check if the given Player is touching the ground.<br></br><br></br>
	///     
	///     Low precision, can return true if the player is hovering slightly above the ground.<br></br>
	///     If precision is necessary, use IsOnGroundPrecise().
	/// </summary>
	/// <param name="player">The player.</param>
	/// <returns>True if the player is touching the ground, false otherwise.</returns>
	public static bool IsOnGround(this Player player)
	{
		for (int i = 0; i < 3; i++)
		{
			var tileX = Main.tile[(int)((player.position.X + (player.width * i / 2f)) / 16f), (int)(player.position.Y / 16f) + 1 + (int)(2 * player.gravDir)];

			if (tileX.HasTile && (Main.tileSolid[tileX.TileType] || Main.tileSolidTop[tileX.TileType]) && player.velocity.Y == 0f)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	///     A helper method to check if the given Player is touching the ground.<br></br><br></br>
	///     
	///     High precision, if precision is unnecessary or undesired, use IsOnGround().
	/// </summary>
	/// <param name="player">The player.</param>
	/// <returns>True if the player is touching the ground, false otherwise.</returns>
	public static bool IsOnGroundPrecise(this Player player)
	{
		for (int i = 0; i < 3; i++)
		{
			var tileX = Main.tile[(int)((player.position.X + (player.width * i / 2f)) / 16f), (int)((player.position.Y + (player.gravDir == 1 ? player.height + 1 : -1)) / 16f)];

			if (tileX.HasTile && (Main.tileSolid[tileX.TileType] || Main.tileSolidTop[tileX.TileType]) && player.velocity.Y == 0f)
			{
				return true;
			}
		}
		return false;
	}

	public static bool PlayerDoublePressedSetBonusActivateKey(this Player player)
	{
		return (player.doubleTapCardinalTimer[Main.ReversedUpDownArmorSetBonuses ? 1 : 0] < 15 && ((player.releaseUp && Main.ReversedUpDownArmorSetBonuses && player.controlUp) || (player.releaseDown && !Main.ReversedUpDownArmorSetBonuses && player.controlDown)));
	}

	public static void GetPointOnSwungItemPath(float spriteWidth, float spriteHeight, float normalizedPointOnPath, float itemScale, out Vector2 location, out Vector2 outwardDirection, Player player)
	{
		float num = (float)Math.Sqrt(spriteWidth * spriteWidth + spriteHeight * spriteHeight);
		float num2 = (float)(player.direction == 1).ToInt() * ((float)Math.PI / 2f);
		if (player.gravDir == -1f)
		{
			num2 += (float)Math.PI / 2f * (float)player.direction;
		}
		outwardDirection = player.itemRotation.ToRotationVector2().RotatedBy(3.926991f + num2);
		location = player.RotatedRelativePoint(player.itemLocation + outwardDirection * num * normalizedPointOnPath * itemScale);
	}
	public static int FindClosestNPC(this Entity entity, float maxDistance, Func<NPC, bool> invalidNPCPredicate)
	{
		int closest = -1;
		float lastDistance = maxDistance;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			NPC npc = Main.npc[i];
			if (invalidNPCPredicate.Invoke(npc))
			{
				continue;
			}

			if (Vector2.Distance(entity.Center, npc.Center) < lastDistance)
			{
				lastDistance = Vector2.Distance(entity.Center, npc.Center);
				closest = i;
			}
		}

		return closest;
	}

	public static void DrawGas(string Texture, Color color, Projectile projectile, float spread, int iterations)
	{
		Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
		int frameHeight = texture.Height / Main.projFrames[projectile.type];
		Rectangle frame = new Rectangle(0, frameHeight * projectile.frame, texture.Width, frameHeight);
		Vector2 drawPos = projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(texture, drawPos, frame, color * projectile.Opacity, projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);

		for (int i = 0; i < iterations; i++)
		{
			Main.EntitySpriteDraw(texture, drawPos + new Vector2(0, projectile.width / spread * ((float)projectile.alpha) / 128).RotatedBy(i * (MathHelper.TwoPi) / iterations), frame, color * projectile.Opacity * 0.4f, projectile.rotation + ((float)projectile.alpha / 128) * (i / 128), new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.FlipVertically, 0);
		}
	}
	public static void Load<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TagCompound tag)
		where TKey : notnull
	{
		if (tag.ContainsKey("keys") && tag.ContainsKey("values"))
		{
			TKey[] keys = tag.Get<TKey[]>("keys");
			TValue[] values = tag.Get<TValue[]>("values");

			for (int i = 0; i < keys.Length; i++)
			{
				dictionary[keys[i]] = values[i];
			}
		}
	}

	public static Color MultiplyRGBByFloat(this Color color, float multiplier)
	{
		color.R = (byte)(color.R * multiplier);
		color.G = (byte)(color.G * multiplier);
		color.B = (byte)(color.B * multiplier);
		return color;
	}
	public static Color CycleThroughColors(Color[] Colors, int Rate, float Offset = 0)
	{
		float fade = ((Main.GameUpdateCount + Offset) % Rate) / (float)Rate;
		int index = (int)(((Main.GameUpdateCount + Offset) / (float)Rate) % Colors.Length);
		int nextIndex = (index + 1) % Colors.Length;

		return Color.Lerp(Colors[index], Colors[nextIndex], fade);
	}
	public static TagCompound Save<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
	   where TKey : notnull
	{
		TKey[] keys = dictionary.Keys.ToArray();
		TValue[] values = dictionary.Values.ToArray();
		var tag = new TagCompound();
		tag.Set("keys", keys);
		tag.Set("values", values);
		return tag;
	}
	public static bool IsTrueMeleeProjectile(this Projectile projectile)
	{
		return projectile.DamageType == DamageClass.Melee && (projectile.aiStyle == ProjAIStyleID.Spear || projectile.aiStyle == ProjAIStyleID.ShortSword || projectile.aiStyle == ProjAIStyleID.NightsEdge || projectile.type == ProjectileID.Terragrim || projectile.type == ProjectileID.Arkhalis);
	}
	public static Rectangle GetDims(this ModTexturedType texturedType) =>
		Main.netMode == NetmodeID.Server ? Rectangle.Empty : texturedType.GetTexture().Frame();

	public static Rectangle GetDims(this ModItem modItem) =>
		Main.netMode == NetmodeID.Server ? Rectangle.Empty : modItem.GetTexture().Frame();

	public static Rectangle GetDims(this ModProjectile modProjectile) =>
		Main.netMode == NetmodeID.Server ? Rectangle.Empty : modProjectile.GetTexture().Frame();

	public static Asset<Texture2D> GetTexture(this ModTexturedType texturedType) =>
		ModContent.Request<Texture2D>(texturedType.Texture);

	public static Asset<Texture2D> GetTexture(this ModItem modItem) =>
		ModContent.Request<Texture2D>(modItem.Texture);

	public static Asset<Texture2D> GetTexture(this ModProjectile modProjectile) =>
		ModContent.Request<Texture2D>(modProjectile.Texture);

	#region Item DefaultToX() methods
	/// <summary>
	/// This method sets a variety of Item values common to arrow items.<br/>
	/// Specifically: <code>
	/// width = 10;
	/// height = 28;
	/// ammo = <see cref="AmmoID.Arrow"/>;
	/// DamageType = <see cref="DamageClass.Ranged"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// damage = <paramref name="damage"/>;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// consumable = <paramref name="consumable"/>;
	/// </code>
	/// </summary>
	public static void DefaultToArrow(this Item item, int damage, int projectile, float shootSpeed, float knockback, bool consumable = true)
	{
		item.width = 10;
		item.height = 28;
		item.ammo = AmmoID.Arrow;
		item.DamageType = DamageClass.Ranged;
		item.maxStack = Item.CommonMaxStack;
		item.damage = damage;
		item.shoot = projectile;
		item.shootSpeed = shootSpeed;
		item.knockBack = knockback;
		item.consumable = consumable;
	}
	/// <summary>
	/// This method sets a variety of Item values common to bullet items.<br/>
	/// Specifically: <code>
	/// width = 8;
	/// height = 8;
	/// ammo = <see cref="AmmoID.Bullet"/>;
	/// DamageType = <see cref="DamageClass.Ranged"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// damage = <paramref name="damage"/>;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// consumable = <paramref name="consumable"/>;
	/// </code>
	/// </summary>
	public static void DefaultToBullet(this Item item, int damage, int projectile, float shootSpeed, float knockback, bool consumable = true)
	{
		item.width = 8;
		item.height = 8;
		item.ammo = AmmoID.Bullet;
		item.DamageType = DamageClass.Ranged;
		item.maxStack = Item.CommonMaxStack;
		item.damage = damage;
		item.shoot = projectile;
		item.shootSpeed = shootSpeed;
		item.knockBack = knockback;
		item.consumable = consumable;
	}
	/// <summary>
	/// This method sets a variety of Item values common to rhotuka spinner items.<br/>
	/// Specifically: <code>
	/// width = 26;
	/// height = 26;
	/// ammo = <see cref="ModContent.ItemType"/>; (Where <typeparamref name="T"/> is <see cref="RhotukaSpinner"/>)
	/// DamageType = <see cref="DamageClass.Ranged"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// damage = <paramref name="damage"/>;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// consumable = <paramref name="consumable"/>;
	/// </code>
	/// </summary>
	public static void DefaultToSpinner(this Item item, int damage, int projectile, float shootSpeed, float knockback, bool consumable = true)
	{
		item.width = 26;
		item.height = 26;
		item.ammo = ModContent.ItemType<RhotukaSpinner>();
		item.DamageType = DamageClass.Ranged;
		item.maxStack = Item.CommonMaxStack;
		item.damage = damage;
		item.shoot = projectile;
		item.shootSpeed = shootSpeed;
		item.knockBack = knockback;
		item.consumable = consumable;
	}
	/// <summary>
	/// This method sets a variety of Item values common to rhotuka spinner items.<br/>
	/// Specifically: <code>
	/// width = 10;
	/// height = 12;
	/// ammo = <see cref="ModContent.ItemType"/>; (Where <typeparamref name="T"/> is <see cref="Canister"/>)
	/// DamageType = <see cref="DamageClass.Ranged"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// damage = <paramref name="damage"/>;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = 0f;
	/// knockBack = 0f;
	/// consumable = <paramref name="consumable"/>;
	/// </code>
	/// </summary>
	public static void DefaultToCanister(this Item item, int damage, int projectile, bool consumable = true)
	{
		item.width = 10;
		item.height = 12;
		item.ammo = ModContent.ItemType<Canister>();
		item.DamageType = DamageClass.Ranged;
		item.maxStack = Item.CommonMaxStack;
		item.damage = damage;
		item.shoot = projectile;
		item.shootSpeed = 0f;
		item.knockBack = 0f;
		item.consumable = consumable;
	}
	/// <summary>
	/// This method sets a variety of Item values common to armor items.<br/>
	/// Specifically: <code>
	/// width = 16;
	/// height = 16;
	/// defense = <paramref name="defense"/>;
	/// </code>
	/// </summary>
	public static void DefaultToArmor(this Item item, int defense)
	{
		item.width = 16;
		item.height = 16;
		item.defense = defense;
	}
	/// <summary>
	/// This method sets a variety of Item values common to monster banner items.<br/>
	/// Specifically:<code>
	/// createTile = <see cref="ModContent.TileType"/>; (Where <typeparamref name="T"/> is <see cref="MonsterBanner"/>)
	/// placeStyle = <paramref name="tileStyleToPlace"/>;
	/// width = 10;
	/// height = 24;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// useAnimation = 15;
	/// useTime = 10;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// useTurn = true;
	/// autoReuse = true;
	/// consumable = true;
	/// rare = <see cref="ItemRarityID.Blue"/>;
	/// value = 10 silver;
	/// </code>
	/// </summary>
	public static void DefaultToMonsterBanner(this Item item, int tileStyleToPlace)
	{
		item.DefaultToPlaceableTile(ModContent.TileType<MonsterBanner>(), tileStyleToPlace);
		item.width = 10;
		item.height = 24;
		item.rare = ItemRarityID.Blue;
		item.value = Item.buyPrice(silver: 10);
	}
	public enum TreasureBagRarities : int
	{
		EyeTier = ItemRarityID.Blue,
		EvilTier = ItemRarityID.Green,
		SkeleTier = ItemRarityID.Orange,
		WofTier = ItemRarityID.LightRed,
		MechTier = ItemRarityID.Pink,
		PlantTier = ItemRarityID.LightPurple,
		GolemTier = ItemRarityID.Lime,
		LunarTier = ItemRarityID.Yellow,
		// below are maybe temp values, change them if you think they should use custom rarities
		WosTier = ItemRarityID.Cyan,
		earlySHMTier = ItemRarityID.Red,
		ArmaTier = ItemRarityID.Purple
	}
	/// <summary>
	/// This method sets a variety of Item values common to treasure bag items.<br/>
	/// Specifically:<code>
	/// width = 24;
	/// height = 24;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// consumable = true;
	/// rare = (<see cref="int"/>)<paramref name="rarity"/>;
	/// expert = true;
	/// </code>
	/// </summary>
	public static void DefaultToTreasureBag(this Item item, TreasureBagRarities rarity)
	{
		item.width = 24;
		item.height = 24;
		item.maxStack = Item.CommonMaxStack;
		item.consumable = true;
		item.rare = (int)rarity;
		item.expert = true;
	}
	/// <summary>
	/// This method sets a variety of Item values common to summoning items.<br/>
	/// Specifically:<code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// consumable = <paramref name="consumable"/>;
	/// useAnimation = <paramref name="useAnim"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useTime = <paramref name="useTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.HoldUp"/>;
	/// </code>
	/// </summary>
	public static void DefaultToSpawner(this Item item, bool consumable = true, int useAnim = 45, int useTime = 45, bool useTurn = false, int width = 22, int height = 14)
	{
		item.DefaultToConsumable(consumable, useAnim, useTime, useTurn, width, height);
	}
	/// <summary>
	/// This method sets a variety of Item values common to consumable items.<br/>
	/// Specifically:<code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// consumable = <paramref name="consumable"/>;
	/// useAnimation = <paramref name="useAnim"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useTime = <paramref name="useTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.HoldUp"/>;
	/// </code>
	/// </summary>
	public static void DefaultToConsumable(this Item item, bool consumable = true, int useAnim = 30, int useTime = 30, bool useTurn = false, int width = 18, int height = 18)
	{
		item.width = width;
		item.height = height;
		item.maxStack = Item.CommonMaxStack;
		item.consumable = consumable;
		item.useAnimation = useAnim;
		item.useTurn = useTurn;
		item.useTime = useTime;
		item.useStyle = ItemUseStyleID.HoldUp;
	}
	/// <summary>
	/// This method sets a variety of Item values common to useable items.<br/>
	/// Specifically:<code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// autoReuse = true;
	/// useTurn = true;
	/// consumable = <paramref name="consumable"/>;
	/// useAnimation = <paramref name="useAnim"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useTime = <paramref name="useTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// </code>
	/// </summary>
	public static void DefaultToUseable(this Item item, bool consumable = true, int useAnim = 15, int useTime = 15, bool useTurn = false, int width = 20, int height = 20)
	{
		item.width = width;
		item.height = height;
		item.maxStack = Item.CommonMaxStack;
		item.autoReuse = true;
		item.useTurn = true;
		item.consumable = consumable;
		item.useAnimation = useAnim;
		item.useTurn = useTurn;
		item.useTime = useTime;
		item.useStyle = ItemUseStyleID.Swing;
	}
	/// <summary>
	/// This method sets a variety of Item values common to fish items.<br/>
	/// Specifically:<code>
	/// width = 26;
	/// height = 26;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// value = 25 silver;
	/// </code>
	/// </summary>
	public static void DefaultToFish(this Item item)
	{
		item.width = 26;
		item.height = 26;
		item.maxStack = Item.CommonMaxStack;
		item.value = Item.sellPrice(0, 0, 5);
	}
	/// <summary>
	/// This method sets a variety of Item values common to bar items.<br/>
	/// Specifically:<code>
	/// createTile = <see cref="ModContent.TileType"/>; (Where <typeparamref name="T"/> is <see cref="PlacedBars"/>)
	/// placeStyle = <paramref name="tileStyleToPlace"/>;
	/// width = 20;
	/// height = 20;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// useAnimation = 15;
	/// useTime = 10;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// useTurn = true;
	/// autoReuse = true;
	/// consumable = true;
	/// </code>
	/// </summary>
	public static void DefaultToBar(this Item item, int tileStyleToPlace)
	{
		item.DefaultToPlaceableTile(ModContent.TileType<PlacedBars>(), tileStyleToPlace);
		item.width = 20;
		item.height = 20;
	}
	/// <summary>
	/// This method sets a variety of Item values common to herb items.<br/>
	/// Specifically:<code>
	/// width = 12;
	/// height = 14;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// value = 20 copper;
	/// </code>
	/// </summary>
	public static void DefaultToHerb(this Item item)
	{
		item.width = 12;
		item.height = 14;
		item.maxStack = Item.CommonMaxStack;
		item.value = Item.sellPrice(0, 0, 0, 20);
	}
	/// <summary>
	/// This method sets a variety of Item values common to shard items.<br/>
	/// Specifically:<code>
	/// createTile = <see cref="ModContent.TileType"/>; (Where <typeparamref name="T"/> is <paramref name="tier2"/> ? <see cref="ShardsTier2"/> : <see cref="Shards"/>)
	/// placeStyle = <paramref name="tileStyleToPlace"/>;
	/// width = 20;
	/// height = 20;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// useAnimation = 15;
	/// useTime = 10;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// useTurn = true;
	/// autoReuse = true;
	/// consumable = true;
	/// rare = <paramref name="tier2"/> ? <see cref="ItemRarityID.Lime"/> : <see cref="ItemRarityID.Green"/>;
	/// value = <paramref name="tier2"/> ? 60 silver : 30 silver;
	/// </code>
	/// </summary>
	public static void DefaultToShard(this Item item, int tileStyleToPlace, bool tier2 = false)
	{
		item.DefaultToPlaceableTile(tier2 ? ModContent.TileType<ShardsTier2>() : ModContent.TileType<Shards>(), tileStyleToPlace);
		item.width = 20;
		item.height = 20;
		item.rare = tier2 ? ItemRarityID.Lime : ItemRarityID.Green;
		item.value = Item.sellPrice(0, 0, tier2 ? 12 : 6);
	}
	/// <summary>
	/// This method sets a variety of Item values common to painting items.<br/>
	/// Specifically:<code>
	/// createTile = <paramref name="tileIDToPlace"/>;
	/// placeStyle = <paramref name="tileStyleToPlace"/>;
	/// width = 30;
	/// height = 30;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// useAnimation = 15;
	/// useTime = 10;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// useTurn = true;
	/// autoReuse = true;
	/// consumable = true;
	/// value = 50 silver;
	/// </code>
	/// </summary>
	public static void DefaultToPainting(this Item item, int tileIDToPlace, int tileStyleToPlace)
	{
		item.DefaultToPlaceableTile(tileIDToPlace, tileStyleToPlace);
		item.width = 30;
		item.height = 30;
		item.value = Item.sellPrice(0, 0, 10);
	}
	/// <summary>
	/// This method sets a variety of Item values common to tome material items.<br/>
	/// Specifically:<code>
	/// width = 16;
	/// height = 20;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// value = 10 silver;<br/>
	/// <see cref="Item.GetGlobalItem"/>.TomeMaterial = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
	/// </code>
	/// </summary>
	public static void DefaultToTomeMaterial(this Item item)
	{
		item.width = 16;
		item.height = 20;
		item.maxStack = Item.CommonMaxStack;
		item.value = Item.sellPrice(0, 0, 2);
		item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
	}
	/// <summary>
	/// This method sets a variety of Item values common to stamina scroll items.<br/>
	/// Specifically:<code>
	/// width = 20;
	/// height = 20;
	/// accessory = true;
	/// rare = <see cref="ItemRarityID.Green"/>;
	/// useStyle = <see cref="ItemUseStyleID.HoldUp"/>;
	/// UseSound = new <see cref="SoundStyle"/>("Avalon/Sounds/Item/Scroll");<br></br>
	/// <see cref="Item.GetGlobalItem"/>.StaminaScroll = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
	/// </code>
	/// </summary>
	public static void DefaultToStaminaScroll(this Item item)
	{
		item.CloneDefaults(ModContent.ItemType<BlankScroll>());
		item.maxStack = 1;
		item.accessory = true;
		item.rare = ItemRarityID.Green;
		item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll = true;
	}
	/// <summary>
	/// This method sets a variety of Item values common to miscellaneous items.<br/>
	/// Specifically:<code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// </code>
	/// </summary>
	public static void DefaultToMisc(this Item item, int width = 16, int height = 16)
	{
		item.width = width;
		item.height = height;
		item.maxStack = Item.CommonMaxStack;
	}
	/// <summary>
	/// This method sets a variety of Item values common to genie items. (Genies are planned to be pet-like, hence all the redundant values being set)<br/>
	/// Specifically:<code>
	/// width = 16;
	/// height = 30;
	/// accessory = true;
	/// damage = 0
	/// noMelee = true;
	/// useAnimation = 20;
	/// useTime = 20;
	/// buffType = 0;
	/// shoot = 0;
	/// rare = <see cref="ItemRarityID.Green"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item2"/>;
	/// value = 20 gold;<br/>
	/// <see cref="Item.GetGlobalItem"/>.Genie = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
	/// </code>
	/// </summary>
	public static void DefaultToGenie(this Item item)
	{
		item.DefaultToVanitypet(0, 0);
		item.accessory = true;
		item.rare = ItemRarityID.Green;
		item.value = Item.buyPrice(0, 20);
		item.GetGlobalItem<AvalonGlobalItemInstance>().Genie = true;
	}
	public enum PotionCorkType : int
	{
		None = ItemRarityID.White,
		Default = ItemRarityID.Blue,
		Obsidian = ItemRarityID.Green,
		Elixir = ItemRarityID.Lime
	}
	/// <summary>
	/// This method sets a variety of Item values common to buff potion items.<br/>
	/// Specifically: <code>
	/// width = 14;
	/// height = 24;
	/// UseSound = <see cref="SoundID.Item3"/>;
	/// useStyle = <see cref="ItemUseStyleID.DrinkLiquid"/>;
	/// useTurn = true;
	/// useAnimation = 17;
	/// useTime = 17;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// consumable = true;
	/// buffType = <paramref name="buffType"/>;
	/// buffTime = <paramref name="buffDuration"/>;
	/// rare = (<see cref="int"/>)<paramref name="cork"/>;
	/// if (<paramref name="cork"/> == <see cref="PotionCorkType.None"/>) value = 0;
	/// if (<paramref name="cork"/> == <see cref="PotionCorkType.Default"/>) value = 10 silver;
	/// if (<paramref name="cork"/> == <see cref="PotionCorkType.Obsidian"/>) value = 20 silver;
	/// if (<paramref name="cork"/> == <see cref="PotionCorkType.Elixir"/>) value = 50 silver;
	/// </code>
	/// </summary>
	public static void DefaultToBuffPotion(this Item item, int buffType, int buffDuration, PotionCorkType cork = PotionCorkType.Default)
	{
		item.width = 14;
		item.height = 24;
		item.UseSound = SoundID.Item3;
		item.useStyle = ItemUseStyleID.DrinkLiquid;
		item.useTurn = true;
		item.useAnimation = 17;
		item.useTime = 17;
		item.maxStack = Item.CommonMaxStack;
		item.consumable = true;
		item.buffType = buffType;
		item.buffTime = buffDuration;
		item.rare = (int)cork;
		item.value = cork switch
		{
			PotionCorkType.None => 0,
			PotionCorkType.Default => Item.sellPrice(silver: 2),
			PotionCorkType.Obsidian => Item.sellPrice(silver: 4),
			PotionCorkType.Elixir => Item.sellPrice(silver: 10),
			_ => 0,
		};
	}
	/// <summary>
	/// This method sets a variety of Item values common to stamina potion items.<br/>
	/// Specifically: <code>
	/// width = 14;
	/// height = 24;
	/// UseSound = <see cref="SoundID.Item3"/>;
	/// useStyle = <see cref="ItemUseStyleID.DrinkLiquid"/>;
	/// useTurn = true;
	/// useAnimation = 17;
	/// useTime = 17;
	/// maxStack = <see cref="Item.CommonMaxStack"/>;
	/// consumable = true;<br></br>
	/// <see cref="Item.GetGlobalItem"/>.HealStamina = <paramref name="staminaAmount"/>; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
	/// </code>
	/// </summary>
	public static void DefaultToStaminaPotion(this Item item, int staminaAmount)
	{
		item.width = 14;
		item.height = 24;
		item.UseSound = SoundID.Item3;
		item.useStyle = ItemUseStyleID.DrinkLiquid;
		item.useTurn = true;
		item.useAnimation = 17;
		item.useTime = 17;
		item.maxStack = Item.CommonMaxStack;
		item.consumable = true;
		item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = staminaAmount;
	}
	/// <summary>
	/// This method sets a variety of Item values common to tome items.<br/>
	/// Specifically: <code>
	/// width = 24;
	/// height = 24;
	/// rare = Math.Min(<paramref name="grade"/> + <paramref name="rarityBonus"/>, <see cref="ItemRarityID.Purple"/>);<br></br>
	/// <see cref="Item.GetGlobalItem"/>.Tome = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)<br></br>
	/// <see cref="Item.GetGlobalItem"/>.TomeGrade = <paramref name="grade"/>; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
	/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> == 1) value = 1 gold 50 silver;
	/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> == 2) value = 3 gold;
	/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> &gt;= 3 and &lt;= 7) value = 10 gold;
	/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> &gt;= 8) value = 15 gold;
	/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> &gt;= 9) value = 25 gold;
	/// </code>
	/// </summary>
	public static void DefaultToTome(this Item item, int grade, int rarityBonus = 0)
	{
		item.width = 24;
		item.height = 24;
		item.rare = Math.Min(grade + rarityBonus, ItemRarityID.Purple);
		item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = grade;
		item.value = item.rare switch
		{
			1 => Item.sellPrice(silver: 30),
			2 => Item.sellPrice(silver: 60),
			>= 3 and <= 7 => Item.sellPrice(gold: 2),
			8 => Item.sellPrice(gold: 3),
			>= 9 => Item.sellPrice(gold: 5),
			_ => 0,
		};
	}
	/// <summary>
	/// This method sets a variety of Item values common to axe items.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// axe = <paramref name="axePowerTimes5"/> / 5;
	/// autoReuse = true;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// scale = <paramref name="scale"/>;
	/// tileBoost = <paramref name="tileRangeModifier"/>;
	/// useTime = <paramref name="miningSpeed"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToAxe(this Item item, int axePowerTimes5, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.axe = axePowerTimes5 / 5;
		item.autoReuse = true;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.scale = scale;
		item.tileBoost = tileRangeModifier;
		item.useTime = miningSpeed;
		item.useAnimation = useAnimation;
		item.useTurn = useTurn;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to pickaxe items.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// pick = <paramref name="pickaxePower"/>;
	/// autoReuse = true;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// scale = <paramref name="scale"/>;
	/// tileBoost = <paramref name="tileRangeModifier"/>;
	/// useTime = <paramref name="miningSpeed"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToPickaxe(this Item item, int pickaxePower, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.pick = pickaxePower;
		item.autoReuse = true;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.scale = scale;
		item.tileBoost = tileRangeModifier;
		item.useTime = miningSpeed;
		item.useAnimation = useAnimation;
		item.useTurn = useTurn;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to hammer items.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// hammer = <paramref name="hammerPower"/>;
	/// autoReuse = true;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// scale = <paramref name="scale"/>;
	/// tileBoost = <paramref name="tileRangeModifier"/>;
	/// useTime = <paramref name="miningSpeed"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToHammer(this Item item, int hammerPower, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.hammer = hammerPower;
		item.autoReuse = true;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.scale = scale;
		item.tileBoost = tileRangeModifier;
		item.useTime = miningSpeed;
		item.useAnimation = useAnimation;
		item.useTurn = useTurn;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to hammer items.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// hammer = <paramref name="hammerPower"/>;
	/// axe = <paramref name="hammerPower"/> / 5;
	/// autoReuse = true;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// scale = <paramref name="scale"/>;
	/// tileBoost = <paramref name="tileRangeModifier"/>;
	/// useTime = <paramref name="miningSpeed"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToHamaxe(this Item item, int hammerPower, int axePowerTimes5, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.hammer = hammerPower;
		item.axe = axePowerTimes5;
		item.autoReuse = true;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.scale = scale;
		item.tileBoost = tileRangeModifier;
		item.useTime = miningSpeed;
		item.useAnimation = useAnimation;
		item.useTurn = useTurn;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to pickaxe axe items.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// pick = <paramref name="pickaxePower"/>;
	/// axe = <paramref name="axePowerTimes5"/> / 5;
	/// autoReuse = true;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// scale = <paramref name="scale"/>;
	/// tileBoost = <paramref name="tileRangeModifier"/>;
	/// useTime = <paramref name="miningSpeed"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToPickaxeAxe(this Item item, int pickaxePower, int axePowerTimes5, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.pick = pickaxePower;
		item.axe = axePowerTimes5 / 5;
		item.autoReuse = true;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.scale = scale;
		item.tileBoost = tileRangeModifier;
		item.useTime = miningSpeed;
		item.useAnimation = useAnimation;
		item.useTurn = useTurn;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to chainsaw items.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// axe = <paramref name="axePowerTimes5"/> / 5;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// tileBoost = <paramref name="tileRangeModifier"/>; (Defaults to -1 if <see cref="ItemID.Sets.IsChainsaw"></see>[Item.type] == true)
	/// useTime = <paramref name="miningSpeed"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsChainsaw"></see>[Item.type] == true)
	/// useAnimation = <paramref name="useAnimation"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsChainsaw"></see>[Item.type] == true)
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// UseSound = <see cref="SoundID.Item23"/>;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// channel = true;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shootSpeed = <paramref name="projectile"/>;
	/// </code>
	/// </summary>
	public static void DefaultToChainsaw(this Item item, int projectile, int axePowerTimes5, int damage, float knockback, int miningSpeed, int tileRangeModifier = 0, int useAnimation = 15, float shootSpeed = 40f, int width = 20, int height = 12)
	{
		item.width = width;
		item.height = height;
		item.axe = axePowerTimes5 / 5;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.tileBoost = tileRangeModifier;
		item.useTime = miningSpeed;
		item.useAnimation = useAnimation;
		item.useStyle = ItemUseStyleID.Shoot;
		item.UseSound = SoundID.Item23;
		item.noMelee = true;
		item.noUseGraphic = true;
		item.channel = true;
		item.shootSpeed = shootSpeed;
		item.shoot = projectile;
	}
	/// <summary>
	/// This method sets a variety of Item values common to drill items.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// pick = <paramref name="pickPower"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// tileBoost = <paramref name="tileRangeModifier"/>; (Defaults to -1 if <see cref="ItemID.Sets.IsDrill"></see>[Item.type] == true)
	/// useTime = <paramref name="miningSpeed"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsDrill"></see>[Item.type] == true)
	/// useAnimation = <paramref name="useAnimation"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsDrill"></see>[Item.type] == true)
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// UseSound = <see cref="SoundID.Item23"/>;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// channel = true;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shootSpeed = <paramref name="projectile"/>;
	/// </code>
	/// </summary>
	public static void DefaultToDrill(this Item item, int projectile, int pickPower, int damage, int miningSpeed, int tileRangeModifier = 0, int useAnimation = 15, float shootSpeed = 32f, float knockback = 0.5f, int width = 20, int height = 12)
	{
		item.width = width;
		item.height = height;
		item.pick = pickPower;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.tileBoost = tileRangeModifier;
		item.useTime = miningSpeed;
		item.useAnimation = useAnimation;
		item.useStyle = ItemUseStyleID.Shoot;
		item.UseSound = SoundID.Item23;
		item.noMelee = true;
		item.noUseGraphic = true;
		item.channel = true;
		item.shootSpeed = shootSpeed;
		item.shoot = projectile;
	}
	/// <summary>
	/// This method sets a variety of Item values common to grappling hook items.<br/>
	/// Specifically: <code>
	/// width = 18;
	/// height = 28;
	/// damage = 0;
	/// knockBack = 7f;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shootSpeed = <paramref name="projectile"/>;
	/// useTime = 20;
	/// useAnimation = 20;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToGrapplingHook(this Item item, int projectile, float shootSpeed)
	{
		item.width = 18;
		item.height = 28;
		item.damage = 0;
		item.knockBack = 7f;
		item.noMelee = true;
		item.noUseGraphic = true;
		item.shootSpeed = shootSpeed;
		item.shoot = projectile;
		// don't listen to example mod's lies and slander, these next three values don't actually need to be set to anything in particular (but these are the vanilla values for them)
		item.useTime = 20;
		item.useAnimation = 20;
		item.useStyle = ItemUseStyleID.Shoot;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to fishing pole items.<br/>
	/// Specifically: <code>
	/// width = 24;
	/// height = 28;
	/// fishingPole = <paramref name="fishingPower"/>
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shoot = <paramref name="projectile"/>;
	/// useTime = 8;
	/// useAnimation = 8;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToFishingPole(this Item item, int projectile, int fishingPower, float shootSpeed)
	{
		item.width = 24;
		item.height = 28;
		item.fishingPole = fishingPower;
		item.shootSpeed = shootSpeed;
		item.shoot = projectile;
		item.useTime = 8;
		item.useAnimation = 8;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to ranged weapons.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// autoReuse = <paramref name="autoReuse"/>;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Ranged"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// noMelee = true;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shoot = <paramref name="baseProjType"/>;
	/// reuseDelay = <paramref name="reuseDelay"/>;
	/// useTime = <paramref name="useTime"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// useAmmo = <paramref name="ammoID"/>;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// </code>
	/// </summary>
	public static void DefaultToRangedWeapon(this Item item, int width, int height, int baseProjType, int ammoID, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int reuseDelay = 0, int crit = 0)
	{
		item.DefaultToRangedWeapon(baseProjType, ammoID, useTime, shootSpeed, autoReuse);
		item.width = width;
		item.height = height;
		item.crit = crit;
		item.damage = damage;
		item.knockBack = knockback;
		item.reuseDelay = reuseDelay;
		item.useAnimation = useAnimation;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToRangedWeapon"/>
	/// Additional values specific to guns:
	/// <code>
	/// shoot = <see cref="ProjectileID.Bullet"/>;
	/// useAmmo = <see cref="AmmoID.Bullet"/>;
	/// UseSound = <see cref="SoundID.Item11"/>;
	/// </code>
	/// </summary>
	public static void DefaultToGun(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int reuseDelay = 0, int crit = 0, int width = 50, int height = 14)
	{
		item.DefaultToRangedWeapon(width, height, ProjectileID.Bullet, AmmoID.Bullet, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
		item.UseSound = SoundID.Item11;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToRangedWeapon"/>
	/// Additional values specific to repeaters:
	/// <code>
	/// shoot = <see cref="ProjectileID.WoodenArrowFriendly"/>;
	/// useAmmo = <see cref="AmmoID.Arrow"/>;
	/// UseSound = <see cref="SoundID.Item5"/>;
	/// </code>
	/// </summary>
	public static void DefaultToRepeater(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, int reuseDelay = 0, int crit = 0, int width = 50, int height = 18)
	{
		item.DefaultToRangedWeapon(width, height, ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
		item.UseSound = SoundID.Item5;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToRangedWeapon"/>
	/// Additional values specific to launchers:
	/// <code>
	/// shoot = <see cref="ProjectileID.RocketI"/>;
	/// useAmmo = <see cref="AmmoID.Rocket"/>;
	/// UseSound = <see cref="SoundID.Item11"/>;
	/// </code>
	/// </summary>
	public static void DefaultToLauncher(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, int reuseDelay = 0, int crit = 0, int width = 50, int height = 20)
	{
		item.DefaultToRangedWeapon(width, height, ProjectileID.RocketI, AmmoID.Rocket, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
		item.UseSound = SoundID.Item11;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToRangedWeapon"/>
	/// Additional values specific to blowpipes:
	/// <code>
	/// shoot = <see cref="ProjectileID.PurificationPowder"/>; (vanilla sets it to this for blowpipes, functionally should be no difference to if it were set to <see cref="ProjectileID.Seed"/>)
	/// useAmmo = <see cref="AmmoID.Dart"/>;
	/// UseSound = <see cref="SoundID.Item63"/>; (note that all vanilla dart weapons use a unique sound; this is just a placeholder)
	/// </code>
	/// </summary>
	public static void DefaultToBlowpipe(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, int reuseDelay = 0, int crit = 0, int width = 38, int height = 6)
	{
		item.DefaultToRangedWeapon(width, height, ProjectileID.PurificationPowder, AmmoID.Dart, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
		item.UseSound = SoundID.Item63;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToRangedWeapon"/>
	/// Additional values specific to bows:
	/// <code>
	/// shoot = <see cref="ProjectileID.WoodenArrowFriendly"/>;
	/// useAmmo = <see cref="AmmoID.Arrow"/>;
	/// UseSound = <see cref="SoundID.Item5"/>;
	/// </code>
	/// </summary>
	public static void DefaultToBow(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int reuseDelay = 0, int crit = 0, int width = 14, int height = 30)
	{
		item.DefaultToRangedWeapon(width, height, ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
		item.UseSound = SoundID.Item5;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToRangedWeapon"/>
	/// Additional values specific to longbows:
	/// <code>
	/// channel = true;
	/// noUseGraphic = true;
	/// shoot = <see cref="ProjectileID.WoodenArrowFriendly"/>;
	/// useAmmo = <see cref="AmmoID.Arrow"/>;
	/// </code>
	/// </summary>
	public static void DefaultToLongbow(this Item item, int damage, float knockback, float shootSpeed, int singleUseTime, int crit = 0, int width = 16, int height = 50)
	{
		item.DefaultToRangedWeapon(width, height, ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, damage, knockback, shootSpeed, singleUseTime, singleUseTime, false, 0, crit);
		item.channel = true;
		item.noUseGraphic = true;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToRangedWeapon"/>
	/// Additional values specific to flamethrowers:
	/// <code>
	/// consumeAmmoOnFirstShotOnly = <paramref name="consumeAmmoOnFirstShotOnly"/>;
	/// shoot = <paramref name="projectile"/>;
	/// useAmmo = <see cref="AmmoID.Gel"/>;
	/// UseSound = <see cref="SoundID.Item34"/>;
	/// </code>
	/// </summary>
	/// <param name="consumeAmmoOnFirstShotOnly">Set this to false if you have custom logic inside <see cref="ModItem.CanConsumeAmmo"/></param>
	public static void DefaultToFlamethrower(this Item item, int projectile, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool consumeAmmoOnFirstShotOnly = true, int crit = 0, int width = 50, int height = 18)
	{
		item.DefaultToRangedWeapon(width, height, projectile, AmmoID.Gel, damage, knockback, shootSpeed, useTime, useAnimation, true, 0, crit);
		item.consumeAmmoOnFirstShotOnly = consumeAmmoOnFirstShotOnly;
		item.UseSound = SoundID.Item34;
	}
	/// <summary>
	/// This method sets a variety of Item values common to thrown weapons.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// autoReuse = <paramref name="autoReuse"/>;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Ranged"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// maxStack = <paramref name="consumable"/> ? <see cref="Item.CommonMaxStack"/> : 1;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shoot = <paramref name="projectile"/>;
	/// reuseDelay = <paramref name="reuseDelay"/>;
	/// useTime = <paramref name="singleUseTime"/>;
	/// useAnimation = <paramref name="singleUseTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToThrownWeapon(this Item item, int projectile, int damage, float knockback, float shootSpeed, int singleUseTime, bool autoReuse = true, bool consumable = true, int reuseDelay = 0, int crit = 0, int width = 18, int height = 20)
	{
		item.DefaultToThrownWeapon(projectile, singleUseTime, shootSpeed, autoReuse);
		item.width = width;
		item.height = height;
		item.consumable = consumable;
		item.crit = crit;
		item.damage = damage;
		item.knockBack = knockback;
		item.maxStack = consumable ? Item.CommonMaxStack : 1;
		item.noUseGraphic = true;
		item.reuseDelay = reuseDelay;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to vanity items.<br/>
	/// Specifically: <code>
	/// width = 18;
	/// height = 18;
	/// vanity = true;
	/// </code>
	/// </summary>
	public static void DefaultToVanity(this Item item)
	{
		item.width = 18;
		item.height = 18;
		item.vanity = true;
	}
	/// <summary>
	/// This method sets a variety of Item values common to boss mask items.<br/>
	/// Specifically: <code>
	/// width = 18;
	/// height = 18;
	/// rare = <see cref="ItemRarityID.Blue"/>;
	/// value = 3 gold 75 silver;
	/// vanity = true;
	/// </code>
	/// </summary>
	public static void DefaultToBossMask(this Item item)
	{
		item.DefaultToVanity();
		item.rare = ItemRarityID.Blue;
		item.value = Item.sellPrice(silver: 75);
	}
	/// <summary>
	/// This method sets a variety of Item values common to sword weapons.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// autoReuse = <paramref name="autoReuse"/>;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// scale = <paramref name="scale"/>;
	/// useTime = <paramref name="singleUseTime"/>;
	/// useAnimation = <paramref name="singleUseTime"/>;
	/// useTurn = <paramref name="useTurn"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToSword(this Item item, int damage, float knockback, int singleUseTime, bool autoReuse = true, int crit = 0, float scale = 1f, bool useTurn = true, int width = 40, int height = 40)
	{
		item.width = width;
		item.height = height;
		item.autoReuse = autoReuse;
		item.crit = crit;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.scale = scale;
		item.useTime = singleUseTime;
		item.useAnimation = singleUseTime;
		item.useTurn = useTurn;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToSword"/>
	/// Additional values specific to beam swords:
	/// <code>
	/// noMelee = <paramref name="noMelee"/>;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shootsEveryUse = <paramref name="shootsEveryUse"/>;
	/// useTime = <paramref name="useTime"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// </code>
	/// </summary>
	public static void DefaultToProjectileSword(this Item item, int projectile, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, bool shootsEveryUse = false, bool noMelee = false, int crit = 0, float scale = 1f, bool useTurn = false, int width = 40, int height = 40)
	{
		item.DefaultToSword(damage, knockback, useAnimation, autoReuse, crit, scale, useTurn, width, height);
		item.noMelee = noMelee;
		item.shoot = projectile;
		item.shootSpeed = shootSpeed;
		item.shootsEveryUse = shootsEveryUse;
		item.useTime = useTime;
	}
	/// <summary>
	/// <inheritdoc cref="DefaultToSword"/>
	/// Additional values specific to maces:
	/// <code>
	/// noMelee = true;
	/// noUseGraphic = true;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = 6;
	/// useTurn = false;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// </code>
	/// </summary>
	public static void DefaultToMace(this Item item, int projectile, int damage, float knockback, float scale, int singleUseTime, bool autoReuse = true, int crit = 6, int width = 40, int height = 40)
	{
		item.DefaultToSword(damage, knockback, singleUseTime, autoReuse, crit, scale, false, width, height);
		item.noMelee = true;
		item.noUseGraphic = true;
		item.shoot = projectile;
		item.shootSpeed = 6f;
		item.useStyle = ItemUseStyleID.Shoot;
	}
	/// <summary>
	/// This method sets a variety of Item values common to flail weapons.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// channel = true;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// scale = <paramref name="scale"/>;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// useTime = <paramref name="singleUseTime"/>;
	/// useAnimation = <paramref name="singleUseTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// </code>
	/// </summary>
	public static void DefaultToFlail(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, int crit = 0, float scale = 1.1f, int width = 28, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.channel = true;
		item.crit = crit;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.noMelee = true;
		item.noUseGraphic = true;
		item.scale = scale;
		item.shootSpeed = shootSpeed;
		item.shoot = projectile;
		item.useTime = singleUseTime;
		item.useAnimation = singleUseTime;
		item.useStyle = ItemUseStyleID.Shoot;
	}
	/// <summary>
	/// This method sets a variety of Item values common to yoyo weapons.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// channel = true;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// useTime = 25;
	/// useAnimation = 25;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToYoyo(this Item item, int projectile, int damage, float knockback, float shootSpeed, int crit = 0, int width = 24, int height = 24)
	{
		item.width = width;
		item.height = height;
		item.channel = true;
		item.crit = crit;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.noMelee = true;
		item.noUseGraphic = true;
		item.shootSpeed = shootSpeed;
		item.shoot = projectile;
		item.useTime = 25;
		item.useAnimation = 25;
		item.useStyle = ItemUseStyleID.Shoot;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to spear weapons.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// autoReuse = <paramref name="autoReuse"/>;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// scale = <paramref name="scale"/>;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// useTime = <paramref name="singleUseTime"/>;
	/// useAnimation = <paramref name="singleUseTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToSpear(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, bool autoReuse = false, int crit = 0, float scale = 1f, int width = 40, int height = 40)
	{
		item.DefaultToSpear(projectile, shootSpeed, singleUseTime);
		item.width = width;
		item.height = height;
		item.autoReuse = autoReuse;
		item.crit = crit;
		item.damage = damage;
		item.knockBack = knockback;
		item.scale = scale;
	}
	/// <summary>
	/// <inheritdoc	cref="DefaultToSpear"/>
	/// Additional values specific to shortswords:
	/// <code>
	/// useStyle = <see cref="ItemUseStyleID.Rapier"/>;
	/// </code>
	/// </summary>
	public static void DefaultToShortsword(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, bool autoReuse = false, int crit = 0, float scale = 1f, int width = 24, int height = 28)
	{
		item.DefaultToSpear(projectile, damage, knockback, singleUseTime, shootSpeed, autoReuse, crit, scale, width, height);
		item.useStyle = ItemUseStyleID.Rapier;
	}
	/// <summary>
	/// This method sets a variety of Item values common to boomerang weapons.<br/>
	/// Specifically: <code>
	/// width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// autoReuse = <paramref name="autoReuse"/>;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Melee"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// noMelee = true;
	/// noUseGraphic = true;
	/// shoot = <paramref name="projectile"/>;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// useTime = <paramref name="singleUseTime"/>;
	/// useAnimation = <paramref name="singleUseTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// UseSound = <see cref="SoundID.Item1"/>;
	/// </code>
	/// </summary>
	public static void DefaultToBoomerang(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, bool autoReuse = false, int crit = 0, int width = 14, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.autoReuse = autoReuse;
		item.crit = crit;
		item.damage = damage;
		item.DamageType = DamageClass.Melee;
		item.knockBack = knockback;
		item.noMelee = true;
		item.noUseGraphic = true;
		item.shoot = projectile;
		item.shootSpeed = shootSpeed;
		item.useTime = singleUseTime;
		item.useAnimation = singleUseTime;
		item.useStyle = ItemUseStyleID.Swing;
		item.UseSound = SoundID.Item1;
	}
	/// <summary>
	/// This method sets a variety of Item values common to magic weapons.<br/>
	/// Specifically: <code>width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// autoReuse = <paramref name="autoReuse"/>;
	/// crit = <paramref name="crit"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Magic"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// mana = <paramref name="manaUsed"/>;
	/// noMelee = true;
	/// shootSpeed = <paramref name="shootSpeed"/>;
	/// shoot = <paramref name="projectile"/>;
	/// useTime = <paramref name="useTime"/>;
	/// useAnimation = <paramref name="useAnimation"/>;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// </code>
	/// </summary>
	public static void DefaultToMagicWeapon(this Item item, int width, int height, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int crit = 0)
	{
		item.width = width;
		item.height = height;
		item.autoReuse = autoReuse;
		item.crit = crit;
		item.damage = damage;
		item.DamageType = DamageClass.Magic;
		item.knockBack = knockback;
		item.mana = manaUsed;
		item.noMelee = true;
		item.shootSpeed = shootSpeed;
		item.shoot = projectile;
		item.useTime = useTime;
		item.useAnimation = useAnimation;
		item.useStyle = ItemUseStyleID.Shoot;
	}
	/// <summary>
	/// <inheritdoc	cref="DefaultToMagicWeapon"/>
	/// Additional values specific to spell books:
	/// <code>
	/// autoReuse = true;
	/// scale = <paramref name="scale"/>
	/// </code>
	/// </summary>
	public static void DefaultToSpellBook(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int useTime, int useAnimation, float scale = 0.9f, int crit = 0, int width = 24, int height = 28)
	{
		item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, useTime, useAnimation, true, crit);
		item.scale = scale;
	}
	/// <summary>
	/// This method sets a variety of Item values common to magic staff weapons.<br/>
	/// Specifically: <code>
	/// <inheritdoc	cref="DefaultToMagicWeapon"/>
	/// </code>
	/// </summary>
	public static void DefaultToStaff(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int crit = 0, int width = 40, int height = 40)
	{
		item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, useTime, useAnimation, autoReuse, crit);
	}
	/// <summary>
	/// <inheritdoc	cref="DefaultToMagicWeapon"/>
	/// Additional values specific to channeling:
	/// <code>
	/// autoReuse = false;
	/// channel = true;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// </code>
	/// </summary>
	public static void DefaultToMagicWeaponChanneled(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int singleUseTime, int crit = 0, int width = 26, int height = 28)
	{
		item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, singleUseTime, singleUseTime, false, crit);
		item.channel = true;
		item.useStyle = ItemUseStyleID.Swing;
	}
	/// <summary>
	/// <inheritdoc	cref="DefaultToMagicWeapon"/>
	/// Additional values specific to swung weapons:
	/// <code>
	/// noUseGraphic = <paramref name="noUseGraphic"/>;
	/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
	/// </code>
	/// </summary>
	public static void DefaultToMagicWeaponSwing(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int singleUseTime, bool autoReuse = false, int crit = 0, bool noUseGraphic = false, int width = 26, int height = 28)
	{
		item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, singleUseTime, singleUseTime, autoReuse, crit);
		item.noUseGraphic = noUseGraphic;
		item.useStyle = ItemUseStyleID.Swing;
	}
	/// <summary>
	/// This method sets a variety of Item values common to minion weapons.<br/>
	/// Specifically: <code>width = <paramref name="width"/>;
	/// height = <paramref name="height"/>;
	/// autoReuse = true;
	/// buffType = <paramref name="buff"/>;
	/// damage = <paramref name="damage"/>;
	/// DamageType = <see cref="DamageClass.Summon"/>;
	/// knockBack = <paramref name="knockback"/>;
	/// mana = <paramref name="manaUsed"/>;
	/// noMelee = true;
	/// reuseDelay = 2;
	/// shootSpeed = 10;
	/// shoot = <paramref name="projectile"/>;
	/// useTime = <paramref name="singleUseTime"/>;
	/// useAnimation = <paramref name="singleUseTime"/>;
	/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
	/// </code>
	/// </summary>
	public static void DefaultToMinionWeapon(this Item item, int projectile, int buff, int damage, float knockback, int singleUseTime = 36, int manaUsed = 10, int width = 26, int height = 28)
	{
		item.width = width;
		item.height = height;
		item.autoReuse = true;
		item.buffType = buff;
		item.damage = damage;
		item.DamageType = DamageClass.Summon;
		item.knockBack = knockback;
		item.mana = manaUsed;
		item.noMelee = true;
		item.reuseDelay = 2;
		item.shootSpeed = 10f;
		item.shoot = projectile;
		item.useTime = singleUseTime;
		item.useAnimation = singleUseTime;
		item.useStyle = ItemUseStyleID.Swing;
	}
	/// <summary>
	/// <inheritdoc	cref="DefaultToMinionWeapon"/>
	/// Additional values specific to upgradeable minion weapons:
	/// <code>
	/// buffType = 0; (buff added in the counter projectile's AI)
	/// shoot = 0; (based on how our prime staff currently works, if it calls ModItem.Shoot, the arms will disappear and reappear on alternating uses after exceeding the player's max minion count)
	/// </code>
	/// </summary>
	public static void DefaultToMinionWeaponUpgradeable(this Item item, int damage, float knockback, int singleUseTime = 36, int manaUsed = 10, int width = 26, int height = 28)
	{
		item.DefaultToMinionWeapon(0, 0, damage, knockback, singleUseTime, manaUsed, width, height);
	}
	#endregion Item DefaultToX() methods
}
