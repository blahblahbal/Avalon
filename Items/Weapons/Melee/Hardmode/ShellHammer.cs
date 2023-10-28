using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class ShellHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 42;
        Item.knockBack = 12f;
        Item.useTurn = false;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = Item.maxStack = 1;
        Item.useTime = 50;
        Item.useAnimation = 35;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Shell>();
        Item.shootSpeed = 7f;
        Item.damage = 98;
        Item.value = Item.sellPrice(0, 6, 20);
    }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 0.5f);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        velocity.Y -= 3f;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.ChlorophyteBar, 18)
            .AddIngredient(ItemID.TurtleShell)
            //.AddIngredient(ModContent.ItemType<VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
