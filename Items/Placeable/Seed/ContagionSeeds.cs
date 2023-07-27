using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Seed;

public class ContagionSeeds : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.width = 16;
        Item.height = 16;
        Item.useTime = 10;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
    }

    public override bool? UseItem(Player player)
    {
        Terraria.Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
        if (tile.HasTile && tile.TileType == TileID.Dirt && player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, Terraria.DataStructures.TileReachCheckSettings.Simple))
        {
            Main.tile[Player.tileTargetX, Player.tileTargetY].TileType = (ushort)ModContent.TileType<Tiles.Contagion.Ickgrass>();
            SoundEngine.PlaySound(SoundID.Dig, player.Center);
            return true;
        }
        if (tile.HasTile && tile.TileType == TileID.Mud && player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, Terraria.DataStructures.TileReachCheckSettings.Simple))
        {
            Main.tile[Player.tileTargetX, Player.tileTargetY].TileType = (ushort)ModContent.TileType<Tiles.Contagion.ContagionJungleGrass>();
            SoundEngine.PlaySound(SoundID.Dig, player.Center);
            return true;
        }
        return false;
    }
}