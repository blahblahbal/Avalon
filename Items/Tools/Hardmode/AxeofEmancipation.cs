using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class AxeofEmancipation : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 38;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.axe = 40;
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.useTime = 18;
        Item.knockBack = 7f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 8);
        Item.scale = 1.3f;
        Item.useAnimation = 18;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.HallowedWeapons, player.direction * 2, 0f, 150, default, 1.4f);

        Dust dust = Dust.NewDustDirect
        (
            new Vector2(hitbox.X, hitbox.Y),
            hitbox.Width,
            hitbox.Height,
            DustID.HallowedWeapons,
            player.velocity.X * 0.2f + (player.direction * 3),
            player.velocity.Y * 0.2f,
            100,
            default,
            1.2f
        );

        dust.noGravity = true;
        dust.velocity.X /= 2f;
        dust.velocity.Y /= 2f;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe()
    //        .AddIngredient(ItemID.HallowedBar, 10)
    //        .AddIngredient(ItemID.LunarBar, 10)
    //        .AddIngredient(ModContent.ItemType<Material.SoulofPlight>(), 15)
    //        .AddTile(TileID.MythrilAnvil)
    //        .Register();
    //}
}
