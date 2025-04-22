using Avalon.Projectiles.Tools;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

internal class TorchLauncher : ModItem
{
    //public static readonly Dictionary<int, int> TorchProjectile = new()
    //{
    //    { ItemID.Torch, ModContent.ProjectileType<Torch>() },
    //    { ItemID.BlueTorch, ModContent.ProjectileType<BlueTorch>() },
    //    { ItemID.RedTorch, ModContent.ProjectileType<RedTorch>() },
    //    { ItemID.GreenTorch, ModContent.ProjectileType<GreenTorch>() },
    //    { ItemID.PurpleTorch, ModContent.ProjectileType<PurpleTorch>() },
    //    { ItemID.WhiteTorch, ModContent.ProjectileType<WhiteTorch>() },
    //    { ItemID.YellowTorch, ModContent.ProjectileType<YellowTorch>() },
    //    { ItemID.DemonTorch, ModContent.ProjectileType<DemonTorch>() },
    //    { ItemID.CursedTorch, ModContent.ProjectileType<CursedTorch>() },
    //    { ItemID.IceTorch, ModContent.ProjectileType<IceTorch>() },
    //    { ItemID.OrangeTorch, ModContent.ProjectileType<OrangeTorch>() },
    //    { ItemID.IchorTorch, ModContent.ProjectileType<IchorTorch>() },
    //    { ItemID.UltrabrightTorch, ModContent.ProjectileType<UltrabrightTorch>() },
    //    { ModContent.ItemType<Placeable.Light.HoneyTorch>(), ModContent.ProjectileType<JungleTorch>() },
    //    { ModContent.ItemType<Placeable.Light.PathogenTorch>(), ModContent.ProjectileType<PathogenTorch>() },
    //    { ModContent.ItemType<Placeable.Light.SlimeTorch>(), ModContent.ProjectileType<SlimeTorch>() },
    //    { ModContent.ItemType<Placeable.Light.CyanTorch>(), ModContent.ProjectileType<CyanTorch>() },
    //    { ModContent.ItemType<Placeable.Light.LimeTorch>(), ModContent.ProjectileType<LimeTorch>() },
    //    { ModContent.ItemType<Placeable.Light.BrownTorch>(), ModContent.ProjectileType<BrownTorch>() },
    //    { ItemID.BoneTorch, ModContent.ProjectileType<BoneTorch>() },
    //    { ItemID.RainbowTorch, ModContent.ProjectileType<RainbowTorch>() },
    //    { ItemID.PinkTorch, ModContent.ProjectileType<PinkTorch>() },
    //};

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 1;
        Item.UseSound = SoundID.Item5;
        Item.shootSpeed = 8f;
        Item.useAmmo = 8;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 16;
        Item.knockBack = 0f;
        Item.shoot = ModContent.ProjectileType<Torch>();
        Item.value = 39000;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 16;
        Item.height = dims.Height;
    }

    public override void AddRecipes() => CreateRecipe().AddIngredient(ItemID.Torch, 50)
        .AddIngredient(ItemID.IronBar, 10).AddIngredient(ItemID.Wood, 20).AddTile(TileID.Anvils).Register();
}
