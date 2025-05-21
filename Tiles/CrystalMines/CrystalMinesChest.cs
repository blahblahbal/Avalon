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

	public override bool LockChest(int i, int j, ref short frameXAdjustment, ref bool manual)
	{
		return false; // todo: replace this with checks for if oblivion has been defeated, to prevent regular locking via chest lock or sonic screwdriver mk 3 before it can be opened
		return base.LockChest(i, j, ref frameXAdjustment, ref manual);
	}
	public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
	{
		return false; // todo: replace this with checks for if oblivion has been defeated, to prevent regular unlocking via chest lock or sonic screwdriver mk 3 (also make sure to prevent mk 3 from opening this chest in its own code)
		return base.UnlockChest(i, j, ref frameXAdjustment, ref dustType, ref manual);
	}
}
