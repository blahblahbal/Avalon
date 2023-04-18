using Avalon.Common;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace Avalon.Hooks
{
    internal class Heartsick : ModHook
    {
        protected override void Apply()
        {
            On_Player.PickupItem += OnPickupItem;
        }
        private static Item OnPickupItem(On_Player.orig_PickupItem orig, Player self, int playerIndex, int worldItemArrayIndex, Item itemToPickUp)
        {
            if (self.GetModPlayer<AvalonPlayer>().Heartsick)
            {
                if (itemToPickUp.type == ItemID.Heart || itemToPickUp.type == ItemID.CandyApple || itemToPickUp.type == ItemID.CandyCane)
                {
                    SoundEngine.PlaySound(SoundID.Item7, self.position);
                    self.Heal(30);
                    itemToPickUp = new Item();
                    //return orig(self, playerIndex, worldItemArrayIndex, itemToPickUp);
                }
            }
            return orig(self, playerIndex, worldItemArrayIndex, itemToPickUp);
        }
    }
}
