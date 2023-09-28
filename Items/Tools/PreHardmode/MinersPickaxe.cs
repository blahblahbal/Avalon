using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class MinersPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 10;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.pick = 60;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 18;
        Item.knockBack = 3.5f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 1;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 16000;
        Item.useAnimation = 19;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
}
