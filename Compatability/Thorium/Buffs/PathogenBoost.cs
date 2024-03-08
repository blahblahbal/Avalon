using Avalon.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Buffs
{
    public class PathogenBoost : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            rare = 3;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PathogenBoostPlayer>().PathogenBoost = true;
        }
    }
    public class PathogenBoostPlayer : ModPlayer
    {
        public bool PathogenBoost;
        public override void ResetEffects()
        {
            PathogenBoost = false;
        }
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            if (!PathogenBoost) return;

            if (victim is Player player)
            {
                player.AddBuff(ModContent.BuffType<Pathogen>(), 60 * 3);
            }
            else if (victim is NPC npc)
            {
                npc.AddBuff(ModContent.BuffType<Pathogen>(), 60 * 3);
            }
        }
    }
}
