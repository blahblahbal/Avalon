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

namespace Avalon.Common
{
    public class OldAvalonSavesConverter : ModSystem
    {
        /*private static ILog Avalog => ModContent.GetInstance<ExxoAvalonOrigins>().Logger;

        private void TestError()
        {
            Avalog.Debug("Avalon: And error occured when transporting avalon files to tmod");
        }

        public override void PostSetupContent()
        {
            string PlayerPath = Path.Combine(Main.PlayerPath + @"\");
            if (!Main.dedServ)
            {
                try
                {
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
                                    if (Path.HasExtension(Path.Combine(PlayerPath + fileName)))
                                    {
                                        Avalog.Debug("Avalon: Player already exists at: " + Path.Combine(PlayerPath + fileName));
                                        string AvalonPurgatory = Path.Combine(Main.SavePath + @"\Mods" + @"\Avalon Purgatory");
                                        Directory.CreateDirectory(AvalonPurgatory);
                                        if (Directory.Exists(AvalonPurgatory))
                                        {
                                            File.Copy(filePathArray[fileCount], Path.Combine(AvalonPurgatory + @"\" + fileName), false);
                                            if (Path.HasExtension(Path.Combine(AvalonPurgatory + @"\" + fileName)))
                                            {
                                                PlayerFileData plr = Player.GetFileData(Path.Combine(AvalonPurgatory + @"\" + fileName), false);
                                                plr.Rename("ExxoAvalonV2:" + plr.Name);
                                                File.Move(Path.Combine(AvalonPurgatory + @"\" + fileName), Path.Combine(PlayerPath + "ExxoAvalon" + fileNameRaw + ".plr"));
                                                File.Move(Path.Combine(AvalonPurgatory + @"\" + fileNameRaw + ".tplr"), Path.Combine(PlayerPath + "ExxoAvalon" + fileNameRaw + ".tplr"));
                                                File.Delete(Path.Combine(AvalonPurgatory + @"\" + fileName + ".bak"));
                                            }
                                        }
                                        Directory.Delete(AvalonPurgatory);
                                    }
                                    else
                                    {
                                        File.Copy(filePathArray[fileCount], Path.Combine(PlayerPath + fileName), false);
                                    }
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
        }*/
    }
}
