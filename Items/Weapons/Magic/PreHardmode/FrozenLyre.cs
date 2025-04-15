using Avalon.Items.Material.Shards;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class FrozenLyre : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = note;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 16;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.shootSpeed = 6f;
        Item.mana = 4;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = 20;
        Item.knockBack = 1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.IceNote>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.holdStyle = 3;
        Item.value = Item.sellPrice(0, 0, 40, 0);
        Item.useAnimation = 20;
    }
    public SoundStyle note = new SoundStyle("Terraria/Sounds/Item_26")
    {
        Volume = 1f,
        Pitch = 0f,
        PitchVariance = 0.5f,
        MaxInstances = 10,
    };
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6, 0);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddRecipeGroup("IronBar", 4)
            .AddIngredient(ModContent.ItemType<Icicle>(), 50)
            .AddIngredient(ItemID.FallenStar, 8)
            .AddIngredient(ModContent.ItemType<FrostShard>(), 4)
            .AddTile(TileID.IceMachine)
            .Register();
    }
}
