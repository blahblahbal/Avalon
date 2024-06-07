using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class SpiritPoppy : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(0, 0, 200), LanguageManager.Instance.GetText("Spirit Poppy"));
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.addTile(Type);
        Main.tileObsidianKill[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileFrameImportant[Type] = true;
        HitSound = SoundID.Shatter;
        Main.tileLighted[Type] = true;
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0f / 255f;
        g = 0f / 255f;
        b = 102f / 255f;
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 16,
            ModContent.ItemType<Items.Consumables.SpiritPoppy>());
    }
    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
        var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
        Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/SpiritPoppy_Glow").Value, pos, frame, Color.White);
    }
}
