using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class MarrowMasher : ModItem
{
    //public override void SetStaticDefaults()1
    //{
    //    DisplayName.SetDefault("Marrow Masher");
    //    Tooltip.SetDefault("Critical strikes have increased knockback");
    //}
    public float scaleMult = 1.25f; // set this to same as in the projectile file
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 58;
        Item.autoReuse = true;
        Item.scale = scaleMult;
        Item.crit = 6;
        Item.shootSpeed = 6f; //so the knockback works properly
        Item.rare = ItemRarityID.Green;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = Item.useAnimation = 30;
        Item.knockBack = 6.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.MarrowMasher>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 0, 40, 0);
        Item.ArmorPenetration += 15;
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
        if (swing == 1)
        {
            swing--;
            position = player.Center + new Vector2(0, dims.Height * Item.scale * posMult);
        }
        else
        {
            swing++;
            position = player.Center + new Vector2(0, -dims.Height * Item.scale * posMult);
        }
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
