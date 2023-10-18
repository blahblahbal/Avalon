using Avalon.Common;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class DeathMessages : ModHook
{
    protected override void Apply() => On_Lang.CreateDeathMessage += OnCreateDeathMessage;

    private static NetworkText OnCreateDeathMessage(
        On_Lang.orig_CreateDeathMessage orig, string deadPlayerName, int plr = -1, int npc = -1, int proj = -1,
        int other = -1, int projType = 0, int plrItemType = 0)
    {
        NetworkText networkText = NetworkText.Empty;
        NetworkText networkText2 = NetworkText.Empty;
        NetworkText networkText3 = NetworkText.Empty;
        NetworkText networkText4 = NetworkText.Empty;
        if (proj >= 0)
        {
            networkText = NetworkText.FromKey(Lang.GetProjectileName(projType).Key);
        }

        if (npc >= 0)
        {
            networkText2 = Main.npc[npc].GetGivenOrTypeNetName();
        }

        if (plr >= 0 && plr < 255)
        {
            networkText3 = NetworkText.FromLiteral(Main.player[plr].name);
        }

        if (plrItemType >= 0)
        {
            networkText4 = NetworkText.FromKey(Lang.GetItemName(plrItemType).Key);
        }

        bool flag = networkText != NetworkText.Empty;
        bool flag2 = plr >= 0 && plr < 255;
        bool flag3 = networkText2 != NetworkText.Empty;
        NetworkText result = NetworkText.Empty;
        NetworkText empty = NetworkText.Empty;
        empty = NetworkText.FromKey(Language.RandomFromCategory("DeathTextGeneric").Key, deadPlayerName,
            Main.worldName);
        if (Main.rand.NextBool(4))
        {
            int msg = Main.rand.Next(4);
            if (msg == 0)
            {
                empty = NetworkText.FromKey("Mods.Avalon.DeathText.Generic_1", deadPlayerName);
            }
            if (msg == 1)
            {
                empty = NetworkText.FromKey("Mods.Avalon.DeathText.Generic_2", deadPlayerName);
            }
            if (msg == 2)
            {
                empty = NetworkText.FromKey("Mods.Avalon.DeathText.Generic_3", deadPlayerName);
            }
            if (msg == 3)
            {
                empty = NetworkText.FromKey("Mods.Avalon.DeathText.Generic_4", deadPlayerName);
            }
        }

        if (flag2)
        {
            result = NetworkText.FromKey("DeathSource.Player", empty, networkText3, flag ? networkText : networkText4);
        }
        else if (flag3)
        {
            result = NetworkText.FromKey("DeathSource.NPC", empty, networkText2);
        }
        else if (flag)
        {
            result = NetworkText.FromKey("DeathSource.Projectile", empty, networkText);
        }
        else
        {
            #region falling
            if (other == 0)
            {
                switch (Main.rand.Next(16) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Fell_1", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("DeathText.Fell_2", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_1", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_2", deadPlayerName);
                        break;
                    case 5:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_3", deadPlayerName);
                        break;
                    case 6:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_4", deadPlayerName);
                        break;
                    case 7:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_5", deadPlayerName);
                        break;
                    case 8:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_6", deadPlayerName);
                        break;
                    case 9:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_7", deadPlayerName, Main.worldName);
                        break;
                    case 10:
                        result = NetworkText.FromKey("DeathText.Fell_3", deadPlayerName);
                        break;
                    case 11:
                        result = NetworkText.FromKey("DeathText.Fell_4", deadPlayerName);
                        break;
                    case 12:
                        result = NetworkText.FromKey("DeathText.Fell_5", deadPlayerName);
                        break;
                    case 13:
                        result = NetworkText.FromKey("DeathText.Fell_6", deadPlayerName);
                        break;
                    case 14:
                        result = NetworkText.FromKey("DeathText.Fell_7", deadPlayerName);
                        break;
                    case 15:
                        result = NetworkText.FromKey("DeathText.Fell_8", deadPlayerName);
                        break;
                    case 16:
                        result = NetworkText.FromKey("DeathText.Fell_9", deadPlayerName);
                        break;
                }
            }
            #endregion falling
            #region drowing
            else if (other == 1)
            {
                switch (Main.rand.Next(10) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Drowned_1", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("DeathText.Drowned_2", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("DeathText.Drowned_3", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("DeathText.Drowned_4", deadPlayerName);
                        break;
                    case 5:
                        result = NetworkText.FromKey("DeathText.Drowned_5", deadPlayerName);
                        break;
                    case 6:
                        result = NetworkText.FromKey("DeathText.Drowned_6", deadPlayerName);
                        break;
                    case 7:
                        result = NetworkText.FromKey("DeathText.Drowned_7", deadPlayerName);
                        break;
                    case 8:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Drowned_1", deadPlayerName);
                        break;
                    case 9:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Drowned_2", deadPlayerName);
                        break;
                    case 10:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Drowned_3", deadPlayerName);
                        break;
                }
            }
            #endregion drowing
            #region lava
            else if (other == 2)
            {
                switch (Main.rand.Next(8) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Lava_1", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("DeathText.Lava_2", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("DeathText.Lava_3", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("DeathText.Lava_4", deadPlayerName);
                        break;
                    case 5:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Lava_1", deadPlayerName);
                        break;
                    case 6:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Lava_2", deadPlayerName);
                        break;
                    case 7:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Lava_3", deadPlayerName);
                        break;
                    case 8:
                        result = NetworkText.FromKey("DeathText.Lava_5", deadPlayerName);
                        break;
                }
            }
            #endregion lava
            #region petrification
            else if (other == 5)
            {
                switch (Main.rand.Next(5) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Petrified_1", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("DeathText.Petrified_2", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("DeathText.Petrified_3", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("DeathText.Petrified_4", deadPlayerName);
                        break;
                    case 5:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Petrified_1", deadPlayerName);
                        break;
                }
            }
            #endregion petrification
            #region electrocution
            else if (other == 10)
            {
                switch (Main.rand.Next(6) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Electrocuted_1", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("DeathText.Electrocuted_2", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("DeathText.Electrocuted_3", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("DeathText.Electrocuted_4", deadPlayerName);
                        break;
                    case 5:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Electrocuted_1", deadPlayerName);
                        break;
                    case 6:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Electrocuted_2", deadPlayerName);
                        break;
                }
            }
            #endregion electrocution
            else
            {
                switch (other)
                {
                    case 3:
                        result = NetworkText.FromKey("DeathText.Default", empty);
                        break;
                    case 4:
                        result = NetworkText.FromKey("DeathText.Slain", deadPlayerName);
                        break;
                    case 5:
                        result = NetworkText.FromKey("DeathText.Petrified_" + (Main.rand.Next(4) + 1), deadPlayerName);
                        break;
                    case 6:
                        result = NetworkText.FromKey("DeathText.Stabbed", deadPlayerName);
                        break;
                    case 7:
                        result = NetworkText.FromKey("DeathText.Suffocated_" + (Main.rand.Next(2) + 1), deadPlayerName);
                        break;
                    case 8:
                        result = NetworkText.FromKey("DeathText.Burned_" + (Main.rand.Next(4) + 1), deadPlayerName);
                        break;
                    case 9:
                        result = NetworkText.FromKey("DeathText.Poisoned", deadPlayerName);
                        break;
                    case 10:
                        result = NetworkText.FromKey("DeathText.Electrocuted_" + (Main.rand.Next(4) + 1), deadPlayerName);
                        break;
                    case 11:
                        result = NetworkText.FromKey("DeathText.TriedToEscape", deadPlayerName);
                        break;
                    case 12:
                        result = NetworkText.FromKey("DeathText.WasLicked_" + (Main.rand.Next(2) + 1), deadPlayerName);
                        break;
                    case 13:
                        result = NetworkText.FromKey("DeathText.Teleport_1", deadPlayerName);
                        break;
                    case 14:
                        result = NetworkText.FromKey("DeathText.Teleport_2_Male", deadPlayerName);
                        break;
                    case 15:
                        result = NetworkText.FromKey("DeathText.Teleport_2_Female", deadPlayerName);
                        break;
                    case 16:
                        result = NetworkText.FromKey("DeathText.Inferno", deadPlayerName);
                        break;
                    case 17:
                        result = NetworkText.FromKey("DeathText.DiedInTheDark", deadPlayerName);
                        break;
                    case 18:
                        result = NetworkText.FromKey("DeathText.Starved_" + (Main.rand.Next(3) + 1), deadPlayerName);
                        break;
                    case 19:
                        result = NetworkText.FromKey("DeathText.Space_" + (Main.rand.Next(5) + 1), deadPlayerName, Main.worldName);
                        break;
                    case 254:
                        result = NetworkText.Empty;
                        break;
                    case 255:
                        result = NetworkText.FromKey("DeathText.Slain", deadPlayerName);
                        break;
                }
            }
        }

        return result;
    }
}