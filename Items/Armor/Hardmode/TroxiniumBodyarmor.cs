using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class TroxiniumBodyarmor : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(15);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 2, 60);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
	{
		if (!Main.gameMenu)
		{
			color *= 4f;
		}
	}
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, 0);
		ItemGlowmask.GlowColors.TryGetValue(Type, out Color color);

		BodyGlowmaskPlayer.RegisterData(Item.bodySlot, () => color);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Generic) += 0.08f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 24)
			.AddTile(TileID.MythrilAnvil).Register();
	}
}
