using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Shaders;

namespace Avalon.Effects;

internal class CalculatorSpectaclesShader : MiscShaderData
{
	public CalculatorSpectaclesShader(Ref<Effect> shader, string passName) : base(shader, passName)
	{
	}
	public override void Apply()
	{
		Shader.Parameters["new_color"].SetValue(Color.White.ToVector4());
		//Shader.Parameters["key_color"].SetValue(Color.Transparent.ToVector4());
		base.Apply();
	}
}
