using Avalon.Common;
using Avalon.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Hooks;

internal class UIWorldCreation// : ModHook
{
    private static bool finishedMenus;
    private static bool runBeforeMenus;
    private static bool skipMenus;
    private static int count;
    private static bool isFirst;

    //protected override void Apply()
    //{
    //    On_UIWorldSelect.NewWorldClick += OnNewWorldClick;
    //}
    private static void OnNewWorldClick(On_UIWorldSelect.orig_NewWorldClick orig, UIWorldSelect self, UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(SoundID.MenuOpen);
        Main.newWorldName = Lang.gen[57].Value + " " + (Main.WorldList.Count + 1);
        //Main.menuMode = 888;
        //Main.MenuUI.SetState(new UIWorldCreation());
        finishedMenus = false;
        runBeforeMenus = true;
        isFirst = false;
        EvilMenu();
    }

    private static void EvilMenu()
    {
        UIList optionsList = new UIList();
        optionsList.Add(new ListItem(Language.GetTextValue("LegacyMisc.101"), Main.Assets.Request<Texture2D>("Images/UI/IconCorruption").Value, delegate
        {
            WorldGen.WorldGenParam_Evil = 0;
            ShinyMenu();
        }));
        optionsList.Add(new ListItem(Language.GetTextValue("LegacyMisc.102"), Main.Assets.Request<Texture2D>("Images/UI/IconCrimson").Value, delegate
        {
            WorldGen.WorldGenParam_Evil = 1;
            ShinyMenu();
        }));
        optionsList.Add(new ListItem("Contagion", ModContent.Request<Texture2D>("Sprites/IconContagion").Value, delegate
        {
            WorldGen.WorldGenParam_Evil = 2;
            ShinyMenu();
        }));
        optionsList.Add(new ListItem(Language.GetTextValue("LegacyMisc.103"), ModContent.Request<Texture2D>("Sprites/IconRandom").Value, delegate
        {
            WorldGen.WorldGenParam_Evil = -1;
            ShinyMenu();
        }));
        Main.MenuUI.SetState(new GenericSelectionMenu(Language.GetTextValue("LegacyMisc.100"), optionsList, delegate
        {
            if (isFirst)
            {
                Main.menuMode = -7;
            }
            else
            {
                runBeforeMenus = true;
                Main.menuMode = 7;
            }
        }));
    }

    private static void ShinyMenu()
    {
        UIList optionsList = new UIList();
        UIListGrid list;

        #region copper

        list = new UIListGrid();
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconCopper"), "Copper", delegate
        {
            AvalonWorld.copperOre = AvalonWorld.CopperVariant.copper;
        }));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconTin"), "Tin", delegate
        {
            AvalonWorld.copperOre = AvalonWorld.CopperVariant.tin;
        }));
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconBronze"), "Bronze", delegate
        //{
        //	AvalonWorld.copperOre = AvalonWorld.CopperVariant.bronze;
        //}));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconOreRandom"), "Random", delegate
        {
            AvalonWorld.copperOre = AvalonWorld.CopperVariant.random;
        }, true));
        optionsList.Add(new ListItemSelection("Copper Tier Ore", list));

        #endregion

        #region iron

        list = new UIListGrid();
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconIron"), "Iron", delegate
        {
            AvalonWorld.ironOre = AvalonWorld.IronVariant.iron;
        }));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconLead"), "Lead", delegate
        {
            AvalonWorld.ironOre = AvalonWorld.IronVariant.lead;
        }));
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconNickel"), "Nickel", delegate
        //{
        //	AvalonWorld.ironOre = AvalonWorld.IronVariant.nickel;
        //}));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconOreRandom"), "Random", delegate
        {
            AvalonWorld.ironOre = AvalonWorld.IronVariant.random;
        }, true));
        optionsList.Add(new ListItemSelection("Iron Tier Ore", list));

        #endregion

        #region silver

        list = new UIListGrid();
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconSilver"), "Silver", delegate
        {
            AvalonWorld.silverOre = AvalonWorld.SilverVariant.silver;
        }));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconTungsten"), "Tungsten", delegate
        {
            AvalonWorld.silverOre = AvalonWorld.SilverVariant.tungsten;
        }));
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconZinc"), "Zinc", delegate
        //{
        //	AvalonWorld.silverOre = AvalonWorld.SilverVariant.zinc;
        //}));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconOreRandom"), "Random", delegate
        {
            AvalonWorld.silverOre = AvalonWorld.SilverVariant.random;
        }, true));
        optionsList.Add(new ListItemSelection("Silver Tier Ore", list));

        #endregion

        #region gold

        list = new UIListGrid();
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconGold"), "Gold", delegate
        {
            AvalonWorld.goldOre = AvalonWorld.GoldVariant.gold;
        }));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconPlatinum"), "Platinum", delegate
        {
            AvalonWorld.goldOre = AvalonWorld.GoldVariant.platinum;
        }));
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconBismuth"), "Bismuth", delegate
        //{
        //	AvalonWorld.goldOre = AvalonWorld.GoldVariant.bismuth;
        //}));
        list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconOreRandom"), "Random", delegate
        {
            AvalonWorld.goldOre = AvalonWorld.GoldVariant.random;
        }, true));
        optionsList.Add(new ListItemSelection("Gold Tier Ore", list));

        #endregion

        #region rhodium

        //list = new UIListGrid();
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconRhodium"), "Rhodium", delegate
        //{
        //	AvalonWorld.rhodiumOre = AvalonWorld.RhodiumVariant.rhodium;
        //}));
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconOsmium"), "Osmium", delegate
        //{
        //	AvalonWorld.rhodiumOre = AvalonWorld.RhodiumVariant.osmium;
        //}));
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconIridium"), "Iridium", delegate
        //{
        //	AvalonWorld.rhodiumOre = AvalonWorld.RhodiumVariant.iridium;
        //}));
        //list.Add(new UIImageButtonCustom(ModContent.Request<Texture2D>("Sprites/IconOreRandom"), "Random", delegate
        //{
        //	AvalonWorld.rhodiumOre = AvalonWorld.RhodiumVariant.random;
        //}, true));
        //optionsList.Add(new ListItemSelection("Rhodium Tier Ore", list));

        #endregion
    }
}
