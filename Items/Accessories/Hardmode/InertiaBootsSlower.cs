using Avalon.Common.Players;
using Avalon.Items.Armor.Hardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shoes, EquipType.Wings)]
class InertiaBootsSlower : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(1000, 9f, 1.2f, true);
    }

    public override void SetDefaults()
    {
        Item.defense = 4;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = 30;
        Item.value = Item.sellPrice(0, 16, 45, 0);
        Item.accessory = true;
        Item.height = 30;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<InertiaBoots>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        //player.Avalon().noSticky = true;
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
        //player.Avalon().inertiaBoots = true;
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
        if (player.velocity.X is > 4f or < -4f)
        {
            var newColor = default(Color);
            var num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Cloud, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 100, newColor, 2f);
            Main.dust[num].noGravity = true;
        }
    }
}
