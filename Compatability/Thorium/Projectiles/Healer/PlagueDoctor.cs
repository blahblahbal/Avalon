using Avalon.Compatability.Thorium.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Projectiles.Scythe;
using ThoriumMod.Utilities;

namespace Avalon.Compatability.Thorium.Projectiles.Healer
{
    [ExtendsFromMod("ThoriumMod")]
    public class PlagueDoctor : ScythePro
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false; // ModLoader.HasMod("ThoriumMod");
        }
        public override void SafeSetDefaults()
        {
            Projectile.idStaticNPCHitCooldown = 15;
            Projectile.Size = new Vector2(100f);
            dustType = DustID.DryadsWard;
            dustOffset = new Vector2(-44,0);
        }
        public override void SafeOnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            foreach (Player player in Main.player)
            {
                if(player.whoAmI != Projectile.owner && player.Distance(Projectile.Center) < 400 && player.active && !target.SpawnedFromStatue)
                {
                    player.AddPVPBuff(ModContent.BuffType<PathogenBoost>(),60 * 5);
                }
            }
        }
        public override void ModifyDust(Dust dust, Vector2 position, int scytheIndex)
        {
            dust.velocity += new Vector2(2 * Main.player[Projectile.owner].direction * (scytheIndex == 0 ? 1 : -1), 0).RotatedBy(Projectile.rotation);
        }
    }
}
