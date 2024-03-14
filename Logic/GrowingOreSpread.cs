using Terraria;


namespace Avalon.Logic;
public class GrowingOreSpread
{
    public static bool GrowingOre(int i, int j, int growingType)
    {
        int num = 40;
        int num2 = 130;
        int num3 = 35;
        int num4 = 85;
        if (j < Main.rockLayer)
        {
            num /= 2;
            num2 /= 2;
            num3 = (int)(num3 * 1.5);
            num4 = (int)(num4 * 1.5);
        }
        int num5 = 0;
        for (int k = i - num3; k < i + num3; k++)
        {
            for (int l = j - num3; l < j + num3; l++)
            {
                if (WorldGen.InWorld(k, l) && Main.tile[k, l].HasTile && Main.tile[k, l].TileType == growingType)
                {
                    num5++;
                }
            }
        }
        if (num5 > num)
        {
            return false;
        }
        num5 = 0;
        for (int m = i - num4; m < i + num4; m++)
        {
            for (int n = j - num4; n < j + num4; n++)
            {
                if (WorldGen.InWorld(m, n) && Main.tile[m, n].HasTile && Main.tile[m, n].TileType == growingType)
                {
                    num5++;
                }
            }
        }
        if (num5 > num2)
        {
            return false;
        }
        return true;
    }
}
