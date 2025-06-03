using Avalon.Common;
using Avalon.ModSupport;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class BloodMoonTranforms : ModHook
	{
		public override bool IsLoadingEnabled(Mod mod) => !AltLibrarySupport.Enabled;
		protected override void Apply()
        {
            On_NPC.UpdateNPC_BloodMoonTransformations += OnBloodMoonTransformations;
        }
        private static void OnBloodMoonTransformations(On_NPC.orig_UpdateNPC_BloodMoonTransformations orig, NPC self)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && Main.bloodMoon)
            {
                bool flag = false;
                if (self.value == 0f)
                {
                    flag = true;
                }
                if (ModContent.GetInstance<AvalonWorld>().WorldEvil != WorldGeneration.Enums.WorldEvil.Contagion)
                {
                    self.AttemptToConvertNPCToEvil(WorldGen.crimson);
                }
                else
                {
                    self.AttemptToConvertNPCToContagion();
                }
                if (flag)
                {
                    self.value = 0f;
                }
            }
        }
    }
}
