using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class HallowedGreatsword : ModItem
{
    public float scaleMult = 1.35f; // set this to same as in the projectile file
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 88;
        Item.autoReuse = true;
        Item.scale = scaleMult;
        Item.crit = 6;
        Item.shootSpeed = 6f; // so the knockback works properly
        Item.rare = ItemRarityID.Pink;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = Item.useAnimation = 22;
        Item.knockBack = 8f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.HallowedClaymore>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 5);
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
        CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 25)
            .AddIngredient(ItemID.SoulofMight, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
