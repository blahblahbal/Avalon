using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class RocketinaBottle : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 2);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        //player.GetJumpState<RocketBottleJump>().Enable();
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1)
    //        .AddIngredient(ItemID.Amethyst, 12)
    //        .AddIngredient(ItemID.Chain)
    //        .AddTile(TileID.Anvils)
    //        .Register();
    //}
}
/*public class RocketBottleJump : ExtraJump
{
    public override Position GetDefaultPosition() => new After(BlizzardInABottle);

    public override IEnumerable<Position> GetModdedConstraints()
    {
        // By default, modded extra jumps set to be between two vanilla extra jumps (via After and Before) are ordered in load order.
        // This hook allows you to organize where this extra jump is located relative to other modded extra jumps that are also
        // placed between the same two vanila extra jumps.
        yield return new Before(ModContent.GetInstance<MultipleUseExtraJump>());
    }

    public override float GetDurationMultiplier(Player player)
    {
        // Use this hook to set the duration of the extra jump
        // The XML summary for this hook mentions the values used by the vanilla extra jumps
        return 2.25f;
    }

    public override void UpdateHorizontalSpeeds(Player player)
    {
        // Use this hook to modify "player.runAcceleration" and "player.maxRunSpeed"
        // The XML summary for this hook mentions the values used by the vanilla extra jumps
        player.runAcceleration *= 1.75f;
        player.maxRunSpeed *= 2f;
    }

    public override void OnStarted(Player player, ref bool playSound)
    {
        // Use this hook to trigger effects that should appear at the start of the extra jump
        // This example mimicks the logic for spawning the puff of smoke from the Cloud in a Bottle
        int offsetY = player.height;
        if (player.gravDir == -1f)
            offsetY = 0;

        offsetY -= 16;

        
        SoundEngine.PlaySound(SoundID.Item11, player.Center);
        player.velocity.Y -= player.gravDir * 16.5f;

        SpawnCloudPoof(player, player.Top + new Vector2(-16f, offsetY));
        SpawnCloudPoof(player, player.position + new Vector2(-36f, offsetY));
        SpawnCloudPoof(player, player.TopRight + new Vector2(4f, offsetY));
    }

    private static void SpawnCloudPoof(Player player, Vector2 position)
    {
        Gore gore = Gore.NewGoreDirect(player.GetSource_FromThis(), position, -player.velocity, Main.rand.Next(11, 14));
        gore.velocity.X = gore.velocity.X * 0.1f - player.velocity.X * 0.1f;
        gore.velocity.Y = gore.velocity.Y * 0.1f - player.velocity.Y * 0.05f;
    }

    public override void ShowVisuals(Player player)
    {
        // Use this hook to trigger effects that should appear throughout the duration of the extra jump
        // This example mimics the logic for spawning the dust from the Blizzard in a Bottle
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
    }
}*/
