using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Tiles.CrystalMines;

public class CrystalMinesChest : ChestTemplate
{
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.CrystalMinesChest>();
	protected override int ChestKeyItemId => ModContent.ItemType<Items.Other.CrystalMinesKey>();
	protected override bool CanBeLocked => true;
	//protected override Color LockedMapColor => new(188, 119, 247);
	//protected override Color UnlockedMapColor => new(188, 119, 247);

	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(188, 119, 247));
		base.SetStaticDefaults();
		DustType = ModContent.DustType<CrystalDust>();
	}
}
