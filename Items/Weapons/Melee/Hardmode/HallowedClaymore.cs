using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class HallowedClaymore : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public float scaleMult = 1.35f; // set this to same as in the projectile file

	// unused code
	//public override void SetDefaults()
	//{
	//    Rectangle dims = this.GetDims();
	//    Item.UseSound = SoundID.Item1;
	//    Item.DamageType = DamageClass.Melee;
	//    Item.damage = 200;
	//    Item.autoReuse = true;
	//    Item.scale = scaleMult;
	//    Item.crit = 6;
	//    Item.shootSpeed = 6f; // so the knockback works properly
	//    Item.rare = ItemRarityID.Pink;
	//    Item.noUseGraphic = true;
	//    Item.noMelee = true;
	//    Item.width = dims.Width;
	//    Item.height = dims.Height;
	//    Item.useTime = Item.useAnimation = 40;
	//    Item.knockBack = 8f;
	//    Item.shoot = ModContent.ProjectileType<Projectiles.Melee.HallowedClaymore>();
	//    Item.useStyle = ItemUseStyleID.Shoot;
	//    Item.value = Item.sellPrice(0, 5);
	//    Item.ArmorPenetration += 15;
	//}

	//public override bool MeleePrefix()
	//{
	//    return true;
	//}

	//public int swing;
	//private int combo = 1;
	//public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	//{
	//        scaleMult = 1.35f;
	//        Item.scale = scaleMult*1.1f;
	//        Rectangle dims = this.GetDims();
	//        float posMult = 1 + (dims.Height * scaleMult - 26) / 26 * 0.1f;
	//        velocity = Vector2.Zero;
	//        int height = dims.Height;
	//        if (player.gravDir == -1)
	//        {
	//            height = -dims.Height;
	//        }
	//        if (player.direction == -1)
	//        {


	//            if (swing == 1)
	//            {
	//                swing--;
	//                position = player.Center + new Vector2(0, -height * 1.4f * posMult).RotatedBy(MathHelper.PiOver2 * 0.99f);
	//            }
	//            else
	//            {
	//                swing++;
	//                position = player.Center + new Vector2(0, height * 1.4f * posMult).RotatedBy(-MathHelper.PiOver2 * 0.99f);
	//            }

	//        }
	//        else
	//        {
	//            if (swing == 1)
	//            {
	//                swing--;
	//                position = player.Center + new Vector2(0, height * 1.4f * posMult).RotatedBy(MathHelper.PiOver2 * 0.99f);
	//            }
	//            else
	//            {
	//                swing++;
	//                position = player.Center + new Vector2(0, -height * 1.4f * posMult).RotatedBy(-MathHelper.PiOver2 * 0.99f);
	//            }
	//        }

	//    combo++;

	//}
	//public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	//{
	//    Projectile.NewProjectile(source, position,velocity,type, damage * (scaleMult == 3 ? 5 : scaleMult == 2 ? 1 : 5/2), knockback, -1, scaleMult, scaleMult == 3 ? 5f: scaleMult == 2 ? 3 : 2);
	//    return false;
	//}
	//public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	//{

	//}
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
	/*
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 25)
            .AddIngredient(ItemID.SoulofMight, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    */
}
