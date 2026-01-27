using Avalon.Items.Material.Bars;
using MagicStorage.Components;
using MagicStorage.CrossMod.Storage;
using MagicStorage.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MagicStorageSupport;

// This file showcases how to make a basic tier for Storage Units
// To implement a custom Storage Unit tier, you need several components:
//   1. The StorageUnitTier which defines the tier's properties and upgrade paths
//   2. A StorageUnit tile that uses the tier
//   3. A BaseStorageUnitItem item which places the tile
//   4. A BaseStorageUpgradeItem item that applies this tier to a placed Storage Unit of the previous tier
//   5. A BaseStorageCoreItem item that is extracted when using a Storage Core Wrench
// Most of this is handled automatically for you, and you just have to define the relations between the components and their tier
// In this example, the Demonite and Crimtane tiers can be upgraded to the Example tier, which can then be upgraded to the Hellstone tier

//Class for the Storage Tier
[ExtendsFromMod("MagicStorage")]
public class BacciliteStorageUnitTier : StorageUnitTier
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("MagicStorage");
	}
	public override int UpgradeItemType => ModContent.ItemType<BacciliteUpgrade>();

	public override int CoreItemType => ModContent.ItemType<BacciliteCore>();

	// Keeping parity with other evil ore storage values
	public override int Capacity => 80;

	public override int StorageUnitItemType => ModContent.ItemType<BacciliteStorageUnitItem>();

	public override int StorageUnitTileType => ModContent.TileType<BacciliteStorageUnitTile>();

	public override void SetStaticDefaults()
	{
		// Keeping same upgrade path as other evil ores
		SetUpgradeableFrom(StorageUnitTier.Basic);
		SetUpgradeableTo(StorageUnitTier.Hellstone);
	}

	public override void Frame(StorageUnitFullness fullness, bool active, out int frameX, out int frameY)
	{
		// Based on the ExampleStorageUnitTile.png spritesheet:
		//   Style columns are 36 pixels wide total
		//   Active styles are in the first three columns
		//   Each fullness type takes up one style column
		//   There is only one row of styles
		const int STYLE_WIDTH = 36;
		const int FULLNESS_STYLES = 3;

		frameX = active ? 0 : FULLNESS_STYLES * STYLE_WIDTH;
		frameX += (int)fullness * STYLE_WIDTH;
		frameY = 0;
	}

	public override void GetState(int frameX, int frameY, out StorageUnitFullness fullness, out bool active)
	{
		// Based on the ExampleStorageUnitTile.png spritesheet:
		//   Style columns are 36 pixels wide total
		//   Active styles are in the first three columns
		//   Each fullness type takes up one style column
		const int STYLE_WIDTH = 36;
		const int FULLNESS_STYLES = 3;

		active = frameX < FULLNESS_STYLES * STYLE_WIDTH;
		fullness = (StorageUnitFullness)(frameX % (FULLNESS_STYLES * STYLE_WIDTH) / STYLE_WIDTH);
	}

	public override bool IsValidTile(int frameX, int frameY)
	{
		// Since this example only has one row of styles, there's no check needed
		// However, if you were using a spritesheet with multiple tiers, you can check the framing here
		return true;
	}
}

//Class for the corresponding storage unit
[ExtendsFromMod("MagicStorage")]
internal class BacciliteStorageUnitTile : MagicStorage.Components.StorageUnit
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("MagicStorage");
	}
	public override void ModifyObjectData()
	{
		// By default, StorageUnit expects spritesheets to have 6 style columns per row
		// If you want to modify this behavior, update:
		//   TileObjectData.newTile.StyleHorizontal
		//   TileObjectData.newTile.StyleMultiplier
		//   TileObjectData.newTile.StyleWrapLimit
		base.ModifyObjectData();
	}

	public override TEStorageUnit GetTileEntity()
	{
		// By default, StorageUnit places a TEStorageUnit tile entity
		// Use this method to place a custom tile entity if needed
		return base.GetTileEntity();
	}

	protected override bool GetGlowmask(int x, int y, int type, int frameX, int frameY, out Asset<Texture2D> asset, out Color drawColor)
	{
		// By default, StorageUnit.GetGlowMask() uses Texture + "_Glow" for loading the glowmask texture
		// It will also emit a Color.White light blended with the world's lighting
		// Use this method to modify the glowmask behavior if needed
		return base.GetGlowmask(x, y, type, frameX, frameY, out asset, out drawColor);
	}
}

//Class for the storage unit's item
[ExtendsFromMod("MagicStorage")]
public class BacciliteStorageUnitItem : BaseStorageUnitItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("MagicStorage");
	}
	public override StorageUnitTier Tier => ModContent.GetInstance<BacciliteStorageUnitTier>();

	public override void SetDefaults()
	{
		base.SetDefaults();
		Item.rare = ItemRarityID.Pink;
	}
}

//Class for the crafted upgrade item
[ExtendsFromMod("MagicStorage")]
public class BacciliteUpgrade : BaseStorageUpgradeItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("MagicStorage");
	}
	public override StorageUnitTier Tier => ModContent.GetInstance<BacciliteStorageUnitTier>();

	public override void SetDefaults()
	{
		base.SetDefaults();
		Item.value = Item.sellPrice(silver: 32);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<BacciliteBar>(), 10)
			.AddIngredient(ItemID.Amethyst)
			.AddTile(TileID.Anvils)
			.Register();
	}
}

//Class for the core that drops from the storage unit
[ExtendsFromMod("MagicStorage")]
public class BacciliteCore : BaseStorageCore
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("MagicStorage");
	}
	public override StorageUnitTier Tier => ModContent.GetInstance<BacciliteStorageUnitTier>();

	// The tooltip will default to the localization at:  Mods.MagicStorage.Items.StorageCore.CommonTooltip
	// If you want to customize the tooltip, you can override it here
	public override LocalizedText Tooltip => base.Tooltip;
}
