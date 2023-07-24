using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Dyes;

public class CloudyDye : ModItem
{
    public override void SetStaticDefaults()
    {
        // Avoid loading assets on dedicated servers. They don't use graphics cards.
        if (!Main.dedServ)
        {
            // The following code creates an effect (shader) reference and associates it with this item's type ID.
            GameShaders.Armor.BindShader(
                Item.type,
                new ArmorShaderData(
                    new Ref<Effect>(Mod.Assets.Request<Effect>("Effects/CloudyDye", AssetRequestMode.ImmediateLoad)
                        .Value), "CloudyDye").UseImage("Images/Misc/Perlin") // Be sure to update the effect path and pass name here.
            );
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
        CreateRecipe(1)
            .AddIngredient(ItemID.BottledWater)
            .AddIngredient(ItemID.Cloud)
            .AddTile(TileID.DyeVat)
            .Register();
    }
}
