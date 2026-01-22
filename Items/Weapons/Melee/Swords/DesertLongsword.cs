using Avalon.Dusts;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class DesertLongsword : ModItem
{
	public override void SetDefaults()
	{
		Item.UseSound = SoundID.Item1;
		Item.damage = 29;
		Item.scale = 1f;
		Item.rare = ItemRarityID.Green;
		Item.width = Item.height = 28;
		Item.useTime = 27;
		Item.knockBack = 3f;
		Item.DamageType = DamageClass.Melee;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = Item.sellPrice(0, 0, 54, 0);
		Item.useAnimation = 27;
		Item.useTime = 70;
		Item.shootSpeed = 3;
		Item.shoot = ModContent.ProjectileType<DesertLongswordTornado>();
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
		Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * player.direction * player.gravDir);
		Dust d = Dust.NewDustPerfect(location2, DustID.Sand);
		d.alpha = 128;
		d.velocity = vector2 * 5;
		d.noGravity = true;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		damage = (int)(damage * 0.45f);
		velocity = new Vector2(Math.Sign(velocity.X) * velocity.Length(), velocity.Y * 0.2f);
		position += velocity * 3;
		position.Y -= 20;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.AntlionMandible)
			.AddIngredient(ItemID.SandBlock, 60)
			.AddIngredient(ModContent.ItemType<Material.Beak>(), 5)
			.AddIngredient(ItemID.Amber, 5)
			.AddTile(TileID.Anvils).Register();
	}
}
