using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Items.Other;

class GoldApple : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.IgnoresEncumberingStone[Type] = true;
        ItemID.Sets.IsAPickup[Type] = true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
    }

    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        spriteBatch.Draw(Terraria.GameContent.TextureAssets.Item[Item.type].Value, Item.position - Main.screenPosition, null, new Color(200, 200, 200, 200), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }
    public override void Update(ref float gravity, ref float maxFallSpeed)
    {
        float num16 = Main.rand.Next(90, 111) * 0.01f;
        num16 *= Main.essScale * 0.5f;
        Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.86f * num16, 0.74f * num16, 0.24f * num16);
    }
    public override bool CanPickup(Player player)
    {
        return true;
    }
}
