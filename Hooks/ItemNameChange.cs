using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class ItemNameChange : ModHook
{
    protected override void Apply()
    {
        //On_Lang.GetItemNameValue += On_Lang_GetItemNameValue;
        On_Lang.GetItemName += On_Lang_GetItemName;
    }

    private LocalizedText On_Lang_GetItemName(On_Lang.orig_GetItemName orig, int id)
    {
        if (ModContent.GetInstance<AvalonConfig>().VanillaRenames)
        {
            if (id == ItemID.DarkBlueSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Mushrooms");
            if (id == ItemID.BlueSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Hallow");
            if (id == ItemID.GreenSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Purity");
            if (id == ItemID.PurpleSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Corruption");
            if (id == ItemID.RedSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Crimson");
            if (id == ItemID.SandSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Desert");
            if (id == ItemID.SnowSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Snow");
            if (id == ItemID.DirtSolution)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Solutions.Forest");
            if (id == ItemID.FieryGreatsword)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.Volcano");
            if (id == ItemID.PurpleMucos)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.PurpleMucos");
            if (id == ItemID.CoinGun)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.CoinGun");
            if (id == ItemID.FrostsparkBoots)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.FrostsparkBoots");
            if (id == ItemID.CrimsonPlanterBox)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.CrimsonPlanterBox");
            if (id == ItemID.HandOfCreation)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.HandOfCreation");
            if (id == ItemID.HighTestFishingLine)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.FishingLine");
            if (id == ItemID.BloodMoonStarter)
                return Language.GetText("Mods.Avalon.VanillaItemRenames.BloodyTear");
        }
        return orig.Invoke(id);
    }

    private string On_Lang_GetItemNameValue(On_Lang.orig_GetItemNameValue orig, int id)
    {
        if (id == ItemID.DarkBlueSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Mushrooms");
        if (id == ItemID.BlueSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Hallow");
        if (id == ItemID.GreenSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Purity");
        if (id == ItemID.PurpleSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Corruption");
        if (id == ItemID.RedSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Crimson");
        if (id == ItemID.SandSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Desert");
        if (id == ItemID.SnowSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Snow");
        if (id == ItemID.DirtSolution)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Solutions.Forest");
        if (id == ItemID.FieryGreatsword)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.Volcano");
        if (id == ItemID.PurpleMucos)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.PurpleMucos");
        if (id == ItemID.CoinGun)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.CoinGun");
        if (id == ItemID.FrostsparkBoots)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.FrostsparkBoots");
        if (id == ItemID.CrimsonPlanterBox)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.CrimsonPlanterBox");
        if (id == ItemID.HandOfCreation)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.HandOfCreation");
        if (id == ItemID.HighTestFishingLine)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.FishingLine");
        if (id == ItemID.BloodMoonStarter)
            return Language.GetTextValue("Mods.Avalon.VanillaItemRenames.BloodyTear");
        return orig.Invoke(id);
    }
}
