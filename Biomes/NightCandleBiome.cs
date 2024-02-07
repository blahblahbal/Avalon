using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class NightCandleBiome : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.None;

    public override int Music => Main.curMusic;
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override string MapBackground => BackgroundPath;

    public override bool IsBiomeActive(Player player)
    {
        return player.DoesTileExistInBoxAroundPlayer(30, ModContent.TileType<Tiles.SoulCandles.NightCandle>());
    }
}
