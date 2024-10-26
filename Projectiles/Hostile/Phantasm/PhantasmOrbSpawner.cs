using Avalon.NPCs.Bosses.Hardmode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.Phantasm
{
	public class PhantasmOrbSpawner : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.hide = true;
			Projectile.aiStyle = -1;
			Projectile.extraUpdates = 5;
			Projectile.timeLeft = 300;
			Projectile.Size = new Vector2(16);
		}
		public override void OnKill(int timeLeft)
		{
			 if(Main.netMode!= NetmodeID.MultiplayerClient)
			{
				Point point = (Projectile.Center - (Projectile.oldVelocity * 16)).ToPoint();
				NPC.NewNPC(Projectile.GetSource_FromThis(), point.X, point.Y, ModContent.NPCType<PhantasmHealthOrbs>(), 0, Projectile.ai[0]);
			}
		}
	}
}
