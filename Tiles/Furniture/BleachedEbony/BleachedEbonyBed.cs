using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.BleachedEbony;

public class BleachedEbonyBed : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;
        TileID.Sets.CanBeSleptIn[Type] = true;
        TileID.Sets.IsValidSpawnPoint[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
        TileObjectData.newTile.CoordinateHeights = new[] {16, 18};
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Bed"));
        TileID.Sets.DisableSmartCursor[Type] = true;
        AdjTiles = new int[] {TileID.Beds};
        DustType = -1;
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public override bool RightClick(int i, int j)
    {
        Player player = Main.LocalPlayer;
        Tile tile = Main.tile[i, j];
        int spawnX = (i - (tile.TileFrameX / 18)) + (tile.TileFrameX >= 72 ? 5 : 2);
        int spawnY = j + 2;

        if (tile.TileFrameY % 38 != 0)
        {
            spawnY--;
        }

        if (!Player.IsHoveringOverABottomSideOfABed(i, j))
        {
            if (player.IsWithinSnappngRangeToTile(i, j, Terraria.GameContent.PlayerSleepingHelper.BedSleepingMaxDistance))
            {
                player.GamepadEnableGrappleCooldown();
                player.sleeping.StartSleeping(player, i, j);
            }
        }
        else
        {
            player.FindSpawn();

            if (player.SpawnX == spawnX && player.SpawnY == spawnY)
            {
                player.RemoveSpawn();
                Main.NewText(Terraria.Localization.Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, 240, 20);
            }
            else if (Player.CheckSpawn(spawnX, spawnY))
            {
                player.ChangeSpawn(spawnX, spawnY);
                Main.NewText(Terraria.Localization.Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, 240, 20);
            }
        }

        return true;
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;

        if (!Player.IsHoveringOverABottomSideOfABed(i, j))
        {
            if (player.IsWithinSnappngRangeToTile(i, j, PlayerSleepingHelper.BedSleepingMaxDistance))
            { // Match condition in RightClick. Interaction should only show if clicking it does something
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = ItemID.SleepingIcon;
            }
        }
        else
        {
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonyBed>();
        }
    }
}
