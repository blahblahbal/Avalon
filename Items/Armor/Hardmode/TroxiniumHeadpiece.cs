using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
public class TroxiniumHeadpiece : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(11);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 3, 40);
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

		HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
		{
			Texture = ModContent.Request<Texture2D>(Texture + "_Head_Glow"),
			Color = (PlayerDrawSet drawInfo) => color
		});
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 12)
			.AddTile(TileID.MythrilAnvil).Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<TroxiniumBodyarmor>() && legs.type == ModContent.ItemType<TroxiniumCuisses>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Troxinium", Language.GetTextValue("Mods.Avalon.RangedText"));
		//player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Troxinium.Ranged");
		player.GetModPlayer<AvalonPlayer>().HyperRanged = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Ranged) += 0.09f;
		player.GetModPlayer<AvalonPlayer>().AmmoCost85 = true;
	}
}
