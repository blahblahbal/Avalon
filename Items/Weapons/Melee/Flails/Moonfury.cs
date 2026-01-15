using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Projectiles.Melee.Flails;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Flails;

public class Moonfury : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}
	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<MoonfuryProj>(), 35, 6.75f, 42, 12f);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 4);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.BlueMoon)
			.AddIngredient(ItemID.Sunfury)
			.AddIngredient(ItemID.BallOHurt)
			.AddIngredient(ModContent.ItemType<Sporalash>())
			.AddTile(TileID.DemonAltar).Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.BlueMoon)
			.AddIngredient(ItemID.Sunfury)
			.AddIngredient(ItemID.TheMeatball)
			.AddIngredient(ModContent.ItemType<Sporalash>())
			.AddTile(TileID.DemonAltar).Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.BlueMoon)
			.AddIngredient(ItemID.Sunfury)
			.AddIngredient(ModContent.ItemType<TheCell>())
			.AddIngredient(ModContent.ItemType<Sporalash>())
			.AddTile(TileID.DemonAltar).Register();
	}
}
