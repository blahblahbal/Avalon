using Avalon.Common.Players;
using Avalon.Particles;
using Avalon.Particles.OldParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common
{
    public class BiomeEffects : ModPlayer
    {
        public override void PostUpdate()
        {
            if (Player.whoAmI == Main.myPlayer && ModContent.GetInstance<AvalonClientConfig>().BiomeParticlesEnabled)
            {
                if (ExxoAvalonOrigins.Depths != null && (bool)ExxoAvalonOrigins.Depths.Call("InDepths", Player)) //for the future //THE FUTURE IS NOW OLD MAN
                {
                    return;
                }

                if (Player.position.Y > (Main.UnderworldLayer - 100) * 16)
                {
                    LegacyParticleDeleteSoon particleType = new HellEmbers();
                    if ((Player.GetModPlayer<AvalonBiomePlayer>().ZoneNearHellcastle || Player.GetModPlayer<AvalonBiomePlayer>().ZoneHellcastle) && !Main.rand.NextBool(6))
                    {
                        particleType = new SoulEmbers();
                    }

                    if (Main.rand.NextBool(20))
                    OldParticleSystemDeleteSoon.AddParticle(particleType, new Vector2(Main.rand.Next(-2000,2000) + Player.position.X, MathHelper.Clamp(Player.position.Y + 600,Main.UnderworldLayer * 16, (Main.maxTilesY - 37) * 16)), new Vector2(Main.rand.NextFloat(-5,5), -1), default);
                    if(Main.rand.NextBool(3) && Player.position.Y > (Main.UnderworldLayer - 40) * 16)
                        OldParticleSystemDeleteSoon.AddParticle(particleType, new Vector2(Main.rand.Next(-2000, 2000) + Player.position.X, MathHelper.Clamp(Player.position.Y + 600, Main.UnderworldLayer * 16, (Main.maxTilesY - 37) * 16)), new Vector2(Main.rand.NextFloat(-5, 5), -1), default);
                }
            }
        }
    }
}
