using Avalon.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

[AutoloadEquip(EquipType.Back)]
public class AcidproofTackleBag : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory(26, 30);
		Item.SetShopValues(ItemRarityColor.Yellow8, Item.sellPrice(0, 4));
	}
	public override void UpdateEquip(Player player)
	{
		player.GetModPlayer<MLLPlayer>().accAcidFishing = true;
		player.accFishingLine = true;
		player.accTackleBox = true;
		player.fishingSkill += 10;
		player.accLavaFishing = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.LavaproofTackleBag)
			.AddIngredient<AcidproofFishingHook>()
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
public class AcidproofTackleBagHook : ModHook
{
	protected override void Apply()
	{
		On_Player.RemoveAnglerAccOptionsFromRewardPool += On_Player_RemoveAnglerAccOptionsFromRewardPool;
		On_PlayerDrawLayers.DrawPlayer_10_BackAcc += On_PlayerDrawLayers_DrawPlayer_10_BackAcc;
	}

	private void On_Player_RemoveAnglerAccOptionsFromRewardPool(On_Player.orig_RemoveAnglerAccOptionsFromRewardPool orig, Player self, System.Collections.Generic.List<int> itemIdsOfAccsWeWant, Item itemToTestAgainst)
	{
		if (!itemToTestAgainst.IsAir)
		{
			if (itemToTestAgainst.type == ModContent.ItemType<AcidproofTackleBag>())
			{
				itemIdsOfAccsWeWant.Remove(ItemID.HighTestFishingLine);
				itemIdsOfAccsWeWant.Remove(ItemID.TackleBox);
				itemIdsOfAccsWeWant.Remove(ItemID.AnglerEarring);
			}
		}
		orig(self, itemIdsOfAccsWeWant, itemToTestAgainst);
	}

	private void On_PlayerDrawLayers_DrawPlayer_10_BackAcc(On_PlayerDrawLayers.orig_DrawPlayer_10_BackAcc orig, ref PlayerDrawSet drawinfo)
	{
		bool isAcidBag = drawinfo.drawPlayer.back == EquipLoader.GetEquipSlot(Mod, "AcidproofTackleBag", EquipType.Back);
		if (isAcidBag)
		{
			drawinfo.Position.X -= drawinfo.drawPlayer.direction * 2;
		}
		orig(ref drawinfo);
		if (isAcidBag)
		{
			drawinfo.Position.X += drawinfo.drawPlayer.direction * 2;
		}
	}
}
