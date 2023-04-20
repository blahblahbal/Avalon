using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class DevilsScythe : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.addTile(Type);
        ItemDrop = ModContent.ItemType<Items.Weapons.Magic.Hardmode.DevilsScythe>();
        Main.tileLighted[Type] = true;
        AddMapEntry(new Color(170, 48, 114));
    }
    public override bool RightClick(int i, int j)
    {
        WorldGen.KillTile(i, j);
        if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
        {
            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
        }
        return true;
    }
    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Weapons.Magic.Hardmode.DevilsScythe>();
    }
    //public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    //{
    //    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Weapons.Magic.DevilsScythe>(), pfix: -1);
    //}
}
