using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Items.Material.Shards;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class UrchinMace : ModItem
{
    //public override void SetStaticDefaults()1
    //{
    //    DisplayName.SetDefault("Marrow Masher");
    //    Tooltip.SetDefault("Critical strikes have increased knockback");
    //}
    public float scaleMult = 1.1f; // set this to same as in the projectile file
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 23;
        Item.autoReuse = true;
        Item.scale = scaleMult;
        Item.crit = 6;
        Item.shootSpeed = 6f; //so the knockback works properly
        Item.rare = ItemRarityID.Blue;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = Item.useAnimation = 40;
        Item.knockBack = 6.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.UrchinMace>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 0, 40, 0);
    }
    public override bool MeleePrefix()
    {
        return true;
    }

    public int swing;
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        Rectangle dims = this.GetDims();
        float posMult = 1 + (dims.Height * scaleMult - 26) / 26 * 0.1f;
        velocity = Vector2.Zero;
        int height = dims.Height;
        if (player.gravDir == -1)
        {
            height = -dims.Height;
        }
        if (swing == 1)
        {
            swing--;
            position = player.Center + new Vector2(0, height * Item.scale * posMult);
        }
        else
        {
            swing++;
            position = player.Center + new Vector2(0, -height * Item.scale * posMult);
        }
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type).AddTile(TileID.WorkBenches)
            .AddIngredient(ModContent.ItemType<WaterShard>(), 2)
            .AddIngredient(ItemID.SandBlock, 25)
            .AddIngredient(ItemID.Coral, 15)
            .AddIngredient(ItemID.ShellPileBlock, 10)
            .AddIngredient(ItemID.SharkFin, 2)
            .Register();
    }
}
