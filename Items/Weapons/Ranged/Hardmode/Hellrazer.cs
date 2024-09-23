using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode;

public class Hellrazer : ModItem
{
	private static Asset<Texture2D> glow;
	public override void Load()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
	}
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item11;
        Item.damage = 110;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.useAmmo = AmmoID.Bullet;
        Item.shootSpeed = 8f;
        Item.crit += 10;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.knockBack = 12f;
        Item.useTime = 30;
        Item.shoot = ProjectileID.Bullet;
        Item.value = Item.sellPrice(0, 30, 0, 0);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 30;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item40;
		if (!Main.dedServ)
		{
			Item.GetGlobalItem<ItemGlowmask>().glowTexture = glow;
		}
		Item.GetGlobalItem<ItemGlowmask>().glowOffsetX = 0;
		Item.GetGlobalItem<ItemGlowmask>().glowOffsetY = 0;
		Item.GetGlobalItem<ItemGlowmask>().glowAlpha = 0;
	}
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, 0);
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Rectangle dims = this.GetDims();
        Vector2 vector = dims.Size() / 2f;
        Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
        Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
        float num = Item.velocity.X * 0.2f;
        spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(Texture + "_Glow"), vector2, dims, new Color(250, 250, 250, 250), num, vector, scale, SpriteEffects.None, 0f);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.Bullet)
        {
            type = ProjectileID.ExplosiveBullet;
        }
    }
}
