using Avalon.Common.Players;
using Avalon.Network;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode; 

public class MasterSword : ModItem
{
    int swingCounter = 0;
    public override void SetDefaults()
    {
        Item.Size = new Vector2(54);
        Item.UseSound = SoundID.Item1;
        Item.damage = 90;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 54;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTurn = false;
        Item.value = Item.sellPrice(0, 9, 63, 0);
        Item.useAnimation = 18;
        Item.shootSpeed = 9.5f;
        Item.shoot = ModContent.ProjectileType<MasterSwordBeam>();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.statLife >= player.statLifeMax2 * 0.80f)
        {
            SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/MasterSword"), player.position);
            Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.3f), knockback, player.whoAmI);
            return false;
        }
        return false;
        //if (player.statLife == player.statLifeMax2)
        //{
        //    Vector2 mousePos = Main.MouseScreen;
        //    if (Main.netMode == NetmodeID.MultiplayerClient)
        //    {
        //        player.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
        //        CursorPosition.SendPacket(mousePos, player.whoAmI);
        //    }
        //    else if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        player.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
        //    }
        //    float velX = Main.mouseX + Main.screenPosition.X - player.Center.X;
        //    float velY = Main.mouseY + Main.screenPosition.Y - player.Center.Y;
        //    int ypos = (int)Main.mouseY;
        //    if (player.gravDir == -1f)
        //    {
        //        velY = Main.screenPosition.Y + Main.screenHeight - ypos - player.Center.Y;
        //    }

        //    float velModifier = (float)Math.Sqrt((double)(velX * velX + velY * velY));
        //    velModifier = Item.shootSpeed / velModifier;
        //    velX *= velModifier;
        //    velY *= velModifier;
        //    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, velX, velY, ModContent.ProjectileType<MasterSwordBeam>(), Item.damage, Item.knockBack);
        //    SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/MasterSword"), player.position);
        //}
    }
}
