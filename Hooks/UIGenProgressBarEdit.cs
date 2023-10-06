using Avalon.Common;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class UIGenProgressBarEdit : ModHook
{
    private Asset<Texture2D> texOuterContagion = null!;

    /// <inheritdoc />
    public override void Load()
    {
        texOuterContagion =
            Mod.Assets.Request<Texture2D>(
                $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/LoadingOuterContagion");
    }

    protected override void Apply()
    {
        //IL_UIGenProgressBar.DrawSelf += IL_DrawSelf;
        On_UIGenProgressBar.DrawSelf += On_UIGenProgressBar_DrawSelf;
    }

    private void On_UIGenProgressBar_DrawSelf(On_UIGenProgressBar.orig_DrawSelf orig, UIGenProgressBar self, SpriteBatch spriteBatch)
    {
        Asset<Texture2D> OuterCorrupt = (Asset<Texture2D>)typeof(UIGenProgressBar).GetField("_texOuterCorrupt", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        Asset<Texture2D> OuterCrimson = (Asset<Texture2D>)typeof(UIGenProgressBar).GetField("_texOuterCrimson", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        Asset<Texture2D> OuterLower = (Asset<Texture2D>)typeof(UIGenProgressBar).GetField("_texOuterLower", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        float VisualOverallProgress = (float)typeof(UIGenProgressBar).GetField("_visualOverallProgress", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        float VisualCurrentProgress = (float)typeof(UIGenProgressBar).GetField("_visualCurrentProgress", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        float TargetOverallProgress = (float)typeof(UIGenProgressBar).GetField("_targetOverallProgress", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        float TargetCurrentProgress = (float)typeof(UIGenProgressBar).GetField("_targetCurrentProgress", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        int SmallBarWidth = (int)typeof(UIGenProgressBar).GetField("_smallBarWidth", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        int LongBarWidth = (int)typeof(UIGenProgressBar).GetField("_longBarWidth", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);

        if (OuterCorrupt.IsLoaded && OuterCrimson.IsLoaded && OuterLower.IsLoaded && texOuterContagion.IsLoaded)
        {
            int evil = 0;
            if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Crimson)
            {
                evil = 1;
            }
            else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion)
            {
                evil = 2;
            }
            bool flag = WorldGen.crimson;
            if (WorldGen.drunkWorldGen)
            {
                evil++;
                if (evil > 2) evil = 0;
            }
            VisualOverallProgress = TargetOverallProgress;
            VisualCurrentProgress = TargetCurrentProgress;
            CalculatedStyle dimensions = self.GetDimensions();
            int completedWidth = (int)(VisualOverallProgress * LongBarWidth);
            int completedWidth2 = (int)(VisualCurrentProgress * SmallBarWidth);
            Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
            Color color = default;
            switch (evil)
            {
                case 0:
                    color = new Color(95, 242, 86);
                    break;
                case 1:
                    color = new Color(255, 237, 131); // crimson
                    break;
                case 2:
                    color = new Color(175, 148, 199);
                    break;
            }
            DrawFilling2(spriteBatch, vector + new Vector2(20f, 40f), 16, completedWidth, LongBarWidth, color, Color.Lerp(color, Color.Black, 0.5f), new Color(48, 48, 48));
            color.PackedValue = 4290947159u;
            DrawFilling2(spriteBatch, vector + new Vector2(50f, 60f), 8, completedWidth2, SmallBarWidth, color, Color.Lerp(color, Color.Black, 0.5f), new Color(33, 33, 33));
            Rectangle r = self.GetDimensions().ToRectangle();
            r.X -= 8;
            Texture2D tex = OuterCorrupt.Value;
            switch (evil)
            {
                case 0:
                    tex = OuterCorrupt.Value;
                    break;
                case 1:
                    tex = OuterCrimson.Value;
                    break;
                case 2:
                    tex = texOuterContagion.Value;
                    break;
            }
            spriteBatch.Draw(tex, r.TopLeft(), Color.White);
            spriteBatch.Draw(OuterLower.Value, r.TopLeft() + new Vector2(44f, 60f), Color.White);
        }
            


        /*orig.Invoke(self, spriteBatch);
        Asset<Texture2D> OuterCorrupt = (Asset<Texture2D>)typeof(UIGenProgressBar).GetField("_texOuterCorrupt", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        Asset<Texture2D> OuterCrimson = (Asset<Texture2D>)typeof(UIGenProgressBar).GetField("_texOuterCrimson", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        Asset<Texture2D> OuterLower = (Asset<Texture2D>)typeof(UIGenProgressBar).GetField("_texOuterLower", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        if (OuterCorrupt.IsLoaded && OuterCrimson.IsLoaded && OuterLower.IsLoaded)
        {
            bool flag2 = ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion;
            if (WorldGen.drunkWorldGen && Main.rand.NextBool(2))
            {
                flag2 = !flag2;
            }
            Color color = default(Color);
            color.PackedValue = 4290947159u;
            Rectangle r = self.GetDimensions().ToRectangle();
            r.X -= 8;
            spriteBatch.Draw(flag2 ? ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/LoadingOuterContagion").Value : OuterCorrupt.Value, r.TopLeft(), Color.White);
            //spriteBatch.Draw(flag2 ? ModContent.Request<Texture2D>("Avalon/UI/WorldCreation/LoadingOuterContagion").Value : OuterLower.Value, r.TopLeft() + new Vector2(44f, 60f), Color.White);
        }*/
    }
    private void DrawFilling2(SpriteBatch spritebatch, Vector2 topLeft, int height, int completedWidth, int totalWidth, Color filled, Color separator, Color empty)
    {
        if (completedWidth % 2 != 0)
        {
            completedWidth--;
        }
        spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X, (int)topLeft.Y, completedWidth, height), (Rectangle?)new Rectangle(0, 0, 1, 1), filled);
        spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth, (int)topLeft.Y, totalWidth - completedWidth, height), (Rectangle?)new Rectangle(0, 0, 1, 1), empty);
        spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth - 2, (int)topLeft.Y, 2, height), (Rectangle?)new Rectangle(0, 0, 1, 1), separator);
    }
    private void IL_DrawSelf(ILContext il)
    {
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
