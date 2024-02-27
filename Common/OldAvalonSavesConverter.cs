using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using log4net;
using System.Reflection.Metadata;
using Terraria.IO;
using Avalon.Common.Players;
using static Avalon.Common.Players.AvalonHerbologyPlayer;
using Avalon.Items.Tools;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Potions.Buff;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Armor.Hardmode;
using Avalon.Items.Tools.Hardmode;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Magic.Hardmode;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;
using Terraria.Social;
using Terraria.Utilities;
using System.Security.Cryptography;
using System.Reflection;

namespace Avalon.Common
{
    public class OldAvalonSavesConverter : ModSystem
    {
        /*private static ILog Avalog => ModContent.GetInstance<ExxoAvalonOrigins>().Logger;

        private static void TestError()
        {
            Avalog.Debug("Avalon: An error occured when transporting Avalon files to tML");
        }

        public override void PostSetupContent()
        {
            string PlayerPath = Path.Combine(Main.PlayerPath + @"\");
            if (!Main.dedServ)
            {
                try
                {
                    string AvalonPurgatory = Path.Combine(Main.SavePath + @"\Mods" + @"\Avalon Purgatory");
                    if (Directory.Exists(AvalonPurgatory))
                    {
                        DirectoryInfo DirInf = new(AvalonPurgatory);
                        foreach (FileInfo fileInf in DirInf.GetFiles())
                        {
                            fileInf.Delete();
                        }
                        Directory.Delete(AvalonPurgatory);
                    }
                    string MyGamesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games");
                    string ExxoAvalonV2Path = Path.Combine(MyGamesPath, "Exxo Avalon V2.0");
                    string ExxoAvalonV2Player = Path.Combine(ExxoAvalonV2Path, "Players");
                    string ExxoAvalonV2World = Path.Combine(ExxoAvalonV2Path, "Worlds");
                    if (Directory.Exists(MyGamesPath))
                    {
                        if (Directory.Exists(ExxoAvalonV2Path))
                        {
                            if (Directory.Exists(ExxoAvalonV2Player))
                            {
                                for (int fileCount = 0; fileCount < Directory.GetFiles(ExxoAvalonV2Player, "*.plr").Length; fileCount++)
                                {

                                    string[] filePathArray = Directory.GetFiles(ExxoAvalonV2Player, "*.plr");
                                    string fileNameRaw = Path.GetFileNameWithoutExtension(filePathArray[fileCount]);
                                    string fileName = Path.Combine(fileNameRaw + ".plr");

                                    Directory.CreateDirectory(AvalonPurgatory);
                                    if (Directory.Exists(AvalonPurgatory))
                                    {
                                        File.Copy(filePathArray[fileCount], Path.Combine(AvalonPurgatory + @"\" + fileName), false);
                                        if (Path.HasExtension(Path.Combine(AvalonPurgatory + @"\" + fileName)))
                                        {
                                            //PlayerFileData plr = Player.GetFileData(Path.Combine(AvalonPurgatory + @"\" + fileName), false);
                                            //plr.Rename("ExxoAvalonV2:" + plr.Name);
                                           
                                            Player.SavePlayer(GetFileData(Path.Combine(AvalonPurgatory + @"\" + fileName), false));
                                            File.Move(Path.Combine(AvalonPurgatory + @"\" + fileName), Path.Combine(PlayerPath + "ExxoAvalon" + fileNameRaw + ".plr"), true);
                                            File.Move(Path.Combine(AvalonPurgatory + @"\" + fileNameRaw + ".tplr"), Path.Combine(PlayerPath + "ExxoAvalon" + fileNameRaw + ".tplr"), true);
                                            File.Delete(Path.Combine(AvalonPurgatory + @"\" + fileName + ".bak"));
                                        }
                                    }
                                    Directory.Delete(AvalonPurgatory);

                                    Avalog.Debug("Avalon: PLAYER SUCSESS!, Path: " + filePathArray[fileCount]);
                                }
                               
                            }
                            if (Directory.Exists(ExxoAvalonV2World))
                            {
                                for (int fileCount = 0; fileCount < Directory.GetFiles(ExxoAvalonV2World, "*.wld").Length; fileCount++)
                                {
                                    string[] filePathArray = Directory.GetFiles(ExxoAvalonV2World, "*.wld");
                                    ModContent.GetInstance<ExxoAvalonOrigins>().Logger.Debug("Avalon: WORLD SUCSESS!, Path: " + filePathArray[fileCount]);
                                }
                            }
                        }
                        else
                        {
                            Avalog.Debug("Avalon (1.2 Avalon Converter): Could not find 1.2 Avalon Saves folder (Exxo Avalon V2.0)");
                        }
                    }
                    else
                    {
                        Avalog.Debug("Avalon (1.2 Avalon Converter): How are you playing without a My Game folder?");
                    }
                }
                catch
                {
                    TestError();
                }
            }
            else
            {
                TestError();
            }
        }


        public static PlayerFileData GetFileData(string file, bool cloudSave)
        {
            if (file == null || (cloudSave && SocialAPI.Cloud == null))
            {
                return null;
            }
            PlayerFileData playerFileData = LoadPlayer(file);
            if (playerFileData.Player != null)
            {
                if (playerFileData.Player.loadStatus != 0 && playerFileData.Player.loadStatus != 1)
                {
                    CustomModDataException customDataFail = playerFileData.customDataFail;
                    if (FileUtilities.Exists(file + ".bak", cloudSave))
                    {
                        FileUtilities.Move(file + ".bak", file, cloudSave);
                        //PlayerIO.LoadBackup(file, cloudSave);
                    }
                    playerFileData = LoadPlayer(file);
                    if (playerFileData.Player == null)
                    {
                        return null;
                    }
                    playerFileData.customDataFail = customDataFail;
                }
                return playerFileData;
            }
            return null;
        }

        public static PlayerFileData LoadPlayer(string playerPath)
        {
            PlayerFileData playerFileData = new PlayerFileData(playerPath, false);
            if (Main.rand == null)
            {
                Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
            }
            byte[] plrData = FileUtilities.ReadAllBytes(playerPath, false);
            byte[] tplrData = null; //Assembly.GetExecutingAssembly().GetType(name: "SystemIO").ReadDataBytes(playerPath, false);
            return LoadPlayer2(playerFileData, plrData, tplrData);
        }

        internal static byte[] ENCRYPTION_KEY = new UnicodeEncoding().GetBytes("h3y_gUyZ");

        public static PlayerFileData LoadPlayer2(PlayerFileData playerFileData, byte[] plrData, byte[] tplrData)
        {
            string playerPath = playerFileData.Path;
            Player player = new Player();
            bool gotToReadName = false;
            using (new Main.CurrentPlayerOverride(player))
            {
                try
                {
                    RijndaelManaged rijndaelManaged = new RijndaelManaged();
                    rijndaelManaged.Padding = PaddingMode.None;
                    using (MemoryStream stream = new MemoryStream(plrData))
                    {
                        using CryptoStream input = new CryptoStream(stream, rijndaelManaged.CreateDecryptor(ENCRYPTION_KEY, ENCRYPTION_KEY), CryptoStreamMode.Read);
                        using BinaryReader binaryReader = new BinaryReader(input);
                        int num = binaryReader.ReadInt32();
                        if (num > Main.curRelease)
                        {
                            player.loadStatus = 1;
                            player.name = binaryReader.ReadString();
                        }
                        player.name = binaryReader.ReadString();
                        if (num >= 10)
                        {
                            if (num >= 17)
                            {
                                player.difficulty = binaryReader.ReadByte();
                            }
                            else
                            {
                                bool flag2 = binaryReader.ReadBoolean();
                                if (flag2)
                                {
                                    player.difficulty = 2;
                                }
                            }
                        }
                        player.hair = binaryReader.ReadInt32();
                        if (num >= 82)
                        {
                            player.hairDye = binaryReader.ReadByte();
                        }
                        if (num >= 83)
                        {
                            //player.hideVisual = binaryReader.ReadByte();
                            BitsByte b = binaryReader.ReadByte();
                            for (int k = 0; k < 8; k++)
                            {
                                player.hideVisibleAccessory[k] = b[k];
                            }
                        }
                        if (num <= 17)
                        {
                            if (player.hair == 5 || player.hair == 6 || player.hair == 9 || player.hair == 11)
                            {
                                player.Male = false;
                            }
                            else
                            {
                                player.Male = true;
                            }
                        }
                        else
                        {
                            player.Male = binaryReader.ReadBoolean();
                        }
                        player.statLife = binaryReader.ReadInt32();
                        player.statLifeMax = binaryReader.ReadInt32();
                        if (player.statLifeMax > 700)
                        {
                            player.statLifeMax = 700;
                        }
                        player.statMana = binaryReader.ReadInt32();
                        player.statManaMax = binaryReader.ReadInt32();
                        if (player.statManaMax > 400)
                        {
                            player.statManaMax = 400;
                        }

                        // new in 1.4
                        player.ConsumedLifeCrystals = (player.statLifeMax - 100) / 20;
                        player.ConsumedLifeFruit = (player.statLifeMax - 400) / 5;
                        player.ConsumedManaCrystals = (player.statManaMax - 20) / 20;

                        player.hairColor.R = binaryReader.ReadByte();
                        player.hairColor.G = binaryReader.ReadByte();
                        player.hairColor.B = binaryReader.ReadByte();
                        player.skinColor.R = binaryReader.ReadByte();
                        player.skinColor.G = binaryReader.ReadByte();
                        player.skinColor.B = binaryReader.ReadByte();
                        player.eyeColor.R = binaryReader.ReadByte();
                        player.eyeColor.G = binaryReader.ReadByte();
                        player.eyeColor.B = binaryReader.ReadByte();
                        player.shirtColor.R = binaryReader.ReadByte();
                        player.shirtColor.G = binaryReader.ReadByte();
                        player.shirtColor.B = binaryReader.ReadByte();
                        player.underShirtColor.R = binaryReader.ReadByte();
                        player.underShirtColor.G = binaryReader.ReadByte();
                        player.underShirtColor.B = binaryReader.ReadByte();
                        player.pantsColor.R = binaryReader.ReadByte();
                        player.pantsColor.G = binaryReader.ReadByte();
                        player.pantsColor.B = binaryReader.ReadByte();
                        player.shoeColor.R = binaryReader.ReadByte();
                        player.shoeColor.G = binaryReader.ReadByte();
                        player.shoeColor.B = binaryReader.ReadByte();
                        Main.player[Main.myPlayer].shirtColor = player.shirtColor;
                        Main.player[Main.myPlayer].pantsColor = player.pantsColor;
                        Main.player[Main.myPlayer].hairColor = player.hairColor;
                        if (num >= 110)
                        {
                            player.GetModPlayer<AvalonStaminaPlayer>().StatStam = binaryReader.ReadInt32();
                            player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax = binaryReader.ReadInt32();

                            if (player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax > 300)
                            {
                                player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax = 300;
                            }
                            if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam > 300)
                            {
                                player.GetModPlayer<AvalonStaminaPlayer>().StatStam = 300;
                            }
                        }
                        if (num >= 122)
                        {
                            //player.shmAccSlot = binaryReader.ReadBoolean();
                        }
                        if (num >= 38)
                        {
                            if (num < 122)
                            {
                                int num2 = 11;
                                if (num >= 81)
                                {
                                    num2 = 16;
                                }
                                for (int i = 0; i < num2; i++)
                                {
                                    player.armor[i].netDefaults(binaryReader.ReadInt32());
                                    player.armor[i].Prefix((int)binaryReader.ReadByte());
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    player.armor[i].netDefaults(binaryReader.ReadInt32());
                                    player.armor[i].Prefix((int)binaryReader.ReadByte());
                                }
                            }
                            if (num >= 47)
                            {
                                int num3 = 3;
                                if (num >= 81)
                                {
                                    num3 = 8;
                                }
                                for (int j = 0; j < num3; j++)
                                {
                                    player.dye[j].netDefaults(binaryReader.ReadInt32());
                                    player.dye[j].Prefix((int)binaryReader.ReadByte());
                                }
                            }
                            if (num >= 58)
                            {
                                //LoadOldInventory(player.inventory);
                                for (int k = 0; k < 58; k++)
                                {
                                    // old code
                                    //int num4 = binaryReader.ReadInt32();
                                    //if (num4 >= Main.maxItemTypes)
                                    //{
                                    //    player.inventory[k].netDefaults(0);
                                    //}
                                    //else
                                    //{
                                    //    player.inventory[k].netDefaults(num4);
                                    //    player.inventory[k].stack = binaryReader.ReadInt32();
                                    //    player.inventory[k].Prefix((int)binaryReader.ReadByte());
                                    //}

                                }
                            }
                            if (num >= 58)
                            {
                                //LoadOldInventory(player.bank.item);
                                //LoadOldInventory(player.bank2.item);
                                //for (int m = 0; m < 40; m++)
                                //{
                                //    player.bank.item[m].netDefaults(binaryReader.ReadInt32());
                                //    player.bank.item[m].stack = binaryReader.ReadInt32();
                                //    player.bank.item[m].Prefix((int)binaryReader.ReadByte());
                                //}
                                //for (int n = 0; n < 40; n++)
                                //{
                                //    player.bank2.item[n].netDefaults(binaryReader.ReadInt32());
                                //    player.bank2.item[n].stack = binaryReader.ReadInt32();
                                //    player.bank2.item[n].Prefix((int)binaryReader.ReadByte());
                                //}
                            }
                        }
                        if (num >= 11)
                        {
                            int num15 = 22;
                            if (num < 74)
                            {
                                num15 = 10;
                            }
                            for (int num16 = 0; num16 < num15; num16++)
                            {
                                player.buffType[num16] = binaryReader.ReadInt32();
                                player.buffTime[num16] = binaryReader.ReadInt32();
                                if (player.buffType[num16] == 0)
                                {
                                    num16--;
                                    num15--;
                                }
                            }
                        }
                        for (int num17 = 0; num17 < 200; num17++)
                        {
                            int num18 = binaryReader.ReadInt32();
                            if (num18 == -1)
                            {
                                break;
                            }
                            player.spX[num17] = num18;
                            player.spY[num17] = binaryReader.ReadInt32();
                            player.spI[num17] = binaryReader.ReadInt32();
                            player.spN[num17] = binaryReader.ReadString();
                        }
                        if (num >= 16)
                        {
                            player.hbLocked = binaryReader.ReadBoolean();
                        }
                        if (num >= 98)
                        {
                            player.anglerQuestsFinished = binaryReader.ReadInt32();
                        }
                        if (num >= 107)
                        {
                            ModContent.GetInstance<TomeSlot>().FunctionalItem.netDefaults(binaryReader.ReadInt32());
                            //player.GetModPlayer<>.netDefaults(binaryReader.ReadInt32());
                        }
                        // shm extra accessory
                        if (num >= 123)
                        {
                            //player.eAcc.netDefaults(binaryReader.ReadInt32());
                            //int type = player.eAcc.type;
                            //if (player.eAcc.type == 908) player.lavaMax += 420;
                            //if (player.eAcc.type == 906) player.lavaMax += 420;
                            //if (player.wingsLogic == 0 && player.eAcc.wingSlot >= 0) player.wingsLogic = (int)player.eAcc.wingSlot;
                            //if (type == 158 || type == 396 || type == 1250 || type == 1251 || type == 1252)
                            //{
                            //    player.noFallDmg = true;
                            //}
                            //player.lavaTime = player.lavaMax;
                        }
                        if (num >= 128)
                        {
                            // -2 due to 2 less herbs existing in 1.2
                            for (int i = 0; i < player.GetModPlayer<AvalonHerbologyPlayer>().HerbCounts.Count - 2; i++)
                            {
                                player.GetModPlayer<AvalonHerbologyPlayer>().HerbCounts[i] = binaryReader.ReadInt32();
                            }
                        }
                        if (num >= 129)
                        {
                            player.GetModPlayer<AvalonHerbologyPlayer>().HerbTotal = binaryReader.ReadInt32();
                        }
                        if (num >= 130)
                        {
                            player.GetModPlayer<AvalonHerbologyPlayer>().Tier = (HerbTier)binaryReader.ReadInt32();
                        }
                        if (num >= 131)
                        {
                            player.GetModPlayer<AvalonHerbologyPlayer>().PotionTotal = binaryReader.ReadInt32();
                        }
                        if (num >= 132)
                        {
                            //player.eAcc.Prefix((int)binaryReader.ReadByte());
                        }
                        for (int num19 = 3; num19 < 8; num19++)
                        {
                            int type = player.armor[num19].type;
                            if (type == 908)
                            {
                                player.lavaMax += 420;
                            }
                            if (type == 906)
                            {
                                player.lavaMax += 420;
                            }
                            if (player.wingsLogic == 0 && player.armor[num19].wingSlot >= 0)
                            {
                                player.wingsLogic = (int)player.armor[num19].wingSlot;
                            }
                            if (type == 158 || type == 396 || type == 1250 || type == 1251 || type == 1252)
                            {
                                player.noFallDmg = true;
                            }
                            player.lavaTime = player.lavaMax;
                        }
                        binaryReader.Close();
                        player.PlayerFrame();
                        player.loadStatus = 0;
                        playerFileData.Player = player;
                        return playerFileData;
                    }
                }
                catch (CustomModDataException e)
                {
                    //playerFileData.customDataFail = e;
                    TestError();
                }
                catch
                {
                }
                Player player2 = new Player();
                player2.loadStatus = 2;
                if (gotToReadName && player.name.Length <= Player.nameLen)
                {
                    player2.name = player.name;
                }
                else
                {
                    player2.name = playerPath.Replace('/', Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar)[^1].Split('.')[0];
                }
                playerFileData.Player = player2;
                return playerFileData;
            }
        }

        // NOT FINISHED (DUH)
        private static void LoadOldInventory(Item[] inventory)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                inventory[i].SetDefaults(Data.Sets.Item.OldAvalonItemConversion[inventory[i].type]);
            }
        }*/
    }
}
