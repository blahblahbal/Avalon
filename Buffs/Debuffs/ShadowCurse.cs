using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
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
	public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
	{
		r -= Tier * 0.25f;
		g -= Tier * 0.2f;
		b -= Tier * 0.15f;
	}
}
public class ShadowCurseLayer : PlayerDrawLayer
{
	private static Asset<Texture2D> Tex;
	public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.LastVanillaLayer);
	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		return drawInfo.drawPlayer.HasBuff<ShadowCurse>();
	}
	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		ShadowCursePlayer p = drawInfo.drawPlayer.GetModPlayer<ShadowCursePlayer>();
		var d = new DrawData(Tex.Value, drawInfo.Center.Floor() - Main.screenPosition, Tex.Frame(1, 5, 0, p.Tier), Color.White with { A = 170 }, (float)Math.Sin(Main.timeForVisualEffects * 0.02) * 0.1f, new Vector2(19, 25), 1f + (float)Math.Sin(Main.timeForVisualEffects * (0.1f + p.Tier * 0.075f)) * 0.1f, SpriteEffects.None);
		float amt = (float)(Main.timeForVisualEffects % 50) / 50f;
		//drawInfo.DrawDataCache.Add(d with { color = Color.White with { A = 0 } * amt * (1f - amt), scale = d.scale * (1f + (amt + 1 * 0.1f))});

		drawInfo.DrawDataCache.Add(d with { position = d.position + Main.rand.NextVector2Circular(p.Tier, p.Tier) * 3f, color = Color.White * 0.25f });
		drawInfo.DrawDataCache.Add(d with { position = d.position + Main.rand.NextVector2Circular(p.Tier, p.Tier) * 1.5f, color = Color.White * 0.5f });
		drawInfo.DrawDataCache.Add(d);
	}
	public override void Load()
	{
		Tex = Mod.Assets.Request<Texture2D>("Assets/Textures/ShadowCurseMark");
	}
}
