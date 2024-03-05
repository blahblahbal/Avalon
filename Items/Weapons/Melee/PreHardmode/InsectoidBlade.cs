using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class InsectoidBlade : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GoldShortsword);
        Item.width = 38;
        Item.height = 40;
        Item.useTime = Item.useAnimation = 12;
        Item.damage = 20;
        Item.knockBack = 2;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Orange;
        Item.DamageType = DamageClass.Melee;
        Item.value = Item.sellPrice(0, 0, 54, 0);
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.InsectoidBlade>();
        Item.shootSpeed = 3.3f;
    }
    //public override void MeleeEffects(Player player, Rectangle hitbox)
    //{
    //    if (Main.rand.NextBool(3))
    //    {
    //        int dust = Dust.NewDust(
    //            new Vector2(hitbox.X, hitbox.Y),
    //            hitbox.Width,
    //            hitbox.Height,
    //            //DustID.Blood,
    //            ModContent.DustType<Dusts.MosquitoDust>(),
    //            (player.velocity.X * 0.2f) + (player.direction * 3),
    //            player.velocity.Y * 0.2f,
    //            0,
    //            new Color(),
    //            1f
    //        );
    //        Main.dust[dust].noGravity = true;
    //    }
    //}
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 12)
            .AddIngredient(ModContent.ItemType<MosquitoProboscis>(), 12)
            .AddTile(TileID.Anvils).Register();
    }
}
