using Avalon.Common;
using Avalon.NPCs.BloodMoon;
using Avalon.NPCs.Contagion;
using Avalon.NPCs.Corruption;
using Avalon.NPCs.Dungeon;
using Avalon.NPCs.Hell;
using Avalon.NPCs.Hellcastle;
using Avalon.NPCs.Savanna;
using Avalon.Tiles.Furniture.Functional;
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
	public bool BannerBelt = false;
	const int MAX_DISTANCE = 1200; // 75 * 16
	public List<int> ActiveBanners = new();
	public override void ResetEffects()
	{
		BannerBelt = false;
		ActiveBanners.Clear();
	}
	public override void Load()
	{
		On_Player.HasNPCBannerBuff += HasBannerBuff;
		On_Player.ApplyBannerOffenseBuff_int_refHitModifiers += ApplyBannerOffense;
		On_Player.ApplyBannerDefenseBuff_int_refHurtModifiers += ApplyBannerDefense;
	}
	private void ApplyBannerDefense(On_Player.orig_ApplyBannerDefenseBuff_int_refHurtModifiers orig, Player self, int bannerId, ref Player.HurtModifiers modifiers)
	{
		orig.Invoke(self, bannerId, ref modifiers);
		modifiers.IncomingDamageMultiplier *= self.GetModPlayer<BannerBeltPlayer>().BannerBelt ? 0.75f : 1f;
	}
	private void ApplyBannerOffense(On_Player.orig_ApplyBannerOffenseBuff_int_refHitModifiers orig, Player self, int bannerId, ref NPC.HitModifiers modifiers)
	{
		orig.Invoke(self, bannerId, ref modifiers);
		modifiers.TargetDamageMultiplier *= self.GetModPlayer<BannerBeltPlayer>().BannerBelt ? 1.5f : 1f;
	}
	private bool HasBannerBuff(On_Player.orig_HasNPCBannerBuff orig, Player self, int bannerType)
	{
		if (self.GetModPlayer<BannerBeltPlayer>().ActiveBanners.Contains(bannerType))
		{
			return true;
		}
		foreach (Player p in Main.ActivePlayers)
		{
			if (p == self)
				continue;
			if (p.Center.Distance(self.Center) < MAX_DISTANCE)
			{
				if (p.GetModPlayer<BannerBeltPlayer>().ActiveBanners.Contains(bannerType))
					return true;
			}
		}
		return orig(self,bannerType);
	}
	public override void UpdateEquips()
	{
		if (!BannerBelt)
			return;
		for(int i = 0; i < Player.inventory.Length; i++)
		{
			if (Player.inventory[i].createTile == TileID.Banners)
			{
				if (Item.BannerToNPC(Player.inventory[i].type) != 0)
				{
					ActiveBanners.Add(Player.inventory[i].placeStyle);
				}
			}
			else if (NPCLoader.BannerItemToNPC(Player.inventory[i].type) != -1)
			{
				ActiveBanners.Add(NPCLoader.BannerItemToNPC(Player.inventory[i].type));
			}
		}
		_FindBannersInChest(Player.bank);
		_FindBannersInChest(Player.bank2);
		_FindBannersInChest(Player.bank3);
		_FindBannersInChest(Player.bank4);
		Player.AddBuff(BuffID.MonsterBanner, 2, true);
		if (ActiveBanners.Count > 0)
		{
			foreach(Player p in Main.ActivePlayers)
			{
				if (p == Player)
					continue;
				if(p.Center.Distance(Player.Center) < MAX_DISTANCE)
				{
					p.AddBuff(BuffID.MonsterBanner, 2, true);
				}
			}
		}
	}
	private void _FindBannersInChest(Chest chest)
	{
		for (int i = 0; i < chest.item.Length; i++)
		{
			if (chest.item[i].createTile == TileID.Banners)
			{
				if (Item.BannerToNPC(chest.item[i].type) != 0)
				{
					ActiveBanners.Add(chest.item[i].placeStyle);
				}
			}
			else if (NPCLoader.BannerItemToNPC(chest.item[i].type) != -1)
			{
				ActiveBanners.Add(NPCLoader.BannerItemToNPC(chest.item[i].type));
			}
		}
	}
}
