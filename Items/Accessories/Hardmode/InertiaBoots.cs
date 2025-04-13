using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Armor.Hardmode;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shoes, EquipType.Wings)]
class InertiaBoots : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(1000, 9f, 1.2f, true);
    }

    public override void SetDefaults()
    {
        Item.defense = 4;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = 34;
        Item.value = Item.sellPrice(0, 16, 45, 0);
        Item.accessory = true;
        Item.height = 32;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup("Wings")
            .AddIngredient(ItemID.FrostsparkBoots)
            .AddIngredient(ItemID.BlackBelt)
            .AddIngredient(ItemID.LunarBar, 2)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<InertiaBootsSlower>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
	// TO-DO: Hoverboard style movement (not currently possible with tmod, but just search "1E-05f" in vanilla player.cs to find the vanilla logic if you wanna try your luck)
	public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
	{
		//acceleration = 0.17f;
		acceleration = 0.35f;
	}
	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		ascentWhenFalling = 0.95f;
		ascentWhenRising = 0.15f;
		maxCanAscendMultiplier = 1f;
		maxAscentMultiplier = 1.875f;
		if (player.TryingToHoverUp)
		{
			maxAscentMultiplier = 2.15f;

			player.velocity.Y -= 0.4f * player.gravDir;
			if (player.gravDir == 1f)
			{
				if (player.velocity.Y > 0f)
					player.velocity.Y -= 1f;
				else if (player.velocity.Y > 0f - Player.jumpSpeed)
					player.velocity.Y -= 0.2f;

				if (player.velocity.Y < (0f - Player.jumpSpeed) * 3f)
					player.velocity.Y = (0f - Player.jumpSpeed) * 3f;
			}
			else
			{
				if (player.velocity.Y < 0f)
					player.velocity.Y += 1f;
				else if (player.velocity.Y < Player.jumpSpeed)
					player.velocity.Y += 0.2f;

				if (player.velocity.Y > Player.jumpSpeed * 3f)
					player.velocity.Y = Player.jumpSpeed * 3f;
			}
		}
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().NoSticky = true;
		player.accRunSpeed = 10.29f;
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
		//player.wingsLogic = 45;
		if (!player.mount.Active && player.TryingToHoverDown && !player.controlJump && player.velocity.Y != 0f)
		{
			player.velocity.Y += 0.6f * player.gravDir;
			player.maxFallSpeed = 13.5f;
		}
		//if (player.controlUp && player.controlJump)
		//{
		//    player.velocity.Y = player.velocity.Y - 0.3f * player.gravDir;
		//    if (player.gravDir == 1f)
		//    {
		//        if (player.velocity.Y > 0f)
		//        {
		//            player.velocity.Y = player.velocity.Y - 1f;
		//        }
		//        else if (player.velocity.Y > -Player.jumpSpeed)
		//        {
		//            player.velocity.Y = player.velocity.Y - 0.2f;
		//        }
		//        if (player.velocity.Y < -Player.jumpSpeed * 3f)
		//        {
		//            player.velocity.Y = -Player.jumpSpeed * 3f;
		//        }
		//    }
		//    else
		//    {
		//        if (player.velocity.Y < 0f)
		//        {
		//            player.velocity.Y = player.velocity.Y + 1f;
		//        }
		//        else if (player.velocity.Y < Player.jumpSpeed)
		//        {
		//            player.velocity.Y = player.velocity.Y + 0.2f;
		//        }
		//        if (player.velocity.Y > Player.jumpSpeed * 3f)
		//        {
		//            player.velocity.Y = Player.jumpSpeed * 3f;
		//        }
		//    }
		//}

		// ADD BACK AFTER CAESIUM ADDED
		if (!player.vortexStealthActive && !player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
		{
			float maxSpeedX = 10f;
			float startAccelX = 0.31f;
			float reducedAccelX = 0.29f;
			if (player.TryingToHoverDown && player.controlJump)
			{
				maxSpeedX = 14.5f;
				startAccelX = 0.41f;
				reducedAccelX = 0.39f;
			}
			if (player.controlLeft)
            {
                if (player.velocity.X > (player.vortexStealthActive ? -1f : -(maxSpeedX / 2f)))
                {
                    player.velocity.X -= player.vortexStealthActive ? 0.06f : startAccelX;
                }
                if (player.velocity.X < (player.vortexStealthActive ? -1f : -(maxSpeedX / 2f)) && player.velocity.X > (player.vortexStealthActive ? -2f : -maxSpeedX))
                {
                    player.velocity.X -= player.vortexStealthActive ? 0.04f : reducedAccelX;
                }
            }
            if (player.controlRight)
            {
                if (player.velocity.X < (player.vortexStealthActive ? 1f : maxSpeedX / 2f))
                {
                    player.velocity.X += player.vortexStealthActive ? 0.06f : startAccelX;
                }
                if (player.velocity.X > (player.vortexStealthActive ? 1f : maxSpeedX / 2f) && player.velocity.X < (player.vortexStealthActive ? 2f : maxSpeedX))
                {
                    player.velocity.X += player.vortexStealthActive ? 0.04f : reducedAccelX;
                }
            }
        }
    }
}
