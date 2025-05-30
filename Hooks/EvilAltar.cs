using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Tiles.Ores;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.GameContent.Achievements;
using Avalon.ModSupport;

namespace Avalon.Hooks;

public class EvilAltar : ModHook
{
    protected override void Apply()
    {
		if (!AltLibrarySupport.Enabled)
		{
			IL_WorldGen.SmashAltar += EditAltarSpawn;
			//IL_WorldGen.SmashAltar += WorldGen_SmashAltar;
			On_WorldGen.SmashAltar += On_WorldGen_SmashAltar;
		}
        IL_Player.ItemCheck_UseMiningTools_ActuallyUseMiningTool += IL_Player_ItemCheck_UseMiningTools_ActuallyUseMiningTool;
    }

	private void EditAltarSpawn(ILContext il)
	{
		ILCursor c = new(il);
		while (c.TryGotoNext(MoveType.After, i => i.MatchLdsfld<Main>("drunkWorld"))) //goto all the times Main.drunkWorld is used. This is to replace the drunk world ore cycling
		{
			c.EmitDelegate((bool drunkWorld) =>
			{
				return false; //always return false, allowing the code to always be skipped, this is replimplemented later
			});
		}
		c.GotoPrev(MoveType.After, i => i.MatchLdcI4(12)); //return to int num6 = 12;, which is before any text
		do //loop through every case of using Lang.misc
		{
			if (!c.TryGotoNext(MoveType.After, i => i.MatchLdsfld<Lang>("misc"))) //if there is no more misc's, stop
			{
				break;
			}
			if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(50), i => i.MatchLdcI4(255), i => i.MatchLdcI4(130))) //if color codes exist, before that place the delegate
			{
				c.EmitLdloc0(); //for getting the switch statement num
				c.EmitDelegate((string oldText, int num) => //oldText is the Lang.misc being used, we dont use it though so the IL code for the Lang.misc nolonger becomes invalid
				{
					return GetHardmodeText(num == 0 ? WorldGen.SavedOreTiers.Cobalt : num == 1 ? WorldGen.SavedOreTiers.Mythril : WorldGen.SavedOreTiers.Adamantite);
				});
			}
		}
		while (true);
		c.GotoPrev(MoveType.After, i => i.MatchLdcI4(12)); //return to int num6 = 12;, which is before any text
		do //loop through every case of using the color code (50, 255, 130)
		{
			if (c.TryGotoNext(MoveType.After, i => i.MatchLdcI4(50))) //find R
			{
				c.EmitLdloc0(); //for getting the switch statement num
				c.EmitDelegate((int r, int num) => 
				{
					return GetHardmodeColor(num == 0 ? WorldGen.SavedOreTiers.Cobalt : num == 1 ? WorldGen.SavedOreTiers.Mythril : WorldGen.SavedOreTiers.Adamantite).R; //replace the R color code
				});
			}
			else
			{
				break; //stop if the R isnt found anymore
			}
			if (c.TryGotoNext(MoveType.After, i => i.MatchLdcI4(255))) //find G
			{
				c.EmitLdloc0(); //for getting the switch statement num
				c.EmitDelegate((int g, int num) =>
				{
					return GetHardmodeColor(num == 0 ? WorldGen.SavedOreTiers.Cobalt : num == 1 ? WorldGen.SavedOreTiers.Mythril : WorldGen.SavedOreTiers.Adamantite).G; //replace the G color code
				});
			}
			if (c.TryGotoNext(MoveType.After, i => i.MatchLdcI4(130))) //find B
			{
				c.EmitLdloc0(); //for getting the switch statement num
				c.EmitDelegate((int b, int num) =>
				{
					return GetHardmodeColor(num == 0 ? WorldGen.SavedOreTiers.Cobalt : num == 1 ? WorldGen.SavedOreTiers.Mythril : WorldGen.SavedOreTiers.Adamantite).B; //replace the B color code
				});
			}
		}
		while (true);
		c.GotoPrev(MoveType.After, i => i.MatchLdcI4(12), i => i.MatchStloc(5)); //return to int num6 = 12;
		c.EmitLdloca(2);
		c.EmitDelegate((ref double num4) => //cycles adamantite in drunk seeds and also modifies the spawn rates of duratanium ore patches
		{
			if (Main.drunkWorld) //fixed the cycling of ores in drunk seeds sycling every altar smash rather than only on cobalt smashes
			{
				if (WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
				{
					WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
				}
				else if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
				{
					WorldGen.SavedOreTiers.Adamantite = ModContent.TileType<TroxiniumOre>();
				}
				else if (WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
				{
					WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
				}
			}
			if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
			{
				num4 *= 0.8999999761581421;
			}
		});
		c.GotoNext(MoveType.After, i => i.MatchLdcI4(13), i => i.MatchStloc(6)); //return to int num7 = 13;
		c.EmitLdloca(2);
		c.EmitDelegate((ref double num4) => //cycles cobalt in drunk seeds and also modifies the spawn rates of naquadah ore patches
		{
			if (Main.drunkWorld)
			{
				if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
				{
					WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
				}
				else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
				{
					WorldGen.SavedOreTiers.Cobalt = ModContent.TileType<DurataniumOre>();
				}
				else if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
				{
					WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
				}
			}
			if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<NaquadahOre>())
			{
				num4 *= 0.8999999761581421;
			}
		});
		c.GotoNext(MoveType.After, i => i.MatchLdcI4(14), i => i.MatchStloc(7)); //return to int num6 = 12;
		c.EmitLdloca(2);
		c.EmitDelegate((ref double num4) => //cycles mythril in drunk seeds and also modifies the spawn rates of troxinium ore patches
		{
			if (Main.drunkWorld)
			{
				if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
				{
					WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
				}
				else if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
				{
					WorldGen.SavedOreTiers.Mythril = ModContent.TileType<NaquadahOre>();
				}
				else if (WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
				{
					WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
				}
			}
			if (WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
			{
				num4 *= 0.8999999761581421;
			}
		});
		c.GotoNext(MoveType.After, i => i.MatchLdsfld<Main>("worldSurface"), i => i.MatchStloc(10));
		c.EmitLdloc(0);
		c.EmitLdloca(10);
		c.EmitDelegate((int num, ref double num9) =>
		{
			if (num == ModContent.TileType<NaquadahOre>())
			{
				num9 = Main.rockLayer;
			}
			if (num == ModContent.TileType<TroxiniumOre>())
			{
				num9 = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY) / 3.0;
			}
		});
		c.GotoNext(MoveType.After, i => i.MatchSub(), i => i.MatchConvR8(), i => i.MatchStloc(12));
		c.EmitLdloc(0);
		c.EmitLdloca(12);
		c.EmitDelegate((int num, ref double num10) =>
		{
			if (num == ModContent.TileType<NaquadahOre>())
			{
				num10 = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY - 350.0) / 3.0;
			}
			if (num == ModContent.TileType<TroxiniumOre>())
			{
				num10 = Main.rockLayer - 25.0;
			}
		});
	}

	private void IL_Player_ItemCheck_UseMiningTools_ActuallyUseMiningTool(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.DemonAltar, id => Data.Sets.TileSets.IckyAltar.Contains(id));
    }

    private void On_WorldGen_SmashAltar(On_WorldGen.orig_SmashAltar orig, int i, int j)
    {
		if (AvalonWorld.retroWorld && !Main.drunkWorld)
		{
			if (Main.netMode == 1 || !Main.hardMode || WorldGen.noTileActions || WorldGen.gen)
			{
				return;
			}
			int num = WorldGen.altarCount % 3;
			int num2 = WorldGen.altarCount / 3 + 1;
			float num3 = Main.maxTilesX / 4200;
			int num4 = 1 - num;
			num3 = num3 * 310f - (float)(85 * num);
			num3 *= 0.85f;
			num3 /= (float)num2;
			switch (num)
			{
				case 0:
					if (Main.netMode == 0)
					{
						Main.NewText(Lang.misc[12].Value, 50, byte.MaxValue, 130);
					}
					else if (Main.netMode == 2)
					{
						NetMessage.SendData(25, -1, -1, Lang.misc[12].ToNetworkText(), 255, 50f, 255f, 130f);
					}
					num = 107;
					num3 *= 1.05f;
					break;
				case 1:
					if (Main.netMode == 0)
					{
						Main.NewText(Lang.misc[13].Value, 50, byte.MaxValue, 130);
					}
					else if (Main.netMode == 2)
					{
						NetMessage.SendData(25, -1, -1, Lang.misc[13].ToNetworkText(), 255, 50f, 255f, 130f);
					}
					num = 108;
					break;
				default:
					if (Main.netMode == 0)
					{
						Main.NewText(Lang.misc[14].Value, 50, byte.MaxValue, 130);
					}
					else if (Main.netMode == 2)
					{
						NetMessage.SendData(25, -1, -1, Lang.misc[14].ToNetworkText(), 255, 50f, 255f, 130f);
					}
					num = 111;
					break;
			}
			for (int k = 0; (float)k < num3; k++)
			{
				int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				double num5 = Main.worldSurface;
				if (num == 108)
				{
					num5 = Main.rockLayer;
				}
				if (num == 111)
				{
					num5 = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY) / 3.0;
				}
				int j2 = WorldGen.genRand.Next((int)num5, Main.maxTilesY - 150);
				WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(5, 9 + num4), WorldGen.genRand.Next(5, 9 + num4), (ushort)num);
			}
			int num6 = WorldGen.genRand.Next(3);
			while (num6 != 2)
			{
				int num7 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				int num8 = WorldGen.genRand.Next((int)Main.rockLayer + 50, Main.maxTilesY - 300);
				if (Main.tile[num7, num8].HasTile && Main.tile[num7, num8].TileType == 1)
				{
					if (num6 == 0)
					{
						Main.tile[num7, num8].TileType = 25;
					}
					else
					{
						Main.tile[num7, num8].TileType = 117;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, num7, num8, 1);
					}
					break;
				}
			}
			if (Main.netMode != 1)
			{
				int num9 = Main.rand.Next(2) + 1;
				for (int l = 0; l < num9; l++)
				{
					NPC.SpawnOnPlayer(Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16), 82);
				}
			}
			WorldGen.altarCount++;
			AchievementsHelper.NotifyProgressionEvent(6); //addded so achievements can still be granted
		}
		else
		{
			if (Main.netMode == 1 || !Main.hardMode || WorldGen.noTileActions || WorldGen.gen)
			{
				return;
			}
			int num = WorldGen.altarCount % 3;
			bool flag = false;
			switch (num)
			{
				case 0:
					{
						if (WorldGen.SavedOreTiers.Cobalt == -1)
						{
							flag = true;
							WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
							int rn = WorldGen.genRand.Next(3);
							if (rn == 1)
							{
								WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
							}
							else if (rn == 2)
							{
								WorldGen.SavedOreTiers.Cobalt = ModContent.TileType<DurataniumOre>();
							}
						}
						break;
					}
				case 1:
					{
						if (WorldGen.SavedOreTiers.Mythril == -1)
						{
							flag = true;
							WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
							int rn = WorldGen.genRand.Next(3);
							if (rn == 1)
							{
								WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
							}
							else if (rn == 2)
							{
								WorldGen.SavedOreTiers.Mythril = ModContent.TileType<NaquadahOre>();
							}
						}
						break;
					}
				default:
					{
						if (WorldGen.SavedOreTiers.Adamantite == -1)
						{
							flag = true;
							WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
							int rn = WorldGen.genRand.Next(3);
							if (rn == 1)
							{
								WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
							}
							else if (rn == 2)
							{
								WorldGen.SavedOreTiers.Adamantite = ModContent.TileType<TroxiniumOre>();
							}
						}
						break;
					}
			}
			if (flag)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
			orig.Invoke(i, j);
		}
    }

    private static Color GetHardmodeColor(int ore)
    {
        switch (ore) {
            case TileID.Cobalt:
                return new Color(26, 105, 161);
            case TileID.Palladium:
                return new Color(235, 87, 47);
            case TileID.Mythril:
                return new Color(93, 147, 88);
            case TileID.Orichalcum:
                return new Color(163, 22, 158);
            case TileID.Adamantite:
                return new Color(221, 85, 152);
            case TileID.Titanium:
                return new Color(185, 194, 215);
            default:
                if (ore == ModContent.TileType<DurataniumOre>())
                {
                    return new Color(137, 81, 89);
                }

                if (ore == ModContent.TileType<TroxiniumOre>())
                {
                    return new Color(193, 218, 72);
                }

                if (ore == ModContent.TileType<NaquadahOre>())
                {
                    return new Color(0, 38, 255);
                }

                return new Color(50, 255, 130);
        }
    }

	private static string GetHardmodeText(int ore)
	{
		switch (ore)
		{
			case TileID.Cobalt:
				return Language.GetTextValue("Mods.Avalon.Altars.Cobalt");
			case TileID.Palladium:
				return Language.GetTextValue("Mods.Avalon.Altars.Palladium");
			case TileID.Mythril:
				return Language.GetTextValue("Mods.Avalon.Altars.Mythril");
			case TileID.Orichalcum:
				return Language.GetTextValue("Mods.Avalon.Altars.Orichalcum");
			case TileID.Adamantite:
				return Language.GetTextValue("Mods.Avalon.Altars.Adamantite");
			case TileID.Titanium:
				return Language.GetTextValue("Mods.Avalon.Altars.Titanium");
			default:
				if (ore == ModContent.TileType<DurataniumOre>())
				{
					return Language.GetTextValue("Mods.Avalon.Altars.Duratanium");
				}
				if (ore == ModContent.TileType<NaquadahOre>())
				{
					return Language.GetTextValue("Mods.Avalon.Altars.Naquadah");
				}
				if (ore == ModContent.TileType<TroxiniumOre>())
				{
					return Language.GetTextValue("Mods.Avalon.Altars.Troxinium");
				}
				return typeof(ExxoAvalonOrigins) + ": ore spawn from the smash altar is not known. Maybe another mod is adding more alternate hardmode ores?";
		}
	}
}
