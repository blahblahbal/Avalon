using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Gores;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.BiomeLava
{
	public class ContagionLavaStyle : ModSystem
	{
		public override void Load()
		{
			if (ExxoAvalonOrigins.BiomeLava == null)
			{
				return;
			}

			Mod mod = Mod;
			string name = "contagionlava";
			string texture = Mod.Name + "/Compatability/BiomeLava/ContagionLavaStyle";
			string block = Mod.Name + "/Compatability/BiomeLava/ContagionLavaStyle_Block";
			string slope = Mod.Name + "/Compatability/BiomeLava/ContagionLavaStyle_Slope";
			string waterfall = Mod.Name + "/Compatability/BiomeLava/ContagionLavaStyle_Waterfall";
			Func<int> dust = GetSplashDust;
			Func<int> gore = GetDropletGore;
			Func<int, int, float, float, float, Vector3> color = ModifyLight;
			Func<bool> biomeCheck = IsLavaActive;
			Func<bool> fallGlowmask = lavafallGlowmask;
			Func<Player, NPC, int, Action> buff = InflictDebuff;
			Func<bool> keepOnFire = InflictsOnFire;
			ExxoAvalonOrigins.BiomeLava.Call("ModLavaStyle", mod, name, texture, block, slope, waterfall, dust, gore, color, biomeCheck, fallGlowmask, buff, keepOnFire);
		}

		static bool IsLavaActive()
		{
			return Main.waterStyle == ModContent.Find<ModWaterStyle>("Avalon/ContagionWaterStyle").Slot;
		}

		static int GetSplashDust()
		{
			return ModContent.DustType<PathogenDust>();
		}

		static int GetDropletGore()
		{
			return ModContent.GoreType<ContagionLavaDroplet>();
		}

		static Vector3 ModifyLight(int i, int j, float r, float g, float b)
		{
			return new Vector3(0.5f, 0, 2f);
		}

		static bool lavafallGlowmask()
		{
			return true;
		}

		static Action InflictDebuff(Player player, NPC npc, int onfireDuration)
		{
			int buffID = ModContent.BuffType<Pathogen>();
			if (player != null)
			{
				player.AddBuff(buffID, onfireDuration);
			}
			if (npc != null)
			{
				if (Main.remixWorld && !npc.friendly)
				{
					npc.AddBuff(buffID, onfireDuration);
				}
				else
				{
					npc.AddBuff(buffID, onfireDuration);
				}
			}
			return null;
		}

		static bool InflictsOnFire()
		{
			return true;
		}
	}

	public class PhantomOvergrowthLavaStyle : ModSystem
	{
		public override void Load()
		{
			if (ExxoAvalonOrigins.BiomeLava == null)
			{
				return;
			}

			Mod mod = Mod;
			string name = "phantomovergrowthlava";
			string texture = Mod.Name + "/Compatability/BiomeLava/PhantomOvergrowthLavaStyle";
			string block = Mod.Name + "/Compatability/BiomeLava/PhantomOvergrowthLavaStyle_Block";
			string slope = Mod.Name + "/Compatability/BiomeLava/PhantomOvergrowthLavaStyle_Slope";
			string waterfall = Mod.Name + "/Compatability/BiomeLava/PhantomOvergrowthLavaStyle_Waterfall";
			Func<int> dust = GetSplashDust;
			Func<int> gore = GetDropletGore;
			Func<int, int, float, float, float, Vector3> color = ModifyLight;
			Func<bool> biomeCheck = IsLavaActive;
			Func<bool> fallGlowmask = lavafallGlowmask;
			Func<Player, NPC, int, Action> buff = InflictDebuff;
			Func<bool> keepOnFire = InflictsOnFire;
			ExxoAvalonOrigins.BiomeLava.Call("ModLavaStyle", mod, name, texture, block, slope, waterfall, dust, gore, color, biomeCheck, fallGlowmask, buff, keepOnFire);
		}

		static bool IsLavaActive()
		{
			return Main.waterStyle == ModContent.Find<ModWaterStyle>("Avalon/PhantomOvergrowthWaterStyle").Slot;
		}

		static int GetSplashDust()
		{
			return ModContent.DustType<PhantomDust>();
		}

		static int GetDropletGore()
		{
			return ModContent.GoreType<PhantomLavaDroplet>();
		}

		static Vector3 ModifyLight(int i, int j, float r, float g, float b)
		{
			return new Vector3(2f, 1f, 1.2f);
		}

		static bool lavafallGlowmask()
		{
			return true;
		}

		static Action InflictDebuff(Player player, NPC npc, int onfireDuration)
		{
			int buffID = ModContent.BuffType<ShadowCurse>();
			if (player != null)
			{
				player.AddBuff(buffID, onfireDuration);
			}
			if (npc != null)
			{
				if (Main.remixWorld && !npc.friendly)
				{
					npc.AddBuff(buffID, onfireDuration);
				}
				else
				{
					npc.AddBuff(buffID, onfireDuration);
				}
			}
			return null;
		}

		static bool InflictsOnFire()
		{
			return true;
		}
	}

	public class CaesiumBlastplainsLavaStyle : ModSystem
	{
		public override void Load()
		{
			if (ExxoAvalonOrigins.BiomeLava == null)
			{
				return;
			}

			Mod mod = Mod;
			string name = "caesiumblastplainslava";
			string texture = Mod.Name + "/Compatability/BiomeLava/CaesiumBlastplainsLavaStyle";
			string block = Mod.Name + "/Compatability/BiomeLava/CaesiumBlastplainsLavaStyle_Block";
			string slope = Mod.Name + "/Compatability/BiomeLava/CaesiumBlastplainsLavaStyle_Slope";
			string waterfall = Mod.Name + "/Compatability/BiomeLava/CaesiumBlastplainsLavaStyle_Waterfall";
			Func<int> dust = GetSplashDust;
			Func<int> gore = GetDropletGore;
			Func<int, int, float, float, float, Vector3> color = ModifyLight;
			Func<bool> biomeCheck = IsLavaActive;
			Func<bool> fallGlowmask = lavafallGlowmask;
			Func<Player, NPC, int, Action> buff = InflictDebuff;
			Func<bool> keepOnFire = InflictsOnFire;
			ExxoAvalonOrigins.BiomeLava.Call("ModLavaStyle", mod, name, texture, block, slope, waterfall, dust, gore, color, biomeCheck, fallGlowmask, buff, keepOnFire);
		}

		static bool IsLavaActive()
		{
			return Main.waterStyle == ModContent.Find<ModWaterStyle>("Avalon/CaesiumBlastplainsWaterStyle").Slot;
		}

		static int GetSplashDust()
		{
			return ModContent.DustType<CaesiumLavaDust>();
		}

		static int GetDropletGore()
		{
			return ModContent.GoreType<CaesiumLavaDroplet>();
		}

		static Vector3 ModifyLight(int i, int j, float r, float g, float b)
		{
			return new Vector3(2.5f, 0.1f, 0.1f);
		}

		static bool lavafallGlowmask()
		{
			return true;
		}

		static Action InflictDebuff(Player player, NPC npc, int onfireDuration)
		{
			int buffID = BuffID.OnFire3;
			if (player != null)
			{
				player.AddBuff(buffID, onfireDuration);
			}
			if (npc != null)
			{
				if (Main.remixWorld && !npc.friendly)
				{
					npc.AddBuff(buffID, onfireDuration);
				}
				else
				{
					npc.AddBuff(buffID, onfireDuration);
				}
			}
			return null;
		}

		static bool InflictsOnFire()
		{
			return true;
		}
	}
}
