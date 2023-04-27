using Avalon.Common.Players;
using Avalon.Network;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode; 

public class HellboundHalberd : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.UseSound = SoundID.Item1;
        Item.damage = 99;
        Item.scale = 1f;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        //Item.noMelee = true;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 40, 0, 0);
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Ichor, 60 * 4);
    }
    //public override bool CanUseItem(Player player)
    //{
    //    return player.ownedProjectileCounts[Item.shoot] < 1;
    //}
    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.noUseGraphic = true;
            Item.noMelee = true;
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
                ModContent.ProjectileType<Projectiles.Melee.HellboundHalberd>(), Item.damage * 2, Item.knockBack);
            Item.noUseGraphic = false;
            Item.noMelee = false;
        }
        return true;
    }
}
