using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;
using static Terraria.Mount;

namespace Avalon.Mounts;

internal class TourmalineGemcart : ModMount
{
    public override void SetStaticDefaults()
    {
        MountID.Sets.Cart[Type] = true;
        MountID.Sets.FacePlayersVelocity[Type] = true;

        SetAsMinecart(MountData, ModContent.BuffType<Buffs.TourmalineGemcartBuff>(), MountData.frontTexture);

        // Change properties on MountData here further, for example:
        MountData.spawnDust = 21;
        MountData.delegations.MinecartDust = DelegateMethods.Minecart.Sparks;
        MountData.delegations.MinecartLandingSound = DelegateMethods.Minecart.LandingSound;
        MountData.delegations.MinecartBumperSound = DelegateMethods.Minecart.BumperSound;

        // Important to note is that runSpeed, dashSpeed, and acceleration will get overridden when the player has used the Minecart Upgrade Kit. Keep that in mind when changing the values yourself
    }

    public override void UpdateEffects(Player player)
    {
        // Visuals copied from Diamond Minecart
        if (Main.rand.NextBool(10))
        {
            Vector2 randomOffset = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(22f, 10f);
            Vector2 directionOffset = new Vector2(0f, 10f) * player.Directions;
            Vector2 position = player.Center + directionOffset + randomOffset;
            position = player.RotatedRelativePoint(position);
            Dust dust = Dust.NewDustPerfect(position, ModContent.DustType<Dusts.TourmalineDust>());
            dust.noGravity = true;
            dust.fadeIn = 0.6f;
            dust.scale = 0.4f;
            dust.velocity *= 0.25f;
            dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMinecart, player);
        }
    }
}
