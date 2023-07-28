using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class CaesiumForge : ModItem
{
    //public override void SetStaticDefaults() // woah double comment crazy // ikr // holy crap quadruple comment????? // No way // this is unbelieveable // Finally! The clip you've all been waiting for! https://youtu.be/f8DDN9bDdPE // Shut up nobody asked // get out of here spammer! // Not my fault you don't have good taste in gameplays maybe you should come inside and actually win // No thanks I like it out here // duodecuple comment!!! // Lets gooo! // I will eat you alive. // Nuh uh // OW MY BONES NOOO // :) // France (French: [fʁɑ̃s] Listen), officially the French Republic (French: République française [ʁepyblik fʁɑ̃sɛz]), is a country located primarily in Western Europe. It also includes overseas regions and territories in the Americas and the Atlantic, Pacific and Indian Oceans,[XII] giving it one of the largest discontiguous exclusive economic zones in the world. Its metropolitan area extends from the Rhine to the Atlantic Ocean and from the Mediterranean Sea to the English Channel and the North Sea; overseas territories include French Guiana in South America, Saint Pierre and Miquelon in the North Atlantic, the French West Indies, and many islands in Oceania and the Indian Ocean. Its eighteen integral regions (five of which are overseas) span a combined area of 643,801 km2 (248,573 sq mi) and have a total population of over 68 million as of January 2023. // Shut up nerd // Ubisoft goes Steamworks bye bye, always on DRM. // Ubisoft goes Steamworks bye bye, always on DRM. // Ubisoft goes Steamworks bye bye, always on DRM. // Ubisoft goes Steamworks bye bye, always on DRM. // Ubisoft goes Steamworks bye bye, always on DRM. // Ubisoft goes Steamworks bye bye, always on DRM.// Amazing // Lets go // Ubisoft goes Steamworks bye bye, always on DRM. // Ubisoft goes Steamworks bye bye, always on DRM.
    //{
    //    //DisplayName.SetDefault("Caesium Forge");
    //    //Tooltip.SetDefault("Used to smelt high-end ore");
    //}

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.CaesiumForge>();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = Item.CommonMaxStack;
        Item.value = 50000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.AdamantiteForge)
            .AddIngredient(ModContent.ItemType<CaesiumOre>(), 40)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.TitaniumForge)
            .AddIngredient(ModContent.ItemType<CaesiumOre>(), 40)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
