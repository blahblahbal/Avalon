using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class ElementDust : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = Data.Sets.ItemGroupValues.DroppedTomeMats;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(200, 200, 200, 100);
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 9999;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        if (Main.rand.Next(6) == 0)
        {
            int num28 = Dust.NewDust(Item.position, Item.width, Item.height, DustID.UltraBrightTorch, 0f, 0f, 200, Item.color);
            Main.dust[num28].velocity *= 0.3f;
            Main.dust[num28].velocity.Y--;
            Main.dust[num28].scale *= 0.5f;
            Main.dust[num28].noGravity = true;
        }
    }
}
