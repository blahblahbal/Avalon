using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class PointingLaser : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.useAnimation = 5;
        Item.useTime = 5;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.PointingLaser>();
        Item.shootSpeed = 6f;
        Item.width = dims.Width;
        Item.channel = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.height = dims.Height;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        }
    }
    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        Texture2D texture = Mod.Assets.Request<Texture2D>("Items/Material/PointingLaser_Glow").Value;
        Color c = Color.White;
        if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Red || Main.netMode == NetmodeID.SinglePlayer)
        {
            c = new Color(218, 59, 59);
        }
        if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Yellow)
        {
            c = new Color(218, 183, 59);
        }
        if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Green)
        {
            c = new Color(59, 218, 85);
        }
        if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Blue)
        {
            c = new Color(59, 149, 218);
        }
        if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Pink)
        {
            c = new Color(171, 59, 218);
        }
        spriteBatch.Draw(texture, position, frame, c, 0f, origin, scale, SpriteEffects.None, 0f);
    }
}
