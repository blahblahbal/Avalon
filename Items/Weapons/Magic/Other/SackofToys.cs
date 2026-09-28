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
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<Toys_Lego>(), 28, 1.5f, 10f, 20, consumable: false);
		Item.DamageType = DamageClass.Magic;
		Item.noUseGraphic = false;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 10);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		(type, float velocityMult, int amount) = Main.rand.NextFromList(
		[
			(ModContent.ProjectileType<Toys_Marble>(), 1.3f, Main.rand.Next(2, 4)),
			(ModContent.ProjectileType<Toys_Die>(), 1.3f, Main.rand.Next(2, 4)),
			(ModContent.ProjectileType<Toys_Lego>(), 1.15f, Main.rand.Next(1, 4)),
			(Main.rand.NextFromList([ModContent.ProjectileType<Pot>(), ModContent.ProjectileType<Toys_MonkeyBarrel>()]), 0.85f, 1),
			(Main.rand.NextFromList([ModContent.ProjectileType<Toys_PlushDoll>(), ModContent.ProjectileType<Toys_PlushTeddy>(), ModContent.ProjectileType<Toys_PlushSanta>()]), 1f, 1),
			(Main.rand.NextBool(8) ? ModContent.ProjectileType<Toys_Table>() : ModContent.ProjectileType<Toys_RockingHorse>(), 0.75f, 1),
			(ModContent.ProjectileType<Toys_Ball>(), 1f, 1)
		]
		);
		velocity *= velocityMult;

		for (int i = 0; i < amount; i++)
		{
			Vector2 velocityMod = AvalonUtils.GetShootSpread(velocity, position, ContentSamples.ItemsByType[Type].shootSpeed * velocityMult, MathF.PI / 8f, Main.rand.NextFloat(-2.5f, 2.5f), random: true);
			Projectile.NewProjectile(source, position, velocityMod, type, damage, knockback, player.whoAmI);
		}
		return false;
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