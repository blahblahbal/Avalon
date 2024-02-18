using Avalon.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common
{
    public class AvalonGlobalBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (Data.Sets.Buffs.Elixir[type])
            {
                rare = ModContent.RarityType<ElixirBuffNameRarity>();
            }

            if (Main.LocalPlayer.ownedProjectileCounts[ProjectileID.StormTigerGem] > 0 && type == BuffID.StormTiger)
            {
                tip += "\nUpgrade stage: " + Main.LocalPlayer.ownedProjectileCounts[ProjectileID.StormTigerGem];
            }

            if (Main.LocalPlayer.ownedProjectileCounts[ProjectileID.AbigailCounter] > 0 && type == BuffID.AbigailMinion)
            {
                tip += "\nUpgrade stage: " + Main.LocalPlayer.ownedProjectileCounts[ProjectileID.AbigailCounter];
            }

            base.ModifyBuffText(type, ref buffName, ref tip, ref rare);
        }
        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (player.meleeEnchant > 0)
            {
                for (int i = 0; i < player.buffType.Length; i++)
                {
                    if (BuffID.Sets.IsAFlaskBuff[player.buffType[i]] && player.buffType[i] == ModContent.BuffType<Buffs.ImbuePathogen>())
                    {
                        player.DelBuff(i);
                        break;
                    }
                }
            }
        }
    }
}
