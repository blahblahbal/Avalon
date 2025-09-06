using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	internal class FishingHooks : ModHook
	{
		protected override void Apply()
		{
			On_Projectile.FishingCheck_RollItemDrop += On_Projectile_FishingCheck_RollItemDrop;
		}

		private void On_Projectile_FishingCheck_RollItemDrop(On_Projectile.orig_FishingCheck_RollItemDrop orig, Projectile self, ref FishingAttempt fisher)
		{
			//if (self.Center.Distance(player.Center) < 128f) // placeholder for distance to fishing rift centre
			foreach (var proj in Main.ActiveProjectiles)
			{
				if (proj.type != ModContent.ProjectileType<FishingRiftFront>())
				{
					continue;
				}
				else if (self.Center.Distance(proj.Center) < FishingRiftGlobalProj.MaxDistance)
				{
					var player = Main.player[self.owner];
					bool inCrimson = player.ZoneCrimson;
					bool inCorruption = player.ZoneCorrupt;
					bool inContagion = player.GetModPlayer<AvalonBiomePlayer>().ZoneAnyContagion;
					bool inJungle = player.ZoneJungle;
					bool inSavanna = player.GetModPlayer<AvalonBiomePlayer>().ZoneAnySavanna;

					// there's no reason to set modded biome zones to true/false here since their drops are handled afterwards, which is good because I have no idea how to do it!
					if (inCorruption && !inCrimson && !inContagion)
					{
						player.ZoneCorrupt = false;
						_ = Main.rand.NextBool() ? player.ZoneCrimson = true : player.GetModPlayer<AvalonPlayer>().riftContagionFishing = true;
					}
					else if (inCrimson && !inCorruption && !inContagion)
					{
						player.ZoneCrimson = false;
						_ = Main.rand.NextBool() ? player.ZoneCorrupt = true : player.GetModPlayer<AvalonPlayer>().riftContagionFishing = true;
					}
					else if (inContagion && !inCorruption && !inCrimson)
					{
						_ = Main.rand.NextBool() ? player.ZoneCorrupt = true : player.ZoneCrimson = true;
					}
					if (inJungle && !inSavanna)
					{
						player.ZoneJungle = false;
						player.GetModPlayer<AvalonPlayer>().riftSavannaFishing = true;
					}
					else if (inSavanna && !inJungle)
					{
						player.ZoneJungle = true;
					}

					orig(self, ref fisher);

					player.ZoneCrimson = inCrimson;
					player.ZoneCorrupt = inCorruption;
					player.ZoneJungle = inJungle;
					return;
				}
			}
			orig(self, ref fisher);
		}
	}
}
