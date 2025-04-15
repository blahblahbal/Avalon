using Avalon.Common.Players;
using Avalon.Items.Armor.Hardmode;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shoes, EquipType.Wings)]
public class InertiaBootsSlower : ModItem
{
	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(1000, 9f, 1.2f, true);
	}

	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.defense = 4;
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 16, 45);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<InertiaBoots>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().NoSticky = true;
		player.accRunSpeed = 8.29f;
		// ADD BACK AFTER CAESIUM ARMOR ADDED
		if (!player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
		{
			player.accRunSpeed = 10.29f;
		}
		else
		{
			player.accRunSpeed = 5f;
		}
		player.rocketBoots = 3;
		player.noFallDmg = true;
		player.blackBelt = true;
		player.iceSkate = true;
		player.wingTime = 1000;
		player.empressBrooch = true;
		player.GetModPlayer<AvalonPlayer>().InertiaBoots = true;

		// ADD BACK AFTER CAESIUM ADDED
		if (!player.vortexStealthActive && !player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
		{
			if (player.controlLeft)
			{
				if (player.velocity.X > (player.vortexStealthActive ? -1f : -4f))
				{
					player.velocity.X -= player.vortexStealthActive ? 0.06f : 0.25f;
				}
				if (player.velocity.X < (player.vortexStealthActive ? -1f : -4f) && player.velocity.X > (player.vortexStealthActive ? -2f : -8f))
				{
					player.velocity.X -= player.vortexStealthActive ? 0.04f : 0.25f;
				}
			}
			if (player.controlRight)
			{
				if (player.velocity.X < (player.vortexStealthActive ? 1f : 4f))
				{
					player.velocity.X += player.vortexStealthActive ? 0.06f : 0.25f;
				}
				if (player.velocity.X > (player.vortexStealthActive ? 1f : 4f) && player.velocity.X < (player.vortexStealthActive ? 2f : 8f))
				{
					player.velocity.X += player.vortexStealthActive ? 0.04f : 0.25f;
				}
			}
		}
	}
}
