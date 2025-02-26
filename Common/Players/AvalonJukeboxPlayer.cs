using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Common.Players
{
    internal class AvalonJukeboxPlayer : ModPlayer
    {
        public static List<int> JukeboxTracks = new List<int>
        {
            ItemID.MusicBoxOverworldDay,
            ItemID.MusicBoxEerie,
            ItemID.MusicBoxNight,
            ItemID.MusicBoxTitle,
            ItemID.MusicBoxUnderground,
            ItemID.MusicBoxBoss1,
            ItemID.MusicBoxJungle,
            ItemID.MusicBoxCorruption,
            ItemID.MusicBoxTheHallow,
            ItemID.MusicBoxUndergroundCorruption,
            ItemID.MusicBoxBoss2,
            ItemID.MusicBoxUndergroundHallow,
            ItemID.MusicBoxBoss3,
            ItemID.MusicBoxSnow,
            ItemID.MusicBoxSpace,
            ItemID.MusicBoxCrimson,
            ItemID.MusicBoxBoss4,
            ItemID.MusicBoxAltOverworldDay,
            ItemID.MusicBoxRain,
            ItemID.MusicBoxIce,
            ItemID.MusicBoxDesert,
            ItemID.MusicBoxOcean,
            ItemID.MusicBoxDungeon,
            ItemID.MusicBoxPlantera,
            ItemID.MusicBoxBoss5,
            ItemID.MusicBoxTemple,
            ItemID.MusicBoxEclipse,
            ItemID.MusicBoxMushrooms,
            ItemID.MusicBoxPumpkinMoon,
            ItemID.MusicBoxAltUnderground,
            ItemID.MusicBoxFrostMoon,
            ItemID.MusicBoxUndergroundCrimson,
            ItemID.MusicBoxLunarBoss,
            ItemID.MusicBoxMartians,
            ItemID.MusicBoxPirates,
            ItemID.MusicBoxHell,
            ItemID.MusicBoxTowers,
            ItemID.MusicBoxGoblins,
            ItemID.MusicBoxSandstorm,
            ItemID.MusicBoxDD2,
            ItemID.MusicBoxSpaceAlt,
            ItemID.MusicBoxOceanAlt,
            ItemID.MusicBoxWindyDay,
            ItemID.MusicBoxTownDay,
            ItemID.MusicBoxTownNight,
            ItemID.MusicBoxSlimeRain,
            ItemID.MusicBoxDayRemix,
            ItemID.MusicBoxTitleAlt,
            ItemID.MusicBoxStorm,
            ItemID.MusicBoxGraveyard,
            ItemID.MusicBoxUndergroundJungle,
            ItemID.MusicBoxJungleNight,
            ItemID.MusicBoxQueenSlime,
            ItemID.MusicBoxEmpressOfLight,
            ItemID.MusicBoxDukeFishron,
            ItemID.MusicBoxMorningRain,
            ItemID.MusicBoxConsoleTitle,
            ItemID.MusicBoxUndergroundDesert,
            ItemID.MusicBoxOWRain,
            ItemID.MusicBoxOWDay,
            ItemID.MusicBoxOWNight,
            ItemID.MusicBoxOWUnderground,
            ItemID.MusicBoxOWDesert,
            ItemID.MusicBoxOWOcean,
            ItemID.MusicBoxOWMushroom,
            ItemID.MusicBoxOWDungeon,
            ItemID.MusicBoxOWSpace,
            ItemID.MusicBoxOWUnderworld,
            ItemID.MusicBoxOWSnow,
            ItemID.MusicBoxOWCorruption,
            ItemID.MusicBoxOWUndergroundCorruption,
            ItemID.MusicBoxOWCrimson,
            ItemID.MusicBoxOWUndergroundCrimson,
            ItemID.MusicBoxOWUndergroundSnow,
            ItemID.MusicBoxOWUndergroundHallow,
            ItemID.MusicBoxOWBloodMoon,
            ItemID.MusicBoxOWBoss2,
            ItemID.MusicBoxOWBoss1,
            ItemID.MusicBoxOWInvasion,
            ItemID.MusicBoxOWTowers,
            ItemID.MusicBoxOWMoonLord,
            ItemID.MusicBoxOWPlantera,
            ItemID.MusicBoxOWJungle,
            ItemID.MusicBoxOWWallOfFlesh,
            ItemID.MusicBoxOWHallow,
            ItemID.MusicBoxCredits,
            ItemID.MusicBoxDeerclops,
            ItemID.MusicBoxShimmer,
        };

        public static List<int> AvalonTracks = new List<int>
        {
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxArmageddonSlime>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxBacteriumPrime>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxContagion>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxDarkMatter>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxDesertBeak>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxHellCastle>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxPhantasm>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxSkyFortress>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxTuhrtlOutpost>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxUndergroundContagion>(),
            ModContent.ItemType<Items.Placeable.MusicBoxes.MusicBoxDesertBeakOtherworldly>(),
        };

        public static Dictionary<int, int> TracksByItemID = new Dictionary<int, int>
        {
        };
        public static Dictionary<int, int> TracksByMusicID = new Dictionary<int, int>
        {
        };

        public bool DisplayJukeboxInterface { get; set; }
        public int JukeboxX { get; set; }
        public int JukeboxY { get; set; }
        public bool PlayingATrack = false;
        public int PlayingATrackID { get; set; }


        public override void PostUpdate()
        {
            if (DisplayJukeboxInterface)
            {
                int num9 = (int)((Player.position.X + (Player.width * 0.5)) / 16.0);
                int num10 = (int)((Player.position.Y + (Player.height * 0.5)) / 16.0);
                if (num9 < JukeboxX - Player.lastTileRangeX || num9 > JukeboxX + Player.lastTileRangeX + 1 ||
                    num10 < JukeboxY - Player.lastTileRangeY || num10 > JukeboxY + Player.lastTileRangeY + 1)
                {
                    SoundEngine.PlaySound(SoundID.MenuClose);
                    DisplayJukeboxInterface = false;
                    Player.dropItemCheck();
                }
            }

            if (!Main.playerInventory || Player.GetModPlayer<AvalonHerbologyPlayer>().DisplayHerbologyMenu)
            {
                DisplayJukeboxInterface = false;
            }
        }
        public override void PostUpdateEquips()
        {
            if (PlayingATrack && Main.musicBox2 == -1)
            {
                Main.SceneMetrics.ActiveMusicBox = PlayingATrackID;
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag["Avalon:CurrentTrackID"] = PlayingATrackID;
        }
        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("Avalon:CurrentTrackID"))
            {
                PlayingATrackID = tag.Get<int>("Avalon:CurrentTrackID");
            }
        }
    }
}
