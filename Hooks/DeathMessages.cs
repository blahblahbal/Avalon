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
        NetworkText projName = NetworkText.Empty;
        NetworkText hostileNPCName = NetworkText.Empty;
        NetworkText pvpPlayerName = NetworkText.Empty;
        NetworkText pvpPlayerItemName = NetworkText.Empty;
        if (proj >= 0)
        {
            projName = NetworkText.FromKey(Lang.GetProjectileName(projType).Key);
        }

        if (npc >= 0)
        {
            hostileNPCName = Main.npc[npc].GetGivenOrTypeNetName();
        }

        if (plr >= 0 && plr < 255)
        {
            pvpPlayerName = NetworkText.FromLiteral(Main.player[plr].name);
        }

        if (plrItemType >= 0)
        {
            pvpPlayerItemName = NetworkText.FromKey(Lang.GetItemName(plrItemType).Key);
        }

        bool flag = projName != NetworkText.Empty;
        bool flag2 = plr >= 0 && plr < 255;
        bool flag3 = hostileNPCName != NetworkText.Empty;
        NetworkText result = NetworkText.Empty;
        NetworkText empty = NetworkText.Empty;
        empty = NetworkText.FromKey(Language.RandomFromCategory("DeathTextGeneric").Key, deadPlayerName,
            Main.worldName);
        if (Main.rand.NextBool(6))
        {
            int msg = Main.rand.Next(17) + 1;
            switch (msg)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    empty = NetworkText.FromKey("Mods.Avalon.DeathText.Generic_" + msg, deadPlayerName);
                    break;
                case 8:
                    empty = NetworkText.FromKey("Mods.Avalon.DeathText.Generic_" + msg, deadPlayerName, Main.worldName);
                    break;
            }
        }

        if (flag2)
        {
            result = NetworkText.FromKey("DeathSource.Player", empty, pvpPlayerName, flag ? projName : pvpPlayerItemName);
        }
        else if (flag3)
        {
            result = NetworkText.FromKey("DeathSource.NPC", empty, hostileNPCName);
        }
        else if (flag)
        {
            result = NetworkText.FromKey("DeathSource.Projectile", empty, projName);
        }
        else
        {
            #region falling
            if (other == 0)
            {
                switch (Main.rand.Next(20) + 1)
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
                    case 17:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_8", deadPlayerName);
                        break;
                    case 18:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_9", deadPlayerName);
                        break;
                    case 19:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_10", deadPlayerName);
                        break;
                    case 20:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Fell_11", deadPlayerName);
                        break;
                }
            }
            #endregion falling
            #region drowning
            else if (other == 1)
            {
                switch (Main.rand.Next(11) + 1)
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
                    case 11:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Drowned_4", deadPlayerName);
                        break;
                    case 12:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Drowned_5", deadPlayerName);
                        break;
                }
            }
            #endregion drowning
            #region lava
            else if (other == 2)
            {
                switch (Main.rand.Next(12) + 1)
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
                    case 9:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Lava_4", deadPlayerName);
                        break;
                    case 10:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Lava_5", deadPlayerName);
                        break;
                    case 11:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Lava_6", deadPlayerName);
                        break;
                    case 12:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Lava_7", deadPlayerName);
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
            #region poisoned
            else if (other == 9)
            {
                switch (Main.rand.Next(2) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Poisoned", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Poisoned_1", deadPlayerName);
                        break;
                }
            }
            #endregion
            #region electrocution
            else if (other == 10)
            {
                switch (Main.rand.Next(8) + 1)
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
                    case 7:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Electrocuted_3", deadPlayerName);
                        break;
                    case 8:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Electrocuted_4", deadPlayerName);
                        break;
                    case 9:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Electrocuted_5", deadPlayerName);
                        break;
                }
            }
            #endregion electrocution
            #region space
            else if (other == 19)
            {
                switch (Main.rand.Next(9) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Space_1", deadPlayerName, Main.worldName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("DeathText.Space_2", deadPlayerName, Main.worldName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("DeathText.Space_3", deadPlayerName, Main.worldName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("DeathText.Space_4", deadPlayerName, Main.worldName);
                        break;
                    case 5:
                        result = NetworkText.FromKey("DeathText.Space_5", deadPlayerName, Main.worldName);
                        break;
                    case 6:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Space_1", deadPlayerName, Main.worldName);
                        break;
                    case 7:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Space_2", deadPlayerName, Main.worldName);
                        break;
                    case 8:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Space_3", deadPlayerName, Main.worldName);
                        break;
                }
            }
            #endregion
            #region teleport
            else if (other == 13)
            {
                switch (Main.rand.Next(3) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Teleport_1", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_1", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_2", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_3", deadPlayerName);
                        break;
                }
            }
            else if (other == 14)
            {
                switch (Main.rand.Next(3) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Teleport_2_Male", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_2_Male", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_3_Male", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_4_Male", deadPlayerName);
                        break;
                }
            }
            else if (other == 15)
            {
                switch (Main.rand.Next(3) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.Teleport_2_Female", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_2_Female", deadPlayerName);
                        break;
                    case 3:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_3_Female", deadPlayerName);
                        break;
                    case 4:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Teleport_4_Female", deadPlayerName);
                        break;
                }
            }
            #endregion
            #region escape
            else if (other == 11)
            {
                switch (Main.rand.Next(2) + 1)
                {
                    case 1:
                        result = NetworkText.FromKey("DeathText.TriedToEscape", deadPlayerName);
                        break;
                    case 2:
                        result = NetworkText.FromKey("Mods.Avalon.DeathText.Escape_1", deadPlayerName);
                        break;
                }
            }
            #endregion
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
                    case 6:
                        result = NetworkText.FromKey("DeathText.Stabbed", deadPlayerName);
                        break;
                    case 7:
                        result = NetworkText.FromKey("DeathText.Suffocated_" + (Main.rand.Next(2) + 1), deadPlayerName);
                        break;
                    case 8:
                        result = NetworkText.FromKey("DeathText.Burned_" + (Main.rand.Next(4) + 1), deadPlayerName);
                        break;
                    case 12:
                        result = NetworkText.FromKey("DeathText.WasLicked_" + (Main.rand.Next(2) + 1), deadPlayerName);
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
