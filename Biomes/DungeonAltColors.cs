using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class DungeonAltColors : ModBiome
{
    //public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    //public override string BestiaryIcon => base.BestiaryIcon;
    //public override string BackgroundPath => base.BackgroundPath;
    //public override string MapBackground => BackgroundPath;
    //public override int Music => MusicID.Dungeon;

    public override bool IsBiomeActive(Player player)
    {
        Point tileCoordinates = player.Center.ToTileCoordinates();
        ushort wallType = Main.tile[tileCoordinates.X, tileCoordinates.Y].WallType;

        return ModContent.GetInstance<Systems.BiomeTileCounts>().DungeonAltTiles > 250 &&
            Main.wallDungeon[wallType] && tileCoordinates.Y > Main.worldSurface;
    }
}
