using Avalon;
using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Projectiles.Melee.Flails;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Flails;

public class Sporalash : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}
	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<SporalashProj>(), 21, 6.75f, 46, 10f);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.JungleSpores, 15)
			.AddIngredient(ItemID.Stinger, 10)
			.AddIngredient(ItemID.Vine, 2)
			.AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>(), 2)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.ThornWhip)
			.Register();
	}
}