using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics;
public class LanceTrap : ModTile
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
        return false;
    }
    public override void HitWire(int i, int j)
    {
        if (Wiring.CheckMech(i, j, 90))
        {
            Tile tile = Main.tile[i, j];
            int num140 = 0;
            int num141 = 0;
            float speedX;
            float speedY;
            switch (tile.TileFrameX / 18)
            {
                case 0:
                case 1:
                    num140 = 0;
                    num141 = 1;
                    break;
                case 2:
                    num140 = 0;
                    num141 = -1;
                    break;
                case 3:
                    num140 = -1;
                    num141 = 0;
                    break;
                case 4:
                    num140 = 1;
                    num141 = 0;
                    break;
            }
            speedX = 8 * num140;
            speedY = 8 * num141;
            int damage3 = 60;
            int projType = 186;
            Vector2 vel = new Vector2(i * 16 + 8 + 18 * num140, j * 16 + 8 + 18 * num141);
            Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), (int)vel.X, (int)vel.Y, speedX, speedY, projType, damage3, 2f, Main.myPlayer);
        }
    }
}
