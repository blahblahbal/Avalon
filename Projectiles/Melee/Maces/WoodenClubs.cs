using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Maces.WoodenClubs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Maces;

public class WoodenClubProj : MaceTemplate
{
	public override string Texture => ModContent.GetInstance<WoodenClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<WoodenClub>().DisplayName;
	public override float MaxRotation => MathF.PI;
	public override float SwingRadius => 50f;
	public override float ScaleMult => WoodenClub.ScaleMult;
	public override float StartScaleTime => 0.5f;
	public override float StartScaleMult => 0.95f;
	public override float EndScaleTime => 1f / 3f;
	public override float EndScaleMult => 0.95f;
	public override Func<float, float> EasingFunc => rot => Easings.PowInOut(rot, 5f);
	public override int TrailLength => 0;

	public override void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
		if (Projectile.localAI[2] != 1 && easedRotationProgress > 0.1f)
		{
			Projectile.localAI[2] = 1;
			SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
		}
	}
}
public class AshWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<AshWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<AshWoodClub>().DisplayName;
}
public class BleachedEbonyClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<BleachedEbonyClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<BleachedEbonyClub>().DisplayName;
}
public class BorealWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<BorealWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<BorealWoodClub>().DisplayName;
}
public class CoughwoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<CoughwoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<CoughwoodClub>().DisplayName;
}
public class EbonwoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<EbonwoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<EbonwoodClub>().DisplayName;
}
public class PalmWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<PalmWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<PalmWoodClub>().DisplayName;
}
public class PearlwoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<PearlwoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<PearlwoodClub>().DisplayName;
}
public class ResistantWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<ResistantWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<ResistantWoodClub>().DisplayName;
}
public class RichMahoganyClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<RichMahoganyClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<RichMahoganyClub>().DisplayName;
}
public class ShadewoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<ShadewoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<ShadewoodClub>().DisplayName;
}