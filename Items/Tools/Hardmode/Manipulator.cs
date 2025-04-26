using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class ManipulatorTime : ModItem
{
	public override string Texture => "Avalon/Items/Tools/Hardmode/Manipulator";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 15, 30, true);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Timechanger>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ManipulatorMoon>());
		}
	}
	public override void UpdateInventory(Player player)
	{
		player.accWatch = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Timechanger>())
			.AddIngredient(ModContent.ItemType<Moonphaser>())
			.AddIngredient(ModContent.ItemType<Rainbringer>())
			.AddIngredient(ModContent.ItemType<Sandstormer>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}

public class ManipulatorMoon : ModItem
{
	public override string Texture => "Avalon/Items/Tools/Hardmode/Manipulator";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToSpawner(false, 15, 30, true);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Moonphaser>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ManipulatorRain>());
		}
	}
	public override void UpdateInventory(Player player)
	{
		player.accWatch = 3;
	}
}

public class ManipulatorRain : ModItem
{
	public override string Texture => "Avalon/Items/Tools/Hardmode/Manipulator";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToSpawner(false, 15, 30, true);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Rainbringer>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ManipulatorSand>());
		}
	}
	public override void UpdateInventory(Player player)
	{
		player.accWatch = 3;
	}
}

public class ManipulatorSand : ModItem
{
	public override string Texture => "Avalon/Items/Tools/Hardmode/Manipulator";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToSpawner(false, 15, 30, true);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Sandstormer>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
	}
	public override void HoldItem(Player player)
	{
		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
		{
			SoundEngine.PlaySound(SoundID.Unlock, player.position);
			Item.ChangeItemType(ModContent.ItemType<ManipulatorTime>());
		}
	}
	public override void UpdateInventory(Player player)
	{
		player.accWatch = 3;
	}
}
