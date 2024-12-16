using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Hardmode;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class SoulEdgeSlash : EnergySlashTemplate
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.penetrate = 3;
    }
    public override bool PreDraw(ref Color lightColor)
    {
		float percent = 1f - Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage / (float)SoulEdgePlayer.maxSoulEdge;
		float multiply = 0.8f;
		DrawSlash(
			Color.Lerp(new Color(244,19,0,0),new Color(0,140,244,0),percent) * multiply, 
			Color.Lerp(new Color(255, 140, 163, 0), new Color(88, 219, 255, 0), percent) * multiply, 
			Color.Lerp(new Color(255, 223, 240, 0), new Color(237, 171, 255, 0), percent) * multiply,
			new Color(1f,1f,1f,0f),0,1f,0f,-0.1f,-0.2f,true,true);
		return false;
    }
    public override void AI()
    {
		Player player = Main.player[Projectile.owner];
		int DustType = DustID.SpectreStaff;
		float percent = (player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage / (float)SoulEdgePlayer.maxSoulEdge);
		if (Main.rand.NextBool())
			DustType = (Main.rand.NextFloat() > percent ? DustID.DungeonSpirit : ModContent.DustType<PhantoplasmDust>());

		for (int j = 0; j < 2; j++)
		{
			ClassExtensions.GetPointOnSwungItemPath(60f, 120f, 0.2f + 0.8f * Main.rand.NextFloat(), player.HeldItem.scale, out var location2, out var outwardDirection2, player);
			Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
			Dust d = Dust.NewDustPerfect(location2, DustType, vector2 * 2f, 100, default(Color), 0.7f + Main.rand.NextFloat() * 1.3f);
			d.noGravity = true;
			d.velocity *= 2f;
			if (Main.rand.NextBool(20))
			{
				int num15 = Dust.NewDust(location2, 0, 0, DustType, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 128, default(Color), 1.3f);
				Main.dust[num15].fadeIn = 1.2f;
				Main.dust[num15].noGravity = true;
				Main.dust[num15].velocity *= 2f;
				Main.dust[num15].velocity += vector2 * 5f;
			}
		}

		//float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
		//Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
		//Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
		//Projectile.ai[2] += 0.02f;
		//Projectile.localAI[0] += 0.9f;
		//Player player = Main.player[Projectile.owner];
		//float num = Projectile.localAI[0] / Projectile.ai[1];
		//float num2 = Projectile.ai[0];
		//float num3 = Projectile.velocity.ToRotation();
		//Projectile.rotation = (float)Math.PI * 2f * num2 * num + num3 + num2 * (float)MathHelper.TwoPi + player.fullRotation + MathHelper.Pi;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write7BitEncodedInt(Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage = reader.Read7BitEncodedInt();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
		Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage += damageDone;
		Projectile.netUpdate = true;

		if(Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage >= SoulEdgePlayer.maxSoulEdge)
		{
			for (int i = 0; i < 35; i++)
			{
				Dust d = Dust.NewDustDirect(target.Hitbox.ClosestPointInRect(Projectile.Center) - Projectile.Size / 2f, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>());
				d.noGravity = true;
				d.velocity *= 2;
				d.scale *= 2;
			}
		}
	}
}
