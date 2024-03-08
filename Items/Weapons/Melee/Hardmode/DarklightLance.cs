using Avalon.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode; 

public class DarklightLance : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetStaticDefaults()
    {
        ItemID.Sets.Spears[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.UseSound = SoundID.Item1;
        Item.damage = 99;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 4f;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.knockBack = 5.5f;
        Item.shoot = ModContent.ProjectileType<DarklightLanceProjectile>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 40, 0, 0);
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
    public override bool? UseItem(Player player)
    {
        if (!Main.dedServ && Item.UseSound.HasValue)
        {
            SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
        }

        return null;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.DarkLance)
            .AddIngredient(ItemID.Gungnir)
            .AddIngredient(ItemID.SoulofFright)
            .AddIngredient(ItemID.DarkShard)
            .AddIngredient(ItemID.LightShard);
    }
}
