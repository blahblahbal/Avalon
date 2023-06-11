using Avalon.Common.Players;
using Avalon.Network;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode; 

public class ClearCutter : ModItem
{
    int swingCounter = 0;
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override bool MeleePrefix()
    {
        return true;
    }
    public override void SetDefaults()
    {
        Item.Size = new Vector2(54);
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.LightRed;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTurn = false;
        Item.value = Item.sellPrice(0, 9, 63, 0);
        Item.shootsEveryUse = true;

        Item.noMelee = true;
        Item.damage = 90;
        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.shootSpeed = 9.5f;
        Item.shoot = ModContent.ProjectileType<ClearCutterSlash>();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, (float)player.direction * player.gravDir, player.itemAnimationMax * 0.9f, adjustedItemScale5);
        NetMessage.SendData(13, -1, -1, null, player.whoAmI);
        return false;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.CrystalShard, 35).AddIngredient(ItemID.SoulofLight, 5).AddIngredient(ItemID.PearlstoneBlock,50).AddTile(TileID.MythrilAnvil).Register();
    }
}
