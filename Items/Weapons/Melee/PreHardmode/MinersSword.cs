using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Melee.PreHardmode
{
    class MinersSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.autoReuse = true;
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.LightRed;
            Item.Size = new Vector2(32);
            Item.useTime = 23;
            Item.knockBack = 5.5f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.useAnimation = 23;
            Item.UseSound = SoundID.Item1;
        }
    }
}
