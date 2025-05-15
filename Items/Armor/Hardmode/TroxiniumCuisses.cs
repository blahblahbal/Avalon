using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
public class TroxiniumCuisses : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(13);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 2, 30);
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

		LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
		{
			Texture = ModContent.Request<Texture2D>(Texture + "_Legs_Glow"),
			Color = (PlayerDrawSet drawInfo) => color
		});
	}
	public override void UpdateEquip(Player player)
	{
		player.GetCritChance(DamageClass.Generic) += 5;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 18)
			.AddTile(TileID.MythrilAnvil).Register();
	}
}
