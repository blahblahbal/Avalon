using Avalon.Common;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class UIGenProgressBarEdit : ModHook {
    private Asset<Texture2D> texOuterContagion = null!;

    /// <inheritdoc />
    public override void Load() {
        texOuterContagion =
            Mod.Assets.Request<Texture2D>(
                $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/LoadingOuterContagion");
    }

    protected override void Apply() {
        IL_UIGenProgressBar.DrawSelf += IL_DrawSelf;
    }

    private void IL_DrawSelf(ILContext il) {
        const int rectangleLocal = 6;
        const string texOuterCorruptFieldName = "_texOuterCorrupt";

        var cursor = new ILCursor(il);

        cursor.Index = cursor.Instrs.Count - 1;
        cursor.GotoPrev(instruction => instruction.MatchLdfld<UIGenProgressBar>(texOuterCorruptFieldName));
        cursor.GotoNext(instruction => instruction.MatchLdloc(rectangleLocal));
        cursor.EmitDelegate((Texture2D texOuterCorrupt) =>
            ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion
                ? texOuterContagion.Value
                : texOuterCorrupt);
    }
}
