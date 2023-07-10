using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers
{
    public class BondrewdHelmetGlow : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Head);
        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return true;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player p = drawInfo.drawPlayer;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (p.gravDir == 1f)
            {
                if (p.direction == 1)
                {
                    spriteEffects = SpriteEffects.None;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }

                if (!p.dead)
                {
                    p.legPosition.Y = 0f;
                    p.headPosition.Y = 0f;
                    p.bodyPosition.Y = 0f;
                }
            }
            else
            {
                if (p.direction == 1)
                {
                    spriteEffects = SpriteEffects.FlipVertically;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                }

                if (!p.dead)
                {
                    p.legPosition.Y = 6f;
                    p.headPosition.Y = 6f;
                    p.bodyPosition.Y = 6f;
                }
            }
            var vector3 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.4f);
            if (p.head == EquipLoader.GetEquipSlot(ModContent.GetInstance<ExxoAvalonOrigins>(), "BondrewdHelmet", EquipType.Head))
            {
                var value = new DrawData(Mod.Assets.Request<Texture2D>("Items/Vanity/BondrewdHelmet_Glow").Value,
                    new Vector2(
                        (int)(drawInfo.Position.X - Main.screenPosition.X - (p.bodyFrame.Width / 2) + (p.width / 2)),
                        (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.bodyFrame.Height + 4f)) +
                    p.headPosition + vector3, p.bodyFrame, Color.Lerp(Color.White,new Color(200,128,255,0),(float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.5f + 0.5f) * (drawInfo.colorArmorHead.A / 255f), p.headRotation, vector3, 1f, spriteEffects, 0);
                value.shader = drawInfo.cHead;
                drawInfo.DrawDataCache.Add(value);
            }
        }
    }
}
