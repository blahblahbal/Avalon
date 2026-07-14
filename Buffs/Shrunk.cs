//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.ModLoader;

//namespace Avalon.Buffs;

//public class Shrunk : ModBuff
//{
//	public override void Update(Player player, ref int buffIndex)
//    {
//		player.GetModPlayer<ShrunkPlayer>().Shrink = 0.33f;
//	}
//}
//public class ShrunkPlayer : ModPlayer
//{
//	public float Shrink = 1f;
//	public bool NeedsToRestore = false;
//	public bool NeedsToClear = false;
//	public override void ResetEffects()
//	{
//		var b = Player.Bottom;
//		Player.width = (int)(20 * Shrink);
//		Player.height = (int)(42 * Shrink);
//		Player.Bottom = b;
//		Shrink = 1f;
//	}
//	public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
//	{
//		position.Y += 21 * -Shrink;
//	}
//	public override void ModifyItemScale(Item item, ref float scale)
//	{
//		if (!item.noUseGraphic)
//			scale /= Shrink;
//	}
//	public override void Load()
//	{
//		On_Player.ItemCheck_MeleeHitNPCs += On_Player_ItemCheck_MeleeHitNPCs;
//	}

//	private void On_Player_ItemCheck_MeleeHitNPCs(On_Player.orig_ItemCheck_MeleeHitNPCs orig, Player self, Item sItem, Rectangle itemRectangle, int originalDamage, float knockBack)
//	{
//		orig(self,sItem,Shrink == 1? itemRectangle : itemRectangle with { Width = (int)(itemRectangle.Width * Shrink), Height = (int)(itemRectangle.Height * Shrink) }, originalDamage,knockBack);
//	}

//	public override bool? CanMeleeAttackCollideWithNPC(Item item, Rectangle meleeAttackHitbox, NPC target)
//	{
//		return base.CanMeleeAttackCollideWithNPC(item, meleeAttackHitbox, target);
//	}
//	public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
//	{
//		if (Shrink != 1)
//		{
//			var b = Player.Bottom;
//			drawInfo.drawPlayer.width = 20;
//			drawInfo.drawPlayer.height = 42;
//			drawInfo.drawPlayer.Bottom = b;
//		}
//	}
//	public override void TransformDrawData(ref PlayerDrawSet drawInfo)
//	{
//		if (drawInfo.headOnlyRender)
//			return;
//		if (Shrink != 1)
//		{
//			Vector2 shrinkOrigin = drawInfo.drawPlayer.Bottom.Floor() - Main.screenPosition;
//			for (int i = 0; i < drawInfo.DrawDataCache.Count; i++)
//			{
//				var draw = drawInfo.DrawDataCache[i];
//				draw.scale *= Shrink;
//				//Vector2 textureSize = Vector2.Zero;
//				//if (draw.sourceRect is Rectangle r)
//				//	textureSize = r.Size();
//				//else
//				//	textureSize = draw.texture.Size();
//				draw.position = Vector2.Lerp(draw.position, shrinkOrigin, (1f - Shrink)) + new Vector2(0,Player.gfxOffY);
//				drawInfo.DrawDataCache[i] = draw;
//			}
//		}
//	}
//	public override void PostUpdateRunSpeeds()
//	{
//	}
//}
