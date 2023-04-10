using Terraria;
using Terraria.ModLoader;
using Avalon.Common;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Avalon.Hooks;

internal class AncientHeadphones : ModHook
{
    protected override void Apply()
    {
        On_Main.UpdateAudio_DecideOnNewMusic += OnUpdateAudio_DecideOnNewMusic;
    }
    private static void OnUpdateAudio_DecideOnNewMusic(On_Main.orig_UpdateAudio_DecideOnNewMusic orig, Main self)
    {
        orig(self);
        if (!Main.gameMenu)
        {
            if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().AncientHeadphones)
            {
                bool goblins = false;
                bool pirates = false;
                bool fishron = false;

                Rectangle rectangle = new((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
                int num2 = 5000;
                Rectangle value = default(Rectangle);
                for (int j = 0; j < 200; j++)
                {
                    if (!Main.npc[j].active)
                    {
                        continue;
                    }
                    num2 = 5000;
                    int num3 = 0;
                    switch (Main.npc[j].type)
                    {
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 111:
                        case 471:
                            num3 = 11;
                            break;
                        case 212:
                        case 213:
                        case 214:
                        case 215:
                        case 216:
                        case 491:
                            num3 = 8;
                            break;
                        case 370:
                            num3 = 15;
                            break;
                    }
                    value = new((int)(Main.npc[j].position.X + (float)(Main.npc[j].width / 2)) - num2, (int)(Main.npc[j].position.Y + (float)(Main.npc[j].height / 2)) - num2, num2 * 2, num2 * 2);
                    if (rectangle.Intersects(value))
                    {
                        switch (num3)
                        {
                            case 11:
                                goblins = true;
                                break;
                            case 8:
                                pirates = true;
                                break;
                            case 15:
                                fishron = true;
                                break;
                        }
                    }
                }
                
                if (goblins || fishron)
                {
                    Main.newMusic = MusicID.Boss1;
                }
                if (pirates)
                {
                    Main.newMusic = MusicID.Boss5;
                }
                if (Main.LocalPlayer.ZoneDungeon || Main.LocalPlayer.ZoneUnderworldHeight)
                {
                    Main.newMusic = MusicID.Eerie;
                }
                if (Main.LocalPlayer.ZoneJungle)
                {
                    Main.newMusic = MusicID.Jungle;
                }
                if (Main.LocalPlayer.ZoneBeach)
                {
                    Main.newMusic = MusicID.Ocean;
                }
                if (Main.LocalPlayer.ZoneNormalSpace)
                {
                    Main.newMusic = MusicID.Space;
                }
            }
        }
    }
}
