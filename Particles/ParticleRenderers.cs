using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace Avalon.Particles;

public class ParticleRenderers : ModSystem
{
	public static ParticleRenderer BeforeWorld = new();
	public static ParticleRenderer AfterWorld = new(); // Plumbum added to Avalon? More likely than you think!
	public override void PostUpdateDusts()
	{
		BeforeWorld.Update();
		AfterWorld.Update();
	}
	public override void Load()
	{
		On_Main.DoDraw_WallsTilesNPCs += On_Main_DoDraw_WallsTilesNPCs;
		On_Main.DrawInfernoRings += On_Main_DrawInfernoRings;
	}

	private void On_Main_DrawInfernoRings(On_Main.orig_DrawInfernoRings orig, Main self)
	{
		AfterWorld.Settings.AnchorPosition = -Main.screenPosition;
		AfterWorld.Draw(Main.spriteBatch);
		orig(self);
	}

	private void On_Main_DoDraw_WallsTilesNPCs(On_Main.orig_DoDraw_WallsTilesNPCs orig, Main self)
	{
		//Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
		BeforeWorld.Settings.AnchorPosition = -Main.screenPosition;
		BeforeWorld.Draw(Main.spriteBatch);
		orig(self);
	}
}
