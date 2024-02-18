using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics;
public class CannonballTrap : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(99, 89, 85), this.GetLocalization("MapEntry"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileFrameImportant[Type] = true;
        DustType = DustID.Silt;
    }
    public override bool Slope(int i, int j)
    {
        Main.tile[i, j].TileFrameX += 18;
        if (Main.tile[i, j].TileFrameX > 90) Main.tile[i, j].TileFrameX = 0;
        return false;
    }
    public override void HitWire(int i, int j)
    {
        if (Wiring.CheckMech(i, j, 200))
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX == 0)
            {
                Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2((i - 1) * 16, (j + 1) * 16), Vector2.Zero, ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrapStarter>(), 65, 2f, Main.myPlayer, ai1: 0);
            }
            if (tile.TileFrameX == 18)
            {
                Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2((i + 1) * 16, (j + 1) * 16), Vector2.Zero, ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrapStarter>(), 65, 2f, Main.myPlayer, ai1: 1);
            }
            if (tile.TileFrameX == 36 || tile.TileFrameX == 54)
            {
                Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2((i + 1) * 16, j * 16), Vector2.Zero, ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrapStarter>(), 65, 2f, Main.myPlayer, ai1: 2);
            }
            if (tile.TileFrameX == 72 || tile.TileFrameX == 90)
            {
                Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2((i + 1) * 16, (j + 2) * 16), Vector2.Zero, ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrapStarter>(), 65, 2f, Main.myPlayer, ai1: 3);
            }
        }
    }
}
