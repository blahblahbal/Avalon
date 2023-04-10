using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class Hellcastle : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Hellcastle") : MusicID.Dungeon;
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override string MapBackground => BackgroundPath;

    public override bool IsBiomeActive(Player player)
    {
        Point tileCoordinates = player.Center.ToTileCoordinates();
        ushort wallType = Main.tile[tileCoordinates.X, tileCoordinates.Y].WallType;

        return ModContent.GetInstance<Systems.BiomeTileCounts>().HellCastleTiles > 350 &&
            wallType == ModContent.WallType<Walls.ImperviousBrickWallUnsafe>();
    }
}
