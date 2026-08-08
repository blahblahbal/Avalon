using Avalon.Common.Players;
using Avalon.Projectiles.Summon.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.Summons;


public class PrimeArms : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.buffNoSave[Type] = false;
	}
	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		tip += "\n" + Language.GetTextValue("Mods.Avalon.TooltipEdits.UpgradeStage") + Main.LocalPlayer.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()];
		base.ModifyBuffText(ref buffName, ref tip, ref rare);
	}
	public override void Update(Player player, ref int buffIndex)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] > 0)
		{
			player.GetModPlayer<AvalonPlayer>().PrimeMinion = true;
		}
		if (!player.GetModPlayer<AvalonPlayer>().PrimeMinion)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
		if (player.whoAmI == Main.myPlayer)
		{
			UpdatePrimeMinionStatus(player);
		}
	}
	private void UpdatePrimeMinionStatus(Player player)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] < 1)
		{
			foreach (var projectile in Main.ActiveProjectiles)
			{
				if (projectile.owner == player.whoAmI)
				{
					if (projectile.type == ModContent.ProjectileType<PriminiCannon>() || projectile.type == ModContent.ProjectileType<PriminiLaser>() || projectile.type == ModContent.ProjectileType<PriminiSaw>() || projectile.type == ModContent.ProjectileType<PriminiVice>())
					{
						projectile.Kill();
					}
				}
			}
		}
		else if (player.ownedProjectileCounts[ModContent.ProjectileType<PriminiCannon>()] < 1)
		{
			IEntitySource source = player.GetSource_Misc("PrimeTierSwap");

			Vector2 cannonPos = player.Center + new Vector2(40f, -40f);
			Vector2 laserPos = player.Center + new Vector2(-40f);
			Vector2 sawPos = player.Center + new Vector2(-40f, 40f);
			Vector2 vicePos = player.Center + new Vector2(40f);

			Projectile p1 = Projectile.NewProjectileDirect(source, cannonPos, Vector2.Zero, ModContent.ProjectileType<PriminiCannon>(), 0, 0f, player.whoAmI);
			Projectile p2 = Projectile.NewProjectileDirect(source, laserPos, Vector2.Zero, ModContent.ProjectileType<PriminiLaser>(), 0, 0f, player.whoAmI);
			Projectile p3 = Projectile.NewProjectileDirect(source, sawPos, Vector2.Zero, ModContent.ProjectileType<PriminiSaw>(), 0, 0f, player.whoAmI);
			Projectile p4 = Projectile.NewProjectileDirect(source, vicePos, Vector2.Zero, ModContent.ProjectileType<PriminiVice>(), 0, 0f, player.whoAmI);
			p1.rotation = p1.Center.AngleTo(player.Center);
			p2.rotation = p2.Center.AngleTo(player.Center);
			p3.rotation = p3.Center.AngleTo(player.Center);
			p4.rotation = p4.Center.AngleTo(player.Center);
		}
	}
}
