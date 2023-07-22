using Avalon.Backgrounds;
using Avalon.Common.Players;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class DarkMatter : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/DarkMatterWaterStyle");
    public override string MapBackground => BackgroundPath;

    /// <inheritdoc />
    public override int Music
    {
        get
        {
            if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DarkMatterMonolith)
            {
                return Main.curMusic;
            }

            return ExxoAvalonOrigins.MusicMod != null
                ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DarkMatter")
                : MusicID.Eclipse;
        }
    }

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        ModContent.GetInstance<DarkMatterSurfaceBackgroundStyle>();

    public override void SpecialVisuals(Player player, bool isActive)
    {
        if (!isActive)
        {
            return;
        }

        Main.ColorOfTheSkies = new Color(126, 71, 107);
        player.ManageSpecialBiomeVisuals("Avalon:DarkMatter", isActive);
    }

    public override bool IsBiomeActive(Player player) => ModContent.GetInstance<BiomeTileCounts>().DarkTiles > 450 ||
                                                         player.GetModPlayer<AvalonPlayer>().DarkMatterMonolith;
}
