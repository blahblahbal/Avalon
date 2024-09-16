using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class IckyCactus : ModCactus
{
    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<Snotsand>() };

    public override Asset<Texture2D> GetTexture() => ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Contagion/IckyCactus");

    public override Asset<Texture2D> GetFruitTexture() => ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Contagion/IckyCactus_Fruit");
}
