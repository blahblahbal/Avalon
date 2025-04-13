using Avalon.Common.Players;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class RocketinaBottle : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = 22;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 2);
        Item.height = 30;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<RocketBottleJump>().Enable();
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe()
    //        .AddIngredient(ItemID.Bottle)
    //        .AddIngredient(ModContent.ItemType<BlastShard>(), 5)
    //        .AddIngredient(ItemID.SoulofMight, 8)
    //        .AddTile(TileID.MythrilAnvil)
    //        .Register();
    //}
}
public class RocketBottleJump : ExtraJump
{
    public override Position GetDefaultPosition() => new After(BlizzardInABottle);

    public override IEnumerable<Position> GetModdedConstraints()
    {
        // By default, modded extra jumps set to be between two vanilla extra jumps (via After and Before) are ordered in load order.
        // This hook allows you to organize where this extra jump is located relative to other modded extra jumps that are also
        // placed between the same two vanila extra jumps.
        yield return new Before(CloudInABottle);
    }

    public override float GetDurationMultiplier(Player player)
    {
        // Use this hook to set the duration of the extra jump
        // The XML summary for this hook mentions the values used by the vanilla extra jumps
        return 0f;
    }

    public override void UpdateHorizontalSpeeds(Player player)
    {
        // Use this hook to modify "player.runAcceleration" and "player.maxRunSpeed"
        // The XML summary for this hook mentions the values used by the vanilla extra jumps
        player.runAcceleration *= 2.75f;
        player.maxRunSpeed *= 4f;
    }
    public override void OnStarted(Player player, ref bool playSound)
    {
        // Use this hook to trigger effects that should appear at the start of the extra jump
        // This example mimicks the logic for spawning the puff of smoke from the Cloud in a Bottle
        int offsetY = player.height;
        if (player.gravDir == -1f)
            offsetY = 0;

        offsetY -= 16;

        player.GetModPlayer<AvalonPlayer>().RocketDustTimer = 28;

        int num6 = Gore.NewGore(player.GetSource_FromThis(),
            new Vector2(player.position.X + (player.width / 2) - 16f,
                player.position.Y + (player.gravDir == -1 ? 0 : player.height) - 16f),
            new Vector2(-player.velocity.X, -player.velocity.Y), Main.rand.Next(11, 14));
        Main.gore[num6].velocity.X = (Main.gore[num6].velocity.X * 0.1f) - (player.velocity.X * 0.1f);
        Main.gore[num6].velocity.Y = (Main.gore[num6].velocity.Y * 0.1f) - (player.velocity.Y * 0.05f);
        num6 = Gore.NewGore(player.GetSource_FromThis(),
            new Vector2(player.position.X - 36f,
                player.position.Y + (player.gravDir == -1 ? 0 : player.height) - 16f),
            new Vector2(-player.velocity.X, -player.velocity.Y), Main.rand.Next(11, 14));
        Main.gore[num6].velocity.X = (Main.gore[num6].velocity.X * 0.1f) - (player.velocity.X * 0.1f);
        Main.gore[num6].velocity.Y = (Main.gore[num6].velocity.Y * 0.1f) - (player.velocity.Y * 0.05f);
        num6 = Gore.NewGore(player.GetSource_FromThis(),
            new Vector2(player.position.X + player.width + 4f,
                player.position.Y + (player.gravDir == -1 ? 0 : player.height) - 16f),
            new Vector2(-player.velocity.X, -player.velocity.Y), Main.rand.Next(11, 14));
        Main.gore[num6].velocity.X = (Main.gore[num6].velocity.X * 0.1f) - (player.velocity.X * 0.1f);
        Main.gore[num6].velocity.Y = (Main.gore[num6].velocity.Y * 0.1f) - (player.velocity.Y * 0.05f);

        for (int i = 0; i < 10; i++)
        {
            int d = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0, 0, 0, default, 2f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(6, 6);
            Main.dust[d].noGravity = true;
            Main.dust[d].fadeIn = 2.3f;
            Main.dust[d].customData = 0;
        }
        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0, 0, 0, default, 2f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(5, 5) / 3f;
            Main.dust[d].fadeIn = Main.rand.NextFloat(1, 2);
            Main.dust[d].customData = 0;
        }
        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0, 0, 0, default, 1.4f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-3, 0).RotatedBy(player.velocity.ToRotation());
            Main.dust[d].noGravity = !Main.rand.NextBool(10);
        }
        for (int i = 0; i < 7; i++)
        {
            int d = Dust.NewDust(player.position, player.width, player.height, DustID.SolarFlare, 0, 0, 0, default, 1.4f);
            //Main.dust[d].color = Color.Red;
            Main.dust[d].velocity = (Main.rand.NextVector2Circular(10, 6) + new Vector2(-5, 0).RotatedBy(player.velocity.ToRotation())) / 3f;
            Main.dust[d].noGravity = true;
        }
        for (int i = 0; i < 9; i++)
        {
            Vector2 vel = player.velocity / 20f;
            int g = Gore.NewGore(player.GetSource_FromThis(), new Vector2(player.Center.X - 10, player.position.Y + player.height), Main.rand.NextVector2Circular(10, 6) / 3f + new Vector2(-1, 0).RotatedBy(vel.ToRotation()), Main.rand.Next(61, 63), 0.8f);
            Main.gore[g].alpha = 128;
        }

        SoundEngine.PlaySound(SoundID.Item11, player.Center);
        SoundEngine.PlaySound(SoundID.Item14, player.Center);
        player.velocity.Y -= player.gravDir * 11f;

        //SpawnCloudPoof(player, player.Top + new Vector2(-16f, offsetY));
        //SpawnCloudPoof(player, player.position + new Vector2(-36f, offsetY));
        //SpawnCloudPoof(player, player.TopRight + new Vector2(4f, offsetY));
    }

    private static void SpawnCloudPoof(Player player, Vector2 position)
    {
        Gore gore = Gore.NewGoreDirect(player.GetSource_FromThis(), position, -player.velocity, Main.rand.Next(11, 14));
        gore.velocity.X = gore.velocity.X * 0.1f - player.velocity.X * 0.1f;
        gore.velocity.Y = gore.velocity.Y * 0.1f - player.velocity.Y * 0.05f;
    }

    public override void ShowVisuals(Player player)
    {
        if (player.velocity.Y < 0)
        {
            for (int x = 0; x < 5; x++)
            {
                int d = Dust.NewDust(new Vector2(player.Center.X, player.position.Y + player.height), 10, 10,
                    DustID.Smoke);
            }
        }
        // Use this hook to trigger effects that should appear throughout the duration of the extra jump
        // This example mimics the logic for spawning the dust from the Blizzard in a Bottle
        
    }
}
