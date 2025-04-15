using Avalon.Assets;
using Avalon.Common.Players;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Avalon.NPCs.Hardmode;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Furniture.OrangeDungeon;
using Avalon.Tiles.Furniture.PurpleDungeon;
using Avalon.Tiles.Furniture.YellowDungeon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalTile : GlobalTile
{
	public static bool LockOrUnlock(int X, int Y)
	{
		if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null)
		{
			return false;
		}
		int type = 0;
		Tile tileSafely = Framing.GetTileSafely(X, Y);
		if (!(tileSafely.TileType == ModContent.TileType<OrangeDungeonChest>() || tileSafely.TileType == ModContent.TileType<PurpleDungeonChest>() ||
			tileSafely.TileType == ModContent.TileType<YellowDungeonChest>() || tileSafely.TileType == ModContent.TileType<ContagionChest>())) return false;
		int type2 = tileSafely.TileType;
		int num2 = tileSafely.TileFrameX / 36;

		SoundEngine.PlaySound(SoundID.Unlock, new(X * 16, Y * 16));

		if (num2 == 0)
		{
			for (int i = X; i <= X + 1; i++)
			{
				for (int j = Y; j <= Y + 1; j++)
				{
					Tile tileSafely2 = Framing.GetTileSafely(i, j);
					tileSafely2.TileFrameX += 36;
					for (int k = 0; k < 4; k++)
					{
						Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, type);
					}
				}
			}
		}
		else if (num2 == 1)
		{
			for (int i = X; i <= X + 1; i++)
			{
				for (int j = Y; j <= Y + 1; j++)
				{
					Tile tileSafely2 = Framing.GetTileSafely(i, j);
					tileSafely2.TileFrameX -= 36;
					for (int k = 0; k < 4; k++)
					{
						Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, type);
					}
				}
			}
		}
		return true;
	}

	public override void SetStaticDefaults()
    {
		Main.tileDungeon[TileID.AncientBlueBrick] = true; //a sincier fuck you to all builders
		Main.tileDungeon[TileID.AncientGreenBrick] = true;
		Main.tileDungeon[TileID.AncientPinkBrick] = true;
		TileID.Sets.DungeonBiome[TileID.AncientBlueBrick] = 1;
		TileID.Sets.DungeonBiome[TileID.AncientGreenBrick] = 1;
		TileID.Sets.DungeonBiome[TileID.AncientPinkBrick] = 1;

		int[] spelunkers = { TileID.Crimtane, TileID.Meteorite, TileID.Obsidian, TileID.Hellstone };
        int[] ores = { TileID.Topaz, TileID.Ruby, TileID.Amethyst, TileID.Diamond, TileID.Emerald, TileID.Sapphire, TileID.AmberStoneBlock };
        foreach (int tile in spelunkers)
        {
            Main.tileSpelunker[tile] = true;
        }
        foreach(int tile in ores)
        {
            TileID.Sets.Ore[tile] = true;
        }
    }

	public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        // add back when hardmode/hidden temple is released
        if (false) //Main.tile[i, j].TileType == TileID.LihzahrdAltar && NPC.downedGolemBoss)
        {
            Main.tileFrameCounter[TileID.LihzahrdAltar]++;

            int frameX = Main.tile[i, j].TileFrameX;
            int frameY = Main.tile[i, j].TileFrameY;

            if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 0 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 8)
            {
                frameY += 0;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 8 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 16)
            {
                frameY += 36;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 16 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 24)
            {
                frameY += 36 * 2;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 24 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 32)
            {
                frameY += 36 * 3;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 32 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 40)
            {
                frameY += 36 * 4;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 40 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 48)
            {
                frameY += 36 * 5;
            }
            if (Main.tileFrameCounter[TileID.LihzahrdAltar] == 48)
            {
                Main.tileFrameCounter[TileID.LihzahrdAltar] = 0;
            }
            var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Assets/Textures/LihzahrdAltarPortal").Value,
                new Vector2(i * 16 - (int)Main.screenPosition.X - 0 / 2f, j * 16 - (int)Main.screenPosition.Y) + zero,
                new Rectangle(frameX, frameY, 16, 16), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
	public override void AnimateTile()
	{
		// colors are determined here, drawing is done in Avalon.Hooks.TileDrawingHooks
		#region Warm Gemspark
		if (WarmGemsparkBlock.time <= 50)
		{
			WarmGemsparkBlock.G += 5;
			if (WarmGemsparkBlock.G >= 255)
			{
				WarmGemsparkBlock.G = 255;
			}
		}
		if (WarmGemsparkBlock.time >= 50)
		{
			WarmGemsparkBlock.G -= 5;
			if (WarmGemsparkBlock.G <= 0)
			{
				WarmGemsparkBlock.G = 0;
			}
		}
		WarmGemsparkBlock.time++;
		WarmGemsparkBlock.time = (byte)(WarmGemsparkBlock.time % 101);
		#endregion

		// colors are determined here, drawing is done in Avalon.Hooks.TileDrawingHooks
		#region Cool Gemspark
		if (CoolGemsparkBlock.time <= 31)
		{
			CoolGemsparkBlock.R -= 5;
			if (CoolGemsparkBlock.R <= 0)
			{
				CoolGemsparkBlock.R = 0;
			}
		}
		if (CoolGemsparkBlock.time >= 32 && CoolGemsparkBlock.time <= 112)
		{
			CoolGemsparkBlock.G += 5;
			if (CoolGemsparkBlock.G >= 255)
			{
				CoolGemsparkBlock.G = 255;
			}
			if (CoolGemsparkBlock.G >= 160)
			{
				CoolGemsparkBlock.B -= 5;
				if (CoolGemsparkBlock.B <= 0)
				{
					CoolGemsparkBlock.B = 0;
				}
			}
		}
		if (CoolGemsparkBlock.time >= 112 && CoolGemsparkBlock.time <= 180)
		{
			CoolGemsparkBlock.G -= 5;
			if (CoolGemsparkBlock.G <= 0)
			{
				CoolGemsparkBlock.G = 0;
			}
			if (CoolGemsparkBlock.G <= 160)
			{
				CoolGemsparkBlock.B += 5;
				if (CoolGemsparkBlock.B >= 255)
				{
					CoolGemsparkBlock.B = 255;
				}
			}
		}
		if (CoolGemsparkBlock.time >= 180)
		{
			CoolGemsparkBlock.R += 5;
			if (CoolGemsparkBlock.R >= 160)
			{
				CoolGemsparkBlock.R = 160;
			}
		}
		CoolGemsparkBlock.time++;
		CoolGemsparkBlock.time = (byte)(CoolGemsparkBlock.time % 212); 
		#endregion
	}
	public override void FloorVisuals(int type, Player player)
    {
        if (type == 229 && player.GetModPlayer<AvalonPlayer>().NoSticky)
        {
            player.sticky = false;
        }
    }
	public override void HitWire(int i, int j, int type)
	{
		int left = i;
		int top = j;
		Tile tile = Main.tile[i, j];
		if (type == 105 && ((Main.tile[i, j].TileFrameY >= 0 && Main.tile[i, j].TileFrameY <= 36) || (Main.tile[i, j].TileFrameY >= 162 && Main.tile[i, j].TileFrameY <= 198)))
		{
			if (Main.tile[i, j].TileFrameX >= 108 && Main.tile[i, j].TileFrameX <= 126)
			{
				while (tile.TileFrameX % 36 != 0)
				{
					left--;
					if (Main.tile[left, j].TileFrameX % 36 == 0)
					{
						break;
					}
				}
				while (tile.TileFrameY != 0 || tile.TileFrameY != 162)
				{
					top--;
					if (Main.tile[i, top].TileFrameY == 0 || Main.tile[i, top].TileFrameY == 162)
					{
						break;
					}
				}
				ClassExtensions.SkipWireMulti(left, top, 2, 3);
				if (Wiring.CheckMech(left, top, 30) && MechSpawn(left, top, NPCID.EnchantedSword))
				{
					int n = NPC.NewNPC(Entity.GetSource_None(), left * 16, top * 16, NPCID.EnchantedSword);
					Main.npc[n].value = 0f;
					Main.npc[n].npcSlots = 0f;
					Main.npc[n].SpawnedFromStatue = true;
					Main.npc[n].CanBeReplacedByOtherNPCs = true;
				}
			}
			if (Main.tile[i, j].TileFrameX >= 684 && Main.tile[i, j].TileFrameX <= 702)
			{
				while (tile.TileFrameX % 36 != 0)
				{
					left--;
					if (Main.tile[left, j].TileFrameX % 36 == 0)
					{
						break;
					}
				}
				while (tile.TileFrameY != 0 || tile.TileFrameY != 162)
				{
					top--;
					if (Main.tile[i, top].TileFrameY == 0 || Main.tile[i, top].TileFrameY == 162)
					{
						break;
					}
				}
				ClassExtensions.SkipWireMulti(left, top, 2, 3);
				if (Wiring.CheckMech(left, top, 30) && MechSpawn(left, top, NPCID.CursedHammer))
				{
					int n = NPC.NewNPC(Entity.GetSource_None(), left * 16, top * 16, NPCID.CursedHammer);
					Main.npc[n].value = 0f;
					Main.npc[n].npcSlots = 0f;
					Main.npc[n].SpawnedFromStatue = true;
					Main.npc[n].CanBeReplacedByOtherNPCs = true;
				}
			}
			if (Main.tile[i, j].TileFrameX >= 1044 && Main.tile[i, j].TileFrameX <= 1062)
			{
				while (tile.TileFrameX % 36 != 0)
				{
					left--;
					if (Main.tile[left, j].TileFrameX % 36 == 0)
					{
						break;
					}
				}
				while (tile.TileFrameY != 0 || tile.TileFrameY != 162)
				{
					top--;
					if (Main.tile[i, top].TileFrameY == 0 || Main.tile[i, top].TileFrameY == 162)
					{
						break;
					}
				}
				ClassExtensions.SkipWireMulti(left, top, 2, 3);
				if (Wiring.CheckMech(left, top, 30) && MechSpawn(left, top, NPCID.CrimsonAxe))
				{
					int n = NPC.NewNPC(Entity.GetSource_None(), left * 16, top * 16, NPCID.CrimsonAxe);
					Main.npc[n].value = 0f;
					Main.npc[n].npcSlots = 0f;
					Main.npc[n].SpawnedFromStatue = true;
					Main.npc[n].CanBeReplacedByOtherNPCs = true;
				}
			}
			if (Main.tile[i, j].TileFrameX >= 1188 && Main.tile[i, j].TileFrameX <= 1206)
			{
				while (tile.TileFrameX % 36 != 0)
				{
					left--;
					if (Main.tile[left, j].TileFrameX % 36 == 0)
					{
						break;
					}
				}
				while (tile.TileFrameY != 0 || tile.TileFrameY != 162)
				{
					top--;
					if (Main.tile[i, top].TileFrameY == 0 || Main.tile[i, top].TileFrameY == 162)
					{
						break;
					}
				}
				ClassExtensions.SkipWireMulti(left, top, 2, 3);
				if (Wiring.CheckMech(left, top, 30) && MechSpawn(left, top, ModContent.NPCType<InfectedPickaxe>()))
				{
					int n = NPC.NewNPC(Entity.GetSource_None(), left * 16, top * 16, ModContent.NPCType<InfectedPickaxe>());
					Main.npc[n].value = 0f;
					Main.npc[n].npcSlots = 0f;
					Main.npc[n].SpawnedFromStatue = true;
					Main.npc[n].CanBeReplacedByOtherNPCs = true;
				}
			}
		}
		if (type == ModContent.TileType<Statues>())
		{
			if (Main.tile[i, j].TileFrameX >= 576 && Main.tile[i, j].TileFrameX <= 594)
			{
				if ((Main.tile[i, j].TileFrameY >= 0 && Main.tile[i, j].TileFrameY <= 36) || (Main.tile[i, j].TileFrameY >= 162 && Main.tile[i, j].TileFrameY <= 198))
				{
					while (tile.TileFrameX % 36 != 0)
					{
						left--;
						if (Main.tile[left, j].TileFrameX % 36 == 0)
						{
							break;
						}
					}
					while (tile.TileFrameY != 0 || tile.TileFrameY != 162)
					{
						top--;
						if (Main.tile[i, top].TileFrameY == 0 || Main.tile[i, top].TileFrameY == 162)
						{
							break;
						}
					}
					ClassExtensions.SkipWireMulti(left, top, 2, 3);
					if (Wiring.CheckMech(left, top, 30) && MechSpawn(left, top, ModContent.NPCType<CursedScepter>()))
					{
						int n = NPC.NewNPC(Entity.GetSource_None(), left * 16, top * 16, ModContent.NPCType<CursedScepter>());
						Main.npc[n].value = 0f;
						Main.npc[n].npcSlots = 0f;
						Main.npc[n].SpawnedFromStatue = true;
						Main.npc[n].CanBeReplacedByOtherNPCs = true;
					}
				}
			}
		}
	}
	public static bool MechSpawn(int x, int y, int type)
	{
		int amt = 0;
		foreach (var npc in Main.ActiveNPCs)
		{
			if (!npc.active)
				continue;

			if (type == ModContent.NPCType<InfectedPickaxe>())
			{
				if (npc.type == ModContent.NPCType<InfectedPickaxe>())
					amt++;
			}
			if (type == ModContent.NPCType<CursedScepter>())
			{
				if (npc.type == ModContent.NPCType<CursedScepter>())
					amt++;
			}
			if (type == NPCID.CursedHammer)
			{
				if (npc.type == NPCID.CursedHammer)
					amt++;
			}
			if (type == NPCID.CrimsonAxe)
			{
				if (npc.type == NPCID.CrimsonAxe)
					amt++;
			}
			if (type == NPCID.EnchantedSword)
			{
				if (npc.type == NPCID.EnchantedSword)
					amt++;
			}
		}
		if (amt >= 3)
			return false;

		return true;
	}
	public override void Drop(int i, int j, int type)
    {
        int pid = Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16);

        // icicle drops
        if (type is TileID.Stalactite && Main.tile[i, j].TileFrameX < 54 && Main.tile[i, j].TileFrameY is 0 or 72 && Main.rand.NextBool(2))
        {
            int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Icicle>());
            //if (Main.netMode == NetmodeID.Server)
            //{
            //    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.FromLiteral(""), a, 0f, 0f, 0f, 0);
            //    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
            //}
        }

        // four leaf clover drops
        if (type is TileID.CorruptPlants or TileID.JunglePlants or TileID.JunglePlants2 or TileID.CrimsonPlants or TileID.Plants or TileID.Plants2 ||
            type == ModContent.TileType<Tiles.Contagion.ContagionShortGrass>())
        {
            bool doRealCloverDrop = false;
            bool doFakeCloverDrop = false;
            int realChance = 8000;
            int fakeChance = 500;
            if (pid >= 0)
            {
                Player p = Main.player[pid];
                if (p.RollLuck(realChance) < 1)
                {
                    doRealCloverDrop = true;
                }
                else if (p.RollLuck(fakeChance) < 1)
                {
                    doFakeCloverDrop = true;
                }
            }
            else
            {
                doRealCloverDrop = Main.rand.NextBool(realChance);
                doFakeCloverDrop = Main.rand.NextBool(fakeChance);
            }

            if (doRealCloverDrop)
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FourLeafClover>(), 1, false, 0);
                //if (Main.netMode == NetmodeID.Server)
                //{
                //    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a, 0f, 0f, 0f, 0);
                //    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                //}
            }
            else if (doFakeCloverDrop)
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FakeFourLeafClover>(), 1, false, 0);
                //if (Main.netMode == NetmodeID.Server)
                //{
                //    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a, 0f, 0f, 0f, 0);
                //    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                //}
            }
        }
    }
    public override bool CanPlace(int i, int j, int type)
    {
        if (Data.Sets.TileSets.AvalonPlanterBoxes[Main.tile[i, j + 1].TileType] &&
            (Main.tile[i, j].TileType == TileID.ImmatureHerbs || Main.tile[i, j].TileType == TileID.MatureHerbs ||
            Main.tile[i, j].TileType == TileID.BloomingHerbs || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>() ||
            Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Bloodberry>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Holybird>() ||
            Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Sweetstem>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.TwilightPlume>()))
        {
            return false;
        }
        return base.CanPlace(i, j, type);
    }
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        int pid = Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16);
        if (pid >= 0)
        {
            if (TileID.Sets.Ore[Main.tile[i, j].TileType])
            {
                if (Main.player[pid].GetModPlayer<AvalonPlayer>().OreDupe && Main.player[pid].HeldItem.pick >= ClassExtensions.GetPickaxePower(Main.tile[i, j].TileType, j))
                {
                    if (Data.Sets.TileSets.OresToChunks.ContainsKey(Main.tile[i, j].TileType) && !fail)
                    {
                        int drop = Data.Sets.TileSets.OresToChunks[Main.tile[i, j].TileType];
                        int stack = 1;
                        if (Main.rand.NextBool(3))
                        {
                            stack = 2;
                        }
                        int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop, stack);
                        //if (Main.netMode == NetmodeID.Server)
                        //{
                        //    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a, 0f, 0f, 0f, 0);
                        //}
                        noItem = true;
                    }
                }
            }
            // Probably doesn't work in multiplayer
            if (type == ModContent.TileType<Tiles.UltraResistantWood>() && Main.player[pid].inventory[Main.player[pid].selectedItem].axe < 40)
            {
                fail = true;
            }
        }
		if (!fail && !effectOnly)
		{
			Main.tile[i, j].Get<AvalonTileData>().IsTileActupainted = false;
		}
    }
}
