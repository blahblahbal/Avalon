using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Dyes;

public class ReflectiveIridiumDye : ModItem
{
    public override void SetStaticDefaults()
    {
        // Avoid loading assets on dedicated servers. They don't use graphics cards.
        if (!Main.dedServ)
        {
            // The following code creates an effect (shader) reference and associates it with this item's type ID.
            GameShaders.Armor.BindShader(Item.type,new ReflectiveArmorShaderData(new Ref<Effect>(GameShaders.Armor.GetShaderFromItemId(ItemID.ReflectiveCopperDye).Shader), "ArmorReflectiveColor")).UseColor(new Color(163,209,148));
        }

        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        // Item.dye will already be assigned to this item prior to SetDefaults because of the above GameShaders.Armor.BindShader code in Load().
        // This code here remembers Item.dye so that information isn't lost during CloneDefaults.
        int dye = Item.dye;

        Item.CloneDefaults(ItemID.GelDye);
        Item.rare = ItemRarityID.Orange;
        Item.dye = dye;
    }
    public override void AddRecipes()
    {
        CreateRecipe(2)
            .AddIngredient(ItemID.BottledWater, 2)
            .AddIngredient(ModContent.ItemType<IridiumOre>())
            .AddTile(TileID.DyeVat)
            .Register();
    }
}
