using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace Avalon.Items.Other;

public class Quack : ModItem
{
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.EverythingElse;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 30;
        Item.useStyle = 10;
        Item.value = 100;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool? UseItem(Player player)
    {
        SoundStyle s = new SoundStyle("Terraria/Sounds/Zombie_12") { Pitch = Main.rand.NextFloat(-1f, 1f) };
        SoundEngine.PlaySound(s, player.position);
        return true;
    }
}
