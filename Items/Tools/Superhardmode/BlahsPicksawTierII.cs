using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

class BlahsPicksawTierII : ModItem
{
	private static Asset<Texture2D> textureGlow;
	public override void SetStaticDefaults()
	{
		textureGlow = ModContent.Request<Texture2D>(Texture + "_Glow");
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = textureGlow;
        }
	}
	public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 38;
        Item.damage = 55;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.15f;
        Item.axe = 60;
        Item.pick = 700;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.useTime = 6;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 16;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(gold: 50);
        Item.useAnimation = 6;
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Rectangle dims = this.GetDims();
        Vector2 vector = dims.Size() / 2f;
        Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
        Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
        float num = Item.velocity.X * 0.2f;
        spriteBatch.Draw(textureGlow.Value, vector2, dims, new Color(250, 250, 250, 250), num, vector, scale, SpriteEffects.None, 0f);
    }
}
