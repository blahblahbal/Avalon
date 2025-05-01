using Avalon.Common.Extensions;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class Quack : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.EverythingElse;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMisc(12, 12);
		Item.noUseGraphic = true;
		Item.useTurn = true;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.HiddenAnimation;
		Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
		Item.value = Item.sellPrice(copper: 20);
	}

	public override bool? UseItem(Player player)
	{
		SoundStyle s = new SoundStyle("Terraria/Sounds/Zombie_12") { Pitch = Main.rand.NextFloat(-1f, 1f) };
		SoundEngine.PlaySound(s, player.position);
		return true;
	}
}
