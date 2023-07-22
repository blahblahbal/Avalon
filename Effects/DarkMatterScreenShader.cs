using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace Avalon.Effects;

public class DarkMatterScreenShader : ScreenShaderData
{
    public DarkMatterScreenShader(Ref<Effect> shader, string pissName) : base(shader, pissName)
    {
    }

    public override void Apply()
    {
        var vec = new Color(126, 100, 100).ToVector3(); // 126 71 107
        vec *= 0.4f;
        UseOpacity(Math.Max(vec.X, Math.Max(vec.Y, vec.Z)));
        base.Apply();
    }
}
