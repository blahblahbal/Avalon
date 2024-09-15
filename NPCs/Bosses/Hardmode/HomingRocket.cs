using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Data.Sets;

namespace Avalon.NPCs.Bosses.Hardmode;

public class HomingRocket : ModNPC
{
	private float maxSpeed = 20f;
	private float acceleration = 0.1f;
	private float currentSpeed = 2f; // Initial speed
	private float homingRange = 20f * 16f; // 20 tiles in pixels (1 tile = 16 pixels)
	public override void SetDefaults()
    {
        NPC.width = 14;
        NPC.height = 14;
        NPC.aiStyle = -1;
        NPC.lifeMax = 500;
		NPC.defense = 30;
        NPC.value = 0;
        NPC.noTileCollide = true;
        NPC.timeLeft = 500;
    }
    public override void OnKill()
    {
        foreach (Player P in Main.player)
        {
            if (P.getRect().Intersects(NPC.getRect()))
            {
                P.Hurt(PlayerDeathReason.ByProjectile(P.whoAmI, NPC.whoAmI), NPC.damage, 0);
            }
        }
        SoundEngine.PlaySound(SoundID.Item14, NPC.position);
        NPC.position.X = NPC.position.X + NPC.width / 2;
        NPC.position.Y = NPC.position.Y + NPC.height / 2;
        NPC.width = 22;
        NPC.height = 22;
        NPC.position.X = NPC.position.X - NPC.width / 2;
        NPC.position.Y = NPC.position.Y - NPC.height / 2;
        for (int num341 = 0; num341 < 30; num341++)
        {
            int num342 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num342].velocity *= 1.4f;
        }
        for (int num343 = 0; num343 < 20; num343++)
        {
            int num344 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default(Color), 3.5f);
            Main.dust[num344].noGravity = true;
            Main.dust[num344].velocity *= 7f;
            num344 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num344].velocity *= 3f;
        }
        for (int num345 = 0; num345 < 2; num345++)
        {
            float scaleFactor8 = 0.4f;
            if (num345 == 1)
            {
                scaleFactor8 = 0.8f;
            }
            int num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A0B0_cp_0 = Main.gore[num346];
            expr_A0B0_cp_0.velocity.X = expr_A0B0_cp_0.velocity.X + 1f;
            Gore expr_A0D0_cp_0 = Main.gore[num346];
            expr_A0D0_cp_0.velocity.Y = expr_A0D0_cp_0.velocity.Y + 1f;
            num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A153_cp_0 = Main.gore[num346];
            expr_A153_cp_0.velocity.X = expr_A153_cp_0.velocity.X - 1f;
            Gore expr_A173_cp_0 = Main.gore[num346];
            expr_A173_cp_0.velocity.Y = expr_A173_cp_0.velocity.Y + 1f;
            num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A1F6_cp_0 = Main.gore[num346];
            expr_A1F6_cp_0.velocity.X = expr_A1F6_cp_0.velocity.X + 1f;
            Gore expr_A216_cp_0 = Main.gore[num346];
            expr_A216_cp_0.velocity.Y = expr_A216_cp_0.velocity.Y - 1f;
            num346 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Gore expr_A299_cp_0 = Main.gore[num346];
            expr_A299_cp_0.velocity.X = expr_A299_cp_0.velocity.X - 1f;
            Gore expr_A2B9_cp_0 = Main.gore[num346];
            expr_A2B9_cp_0.velocity.Y = expr_A2B9_cp_0.velocity.Y - 1f;
        }
    }
    //public override void ModifyDamageHitbox(ref Rectangle hitbox)
    //{
    //    base.ModifyDamageHitbox(ref hitbox);
    //}
    public override void AI()
    {
        if (Math.Abs(NPC.velocity.X) >= 5f || Math.Abs(NPC.velocity.Y) >= 5f)
        {
            for (int num264 = 0; num264 < 2; num264++)
            {
                float num265 = 0f;
                float num266 = 0f;
                if (num264 == 1)
                {
                    num265 = NPC.velocity.X * 0.5f;
                    num266 = NPC.velocity.Y * 0.5f;
                }
                int num267 = Dust.NewDust(new Vector2(NPC.position.X + 3f + num265, NPC.position.Y + 3f + num266) - NPC.velocity * 0.5f, NPC.width - 8, NPC.height - 8, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num267].scale *= 2f + Main.rand.Next(10) * 0.1f;
                Main.dust[num267].velocity *= 0.2f;
                Main.dust[num267].noGravity = true;
                num267 = Dust.NewDust(new Vector2(NPC.position.X + 3f + num265, NPC.position.Y + 3f + num266) - NPC.velocity * 0.5f, NPC.width - 8, NPC.height - 8, DustID.Smoke, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num267].fadeIn = 1f + Main.rand.Next(5) * 0.1f;
                Main.dust[num267].velocity *= 0.05f;
            }
        }
		NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
		for (int p = 0; p < Main.player.Length; p++)
		{
			if (Main.player[p].active)
			{
				if (ClassExtensions.NewRectVector2(Main.player[p].position, new Vector2(Main.player[p].width, Main.player[p].height)).Intersects(ClassExtensions.NewRectVector2(NPC.position, new Vector2(NPC.width, NPC.height))))
				{
					NPC.timeLeft = 3;
					break;
				}
			}
		}
		if (NPC.timeLeft <= 3)
		{
			NPC.position.X = NPC.position.X + NPC.width / 2;
			NPC.position.Y = NPC.position.Y + NPC.height / 2;
			NPC.width = 128;
			NPC.height = 128;
			NPC.position.X = NPC.position.X - NPC.width / 2;
			NPC.position.Y = NPC.position.Y - NPC.height / 2;
			NPC.life = 0;
			NPC.checkDead();
		}

		//NPC.velocity = NPC.velocity.SafeNormalize(Vector2.Zero) * currentSpeed;

		// Increase speed gradually until it reaches max speed
		//if (currentSpeed < maxSpeed)
		//{
		//	currentSpeed += acceleration;
		//}
		//if (currentSpeed >= maxSpeed)
		//{
		//	HomeInWithinRange(float.MaxValue);
		//}
		//else
		//{
		//	HomeInWithinRange(homingRange);
		//}
		
		if (Math.Abs(NPC.velocity.X) < 15f && Math.Abs(NPC.velocity.Y) < 15f)
        {
            NPC.velocity *= 1.1f;
        }
		else
		{
			NPC.ai[2] = 1;
		}
        
        float num26 = (float)Math.Sqrt((double)(NPC.velocity.X * NPC.velocity.X + NPC.velocity.Y * NPC.velocity.Y));
        float num27 = NPC.localAI[0];
        if (num27 == 0f)
        {
            NPC.localAI[0] = num26;
            num27 = num26;
        }
        if (NPC.alpha > 0)
        {
            NPC.alpha -= 25;
        }
        if (NPC.alpha < 0)
        {
            NPC.alpha = 0;
        }
        float targetPositionX = NPC.position.X;
        float targetPositionY = NPC.position.Y;
		Vector2 targetPos = NPC.position;
        float desiredDistanceForLockOn = NPC.ai[2] == 1 ? 100000 : NPC.velocity.Length() * 1f;
		//Main.NewText(NPC.velocity.Length());
        bool flag = false;
        int foundPlayer = 0;

		if (NPC.ai[1] == 0)
		{
			for (int playerIndex = 0; playerIndex < Main.player.Length; playerIndex++)
			{
				Player target = Main.player[playerIndex];
				if (target.active && target.statLife > 0 && (NPC.ai[1] == 0f || NPC.ai[1] == playerIndex + 1) &&
					Vector2.Distance(target.Center, NPC.Center) < desiredDistanceForLockOn &&
					Collision.CanHit(new Vector2(NPC.position.X + NPC.width / 2, NPC.position.Y + NPC.height / 2), 1, 1,
						target.position, target.width, target.height))
				{
					flag = true;
					//targetPos = target.Center;
					targetPositionX = target.Center.X;
					targetPositionY = target.Center.Y;
					foundPlayer = playerIndex;
				}
			}
			if (flag)
			{
				NPC.ai[1] = foundPlayer + 1;
			}
			flag = false;
		}

		// OLD CODE
		//if (NPC.ai[1] == 0)
		//{
		//    for (int playerIndex = 0; playerIndex < Main.player.Length; playerIndex++)
		//    {
		//        if (Main.player[playerIndex].active && Main.player[playerIndex].statLife > 0 && (NPC.ai[1] == 0f || NPC.ai[1] == playerIndex + 1))
		//        {
		//            float playerCenterX = Main.player[playerIndex].position.X + Main.player[playerIndex].width / 2;
		//            float playerCenterY = Main.player[playerIndex].position.Y + Main.player[playerIndex].height / 2;
		//            float distanceBetweenPlayerAndNPC = Math.Abs(NPC.position.X + NPC.width / 2 - playerCenterX) + Math.Abs(NPC.position.Y + NPC.height / 2 - playerCenterY);
		//            if (distanceBetweenPlayerAndNPC < desiredDistanceForLockOn && Collision.CanHit(new Vector2(NPC.position.X + NPC.width / 2, NPC.position.Y + NPC.height / 2), 1, 1, Main.player[playerIndex].position, Main.player[playerIndex].width, Main.player[playerIndex].height))
		//            {
		//                desiredDistanceForLockOn = distanceBetweenPlayerAndNPC;
		//                targetPositionX = playerCenterX;
		//                targetPositionY = playerCenterY;
		//                flag = true;
		//                foundPlayer = playerIndex;
		//            }
		//        }
		//    }
		//    if (flag)
		//    {
		//        NPC.ai[1] = foundPlayer + 1;
		//    }
		//    flag = false;
		//}
		if (NPC.ai[1] != 0f)
        {
            int num36 = (int)(NPC.ai[1] - 1f);
            if (Main.player[num36].active)
            {
                float num37 = Main.player[num36].position.X + Main.player[num36].width / 2;
                float num38 = Main.player[num36].position.Y + Main.player[num36].height / 2;
                float num39 = Math.Abs(NPC.position.X + NPC.width / 2 - num37) + Math.Abs(NPC.position.Y + NPC.height / 2 - num38);
                if (num39 < 1000f)
                {
                    flag = true;
                    targetPositionX = Main.player[num36].position.X + Main.player[num36].width / 2;
                    targetPositionY = Main.player[num36].position.Y + Main.player[num36].height / 2;
                }
            }
        }
        if (flag || NPC.ai[2] == 1)
        {
            float num40 = num27;
            float xDistance = targetPositionX - NPC.Center.X;
            float yDistance = targetPositionY - NPC.Center.Y;
            float num43 = (float)Math.Sqrt((double)(xDistance * xDistance + yDistance * yDistance));
            num43 = num40 / num43;
            xDistance *= num43;
            yDistance *= num43;
            int num44 = 8;
            NPC.velocity.X = (NPC.velocity.X * (num44 - 1) + xDistance) / num44;
            NPC.velocity.Y = (NPC.velocity.Y * (num44 - 1) + yDistance) / num44;
        }
	}


	// OTHER CODE, DOESN'T WORK PROPERLY
	private void HomeInWithinRange(float range)
	{
		// Find closest NPC within homing range
		Player closestPlayer = null;
		float closestDistance = range;

		for (int playerIndex = 0; playerIndex < Main.player.Length; playerIndex++)
		{
			//if (Collision.CanHit(new Vector2(NPC.position.X + NPC.width / 2, NPC.position.Y + NPC.height / 2), 1, 1,
			//			Main.player[playerIndex].position, Main.player[playerIndex].width, Main.player[playerIndex].height))
			{
				float distanceToPlayer = Vector2.Distance(Main.player[playerIndex].Center, NPC.Center);
				if (distanceToPlayer < closestDistance)
				{
					closestDistance = distanceToPlayer;
					closestPlayer = Main.player[playerIndex];
				}
			}
		}

		// Home in on closest NPC within range
		if (closestPlayer != null)
		{
			//Vector2 direction = closestPlayer.Center - NPC.Center;
			//direction.Normalize();
			//NPC.velocity = Vector2.Lerp(NPC.velocity, direction * 2, 0.1f);

			float targetPositionX = NPC.position.X;
			float targetPositionY = NPC.position.Y;

			if (closestPlayer.active)
			{
				float num37 = closestPlayer.position.X + closestPlayer.width / 2;
				float num38 = closestPlayer.position.Y + closestPlayer.height / 2;
				float num39 = Math.Abs(NPC.position.X + NPC.width / 2 - num37) + Math.Abs(NPC.position.Y + NPC.height / 2 - num38);
				if (num39 < 1000f)
				{
					//flag = true;
					targetPositionX = closestPlayer.position.X + closestPlayer.width / 2;
					targetPositionY = closestPlayer.position.Y + closestPlayer.height / 2;
				}
			}

			float num26 = (float)Math.Sqrt((double)(NPC.velocity.X * NPC.velocity.X + NPC.velocity.Y * NPC.velocity.Y));
			float num27 = NPC.localAI[0];
			if (num27 == 0f)
			{
				NPC.localAI[0] = num26;
				num27 = num26;
			}
			float num40 = num27;
			float xDistance = targetPositionX - NPC.Center.X;
			float yDistance = targetPositionY - NPC.Center.Y;
			float num43 = (float)Math.Sqrt((double)(xDistance * xDistance + yDistance * yDistance));
			num43 = num40 / num43;
			xDistance *= num43;
			yDistance *= num43;
			int num44 = 8;
			NPC.velocity.X = (NPC.velocity.X * (num44 - 1) + xDistance) / num44;
			NPC.velocity.Y = (NPC.velocity.Y * (num44 - 1) + yDistance) / num44;
		}
	}
}
