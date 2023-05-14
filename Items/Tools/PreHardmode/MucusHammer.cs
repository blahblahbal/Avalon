using Avalon.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class MucusHammer : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 25;
        Item.autoReuse = true;
        Item.hammer = 55;
        Item.useTurn = true;
        //Item.scale = 1.2f;
        Item.rare = ItemRarityID.Blue;
        Item.width = 24;
        Item.height = 28;
        Item.useTime = 35;
        Item.knockBack = 6f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 36, 0);
        Item.useAnimation = 35;
        Item.UseSound = SoundID.Item1;
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (player.itemAnimation % 2 == 0)
        {
            ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.4f + 0.4f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
            Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
            int DustType = ModContent.DustType<ContagionWeapons>();
            if (Main.rand.NextBool(3))
                DustType = DustID.CorruptGibs;

            int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 140, default(Color), 0.7f);
            Main.dust[num15].position = location2;
            Main.dust[num15].fadeIn = 1.2f;
            Main.dust[num15].noGravity = true;
            Main.dust[num15].velocity *= 0.25f;
            Main.dust[num15].velocity += vector2 * 5f;
            Main.dust[num15].velocity.Y *= 0.3f;
        }
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 11).AddIngredient(ModContent.ItemType<Material.Booger>(), 4).AddTile(TileID.Anvils).Register();
    }
}
