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
		int[] small = [ModContent.ProjectileType<Toys_Marble>(), ModContent.ProjectileType<Toys_Die>()];
		int[] dolls = [ModContent.ProjectileType<Toys_Doll>(), ModContent.ProjectileType<Toys_Teddy>()];
		int[] large = [ModContent.ProjectileType<Toys_Table>(), ModContent.ProjectileType<Toys_RockingHorse>()];

		(type, float velocityMult) = Main.rand.Next(5) switch
		{
			0 => (Main.rand.NextFromList(small), 1.3f),
			1 => (ModContent.ProjectileType<Toys_Lego>(), 1.3f),
			2 => (ModContent.ProjectileType<Pot>(), 0.65f),
			3 => (Main.rand.NextFromList(dolls), 0.8f),
			4 => (Main.rand.NextFromList(large), 0.55f),
			_ => throw new System.NotImplementedException()
		};
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