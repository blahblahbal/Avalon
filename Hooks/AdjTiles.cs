using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Players;
using System;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
internal class AdjTiles : ModHook
{
    protected override void Apply()
    {
        On_Player.AdjTiles += OnAdjTiles;
        On_Player.AdjTiles += OnAdjTilesPocketBench;
    }
    private static void OnAdjTilesPocketBench(On_Player.orig_AdjTiles orig, Player self)
    {
        if (self.GetModPlayer<AvalonPlayer>().PocketBench)
        {
            for (int o = 0; o < 50; o++)
            {
                if (Data.Sets.Item.CraftingStationsItemID.Contains(self.inventory[o].type))
                {
                    if (!self.adjTile[self.inventory[o].createTile])
                    {
                        self.adjTile[self.inventory[o].createTile] = true;
                    }
                }
            }
            for (int o = 0; o < self.bank.item.Length; o++)
            {
                if (Data.Sets.Item.CraftingStationsItemID.Contains(self.bank.item[o].type))
                {
                    if (!self.adjTile[self.bank.item[o].createTile])
                    {
                        self.adjTile[self.bank.item[o].createTile] = true;
                    }
                }
            }
            for (int o = 0; o < self.bank2.item.Length; o++)
            {
                if (Data.Sets.Item.CraftingStationsItemID.Contains(self.bank2.item[o].type))
                {
                    if (!self.adjTile[self.bank2.item[o].createTile])
                    {
                        self.adjTile[self.bank2.item[o].createTile] = true;
                    }
                }
            }
            for (int o = 0; o < self.bank3.item.Length; o++)
            {
                if (Data.Sets.Item.CraftingStationsItemID.Contains(self.bank3.item[o].type))
                {
                    if (!self.adjTile[self.bank3.item[o].createTile])
                    {
                        self.adjTile[self.bank3.item[o].createTile] = true;
                    }
                }
            }
            for (int o = 0; o < self.bank4.item.Length; o++)
            {
                if (Data.Sets.Item.CraftingStationsItemID.Contains(self.bank4.item[o].type))
                {
                    if (!self.adjTile[self.bank4.item[o].createTile])
                    {
                        self.adjTile[self.bank4.item[o].createTile] = true;
                    }
                }
            }
            Recipe.FindRecipes();
        }
        else orig.Invoke(self);
        //if (Array.Exists(self.bank.item, element => Data.Sets.Item.CraftingStationsItemID.Contains(element.type)) && self.GetModPlayer<AvalonPlayer>().PocketBench)
        //{
            
        //    for (int o = 0; o < self.bank.item.Length; o++)
        //    {
        //        if (self.bank.item[o].createTile > -1)
        //        {
        //            if (!self.adjTile[self.bank.item[o].createTile])
        //            {
        //                self.adjTile[self.bank.item[o].createTile] = true;
        //            }
        //        }
        //    }
        //}
        //else
        //    orig(self);
    }
    private static void OnAdjTiles(On_Player.orig_AdjTiles orig, Player self)
    {
        int Width = 4;
        int Height = 3;
        if (self.ateArtisanBread)
        {
            Width += 4;
            Height += 4;
        }
        self.GetModPlayer<AvalonPlayer>().oldAdjShimmer = self.GetModPlayer<AvalonPlayer>().AdjShimmer;
        self.GetModPlayer<AvalonPlayer>().AdjShimmer = false;
        int num3 = (int)((self.position.X + self.width / 2) / 16f);
        int num4 = (int)((self.position.Y + self.height) / 16f);
        for (int j = num3 - Width; j <= num3 + Width; j++)
        {
            for (int k = num4 - Height; k < num4 + Height; k++)
            {
                if (Main.tile[j, k].LiquidAmount > 200 && Main.tile[j, k].LiquidType == LiquidID.Shimmer)
                {
                    self.GetModPlayer<AvalonPlayer>().AdjShimmer = true;
                }
            }
        }
        if (self.GetModPlayer<AvalonPlayer>().AdjShimmer != self.GetModPlayer<AvalonPlayer>().oldAdjShimmer)
        {
            Recipe.FindRecipes();
        }
        Recipe.FindRecipes();
        orig(self);
    }
            /*if ((self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt ||
                self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK ||
                self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt) && Main.myPlayer == self.whoAmI)
            {

                //else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().PocketBench)
                //{
                //    for (int k = 0; k < self.inventory.Length; k++)
                //    {
                //        if (self.inventory[k].createTile > -1)
                //        {
                //            if (self.adjTile[self.inventory[k].createTile])
                //            {
                //                self.adjTile[self.inventory[k].createTile] = false;
                //            }
                //        }
                //    }
                //}
                {
                    if (!self.adjTile[TileID.TinkerersWorkbench])
                    {
                        self.adjTile[TileID.TinkerersWorkbench] = true;
                    }
                    else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                    {
                        self.adjTile[TileID.TinkerersWorkbench] = false;
                    }
                    if (!self.adjTile[TileID.WorkBenches])
                    {
                        self.adjTile[TileID.WorkBenches] = true;
                    }
                    else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                    {
                        self.adjTile[TileID.WorkBenches] = false;
                    }
                    if (!self.adjTile[TileID.HeavyWorkBench])
                    {
                        self.adjTile[TileID.HeavyWorkBench] = true;
                    }
                    else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                        !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                    {
                        self.adjTile[TileID.HeavyWorkBench] = false;
                    }
                    if (self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK ||
                        self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                    {
                        if (!self.adjTile[TileID.Anvils])
                        {
                            self.adjTile[TileID.Anvils] = true;
                        }
                        else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                            !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                            !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                        {
                            self.adjTile[TileID.Anvils] = false;
                        }
                        if (!self.adjTile[TileID.MythrilAnvil])
                        {
                            self.adjTile[TileID.MythrilAnvil] = true;
                        }
                        else if (!self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt &&
                            !self.GetModPlayer<ExxoEquipEffectPlayer>().GoblinAK &&
                            !self.GetModPlayer<ExxoEquipEffectPlayer>().BuilderBelt)
                        {
                            self.adjTile[TileID.MythrilAnvil] = false;
                        }
                    }
                }
                Recipe.FindRecipes();
            }
            else
                orig(self);
        }*/
}
