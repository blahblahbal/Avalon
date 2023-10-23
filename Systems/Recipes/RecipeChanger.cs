using Avalon.Common.Players;
using Avalon.Hooks;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Systems.Recipes;

public class RecipeChanger : ModSystem
{
    public override void PostAddRecipes()
    {
        for (int i = 0; i < Recipe.numRecipes; i++)
        {
            Recipe recipe = Main.recipe[i];

            #region pocket workbench adding stations
            if (recipe.requiredTile.Count > 0)
            {
                for (int j = 0; j < recipe.requiredTile.Count; j++)
                {
                    if (recipe.requiredTile[j] != -1)
                    {
                        if (!Data.Sets.Tile.CraftingStations.Contains(recipe.requiredTile[j]))
                        {
                            Data.Sets.Tile.CraftingStations.Add(recipe.requiredTile[j]);
                        }
                    }
                }
            }
            #endregion

            switch (recipe.createItem.type)
            {
                case ItemID.GPS:
                {
                    if (recipe.TryGetIngredient(ItemID.GoldWatch, out Item ing))
                    {
                        recipe.RemoveIngredient(ing);
                        recipe.AddRecipeGroup("Avalon:Tier3Watch");
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
                case ItemID.SunplateBlock:
                    {
                        if (recipe.TryGetIngredient(ItemID.StoneBlock, out Item ing))
                        {
                            recipe.RemoveIngredient(ing);
                            recipe.AddIngredient(ItemID.StoneBlock, 40);
                        }
                        recipe.AddIngredient(ItemID.GoldOre);
                        recipe.ReplaceResult(ItemID.SunplateBlock, 40);
                        break;
                    }
                case ItemID.HandOfCreation:
                {
                    recipe.AddIngredient(ModContent.ItemType<ObsidianGlove>());
                    break;
                }
                case ItemID.ShroomiteBar:
                {
                    if (recipe.TryGetIngredient(ItemID.GlowingMushroom, out Item ing))
                    {
                        recipe.RemoveIngredient(ing);
                        recipe.AddIngredient(ItemID.GlowingMushroom, 10);
                    }
                    break;
                }
                case ItemID.Magiluminescence:
                {
                    if (recipe.TryGetIngredient(ItemID.DemoniteBar, out Item ing))
                    {
                        recipe.RemoveIngredient(ing);
                        recipe.AddRecipeGroup("Avalon:EvilBar", 12);
                    }
                    if (recipe.TryGetIngredient(ItemID.CrimtaneBar, out Item ing2))
                    {
                        recipe.DisableRecipe();
                    }
                    break;
                }
                case ItemID.ShadowCandle:
                {
                    if (recipe.TryGetIngredient(ItemID.DemoniteBar, out Item ing))
                    {
                        recipe.RemoveIngredient(ing);
                        recipe.AddRecipeGroup("Avalon:EvilBar", 3);
                    }
                    if (recipe.TryGetIngredient(ItemID.CrimtaneBar, out Item ing2))
                    {
                        recipe.DisableRecipe();
                    }
                    break;
                }
                case ItemID.AnkhCharm:
                {
                    recipe.AddIngredient(ModContent.ItemType<Items.Accessories.Hardmode.Bayonet>());
                    break;
                }
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

        #region pocket station adding station item ids
        for (int i = 0; i < ItemLoader.ItemCount; i++)
        {
            if (Data.Sets.Tile.CraftingStations.Contains(ContentSamples.ItemsByType[i].createTile))
            {
                Data.Sets.Item.CraftingStationsItemID.Add(ContentSamples.ItemsByType[i].type);
            }
        }
        #endregion


        Dictionary<int, int> MusicLoader_ItemToMusic = (Dictionary<int, int>)typeof(MusicLoader).GetField("itemToMusic", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

        for (int musicID = 0; musicID < MusicLoader.MusicCount; musicID++)
        {
            for (int itemID = 0; itemID < ItemLoader.ItemCount; itemID++)
            {
                if (MusicLoader_ItemToMusic.ContainsKey(itemID))
                {
                    if (MusicLoader_ItemToMusic[itemID] == musicID)
                    {
                        AvalonJukeboxPlayer.TracksByItemID.Add(itemID, musicID);
                        AvalonJukeboxPlayer.TracksByMusicID.Add(musicID, itemID);
                    }
                }
            }
        }
    }
}
