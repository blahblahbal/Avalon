using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;
using static Terraria.Mount;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Mounts;

internal class PeridotGemcart : ModMount
{
    public override void SetStaticDefaults()
    {
        MountID.Sets.Cart[Type] = true;
        MountID.Sets.FacePlayersVelocity[Type] = true;

        MountData.Minecart = true;
        MountData.delegations = new MountDelegatesData();
        MountData.delegations.MinecartDust = DelegateMethods.Minecart.Sparks;
        MountData.spawnDust = 213;
        MountData.buff = ModContent.BuffType<Buffs.PeridotGemcartBuff>();
        MountData.extraBuff = ModContent.BuffType<Buffs.PeridotGemcartBuff>();
        MountData.heightBoost = 10;
        MountData.flightTimeMax = 0;
        MountData.fallDamage = 1f;
        MountData.runSpeed = 13f;
        MountData.dashSpeed = 13f;
        MountData.acceleration = 0.04f;
        MountData.jumpHeight = 15;
        MountData.jumpSpeed = 5.15f;
        MountData.blockExtraJumps = true;
        MountData.totalFrames = 3;
        int[] array = new int[MountData.totalFrames];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = 8 - 0 + 0;
        }
        MountData.playerYOffsets = array;
        MountData.xOffset = 1;
        MountData.bodyFrame = 3;
        MountData.yOffset = 13 + 0;
        MountData.playerHeadOffset = 14;
        MountData.standingFrameCount = 1;
        MountData.standingFrameDelay = 12;
        MountData.standingFrameStart = 0;
        MountData.runningFrameCount = 3;
        MountData.runningFrameDelay = 12;
        MountData.runningFrameStart = 0;
        MountData.flyingFrameCount = 0;
        MountData.flyingFrameDelay = 0;
        MountData.flyingFrameStart = 0;
        MountData.inAirFrameCount = 0;
        MountData.inAirFrameDelay = 0;
        MountData.inAirFrameStart = 0;
        MountData.idleFrameCount = 0;
        MountData.idleFrameDelay = 0;
        MountData.idleFrameStart = 0;
        MountData.idleFrameLoop = false;
        if (Main.netMode != NetmodeID.Server)
        {
            //MountData.backTexture = Asset<Texture2D>.Empty;
            //MountData.backTextureExtra = Asset<Texture2D>.Empty;
            //MountData.frontTexture = ModContent.Request<Texture2D>(Texture);
            //MountData.frontTextureExtra = Asset<Texture2D>.Empty;
            MountData.textureWidth = MountData.frontTexture.Width();
            MountData.textureHeight = MountData.frontTexture.Height();
        }
        // Helper method setting many common properties for a minecart
        //Mount.SetAsMinecart(
        //    MountData,
        //    ModContent.BuffType<ExampleMinecartBuff>(),
        //    MountData.frontTexture
        //);

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
            Dust dust = Dust.NewDustPerfect(position, ModContent.DustType<Dusts.PeridotDust>());
            dust.noGravity = true;
            dust.fadeIn = 0.6f;
            dust.scale = 0.4f;
            dust.velocity *= 0.25f;
            dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMinecart, player);
        }
    }
}
