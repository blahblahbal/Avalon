using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Projectiles.Melee;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class CaesiumScimitar : ModItem
{
    //public override void SetStaticDefaults()
    //{
    //    DisplayName.SetDefault("Caesium Scimitar");
    //    Tooltip.SetDefault("Explodes foes on hit");
    //    SacrificeTotal = 1;
    //}
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 42;
        Item.UseSound = SoundID.Item1;
        Item.damage = 66;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Lime;
        Item.useTime = 18;
        Item.knockBack = 8f;
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.useAnimation = 18;
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        SoundEngine.PlaySound(SoundID.Item14, target.position);
        Projectile.NewProjectile(Item.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ModContent.ProjectileType<CaesiumExplosion>(), Item.damage, 5f, player.whoAmI);
        target.AddBuff(BuffID.OnFire3, 60 * 5);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.CaesiumBar>(), 32)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
