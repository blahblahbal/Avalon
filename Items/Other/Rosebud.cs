using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Items.Other;

public class Rosebud : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemIconPulse[Item.type] = true;
        ItemID.Sets.ItemNoGravity[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
    }

    public override bool CanPickup(Player player) => true;

    public override bool OnPickup(Player player)
    {
        int rand = Main.rand.Next(10, 16);
        if (player.statLife + rand > player.statLifeMax2)
        {
            player.statLife = player.statLifeMax2;
        }
        else
        {
            player.statLife += rand;
        }

        if (Main.myPlayer == player.whoAmI)
        {
            player.HealEffect(rand, true);
        }
        SoundEngine.PlaySound(SoundID.Grab, player.position);
        return false;
    }

    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture + "Glow").Value;
        spriteBatch.Draw
        (
            texture,
            new Vector2
            (
                Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
            ),
            new Rectangle(0, 0, texture.Width, texture.Height),
            Color.White,
            rotation,
            texture.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0f
        );
    }
}
