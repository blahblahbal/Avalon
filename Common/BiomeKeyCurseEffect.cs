using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common
{
	public class BiomeKeyCurseEffect : GlobalItem
	{
		public override bool AppliesToEntity(Item entity, bool lateInstantiation)
		{
			return entity.netID is ItemID.CorruptionKey or ItemID.CrimsonKey or ItemID.DungeonDesertKey or ItemID.FrozenKey or ItemID.JungleKey or ItemID.HallowedKey || entity.type == ModContent.ItemType<Items.Other.ContagionKey>() || entity.type == ModContent.ItemType<Items.Other.UnderworldKey>();
		}
		public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (!NPC.downedPlantBoss)
			{
				var texture = TextureAssets.Item[item.type];
				float interval = 60;
				float amount = (float)(Main.timeForVisualEffects % interval) / interval;
				float amount2 = (float)((Main.timeForVisualEffects + interval / 2) % interval) / interval;
				for (int i = 0; i < 4; i++)
				{
					spriteBatch.Draw(texture.Value, position + new Vector2(0, 2 + 4 * amount2).RotatedBy(i * MathHelper.PiOver2 + MathHelper.PiOver4 + Main.timeForVisualEffects * 0.01f) + Vector2.UnitY * amount2 * -4, new Rectangle(0, 0, texture.Width(), texture.Height()), Color.Orange with { A = 0 } * (1f - amount2) * amount2, 0, origin, scale, SpriteEffects.None, 0f);
					spriteBatch.Draw(texture.Value, position + new Vector2(0, 2 + 4 * amount).RotatedBy(i * MathHelper.PiOver2 + Main.timeForVisualEffects * 0.01f) + Vector2.UnitY * amount * -4, new Rectangle(0, 0, texture.Width(), texture.Height()), Color.Magenta with { A = 0 } * (1f - amount) * amount, 0, origin, scale, SpriteEffects.None, 0f);
				}
			}
			return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
		public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			if (!NPC.downedPlantBoss)
			{
				var texture = TextureAssets.Item[item.type];
				Vector2 vector = texture.Size() / 2f;
				Vector2 value = new((float)(item.width / 2) - vector.X, item.height - texture.Height());
				Vector2 vector2 = item.position - Main.screenPosition + vector + value;
				float interval = 60;
				float amount = (float)(Main.timeForVisualEffects % interval) / interval;
				float amount2 = (float)((Main.timeForVisualEffects + interval / 2) % interval) / interval;
				for (int i = 0; i < 4; i++)
				{
					spriteBatch.Draw(texture.Value, vector2 + new Vector2(0, 2 + 4 * amount2).RotatedBy(i * MathHelper.PiOver2 + MathHelper.PiOver4 + Main.timeForVisualEffects * 0.01f) + Vector2.UnitY * amount2 * -4, new Rectangle(0, 0, texture.Width(), texture.Height()), Color.Orange with { A = 0 } * (1f - amount2) * amount2, rotation, vector, scale, SpriteEffects.None, 0f);
					spriteBatch.Draw(texture.Value, vector2 + new Vector2(0,2 + 4 * amount).RotatedBy(i * MathHelper.PiOver2 + Main.timeForVisualEffects * 0.01f) + Vector2.UnitY * amount * -4, new Rectangle(0, 0, texture.Width(), texture.Height()), Color.Magenta with { A = 0 } * (1f - amount) * amount, rotation, vector, scale, SpriteEffects.None, 0f);
				}
			}
			return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
		}
	}
}
