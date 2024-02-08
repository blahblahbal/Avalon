using Avalon.Common.Players;
using Avalon.Network;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode; 

public class HellboundHalberd : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public float scaleMult = 1.35f; // set this to same as in the projectile file
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.UseSound = SoundID.Item1;
        Item.damage = 213;
        Item.scale = scaleMult;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.shootSpeed = 6f; //so the knockback works properly
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.useTime = Item.useAnimation = 26;
        Item.knockBack = 9f;
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 15, 0, 0);
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.HellboundHalberd>();
    }
    public override bool MeleePrefix()
    {
        return true;
    }

    public static int swing;
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (player.altFunctionUse != 2)
        {
            Rectangle dims = this.GetDims();
            float posMult = 1 + (dims.Height * scaleMult - 36) / 36 * 0.1f;
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
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    //public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    //{
    //    target.AddBuff(BuffID.Ichor, 60 * 4);
    //}
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            Vector2 mousePos = Main.MouseScreen;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                player.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
                CursorPosition.SendPacket(mousePos, player.whoAmI);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)
            {
                player.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
            }
            float velX = mousePos.X + Main.screenPosition.X - player.Center.X;
            float velY = mousePos.Y + Main.screenPosition.Y - player.Center.Y;
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, new Vector2(velX, velY),
                ModContent.ProjectileType<Projectiles.Melee.HellboundHalberdSpear>(), Item.damage, Item.knockBack);
        }
        else
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity,
                ModContent.ProjectileType<Projectiles.Melee.HellboundHalberd>(), Item.damage, Item.knockBack);
        }
        return false;
    }
}
