using Avalon.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Common
{
    public class AvalonGlobalBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (type == BuffID.MonsterBanner)
            {
                tip = tip.Replace(Language.GetTextValue("BuffDescription.MonsterBanner"), Language.GetTextValue("Mods.Avalon.TooltipEdits.MonsterBanner"));
            }

            if (Data.Sets.Buffs.Elixir[type])
            {
                rare = ModContent.RarityType<ElixirBuffNameRarity>();
            }

            if (Main.LocalPlayer.ownedProjectileCounts[ProjectileID.StormTigerGem] > 0 && type == BuffID.StormTiger)
            {
                tip += "\n" + Language.GetTextValue("Mods.Avalon.TooltipEdits.UpgradeStage") + Main.LocalPlayer.ownedProjectileCounts[ProjectileID.StormTigerGem];
            }

            if (Main.LocalPlayer.ownedProjectileCounts[ProjectileID.AbigailCounter] > 0 && type == BuffID.AbigailMinion)
            {
                tip += "\n" + Language.GetTextValue("Mods.Avalon.TooltipEdits.UpgradeStage") + Main.LocalPlayer.ownedProjectileCounts[ProjectileID.AbigailCounter];
            }

            if (Main.LocalPlayer.ownedProjectileCounts[ProjectileID.StardustDragon1] > 0 && type == BuffID.StardustDragonMinion)
            {
                tip += "\n" + Language.GetTextValue("Mods.Avalon.TooltipEdits.UpgradeStage") + Main.LocalPlayer.ownedProjectileCounts[ProjectileID.StardustDragon2];
            }

            base.ModifyBuffText(type, ref buffName, ref tip, ref rare);
        }
    }
}
