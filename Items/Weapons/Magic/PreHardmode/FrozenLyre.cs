using Avalon.Items.Material.Shards;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class FrozenLyre : ModItem
{
	public SoundStyle note = new("Terraria/Sounds/Item_26")
	{
		Volume = 1f,
		Pitch = 0f,
		PitchVariance = 0.5f,
		MaxInstances = 10,
	};
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeapon(30, 24, ModContent.ProjectileType<Projectiles.Magic.IceNote>(), 16, 1f, 4, 6f, 20, 20, true);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 40, 0);
		Item.holdStyle = ItemHoldStyleID.HoldHeavy;
		Item.UseSound = note;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6, 0);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddRecipeGroup("IronBar", 4)
			.AddIngredient(ModContent.ItemType<Icicle>(), 50)
			.AddIngredient(ItemID.FallenStar, 8)
			.AddIngredient(ModContent.ItemType<FrostShard>(), 4)
			.AddTile(TileID.IceMachine)
			.Register();
	}
}
