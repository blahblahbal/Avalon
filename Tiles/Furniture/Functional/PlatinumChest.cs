using Avalon.Common.Templates;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Functional;

public class PlatinumChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PlatinumChest>();
	//protected override int ChestKeyItemId => ModContent.ItemType<Items.Other.PlatinumKey>();
	//protected override bool CanBeLocked => true;
	//protected override Color LockedMapColor => new(188, 119, 247);
	//protected override Color UnlockedMapColor => new(188, 119, 247);
}
