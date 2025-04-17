using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class BloodyAmulet : ModProjectile
{
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.penetrate = -1;
		Projectile.width = dims.Width * 10 / 32;
		Projectile.height = dims.Height * 10 / 32 / Main.projFrames[Projectile.type];
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.damage = 0;
		Projectile.tileCollide = false;
	}

	public override void AI()
	{
		SoundEngine.PlaySound(SoundID.Roar, Main.projectile[Projectile.owner].position);
		Main.dayTime = false;
		Main.time = 0;
		Main.bloodMoon = true;
		if (Main.netMode == NetmodeID.SinglePlayer)
		{
			Main.NewText(Lang.misc[8].Value, 50, byte.MaxValue, 130);
		}
		else
		{
			ChatHelper.BroadcastChatMessage(Lang.misc[8].ToNetworkText(), new Color(50, 255, 130));
		}
		Projectile.active = false;
	}
}
