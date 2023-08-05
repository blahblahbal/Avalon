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

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class EvilAltar : ModHook
{
    protected override void Apply()
    {
        IL_WorldGen.SmashAltar += WorldGen_SmashAltar;
        On_WorldGen.SmashAltar += On_WorldGen_SmashAltar;
    }

    private void On_WorldGen_SmashAltar(On_WorldGen.orig_SmashAltar orig, int i, int j)
    {
        if (Main.netMode == 1 || !Main.hardMode || WorldGen.noTileActions || WorldGen.gen)
        {
            return;
        }
        int num = WorldGen.altarCount % 3;
        int num3 = WorldGen.altarCount / 3 + 1;
        double num4 = (double)Main.maxTilesX / 4200.0;
        int num5 = 1 - num;
        num4 = num4 * 310.0 - (double)(85 * num);
        num4 *= 0.85;
        num4 /= (double)num3;
        bool flag = false;
        if (Main.drunkWorld)
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
                    int num7 = 12;
                    if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium || WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
                    {
                        num4 *= 0.89999997615814209;
                    }
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Cobalt"), GetHardmodeColor(TileID.Cobalt));
                        }
                        else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Palladium"), GetHardmodeColor(TileID.Palladium));
                        }
                        else if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Duratanium"), GetHardmodeColor(ModContent.TileType<DurataniumOre>()));
                        }
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Cobalt")), GetHardmodeColor(TileID.Cobalt));
                        }
                        else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Palladium")), GetHardmodeColor(TileID.Palladium));
                        }
                        else if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Duratanium")), GetHardmodeColor(ModContent.TileType<DurataniumOre>()));
                        }
                    }
                    num = WorldGen.SavedOreTiers.Cobalt;
                    num4 *= 1.0499999523162842;
                    break;
                }
            case 1:
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
                    int num8 = 13;
                    if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum || WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
                    {
                        num8 += 9;
                        num4 *= 0.89999997615814209;
                    }
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Mythril"), GetHardmodeColor(TileID.Mythril));
                        }
                        else if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Orichalcum"), GetHardmodeColor(TileID.Orichalcum));
                        }
                        else if (WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Naquadah"), GetHardmodeColor(ModContent.TileType<NaquadahOre>()));
                        }
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Mythril")), GetHardmodeColor(TileID.Mythril));
                        }
                        else if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Orichalcum")), GetHardmodeColor(TileID.Orichalcum));
                        }
                        else if (WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Naquadah")), GetHardmodeColor(ModContent.TileType<NaquadahOre>()));
                        }
                    }
                    num = WorldGen.SavedOreTiers.Mythril;
                    break;
                }
            default:
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
                    int num6 = 14;
                    if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium || WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
                    {
                        num6 += 9;
                        num4 *= 0.89999997615814209;
                    }
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Adamantite"), GetHardmodeColor(TileID.Adamantite));
                        }
                        else if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Titanium"), GetHardmodeColor(TileID.Titanium));
                        }
                        else if (WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
                        {
                            Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Troxinium"), GetHardmodeColor(ModContent.TileType<TroxiniumOre>()));
                        }
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        if (WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Adamantite")), GetHardmodeColor(TileID.Adamantite));
                        }
                        else if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Titanium")), GetHardmodeColor(TileID.Titanium));
                        }
                        else if (WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Altars.Troxinium")), GetHardmodeColor(ModContent.TileType<TroxiniumOre>()));
                        }
                    }
                    num = WorldGen.SavedOreTiers.Adamantite;
                    break;
                }
        }
        if (flag)
        {
            NetMessage.SendData(MessageID.WorldData);
        }
        for (int k = 0; (double)k < num4; k++)
        {
            int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double num9 = Main.worldSurface;
            if (num == 108 || num == 222)
            {
                num9 = Main.rockLayer;
            }
            if (num == 111 || num == 223)
            {
                num9 = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY) / 3.0;
            }
            int j2 = WorldGen.genRand.Next((int)num9, Main.maxTilesY - 150);
            if (Main.remixWorld)
            {
                double num10 = Main.maxTilesX - 350;
                if (num == 108 || num == 222)
                {
                    num10 = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY - 350.0) / 3.0;
                }
                if (num == 111 || num == 223)
                {
                    num10 = Main.rockLayer - 25.0;
                }
                j2 = WorldGen.genRand.Next((int)Main.worldSurface + 15, (int)num10);
            }
            if (Main.tenthAnniversaryWorld)
            {
                WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(5, 11 + num5), WorldGen.genRand.Next(5, 11 + num5), (ushort)num);
            }
            else
            {
                WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(5, 9 + num5), WorldGen.genRand.Next(5, 9 + num5), (ushort)num);
            }
        }
        if (Main.netMode != 1)
        {
            int num2 = Main.rand.Next(2) + 1;
            for (int l = 0; l < num2; l++)
            {
                NPC.SpawnOnPlayer(Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16), 82);
            }
        }
        WorldGen.altarCount++;
        AchievementsHelper.NotifyProgressionEvent(6);
    }

    private static Color GetHardmodeColor(int i)
    {
        switch (i) {
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
                if (i == ModContent.TileType<DurataniumOre>())
                {
                    return new Color(137, 81, 89);
                }

                if (i == ModContent.TileType<TroxiniumOre>())
                {
                    return new Color(193, 218, 72);
                }

                if (i == ModContent.TileType<NaquadahOre>())
                {
                    return new Color(0, 38, 255);
                }

                return new Color(50, 255, 130);
        }
    }

    private void WorldGen_SmashAltar(ILContext il)
	{
        ILCursor c = new(il);
        int j = 0;
        while (c.TryGotoNext(i => i.MatchLdcI4(50),
            i => i.MatchLdcI4(255),
            i => i.MatchLdcI4(130)))
		{
            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return GetHardmodeColor(type).R;
            });

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return GetHardmodeColor(type).G;
            });

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return GetHardmodeColor(type).B;
            });

            j++;
		}
	}
}
