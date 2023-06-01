using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.BleachedEbony;

public class BleachedEbonySofa : ModTile
{
    public const int NextStyleHeight = 38; // Calculated by adding all CoordinateHeights + CoordinatePaddingFix.Y applied to all of them + 2
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;
        TileID.Sets.CanBeSatOnForNPCs[Type] = false; // Facilitates calling ModifySittingTargetInfo for NPCs
        TileID.Sets.CanBeSatOnForPlayers[Type] = true; // Facilitates calling ModifySittingTargetInfo for Players

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Sofa"));
        DustType = -1;
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance); // Avoid being able to trigger it from long range
    }

    public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
    {
        // It is very important to know that this is called on both players and NPCs, so do not use Main.LocalPlayer for example, use info.restingEntity
        Tile tile = Framing.GetTileSafely(i, j);

        if (info.RestingEntity.direction == 1) //Facing right
        {
            if (tile.TileFrameX == 0)
            {
                info.DirectionOffset = info.RestingEntity is Player ? -1 : 4; // Default to 6 for players, 2 for NPCs
                info.VisualOffset = new Vector2(5, -1); // Defaults to (0,0)
            }
            if (tile.TileFrameX == 18)
            {
                info.DirectionOffset = info.RestingEntity is Player ? -1 : 0;
                info.VisualOffset = new Vector2(1, -1);
            }
            if (tile.TileFrameX == 36)
            {
                info.DirectionOffset = info.RestingEntity is Player ? -1 : -6;
                info.VisualOffset = new Vector2(-3, -1);
            }
            info.TargetDirection = 1; // Facing right if sat down on while facing right, left otherwise
        }
        else //Facing left
        {
            if (tile.TileFrameX == 0)
            {
                info.DirectionOffset = info.RestingEntity is Player ? 1 : -4;
                info.VisualOffset = new Vector2(-3, -1);
            }
            if (tile.TileFrameX == 18)
            {
                info.DirectionOffset = info.RestingEntity is Player ? 1 : 2;
                info.VisualOffset = new Vector2(1, -1);
            }
            if (tile.TileFrameX == 36)
            {
                info.DirectionOffset = info.RestingEntity is Player ? 1 : 6;
                info.VisualOffset = new Vector2(5, -1);
            }
            info.TargetDirection = -1;
        }

        // The anchor represents the bottom-most tile of the chair. This is used to align the entity hitbox
        // Since i and j may be from any coordinate of the chair, we need to adjust the anchor based on that
        info.AnchorTilePosition.X = i; // Our chair is only 1 wide, so nothing special required
        info.AnchorTilePosition.Y = j;

        if (tile.TileFrameY % NextStyleHeight == 0)
        {
            info.AnchorTilePosition.Y++; // Here, since our chair is only 2 tiles high, we can just check if the tile is the top-most one, then move it 1 down
        }
    }

    public override bool RightClick(int i, int j)
    {
        Player player = Main.LocalPlayer;

        if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
        { // Avoid being able to trigger it from long range
            player.GamepadEnableGrappleCooldown();
            player.sitting.SitDown(player, i, j);
        }

        return true;
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;

        if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
        { // Match condition in RightClick. Interaction should only show if clicking it does something
            return;
        }

        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Furniture.BleachedEbony.BleachedEbonySofa>();
    }
}
