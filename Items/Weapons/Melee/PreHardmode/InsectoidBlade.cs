using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class InsectoidBlade : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<Projectiles.Melee.InsectoidBlade>(), 20, 2f, 12, 3.3f, scale: 0.95f, width: 40, height: 40);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 54);
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
			.AddTile(TileID.Anvils)
			.Register();
	}
}
