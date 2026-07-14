using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ForsakenRelic : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 2);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		var p = player.GetModPlayer<ForesakenRelicVisuals>();
		if (player.immune && !p.Active)
		{
			player.GetCritChance(DamageClass.Generic) += 7;
			player.GetDamage(DamageClass.Generic) += 0.25f;
			p.Active = true;
		}
	}
}
public class ForesakenRelicVisuals : ModPlayer
{
	public bool Active = false;
	public override void ResetEffects()
	{
		Active = false;
	}
	public override void TransformDrawData(ref PlayerDrawSet drawInfo)
	{
		if (Active && Player.immune && drawInfo.shadow == 0)
		{
			float percent = Utils.Remap(Player.immuneTime, 0, Player.longInvince? 60 : 30, 0, 1);
			if (Main.rand.NextFloat(2) < percent)
			{
				int d = Dust.NewDust(Player.position, Player.width, Player.height, DustID.TintableDustLighted);
				Main.dust[d].noGravity = true;
				Main.dust[d].color = new Color(234, 201, 137, 0) * percent;
				Main.dust[d].velocity += Player.velocity;
				Main.dust[d].noLight = true;
				drawInfo.DustCache.Add(d);
			}
			int originalCount = drawInfo.DrawDataCache.Count;
			for (int i = 0; i < originalCount; i++)
			{
				for (int i2 = 0; i2 < 4; i2++)
				{
					var draw = drawInfo.DrawDataCache[i];
					draw.shader = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex;
					draw.color = new Color(234,201,137,0) * 0.075f * percent;
					draw.position += Vector2.UnitY.RotatedBy(i2 * MathHelper.PiOver2) * 2f * (percent + 1);
					drawInfo.DrawDataCache.Add(draw);
				}
			}
		}
	}
}
