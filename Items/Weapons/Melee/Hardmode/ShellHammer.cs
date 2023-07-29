using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class ShellHammer : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 42;
        Item.knockBack = 12f;
        Item.useTurn = Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = Item.maxStack = 1;
        Item.useTime = 75;
        Item.useAnimation = 35;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Shell>();
        Item.shootSpeed = 7f;
        Item.damage = 87;
        Item.value = Item.sellPrice(0, 6, 20);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        velocity.Y = -5f;
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
