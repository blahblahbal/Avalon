using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class PlatinumChest : ChestTemplate
{
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.PlatinumChest>();
    //protected override int ChestKeyItemId => ModContent.ItemType<Items.Other.PlatinumKey>();
    protected override bool CanBeLocked => false;
    //protected override Color LockedMapColor => new(188, 119, 247);
    //protected override Color UnlockedMapColor => new(188, 119, 247);

    public override bool IsLockedChest(int i, int j)
    {
        return Main.tile[i, j].TileFrameX >= 36;
    }
    public override void SetStaticDefaults()
    {
        //DustType = ModContent.DustType<CrystalDust>();
        AddMapEntry(new Color(188, 119, 247));
        base.SetStaticDefaults();
    }
    public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
    {
        if (Main.tile[i, j].TileFrameX >= 36)
        {
            frameXAdjustment = -36;
            return true;
        }
        return false;
    }
    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)(Main.tile[i, j].TileFrameX / 36);
    }
}
