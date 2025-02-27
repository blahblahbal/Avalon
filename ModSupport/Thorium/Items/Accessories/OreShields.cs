using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Utilities;

namespace Avalon.ModSupport.Thorium.Items.Accessories
{
    [ExtendsFromMod("ThoriumMod")]
    [AutoloadEquip(EquipType.Shield)]
    public class BronzeBuckler : ThoriumItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
			return ExxoAvalonOrigins.ThoriumContentEnabled;
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.value = 2000;
            Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 1;
            player.GetThoriumPlayer().MetalShieldMax = 10;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<BronzeBar>(), 10).AddTile(TileID.Anvils).Register();
        }
    }

    [ExtendsFromMod("ThoriumMod")]
    [AutoloadEquip(EquipType.Shield)]
    public class NickelShield : ThoriumItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
			return ExxoAvalonOrigins.ThoriumContentEnabled;
		}
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.value = 4000;
            Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 1;
            player.GetThoriumPlayer().MetalShieldMax = 13;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<NickelBar>(), 10).AddTile(TileID.Anvils).Register();
        }
    }

    [ExtendsFromMod("ThoriumMod")]
    [AutoloadEquip(EquipType.Shield)]
    public class ZincBulwark : ThoriumItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
			return ExxoAvalonOrigins.ThoriumContentEnabled;
		}
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.value = 9000;
            Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
            player.GetThoriumPlayer().MetalShieldMax = 16;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<ZincBar>(), 10).AddTile(TileID.Anvils).Register();
        }
    }

    [ExtendsFromMod("ThoriumMod")]
    [AutoloadEquip(EquipType.Shield)]
    public class BismuthAegis : ThoriumItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
			return ExxoAvalonOrigins.ThoriumContentEnabled;
		}
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.value = 16000;
            Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
            player.GetThoriumPlayer().MetalShieldMax = 19;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<BismuthBar>(), 10).AddTile(TileID.Anvils).Register();
        }
    }

    [ExtendsFromMod("ThoriumMod")]
    public class ShieldBuffs : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
			return ExxoAvalonOrigins.ThoriumContentEnabled;
		}
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<IronShield>() ||
                entity.type == ModContent.ItemType<LeadShield>() ||
                entity.type == ModContent.ItemType<SilverBulwark>() ||
                entity.type == ModContent.ItemType<TungstenBulwark>() ||
                entity.type == ModContent.ItemType<GoldAegis>() ||
                entity.type == ModContent.ItemType<PlatinumAegis>() ||
                entity.type == ModContent.ItemType<ThoriumShield>();
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<IronShield>()) player.GetThoriumPlayer().MetalShieldMax = 11;
            else if (item.type == ModContent.ItemType<LeadShield>()) player.GetThoriumPlayer().MetalShieldMax = 12;
            else if (item.type == ModContent.ItemType<SilverBulwark>()) player.GetThoriumPlayer().MetalShieldMax = 14;
            else if (item.type == ModContent.ItemType<TungstenBulwark>()) player.GetThoriumPlayer().MetalShieldMax = 15;
            else if (item.type == ModContent.ItemType<GoldAegis>()) player.GetThoriumPlayer().MetalShieldMax = 17;
            else if (item.type == ModContent.ItemType<PlatinumAegis>()) player.GetThoriumPlayer().MetalShieldMax = 18;
            else if (item.type == ModContent.ItemType<ThoriumShield>()) player.GetThoriumPlayer().MetalShieldMax = 20;
        }
    }
}
