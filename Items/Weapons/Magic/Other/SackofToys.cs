using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Core;
using Avalon.Projectiles.Magic.Other;
using Avalon.Projectiles.Ranged.Misc;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Other;

public class SackofToys : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<Toys_Lego>(), 28, 1.5f, 9f, 20, consumable: false);
		Item.DamageType = DamageClass.Magic;
		Item.noUseGraphic = false;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 10);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		(type, float velocityMult) = Main.rand.NextFromList(
		[
			(ModContent.ProjectileType<Toys_Marble>(), 1.3f),
			(ModContent.ProjectileType<Toys_Die>(), 1.3f),
			(ModContent.ProjectileType<Toys_Lego>(), 1.15f),
			(ModContent.ProjectileType<Toys_Monkey>(), 1.15f),
			(ModContent.ProjectileType<Pot>(), 0.85f),
			(Main.rand.NextFromList([ModContent.ProjectileType<Toys_PlushDoll>(), ModContent.ProjectileType<Toys_PlushTeddy>(), ModContent.ProjectileType<Toys_PlushSanta>()]), 1f),
			(Main.rand.NextBool(8) ? ModContent.ProjectileType<Toys_Table>() : ModContent.ProjectileType<Toys_RockingHorse>(), 0.75f),
			(ModContent.ProjectileType<Toys_Ball>(), 1f)
		]
		);
		velocity *= velocityMult;

		velocity = AvalonUtils.GetShootSpread(velocity, position, ContentSamples.ItemsByType[Type].shootSpeed * velocityMult, MathF.PI / 8f, random: true);
	}
	public override Vector2? HoldoutOffset()
	{
		return Vector2.Zero;
	}
	public override bool ModifyItemDraw(ref PlayerDrawSet drawInfo, ref DrawData drawData, ref DrawData? coloredDrawData, ref DrawData? glowMaskDrawData)
	{
		drawData.texture = AssetReferences.Items.Weapons.Magic.Other.SackofToysOpen.Asset.Value;
		return base.ModifyItemDraw(ref drawInfo, ref drawData, ref coloredDrawData, ref glowMaskDrawData);
	}
}