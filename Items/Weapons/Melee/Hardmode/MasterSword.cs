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
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.Size = new Vector2(54);
        Item.UseSound = SoundID.Item1;
        Item.damage = 90;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 18;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTurn = true;
        Item.value = Item.sellPrice(0, 9, 63, 0);
        Item.useAnimation = 18;
        Item.shootSpeed = 9.5f;
    }
    public override bool? UseItem(Player player)
    {
        swingCounter++;
        if (swingCounter > 2)
        {
            if (player.statLife == player.statLifeMax2)
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
                float velX = Main.mouseX + Main.screenPosition.X - player.Center.X;
                float velY = Main.mouseY + Main.screenPosition.Y - player.Center.Y;
                int ypos = (int)Main.mouseY;
                if (player.gravDir == -1f)
                {
                    velY = Main.screenPosition.Y + Main.screenHeight - ypos - player.Center.Y;
                }

                float velModifier = (float)Math.Sqrt((double)(velX * velX + velY * velY));
                velModifier = Item.shootSpeed / velModifier;
                velX *= velModifier;
                velY *= velModifier;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.position.X, player.position.Y, velX, velY, ModContent.ProjectileType<MasterSwordBeam>(), Item.damage, Item.knockBack);
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/MasterSword"), player.position);
            }
            swingCounter = 0;
        }
        return true;
    }
}
