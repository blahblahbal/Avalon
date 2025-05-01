using Avalon.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class DarklightLance : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<DarklightLanceProjectile>(), 99, 5.5f, 26, 4f, true);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 40);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
	public override bool? UseItem(Player player)
	{
		if (!Main.dedServ && Item.UseSound.HasValue)
		{
			SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
		}

		return null;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.DarkLance)
			.AddIngredient(ItemID.Gungnir)
			.AddIngredient(ItemID.SoulofFright)
			.AddIngredient(ItemID.DarkShard)
			.AddIngredient(ItemID.LightShard);
	}
}
