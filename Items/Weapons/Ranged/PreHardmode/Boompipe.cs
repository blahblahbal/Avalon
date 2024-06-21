using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class Boompipe : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Boompipe");
        Item.autoReuse = true;
        Item.damage = 11;
        Item.scale = 1f;
        Item.shootSpeed = 14.5f;
        Item.useAmmo = AmmoID.Dart;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 40;
        Item.knockBack = 3.5f;
        Item.shoot = ProjectileID.Seed;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 24000;
        Item.useAnimation = 40;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Orange;
    }
    public override void UseItemFrame(Player player)
    {
        player.bodyFrame.Y = player.bodyFrame.Height * 2;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, -7);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
		int amount = Main.rand.Next(4, 7);
		for (int i = 0; i < amount; i++)
		{
			Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(14));
			if (i == 0)
			{
				perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
				Projectile P = Projectile.NewProjectileDirect(source, position - new Vector2(0, 8), perturbedSpeed * Main.rand.NextFloat(0.85f, 1.1f), type, damage / 2, knockback);
				P.GetGlobalProjectile<BoompipeProjVisuals>().Shards = true;
			}
			else
			{
				Projectile P = Projectile.NewProjectileDirect(source, position - new Vector2(0, 8), perturbedSpeed * Main.rand.NextFloat(0.75f, 1f), ModContent.ProjectileType<BoompipeShrapnel>(), damage / 2, knockback);
				P.GetGlobalProjectile<BoompipeProjVisuals>().Shards = true;
			}
			if (!Main.rand.NextBool(3) && i != 0)
			{
				Gore G = Gore.NewGoreDirect(source, position + Vector2.Normalize(perturbedSpeed) * 50 - new Vector2(16, 24), Vector2.Zero, Main.rand.NextFromList(GoreID.Smoke1, GoreID.Smoke2, GoreID.Smoke3), Main.rand.NextFloat(0.7f, 1f));
				G.velocity = perturbedSpeed * 0.18f * Main.rand.NextFloat(0.45f, 1f);
				G.alpha = Main.rand.Next(80, 175);
			}
			Dust D = Dust.NewDustDirect(position + Vector2.Normalize(perturbedSpeed) * 50 - new Vector2(0, 14), default, default, DustID.Smoke, perturbedSpeed.X * Main.rand.NextFloat(0.55f, 1.1f), perturbedSpeed.Y * Main.rand.NextFloat(0.55f, 1.1f), 100, Main.rand.NextFromList(Color.LightGray, Color.Gray), Main.rand.NextFloat(0.7f, 1.3f));
			D.velocity = perturbedSpeed * 0.15f * Main.rand.NextFloat(0.65f, 1f);
			D.alpha = Main.rand.Next(120, 175);
			D.fadeIn = 1;
			Vector2 perturbedSpeed2 = velocity.RotatedByRandom(MathHelper.ToRadians(24));
			Dust D2 = Dust.NewDustDirect(position + Vector2.Normalize(perturbedSpeed2) * 50 - new Vector2(0, 14), default, default, DustID.Torch, perturbedSpeed.X * Main.rand.NextFloat(0.55f, 1.1f), perturbedSpeed.Y * Main.rand.NextFloat(0.55f, 1.1f), default, default, Main.rand.NextFloat(0.7f, 1.3f));
			D2.velocity = perturbedSpeed2 * 0.15f * Main.rand.NextFloat(0.65f, 1.5f);
			D2.fadeIn = 1;
			D2.noGravity = true;
		}
		return false;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ItemID.HellstoneBar, 15)
            .AddIngredient(ModContent.ItemType<FireShard>(), 1)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public class BoompipeProjVisuals : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool Shards;
        int blastDust;
        public override void PostAI(Projectile projectile)
        {
            if (Shards && blastDust < 20 && Main.rand.NextBool(3 + blastDust))
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Smoke);
                d.velocity = projectile.velocity.RotatedByRandom(0.1f) * 0.5f * (projectile.extraUpdates + 1);
                d.scale = Main.rand.NextFloat(0.3f, 0.7f);
                d.fadeIn = 2;
                d.noGravity = true;
                if (!Main.rand.NextBool(3))
                {
                    Dust d2 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Torch);
                    d2.velocity = projectile.velocity.RotatedByRandom(0.1f) * 0.5f * (projectile.extraUpdates + 1);
                    d2.scale = Main.rand.NextFloat(0.6f, 1.1f);
                    d2.fadeIn = 1;
                    d2.noGravity = true;
                }
            }
            blastDust++;
        }
    }
}
