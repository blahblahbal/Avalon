using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common
{
    public class BiomeKeyCurseEffect : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.netID is ItemID.CorruptionKey or ItemID.CrimsonKey or ItemID.DungeonDesertKey or ItemID.FrozenKey or ItemID.JungleKey or ItemID.HallowedKey || entity.type == ModContent.ItemType<Items.Other.ContagionKey>();
        }
        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (!NPC.downedPlantBoss)
            {
                Texture2D tex = TextureAssets.Item[item.type].Value;
                int offset = -1;
                if (item.netID == ItemID.CrimsonKey) offset = 0;
                if (item.netID == ItemID.DungeonDesertKey) offset = 0;
                for (int i = 0; i < 8; i++)
                {
                    Vector2 vec = new Vector2(0, 2).RotatedBy(Main.timeForVisualEffects * 0.05f + (MathHelper.PiOver4 * i)) * 1.1f;
                    spriteBatch.Draw(tex, position + new Vector2(0, offset + -Math.Abs(vec.Y * 0.5f)) + new Vector2(vec.X, vec.Y * 1.5f), frame, Color.Lerp(new Color(255, 0, 0, 0), new Color(255, 0, 255, 0), (float)Math.Sin(Main.timeForVisualEffects * 0.04f) * 0.5f + 0.5f) * 0.3f, 0, origin, scale, SpriteEffects.None, 0);
                }
            }
            return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (!NPC.downedPlantBoss)
            {
                Texture2D tex = TextureAssets.Item[item.type].Value;
                int offset = -4;
                if (item.netID == ItemID.CrimsonKey) offset = -5;
                if (item.netID == ItemID.DungeonDesertKey) offset = -6;
                for (int i = 0; i < 8; i++)
                {
                    Vector2 vec = new Vector2(0, 2).RotatedBy(Main.timeForVisualEffects * 0.05f + (MathHelper.PiOver4 * i)) * 1.1f;
                    spriteBatch.Draw(tex, item.Center - Main.screenPosition + new Vector2(0, offset + -Math.Abs(vec.Y * 0.5f)) + new Vector2(vec.X, vec.Y * 1.5f), tex.Bounds, Color.Lerp(new Color(255, 0, 0, 0), new Color(255, 0, 255, 0), (float)Math.Sin(Main.timeForVisualEffects * 0.04f) * 0.5f + 0.5f) * 0.3f, rotation, tex.Size() / 2, 1, SpriteEffects.None, 0);
                }
            }
            return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
    }
}
