using System;
using System.Reflection;
using Avalon.Backgrounds;
using Avalon.Biomes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonMenu : ModMenu
{
    public override Asset<Texture2D> Logo
    {
        get
        {
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                return Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/EAOLogoAprilFools");
            }
            return Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/ExxoAvalonLogoContagion");
        }
    }

    public override void PostDrawLogo(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor)
    {
        if (DateTime.Now.Month != 4 && DateTime.Now.Day != 1)
        {
            Texture2D logo = Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/OriginsLogo").Value;
            spriteBatch.Draw(logo, logoDrawCenter + new Vector2(0, 60 * logoScale), new Rectangle(0,0,logo.Width,logo.Height),drawColor,logoRotation * 0.3f,new Vector2(logo.Width / 2, logo.Height / 2),logoScale,SpriteEffects.None,0);
        }
    }

    //int WhatMusic = MusicID.MenuMusic;

    //int[] MusicChoices =
    //{
    //    MusicID.MenuMusic,
    //    MusicID.ConsoleMenu,
    //    MusicID.Title
    //};
    //public override void OnSelected()
    //{
    //    WhatMusic = MusicChoices[Main.rand.Next(3)];
    //    if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
    //    {
    //        WhatMusic = MusicID.Hell;
    //    }
    //}
    //public override int Music => WhatMusic;
    public override void Load()
    {
        base.Load();

        const string lastSelectedModMenuFieldName = "LastSelectedModMenu";
        FieldInfo? lastSelectedModMenuFieldInfo =
            typeof(MenuLoader).GetField(lastSelectedModMenuFieldName, BindingFlags.NonPublic | BindingFlags.Static);

        if (lastSelectedModMenuFieldInfo != null)
        {
            // Sets the menu to be initially set to Exxo Avalon's on game load
            lastSelectedModMenuFieldInfo.SetValue(null, FullName);
        }
        else
        {
            ExxoAvalonOrigins.Mod.Logger.Error(
                $"Could not find field with name {lastSelectedModMenuFieldName} in {typeof(MenuLoader)}");
        }
    }
}
