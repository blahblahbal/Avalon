using ExxoAvalonOrigins.Items.Material.Shards;
using ExxoAvalonOrigins.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Systems;

public class RecipeChanger : ModSystem
{
    public override void PostAddRecipes()
    {
        for (int i = 0; i < Recipe.numRecipes; i++)
        {
            Recipe recipe = Main.recipe[i];
            switch (recipe.createItem.type)
            {
                case ItemID.GPS:
                {
                    if (recipe.TryGetIngredient(ItemID.GoldWatch, out Item ing))
                    {
                        recipe.RemoveIngredient(ing);
                        recipe.AddRecipeGroup("ExxoAvalonOrigins:Tier3Watch");
                    }
                    if (recipe.TryGetIngredient(ItemID.PlatinumWatch, out Item ing2))
                    {
                        recipe.DisableRecipe();
                    }
                    break;
                }
                case ItemID.EnchantedBoomerang:
                {
                    if (recipe.TryGetIngredient(ItemID.FallenStar, out Item ing))
                    {
                        recipe.RemoveIngredient(ing);
                        recipe.AddIngredient(ModContent.ItemType<EnchantedBar>());
                    }
                    break;
                }
                //case ItemID.AnkhCharm:
                //{
                //    recipe.AddIngredient(ModContent.ItemType<Items.Accessories.Bayonet>());
                //    break;
                //}
                //case ItemID.FrostHelmet:
                //{
                //    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ing))
                //    {
                //        recipe.RemoveIngredient(ing);
                //        recipe.AddIngredient(ModContent.ItemType<FeroziumBar>(), 12);
                //        recipe.AddIngredient(ModContent.ItemType<FrigidShard>());
                //    }
                //    else if (recipe.HasIngredient(ItemID.TitaniumBar))
                //    {
                //        recipe.DisableRecipe();
                //    }

                //    break;
                //}
                //case ItemID.FrostBreastplate:
                //{
                //    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ing))
                //    {
                //        recipe.RemoveIngredient(ing);
                //        recipe.AddIngredient(ModContent.ItemType<FeroziumBar>(), 24);
                //        recipe.AddIngredient(ModContent.ItemType<FrigidShard>());
                //    }
                //    else if (recipe.HasIngredient(ItemID.TitaniumBar))
                //    {
                //        recipe.DisableRecipe();
                //    }

                //    break;
                //}
                //case ItemID.FrostLeggings:
                //{
                //    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ing))
                //    {
                //        recipe.RemoveIngredient(ing);
                //        recipe.AddIngredient(ModContent.ItemType<FeroziumBar>(), 18);
                //        recipe.AddIngredient(ModContent.ItemType<FrigidShard>());
                //    }
                //    else if (recipe.HasIngredient(ItemID.TitaniumBar))
                //    {
                //        recipe.DisableRecipe();
                //    }

                //    break;
                //}
                case ItemID.ClayPot:
                {
                    if (recipe.TryGetIngredient(ItemID.ClayBlock, out Item ing))
                    {
                        recipe.RemoveIngredient(ing);
                        recipe.AddIngredient(ItemID.ClayBlock, 4);
                    }

                    break;
                }
                case ItemID.BladeofGrass:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.ThornChakram:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.IvyWhip:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.JungleYoyo:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.ThornWhip:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.JungleHat:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.JungleShirt:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.JunglePants:
                {
                    recipe.AddIngredient(ModContent.ItemType<ToxinShard>());
                    break;
                }
                case ItemID.Flamarang:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.MoltenFury:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.FieryGreatsword:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.MoltenPickaxe:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.MoltenHamaxe:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.PhoenixBlaster:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.ImpStaff:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.MoltenHelmet:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.MoltenBreastplate:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
                case ItemID.MoltenGreaves:
                {
                    recipe.AddIngredient(ModContent.ItemType<FireShard>());
                    break;
                }
            }
        }
    }
}
