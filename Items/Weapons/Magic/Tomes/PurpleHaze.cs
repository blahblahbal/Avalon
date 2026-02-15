using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Avalon.Projectiles.Magic.Tomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Tomes;

public class PurpleHaze : ModItem
{
	public SoundStyle gas = new("Terraria/Sounds/Item_34")
	{
		Volume = 0.5f,
		Pitch = -0.5f,
		PitchVariance = 1.5f,
		MaxInstances = 10,
	};
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<PurpleHazeProj>(), 26, 1.5f, 5, 8f, 7, 21);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 4);
		Item.UseSound = gas;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}

	public override void AddRecipes()
	{
		Recipe.Create(Type).AddIngredient(ItemID.SpellTome).AddIngredient(ModContent.ItemType<Pathogen>(), 20).AddIngredient(ItemID.SoulofNight, 15).AddTile(TileID.Bookcases).Register();
	}
}

