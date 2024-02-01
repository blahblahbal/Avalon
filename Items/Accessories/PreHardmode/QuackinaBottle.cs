using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

class QuackinaBottle : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.height = dims.Height;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<QuackBottleJump>().Enable();
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Other.Quack>())
            .AddIngredient(ItemID.CloudinaBottle)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}

public class QuackBottleJump : ExtraJump
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
        // This example mimics the logic for spawning the puff of smoke from the Cloud in a Bottle
        int offsetY = player.height;
        if (player.gravDir == -1f)
            offsetY = 0;

        offsetY -= 16;

        SoundStyle sound = new SoundStyle("Terraria/Sounds/Zombie_12") { Pitch = Main.rand.NextFloat(-1f, 1f) };

        SoundEngine.PlaySound(sound, player.position);

        for (int i = 0; i < 10; i++)
        {
            Dust dust = Dust.NewDustDirect(player.position + new Vector2(-34f, offsetY), 102, 32, DustID.Cloud, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 100, Color.Gray, 1.5f);
            dust.velocity = dust.velocity * 0.5f - player.velocity * new Vector2(0.1f, 0.3f);
        }

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
}
