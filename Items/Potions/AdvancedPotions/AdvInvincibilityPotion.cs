//using Avalon.Common.Players;
//using Avalon.Items.Accessories.Hardmode;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace Avalon.Items.Potions.AdvancedPotions;

//class AdvInvincibilityPotion : ModItem
//{
//    public override void SetStaticDefaults()
//    {
//        Item.ResearchUnlockCount = 30;
//        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
//            Color.Indigo,
//            Color.Purple
//        };
//    }

//    public override void SetDefaults()
//    {
//        Rectangle dims = this.GetDims();
//        Item.UseSound = SoundID.Item3;
//        Item.consumable = true;
//        Item.rare = ItemRarityID.Lime;
//        Item.width = dims.Width;
//        Item.useTime = 15;
//        Item.useStyle = ItemUseStyleID.DrinkLiquid;
//        Item.maxStack = 9999;
//        Item.value = Item.sellPrice(0, 0, 4, 0);
//        Item.useAnimation = 15;
//        Item.height = dims.Height;
//        Item.potion = true;
//        Item.potionDelay = 60 * 30;
//    }
//    public override bool? UseItem(Player player)
//    {
//        player.immune = true;
//        player.immuneTime = (int)(80 * (player.GetModPlayer<AvalonPlayer>().ThePill ? ThePill.LifeBonusAmount : 1));
//        return base.UseItem(player);
//    }
//}
