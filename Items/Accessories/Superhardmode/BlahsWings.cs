using Avalon.Common.Players;
using Avalon.Items.Material;
using Avalon.Rarities;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

[AutoloadEquip(EquipType.Wings)]
internal class BlahsWings : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(1000, 9f, 1.2f, true);
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 4;
        Item.rare = ModContent.RarityType<BlahRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(2);
        Item.accessory = true;
        Item.height = dims.Height;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe().AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 40)
    //        .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 20)
    //        .AddIngredient(ModContent.ItemType<SoulofTorture>(), 25).AddIngredient(ModContent.ItemType<InertiaBoots>())
    //        .AddIngredient(ModContent.ItemType<GuardianBoots>()).AddIngredient(ItemID.PhilosophersStone)
    //        .AddIngredient(ModContent.ItemType<SouloftheGolem>()).AddIngredient(ModContent.ItemType<ForsakenRelic>())
    //        .AddIngredient(ModContent.ItemType<BubbleBoost>()).AddIngredient(ModContent.ItemType<LuckyPapyrus>())
    //        .AddTile(ModContent.TileType<SolariumAnvil>()).Register();
    //}

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        //player.GetModPlayer<AvalonPlayer>().BlahWings = true;
        player.GetModPlayer<AvalonPlayer>().NoSticky = true;
        player.pStone = true;
        player.GetModPlayer<AvalonPlayer>().TrapImmune =
            player.GetModPlayer<AvalonPlayer>().HeartGolem =
            player.GetModPlayer<AvalonPlayer>().EtherealHeart = true;
            //player.GetModPlayer<AvalonPlayer>().longInvince2 = true;
        //player.buffImmune[ModContent.BuffType<Buffs.Melting>()] = true;
        player.wingTime = 1000;
        if (player.immune)
        {
            player.GetCritChance(DamageClass.Generic) += 7;
            player.GetDamage(DamageClass.Generic) += 0.07f;
        }

        player.accRunSpeed = 10.29f;
        // add back later
        //if (!player.GetModPlayer<ExxoEquipEffectPlayer>().CaesiumBoostActive)
        //{
        //    player.accRunSpeed = 10.29f;
        //}
        //else
        //{
        //    player.accRunSpeed = 5f;
        //}
        player.rocketBoots = 2;
        player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
        player.noFallDmg = true;
        player.blackBelt = true;
        player.iceSkate = true;
        player.empressBrooch = true;
        if (player.controlUp && player.controlJump)
        {
            player.velocity.Y -= 1f * player.gravDir;
            if (player.gravDir == 1f)
            {
                if (player.velocity.Y > 0f)
                {
                    player.velocity.Y++;
                }
                else if (player.velocity.Y > -Player.jumpSpeed)
                {
                    player.velocity.Y -= 0.5f;
                }

                if (player.velocity.Y < -Player.jumpSpeed * 6f)
                {
                    player.velocity.Y = -Player.jumpSpeed * 6f;
                }
            }
            else
            {
                if (player.velocity.Y < 0f)
                {
                    player.velocity.Y++;
                }
                else if (player.velocity.Y < Player.jumpSpeed)
                {
                    player.velocity.Y += 0.5f;
                }

                if (player.velocity.Y > Player.jumpSpeed * 6f)
                {
                    player.velocity.Y = Player.jumpSpeed * 6f;
                }
            }
        }
        if (!player.vortexStealthActive) //&& !player.GetModPlayer<ExxoEquipEffectPlayer>().CaesiumBoostActive)
        {
            if (player.controlLeft)
            {
                if (player.velocity.X > -5f)
                {
                    player.velocity.X -= 0.31f;
                }
                if (player.velocity.X is < -5f and > -10f)
                {
                    player.velocity.X -= 0.29f;
                }
            }
            if (player.controlRight)
            {
                if (player.velocity.X < 5f)
                {
                    player.velocity.X += 0.31f;
                }
                if (player.velocity.X is > 5f and < 10f)
                {
                    player.velocity.X += 0.29f;
                }
            }
        }
        if (player.velocity.X > 6f || player.velocity.X < -6f)
        {
            var newColor2 = default(Color);
            int num2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height,
                DustID.Torch, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 100, newColor2, 2f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
        }

        player.wallSpeed += 4.5f;
        player.tileSpeed += 4.5f;
    }
}
