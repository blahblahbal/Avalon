using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers
{
    public class ForceField : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.SolarShield);
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player p = drawInfo.drawPlayer;
            if (p.HasBuff(ModContent.BuffType<Buffs.ForceField>()) || p.HasBuff(ModContent.BuffType<Buffs.AdvancedBuffs.AdvForceField>()))
            {
                p.GetModPlayer<AvalonPlayer>().ForceFieldRotation += 0.1f;
                if (p.GetModPlayer<AvalonPlayer>().ForceFieldRotation >= Math.PI * 2) p.GetModPlayer<AvalonPlayer>().ForceFieldRotation -= (float)Math.PI * 2f;
                Texture2D tex = Mod.Assets.Request<Texture2D>("Assets/Textures/ForceField").Value;

                Vector2 origin = new Vector2(tex.Width / 2, tex.Height / 2);
                DrawData item = new DrawData(tex, GetDrawPosition(p.position, origin, p.width, p.height, tex.Width, tex.Height, 1, 1), new Rectangle(0, 0, tex.Width, tex.Height), Color.White, p.GetModPlayer<AvalonPlayer>().ForceFieldRotation, origin, 1, SpriteEffects.None);
                drawInfo.DrawDataCache.Add(item);
            }
        }
        public static Vector2 GetDrawPosition(Vector2 position, Vector2 origin, int width, int height, int texWidth, int texHeight, int framecount, float scale)
        {
            return position - Main.screenPosition + new Vector2(width * 0.5f, height) - new Vector2(texWidth * scale / 2f, texHeight * scale / (float)framecount) + (origin * scale) + new Vector2(0f, 5f);
        }
    }
}
