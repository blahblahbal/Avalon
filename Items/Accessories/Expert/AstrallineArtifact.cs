using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Expert;

public class AstrallineArtifact : ModItem
{
	private int AstralCooldown;

	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 15);
		Item.expert = true;
	}

	//public override void ModifyTooltips(List<TooltipLine> tooltips)
	//{
	//    var AstralCooldownInfo = new TooltipLine(Mod, "Controls:AstralCooldown", "Time before you can astral project: " + AstralCooldown / 60 + " seconds");
	//    tooltips.Add(AstralCooldownInfo);
	//}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().AstralProject = true;
		//AstralCooldown = player.GetModPlayer<AvalonPlayer>().AstralCooldown;
	}
}
