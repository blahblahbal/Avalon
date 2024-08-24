using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Players;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Avalon.PlayerDrawLayers;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
class TroxiniumHeadpiece : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 11;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3, 40, 0);
        Item.height = dims.Height;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor * 4f;
    }
    public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
    {
        if (!Main.gameMenu)
		{
			color *= 4f;
		}
    }
	private static Asset<Texture2D> glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");

		if (!Main.dedServ)
		{
			HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
			{
				Texture = ModContent.Request<Texture2D>(Texture + "_Head_Glow"),
				Color = (PlayerDrawSet drawInfo) => new Color(255, 255, 255, 0)
			});
		}
	}
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Vector2 vector = glow.Size() / 2f;
		Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - glow.Height());
		Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
		float num = Item.velocity.X * 0.2f;
		spriteBatch.Draw(glow.Value, vector2, new Rectangle(0, 0, glow.Width(), glow.Height()), new Color(255, 255, 255, 0), num, vector, scale, SpriteEffects.None, 0f);
	}
	public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 12)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<TroxiniumBodyarmor>() && legs.type == ModContent.ItemType<TroxiniumCuisses>();
    }

    public override void UpdateArmorSet(Player player)
    {
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Troxinium", Language.GetTextValue("Mods.Avalon.RangedText"));
		//player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Troxinium.Ranged");
		player.GetModPlayer<AvalonPlayer>().HyperRanged = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Ranged) += 0.09f;
        player.GetModPlayer<AvalonPlayer>().AmmoCost85 = true;
    }
}
