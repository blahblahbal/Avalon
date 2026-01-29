using Avalon.Particles.OldParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class ShadowCurse : ModBuff
{
	private static Asset<Texture2D> Tex;
	public override string Texture => $"Terraria/Images/Buff_1";
    public override void SetStaticDefaults()
    {
		Tex = ModContent.Request<Texture2D>((GetType().Namespace + "." + Name).Replace('.', '/'));
		Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }
	public override void Update(Player player, ref int buffIndex)
	{
		ShadowCursePlayer p = player.GetModPlayer<ShadowCursePlayer>();
		p.Active = true;
		PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
		s.LocalPosition = Main.rand.NextVector2FromRectangle(player.Hitbox);
		s.Velocity = new Vector2(Main.rand.NextFloat(-0.1f, 0.1f),Main.rand.NextFloat(-2f,-1f));
		s.Rotation = s.Velocity.ToRotation();
		s.Velocity += player.velocity;
		s.Scale = new Vector2(2f, 0.3f);
		s.DrawVerticalAxis = false;
		s.FadeInEnd = 2;
		s.FadeOutStart = 2;
		s.FadeOutEnd = 10;
		s.AdditiveAmount = 0f;
		switch (p.Tier)
		{
			case 0:
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(0.25f, 1f, 1f);
				break;
			case 1:
				s.AdditiveAmount = 0.5f;
				s.ColorTint = new Color(0f, 0.5f, 1f);
				break;
			case 2:
				s.Scale *= 1.25f;
				s.ColorTint = new Color(0.2f, 0.3f, 0.5f);
				break;
			case 3:
				s.Scale *= 1.5f;
				s.ColorTint = new Color(0.1f, 0.2f, 0.3f);
				break;
			case 4:
				if (Main.rand.NextBool(4))
				{
					s.ColorTint = Color.Red;
					s.AdditiveAmount = 0f;
				}
				else
				{
					s.ColorTint = Color.Black;
				}
					s.Scale *= 2;
				break;
		}
		s.RotationAcceleration = Main.rand.NextFloat(-p.Tier * 0.01f, p.Tier * 0.01f);
		s.ScaleAcceleration = new Vector2(0.1f, -0.01f);
		Main.ParticleSystem_World_OverPlayers.Add(s);
	}
	public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
	{
		ShadowCursePlayer p = Main.LocalPlayer.GetModPlayer<ShadowCursePlayer>();
		drawParams.Texture = Tex.Value;
		drawParams.SourceRectangle = new Rectangle(0,34 * p.Tier,32,32);
		return base.PreDraw(spriteBatch, buffIndex, ref drawParams);
	}
	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		ShadowCursePlayer p = Main.LocalPlayer.GetModPlayer<ShadowCursePlayer>();
		tip = Language.GetText("Mods.Avalon.Buffs.ShadowCurse.Description").WithFormatArgs((p.getMultiplier() - 1f) * 100).Value;
	}
	public override bool ReApply(Player player, int time, int buffIndex)
	{
		ShadowCursePlayer p = player.GetModPlayer<ShadowCursePlayer>();
		p.Tier++;
		if(p.Tier > 4)
		{
			p.Tier = 4;
		}
		return base.ReApply(player, time, buffIndex);
	}
}
public class ShadowCursePlayer : ModPlayer
{
	public bool Active = false;
	public byte Tier = 0;
	public override void ResetEffects()
	{
		if (!Active) Tier = 0;
		Active = false;
	}
	public float getMultiplier()
	{
		return 1f + ((Tier + 1) * 0.5f);
	}
	public override void ModifyHurt(ref Player.HurtModifiers modifiers)
	{
		modifiers.FinalDamage *= getMultiplier();
	}
}
