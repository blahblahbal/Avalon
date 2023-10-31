using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Avalon.Compatability.Achievements
{
    internal class AchievementsSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ModLoader.TryGetMod("TMLAchievements", out Mod achievements))
            {
                return;
            }

            //achievements.Call("AddAchievement", this, "AchievementName", AchievementCategory.Collector, "Avalon/Assets/Textures/Achievements/AeonsEternity", null, false, false, 2.5f, new string[] { "Collect_" + ModContent.ItemType<Items.Weapons.Melee.PreHardmode.AeonsEternity>() });
        }
    }
}
