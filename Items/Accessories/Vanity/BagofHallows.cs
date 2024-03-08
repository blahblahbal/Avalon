using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Avalon.Items.Material.Shards;

namespace Avalon.Items.Accessories.Vanity;

public class BagofHallows : ModItem
{
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.VanityBags;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 15)
            .AddIngredient(ItemID.PixieDust, 10)
            .AddIngredient(ItemID.UnicornHorn, 2)
            .AddIngredient(ModContent.ItemType<SacredShard>(), 2)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        for (int j = 0; j < 2; j++)
        {
            int num2 = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f),
                player.width, player.height, DustID.Enchanted_Gold, 0f, 0f, 100, default, 2f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].noLight = true;
            Main.dust[num2].velocity.X -= player.velocity.X * 0.5f;
            Main.dust[num2].velocity.Y -= player.velocity.Y * 0.5f;
            int t = player.HasItemInArmorReturnIndex(Type);
            if (t > 10) t -= 10;
            if (t > 0)
            {
                Main.dust[num2].shader = GameShaders.Armor.GetShaderFromItemId(player.dye[t].type);
            }
        }

        //int dust = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
        //    100, Color.White, 2f);
        //Main.dust[dust].noGravity = true;
    }
}
