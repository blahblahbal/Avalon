using ExxoAvalonOrigins.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExxoAvalonOrigins.Tiles;

public class PlacedBars : ModTile
{
    public override void SetStaticDefaults()
    {

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 1;
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
        TileObjectData.newTile.CoordinatePadding = 2;
        //TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileShine[Type] = 1100;
    }

    // selects the map entry depending on the frameX
    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)(Main.tile[i, j].TileFrameX / 18);
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail)
        {
            return;
        }
        int toDrop = 0;
        switch (Main.tile[i, j].TileFrameX / 18)
        {
            //case 0:
            //    toDrop = ModContent.ItemType<CaesiumBar>();
            //    DustType = ModContent.DustType<Dusts.CaesiumDust>();
            //    break;
            //case 1:
            //    toDrop = ModContent.ItemType<OblivionBar>();
            //    DustType = DustID.Adamantite;
            //    break;
            //case 2:
            //    toDrop = ModContent.ItemType<HydrolythBar>();
            //    DustType = DustID.MagicMirror;
            //    break;
            //case 3:
            //    toDrop = ModContent.ItemType<RhodiumBar>();
            //    DustType = DustID.t_LivingWood;
            //    break;
            case 4:
                toDrop = ModContent.ItemType<OsmiumBar>();
                //DustType = ModContent.DustType<Dusts.OsmiumDust>();
                break;
            //case 5:
            //    toDrop = ModContent.ItemType<FeroziumBar>();
            //    DustType = DustID.UltraBrightTorch;
            //    break;
            //case 6:
            //    toDrop = ModContent.ItemType<UnvolanditeBar>();
            //    DustType = DustID.Dirt;
            //    break;
            //case 7:
            //    toDrop = ModContent.ItemType<VorazylcumBar>();
            //    DustType = DustID.CorruptGibs;
            //    break;
            //case 8:
            //    toDrop = ModContent.ItemType<TritanoriumBar>();
            //    DustType = DustID.Stone;
            //    break;
            //case 9:
            //    toDrop = ModContent.ItemType<DurataniumBar>();
            //    DustType = ModContent.DustType<Dusts.DurataniumDust>();
            //    break;
            //case 10:
            //    toDrop = ModContent.ItemType<NaquadahBar>();
            //    DustType = ModContent.DustType<Dusts.NaquadahDust>();
            //    break;
            //case 11:
            //    toDrop = ModContent.ItemType<TroxiniumBar>();
            //    DustType = ModContent.DustType<Dusts.TroxiniumDust>();
            //    break;
            //case 12:
            //    toDrop = ModContent.ItemType<PandemiteBar>();
            //    DustType = DustID.JungleSpore;
            //    break;
            //case 13:
            //    toDrop = ModContent.ItemType<PyroscoricBar>();
            //    DustType = DustID.InfernoFork;
            //    break;
            //case 14:
            //    toDrop = ModContent.ItemType<BeetleBar>();
            //    DustType = DustID.RainCloud;
            //    break;
            //case 15:
            //    //toDrop = ModContent.ItemType<SuperhardmodeBar>();
            //    DustType = DustID.CrimtaneWeapons;
            //    break;
            //case 16:
            //    toDrop = ModContent.ItemType<EnchantedBar>();
            //    break;
            //case 17:
            //    //toDrop = ModContent.ItemType<BerserkerBar>();
            //    DustType = DustID.Ice_Pink;
            //    break;
            //case 18:
            //    toDrop = ModContent.ItemType<BronzeBar>();
            //    DustType = ModContent.DustType<Dusts.BronzeDust>();
            //    break;
            //case 19:
            //    toDrop = ModContent.ItemType<NickelBar>();
            //    DustType = ModContent.DustType<Dusts.NickelDust>();
            //    break;
            //case 20:
            //    toDrop = ModContent.ItemType<ZincBar>();
            //    DustType = ModContent.DustType<Dusts.ZincDust>();
            //    break;
            //case 21:
            //    toDrop = ModContent.ItemType<BismuthBar>();
            //    DustType = ModContent.DustType<Dusts.BismuthDust>();
            //    break;
            //case 22:
            //    toDrop = ModContent.ItemType<IridiumBar>();
            //    DustType = ModContent.DustType<Dusts.IridiumDust>();
            //    break;
            //case 23:
            //    toDrop = ModContent.ItemType<XanthophyteBar>();
            //    DustType = DustID.KryptonMoss;
            //    break;
            //case 24:
            //    toDrop = ModContent.ItemType<CorruptedBar>();
            //    DustType = DustID.Corruption;
            //    break;
        }
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i * 16, j * 16), i * 16, j * 16, 16, 16, toDrop);
    }

    public override bool CreateDust(int i, int j, ref int type)
    {
        if (Main.tile[i, j].TileFrameX / 18 == 16)
        {
            Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.MagicMirror);
            Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Enchanted_Gold);
            Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Enchanted_Pink);
            return false;
        }
        return base.CreateDust(i, j, ref type);
    }
}
