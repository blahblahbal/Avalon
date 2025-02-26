using Avalon.Common;
using Avalon.Reflection;
using Avalon.WorldGeneration.Enums;
using Avalon.WorldGeneration.SecretSeeds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class UIGenProgressBarEdit : ModHook
{
    private Asset<Texture2D> texOuterContagion = null!;

    public override void Load()
    {
        texOuterContagion = Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/LoadingOuterContagion");
    }

    protected override void Apply()
    {
        IL_UIGenProgressBar.DrawSelf += IL_DrawSelf;
		On_UIGenProgressBar.DrawSelf += PreventDrawingRetro;
		On_UIWorldLoad.DrawSelf += EditRetroText;
    }

	private void EditRetroText(On_UIWorldLoad.orig_DrawSelf orig, UIWorldLoad self, SpriteBatch spriteBatch)
	{
		orig.Invoke(self, spriteBatch);
		if (RetroWorldGen.isGeneratingOldWorld)
		{
			UIHeader _progressMessage = self.GetProgressMessage();
			_progressMessage.Text = Main.statusText;
		}
	}

	private void PreventDrawingRetro(On_UIGenProgressBar.orig_DrawSelf orig, UIGenProgressBar self, SpriteBatch spriteBatch)
	{
		if (!RetroWorldGen.isGeneratingOldWorld)
		{
			orig.Invoke(self, spriteBatch);
		}
	}

	private void IL_DrawSelf(ILContext il)
    {
        var cursor = new ILCursor(il);

        cursor.GotoNext(MoveType.After, i => i.MatchLdsfld<WorldGen>(nameof(WorldGen.crimson)));
        cursor.EmitDelegate((bool isCrimson) => ModContent.GetInstance<AvalonWorld>().WorldEvil != WorldEvil.Contagion && isCrimson);

        cursor.GotoNext(MoveType.After, i => i.MatchLdcI4(-8131073), i => i.MatchCall(out _));
        cursor.Emit(OpCodes.Ldloca, 5);
        cursor.EmitDelegate((ref Color color) =>
        {
            if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion)
                color = new Color(175, 148, 199);
        });

        cursor.GotoNext(MoveType.After, i => i.MatchLdfld<UIGenProgressBar>("_texOuterCorrupt"));
        cursor.EmitDelegate((Asset<Texture2D> texOuterCorrupt) =>
            ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion
                ? texOuterContagion
                : texOuterCorrupt);
    }
}
