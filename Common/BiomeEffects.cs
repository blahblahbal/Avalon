//using Avalon.Common.Players;
//using Avalon.Particles;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ModLoader;
// these need a rework I think
//namespace Avalon.Common
//{
//	public class BiomeEffects : ModPlayer
//	{
//		public override void PostUpdate()
//		{
//			if (Player.whoAmI == Main.myPlayer && ModContent.GetInstance<AvalonClientConfig>().BiomeParticlesEnabled)
//			{
//				if (ExxoAvalonOrigins.Depths != null && (bool)ExxoAvalonOrigins.Depths.Call("InDepths", Player)) //for the future //THE FUTURE IS NOW OLD MAN
//				{
//					return;
//				}
//				AvalonBiomePlayer bp = Player.GetModPlayer<AvalonBiomePlayer>();

//				if (bp.ZoneHellcastle || Player.position.Y > (Main.UnderworldLayer - 100) * 16)
//				{
//					if (Main.rand.NextBool(20) || (Main.rand.NextBool(3) && (bp.ZoneHellcastle || (Player.position.Y > (Main.UnderworldLayer - 40) * 16))))
//					{
//						Vector2 spawnPos = new Vector2(Main.rand.Next(-2000, 2000) + Player.position.X, MathHelper.Clamp(Player.position.Y + 600, Main.UnderworldLayer * 16, (Main.maxTilesY - 37) * 16));
//						if ((bp.ZoneNearHellcastle || bp.ZoneHellcastle) && !Main.rand.NextBool(6))
//						{
//							var p = SoulEmbers.Request();
//							p.Velocity = new Vector2(Main.rand.NextFloat(-5, 5), -1);
//							p.Position = spawnPos;
//							Main.ParticleSystem_World_OverPlayers.Add(p);
//						}
//						else
//						{
//							var p = HellEmbers.Request();
//							p.Velocity = new Vector2(Main.rand.NextFloat(-5, 5), -1);
//							p.Position = spawnPos;
//							Main.ParticleSystem_World_OverPlayers.Add(p);
//						}
//					}
//				}
//			}
//		}
//	}
//}
