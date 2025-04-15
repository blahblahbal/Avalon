using Avalon.Common.Players;
using Avalon.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode;

public class PrimeStaff : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Summon;
        Item.damage = 50;
        Item.shootSpeed = 14f;
        Item.mana = 14;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.knockBack = 6.5f;
        //Item.shoot = ModContent.ProjectileType<Projectiles.Summon.PrimeArmsCounter>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 10);
        Item.useAnimation = 30;
        Item.height = dims.Height;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item44;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Bone, 50)
            .AddIngredient(ItemID.HallowedBar, 12)
            .AddIngredient(ItemID.SoulofFright, 20)
            .AddIngredient(ModContent.ItemType<Material.Shards.DemonicShard>(), 3)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        return true;
    }
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<AvalonPlayer>().UpdatePrimeMinionStatus(player.GetSource_ItemUse_WithPotentialAmmo(Item, 0));
            return true;
        }
        return base.UseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        player.GetModPlayer<AvalonPlayer>().UpdatePrimeMinionStatus(source);
        //Main.NewText(player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.PriminiCannon>()]);
        //for (int i = 0; i < Main.maxProjectiles; i++)
        //{
        //    if (Main.projectile[i].type == ModContent.ProjectileType<Projectiles.Summon.PrimeArmsCounter>() && Main.projectile[i].owner == Main.myPlayer)
        //    {
        //        Main.projectile[i].minionSlots++;
        //    }
        //}
        return true;
    }
}
