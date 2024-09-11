using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

class BlahsWarhammer : ModItem
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
        Item.width = 44;
        Item.height = 48;
        Item.UseSound = SoundID.Item1;
        Item.damage = 120;
        Item.autoReuse = true;
        Item.hammer = 250;
        Item.useTurn = true;
        Item.scale = 1.15f;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.useTime = 9;
        Item.knockBack = 20f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 6;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 1016000;
        Item.useAnimation = 9;
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Item.type)
        {
            player.wallSpeed += 0.5f;
        }
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
