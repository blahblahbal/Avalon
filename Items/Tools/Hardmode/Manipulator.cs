using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;

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
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Pink;
		Item.width = dims.Width;
		Item.useTime = 30;
		Item.useTurn = true;
		Item.value = Item.sellPrice(0, 5);
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useAnimation = 15;
		Item.height = dims.Height;
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Timechanger>();
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
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Pink;
		Item.width = dims.Width;
		Item.useTime = 30;
		Item.useTurn = true;
		Item.value = Item.sellPrice(0, 5);
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useAnimation = 15;
		Item.height = dims.Height;
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Moonphaser>();
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
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Pink;
		Item.width = dims.Width;
		Item.useTime = 30;
		Item.useTurn = true;
		Item.value = Item.sellPrice(0, 5);
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useAnimation = 15;
		Item.height = dims.Height;
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Rainbringer>();
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
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Pink;
		Item.width = dims.Width;
		Item.useTime = 30;
		Item.useTurn = true;
		Item.value = Item.sellPrice(0, 5);
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useAnimation = 15;
		Item.height = dims.Height;
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Sandstormer>();
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
