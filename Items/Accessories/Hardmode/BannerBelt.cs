using Avalon.Common;
using Avalon.NPCs.Hardmode.Blaze;
using Avalon.NPCs.Hardmode.ContagionMimic;
using Avalon.NPCs.Hardmode.Cougher;
using Avalon.NPCs.Hardmode.CursedFlamer;
using Avalon.NPCs.Hardmode.CursedScepter;
using Avalon.NPCs.Hardmode.EctoHand;
using Avalon.NPCs.Hardmode.Ickslime;
using Avalon.NPCs.Hardmode.IrateBones;
using Avalon.NPCs.Hardmode.ViralMummy;
using Avalon.NPCs.Hardmode.Viris;
using Avalon.NPCs.PreHardmode.Bactus;
using Avalon.NPCs.PreHardmode.BloodshotEye;
using Avalon.NPCs.PreHardmode.BoneFish;
using Avalon.NPCs.PreHardmode.FallenHero;
using Avalon.NPCs.PreHardmode.Pyrasite;
using Avalon.NPCs.PreHardmode.Rafflesia;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class BannerBelt : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(gold: 5);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<BannerBeltPlayer>().BannerBelt = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Leather, 10)
			.AddRecipeGroup("Avalon:Banners", 8)
			.AddIngredient(ItemID.SoulofMight, 10)
			.AddIngredient(ItemID.SoulofLight, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class BannerBeltPlayer : ModPlayer
{
	public List<int> Banners = new();
	public bool BannerBelt = false;
	public override void ResetEffects()
	{
		BannerBelt = false;
		Banners.Clear();
	}
	public void UpdateBanners(Player Player)
	{
		for (int i = 0; i < Player.inventory.Length; i++)
		{
			if (Player.inventory[i].createTile == TileID.Banners)
			{
				Banners.Add(Player.inventory[i].placeStyle - 21);
			}
			if (Player.inventory[i].createTile == ModContent.TileType<MonsterBanner>())
			{
				Banners.Add(ModdedBanners(Player.inventory[i].placeStyle));
			}
		}
		for (int i = 0; i < Player.bank.item.Length; i++)
		{
			if (Player.bank.item[i].createTile == TileID.Banners)
			{
				Banners.Add(Player.bank.item[i].placeStyle - 21);
			}
			if (Player.bank.item[i].createTile == ModContent.TileType<MonsterBanner>())
			{
				Banners.Add(ModdedBanners(Player.bank.item[i].placeStyle));
			}
		}
		for (int i = 0; i < Player.bank2.item.Length; i++)
		{
			if (Player.bank2.item[i].createTile == TileID.Banners)
			{
				Banners.Add(Player.bank2.item[i].placeStyle - 21);
			}
			if (Player.bank2.item[i].createTile == ModContent.TileType<MonsterBanner>())
			{
				Banners.Add(ModdedBanners(Player.bank2.item[i].placeStyle));
			}
		}
		for (int i = 0; i < Player.bank3.item.Length; i++)
		{
			if (Player.bank3.item[i].createTile == TileID.Banners)
			{
				Banners.Add(Player.bank3.item[i].placeStyle - 21);
			}
			if (Player.bank3.item[i].createTile == ModContent.TileType<MonsterBanner>())
			{
				Banners.Add(ModdedBanners(Player.bank3.item[i].placeStyle));
			}
		}
		for (int i = 0; i < Player.bank4.item.Length; i++)
		{
			if (Player.bank4.item[i].createTile == TileID.Banners)
			{
				Banners.Add(Player.bank4.item[i].placeStyle - 21);
			}
			if (Player.bank4.item[i].createTile == ModContent.TileType<MonsterBanner>())
			{
				Banners.Add(ModdedBanners(Player.bank4.item[i].placeStyle));
			}
		}

		if (Banners.Count > 0)
		{
			Player.AddBuff(BuffID.MonsterBanner, 2, false);
			for (int k = 0; k < Banners.Count; k++)
			{
				Main.SceneMetrics.hasBanner = true;
				Main.SceneMetrics.NPCBannerBuff[Banners[k]] = true;
			}
		}
	}
	public int ModdedBanners(int placeStyle)
	{
		int t = 1;
		switch (placeStyle)
		{
			//case 0:
			//    t = ModContent.NPCType<NPCs.Mime>();
			//    break;
			//case 1:
			//    t = ModContent.NPCType<NPCs.DarkMatterSlime>();
			//    break;
			//case 2:
			//    t = ModContent.NPCType<NPCs.CursedMagmaSkeleton>();
			//    break;
			//case 4:
			//    t = ModContent.NPCType<NPCs.VampireHarpy>();
			//    break;
			//case 5:
			//    t = ModContent.NPCType<NPCs.ArmoredWraith>();
			//    break;
			//case 6:
			//    t = ModContent.NPCType<NPCs.RedAegisBonesHelmet>();
			//    break;
			case 7:
				t = ModContent.NPCType<BloodshotEye>();
				break;
			//case 8:
			//    t = ModContent.NPCType<NPCs.Dragonfly>();
			//    break;
			case 9:
				t = ModContent.NPCType<Blaze>();
				break;
			//case 10:
			//    t = ModContent.NPCType<NPCs.ArmoredHellTortoise>();
			//    break;
			//case 13:
			//    t = ModContent.NPCType<NPCs.ImpactWizard>();
			//    break;
			//case 14:
			//    t = ModContent.NPCType<NPCs.MechanicalDiggerHead>();
			//    break;
			case 15:
				t = ModContent.NPCType<Cougher>();
				break;
			case 16:
				t = ModContent.NPCType<Bactus>();
				break;
			case 17:
				t = ModContent.NPCType<Ickslime>();
				break;
			//case 18:
			//    t = ModContent.NPCType<NPCs.GrossyFloat>();
			//    break;
			case 19:
				t = ModContent.NPCType<PyrasiteHead>();
				break;
			//case 20:
			//    t = ModContent.NPCType<NPCs.EyeBones>();
			//    break;
			//case 21:
			//    t = ModContent.NPCType<NPCs.Ectosphere>();
			//    break;
			//case 22:
			//    t = ModContent.NPCType<NPCs.BombSkeleton>();
			//    break;
			//case 23:
			//    t = ModContent.NPCType<NPCs.CopperSlime>();
			//    break;
			//case 24:
			//    t = ModContent.NPCType<NPCs.TinSlime>();
			//    break;
			//case 25:
			//    t = ModContent.NPCType<NPCs.IronSlime>();
			//    break;
			//case 26:
			//    t = ModContent.NPCType<NPCs.LeadSlime>();
			//    break;
			//case 27:
			//    t = ModContent.NPCType<NPCs.SilverSlime>();
			//    break;
			//case 28:
			//    t = ModContent.NPCType<NPCs.TungstenSlime>();
			//    break;
			//case 29:
			//    t = ModContent.NPCType<NPCs.GoldSlime>();
			//    break;
			//case 30:
			//    t = ModContent.NPCType<NPCs.PlatinumSlime>();
			//    break;
			//case 31:
			//    t = ModContent.NPCType<NPCs.CobaltSlime>();
			//    break;
			//case 32:
			//    t = ModContent.NPCType<NPCs.PalladiumSlime>();
			//    break;
			//case 33:
			//    t = ModContent.NPCType<NPCs.MythrilSlime>();
			//    break;
			//case 34:
			//    t = ModContent.NPCType<NPCs.OrichalcumSlime>();
			//    break;
			//case 35:
			//    t = ModContent.NPCType<NPCs.AdamantiteSlime>();
			//    break;
			//case 36:
			//    t = ModContent.NPCType<NPCs.TitaniumSlime>();
			//    break;
			//case 37:
			//    t = ModContent.NPCType<NPCs.RhodiumSlime>();
			//    break;
			//case 38:
			//    t = ModContent.NPCType<NPCs.OsmiumSlime>();
			//    break;
			//case 39:
			//    t = ModContent.NPCType<NPCs.DurantiumSlime>();
			//    break;
			//case 40:
			//    t = ModContent.NPCType<NPCs.NaquadahSlime>();
			//    break;
			//case 41:
			//    t = ModContent.NPCType<NPCs.TroxiniumSlime>();
			//    break;
			//case 42:
			//    t = ModContent.NPCType<NPCs.UnstableAnomaly>();
			//    break;
			//case 43:
			//    t = ModContent.NPCType<NPCs.MatterMan>();
			//    break;
			//case 44:
			//    t = ModContent.NPCType<NPCs.BronzeSlime>();
			//    break;
			//case 45:
			//    t = ModContent.NPCType<NPCs.NickelSlime>();
			//    break;
			//case 46:
			//    t = ModContent.NPCType<NPCs.ZincSlime>();
			//    break;
			//case 47:
			//    t = ModContent.NPCType<NPCs.BismuthSlime>();
			//    break;
			//case 48:
			//    t = ModContent.NPCType<NPCs.IridiumSlime>();
			//    break;
			//case 49:
			//    t = ModContent.NPCType<NPCs.Hallowor>();
			//    break;
			case 51:
				t = ModContent.NPCType<IrateBones>();
				break;
			case 55:
				t = ModContent.NPCType<CursedScepter>();
				break;
			case 56:
				t = ModContent.NPCType<EctoHand>();
				break;
			//case 57:
			//    t = ModContent.NPCType<NPCs.CloudBat>();
			//    break;
			//case 58:
			//    t = ModContent.NPCType<NPCs.Valkyrie>();
			//    break;
			//case 59:
			//    t = ModContent.NPCType<NPCs.CaesiumSeekerHead>();
			//    break;
			//case 60:
			//    t = ModContent.NPCType<NPCs.CaesiumBrute>();
			//    break;
			//case 61:
			//    t = ModContent.NPCType<NPCs.CaesiumStalker>();
			//    break;
			case 62:
				t = ModContent.NPCType<Rafflesia>();
				break;
			//case 63:
			//    t = ModContent.NPCType<NPCs.PoisonDartFrog>();
			//    break;
			//case 64:
			//    t = ModContent.NPCType<NPCs.CometTail>();
			//    break;
			//case 65:
			//    t = ModContent.NPCType<NPCs.EvilVulture>();
			//    break;
			//case 66:
			//    t = ModContent.NPCType<NPCs.CrystalBones>();
			//    break;
			//case 67:
			//    t = ModContent.NPCType<NPCs.CrystalSpectre>();
			//    break;
			case 68:
				t = ModContent.NPCType<CursedFlamer>();
				break;
			case 69:
				t = ModContent.NPCType<FallenHero>();
				break;
			//case 70:
			//    t = ModContent.NPCType<NPCs.QuickCaribe>();
			//    break;
			//case 71:
			//    t = ModContent.NPCType<NPCs.VorazylcumMite>();
			//    break;
			//case 72:
			//    t = ModContent.NPCType<NPCs.UnvolanditeMite>();
			//    break;
			//case 73:
			//    t = ModContent.NPCType<NPCs.JuggernautSorcerer>();
			//    break;
			//case 74:
			//    t = 0; // ModContent.NPCType<NPCs.JuggernautSwordsman>();
			//    break;
			//case 75:
			//    t = 0; // ModContent.NPCType<NPCs.JuggernautBrute>();
			//    break;
			//case 76:
			//    t = ModContent.NPCType<NPCs.GuardianCorruptor>();
			//    break;
			//case 77:
			//    t = ModContent.NPCType<NPCs.TropicalSlime>();
			//    break;
			case 78:
				t = ModContent.NPCType<ViralMummy>();
				break;
			case 79:
				t = ModContent.NPCType<Viris>();
				break;
			case 80:
				t = ModContent.NPCType<BoneFish>();
				break;
			case 81:
				t = ModContent.NPCType<ContagionMimic>();
				break;
			default:
				t = 0;
				break;
		}
		return t;
	}
	public override void UpdateEquips()
	{
		if (BannerBelt)
		{
			UpdateBanners(Player);
		}
	}
}
public class BannerBeltHook : ModHook
{
	protected override void Apply()
	{
		On_SceneMetrics.Reset += On_SceneMetrics_Reset;
		On_Player.ApplyBannerOffenseBuff_int_refHitModifiers += On_Player_ApplyBannerOffenseBuff_int_refHitModifiers;
		On_Player.ApplyBannerDefenseBuff_int_refHurtModifiers += On_Player_ApplyBannerDefenseBuff_int_refHurtModifiers;
	}

	private void On_Player_ApplyBannerDefenseBuff_int_refHurtModifiers(On_Player.orig_ApplyBannerDefenseBuff_int_refHurtModifiers orig, Player self, int bannerId, ref Player.HurtModifiers modifiers)
	{
		orig.Invoke(self, bannerId, ref modifiers);
		modifiers.IncomingDamageMultiplier *= self.GetModPlayer<BannerBeltPlayer>().BannerBelt ? 0.75f : 1f;
	}

	private void On_Player_ApplyBannerOffenseBuff_int_refHitModifiers(On_Player.orig_ApplyBannerOffenseBuff_int_refHitModifiers orig, Player self, int bannerId, ref NPC.HitModifiers modifiers)
	{
		orig.Invoke(self, bannerId, ref modifiers);
		modifiers.TargetDamageMultiplier *= self.GetModPlayer<BannerBeltPlayer>().BannerBelt ? 1.5f : 1f;
	}

	private void On_SceneMetrics_Reset(On_SceneMetrics.orig_Reset orig, SceneMetrics self)
	{
		orig.Invoke(self);
		if (Main.gameMenu)
			return;
		List<Player> beltPlayer = new();
		foreach (var player in Main.ActivePlayers)
		{
			if (player.active && !player.dead && player.GetModPlayer<BannerBeltPlayer>().BannerBelt)
			{
				beltPlayer.Add(player);
				player.GetModPlayer<BannerBeltPlayer>().UpdateBanners(player);
			}
		}
		foreach (var player in Main.ActivePlayers)
		{
			for (int p = 0; p < beltPlayer.Count; p++)
			{
				if (player.active && !player.dead && Vector2.Distance(player.Center, beltPlayer[p].Center) < 16 * 75)
				{
					player.GetModPlayer<BannerBeltPlayer>().UpdateBanners(beltPlayer[p]);
				}
			}
		}
	}
}
