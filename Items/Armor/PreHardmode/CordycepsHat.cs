using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class CordycepsHat : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(4);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 90);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.TropicalShroomCap>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.04f;
		player.maxMinions++;
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<CordycepsWrappings>() && legs.type == ModContent.ItemType<CordycepsLeggings>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Cordyceps");
		player.GetDamage(DamageClass.Summon) += 0.09f;
	}
	public override void PreUpdateVanitySet(Player player)
	{
		if (Main.rand.NextBool(30))
		{
			int dust = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<Dusts.MosquitoDust>(), Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f), 0, Color.White);
			Main.dust[dust].noGravity = true;
		}
		if (Main.rand.NextBool(70))
		{
			int dust = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<Dusts.MosquitoDustImmortal>(), Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1), 0, Color.White);
			Main.dust[dust].noGravity = true;
		}
	}
}
