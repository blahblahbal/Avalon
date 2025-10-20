using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.ContagionGrasses;

public class ContagionJungleGrass : ModTile
{
    public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
    {
        sightColor = ExxoAvalonOrigins.ContagionBiomeSightColor;
        return true;
    }
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(147, 166, 42));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.JungleGrass[Type] = true;
        TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        RegisterItemDrop(ItemID.MudBlock);
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            noItem = true;
            Main.tile[i, j].TileType = TileID.Mud;
            WorldGen.SquareTileFrame(i, j);
        }
    }
}
