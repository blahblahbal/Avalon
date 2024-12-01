using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Projectiles.Scythe;

namespace Avalon.Compatability.Thorium.Projectiles.Healer
{
    [ExtendsFromMod("ThoriumMod")]
    public class PestilentScythe : ScythePro
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.HasMod("ThoriumMod");
        }
        public override void SafeSetDefaults()
        {
            Projectile.Size = new Vector2(100f);
            dustType = ModContent.DustType<ContagionWeapons>();
            dustOffset = new Vector2(-44,0);
        }

        public override void ModifyDust(Dust dust, Vector2 position, int scytheIndex)
        {
            dust.scale = 1;
            //dust.alpha = 128;
            //dust.velocity += Main.rand.NextVector2Circular(0.3f, 0.3f);
        }
    }
}
