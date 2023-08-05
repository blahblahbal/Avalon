using Avalon.Items.Material;
using Avalon.Items.Material.Bars;
using Avalon.Items.Weapons.Melee;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah;

class BlahsEnergyBlade : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 50;
        Item.height = 54;
        Item.damage = 250;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.shootSpeed = 14f;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.useTime = 14;
        Item.knockBack = 20f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.BlahEnergySlash>();
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(3, 0, 0, 0);
        Item.useAnimation = 14;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        }
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 255);
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(1)
    //        .AddIngredient(ModContent.ItemType<Phantoplasm>(), 45)
    //        .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 40)
    //        .AddIngredient(ModContent.ItemType<SoulofTorture>(), 45)
    //        .AddIngredient(ModContent.ItemType<ElementalExcalibur>())
    //        .AddIngredient(ModContent.ItemType<BerserkerBlade>())
    //        .AddIngredient(ModContent.ItemType<PumpkingsSword>())
    //        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
    //        .Register();
    //}
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Rectangle dims = this.GetDims();
        Vector2 vector = dims.Size() / 2f;
        Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
        Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
        float num = Item.velocity.X * 0.2f;
        spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(Texture + "_Glow"), vector2, dims, new Color(250, 250, 250, 250), num, vector, scale, SpriteEffects.None, 0f);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, (float)player.direction * player.gravDir, player.itemAnimationMax * 0.9f, adjustedItemScale5);
        NetMessage.SendData(13, -1, -1, null, player.whoAmI);
        for (int num194 = 0; num194 < 4; num194++)
        {
            float num195 = velocity.X;
            float num196 = velocity.Y;
            num195 += Main.rand.Next(-40, 41) * 0.05f;
            num196 += Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num195, num196, ModContent.ProjectileType<Projectiles.Melee.BlahBeam>(), damage, knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }
}
