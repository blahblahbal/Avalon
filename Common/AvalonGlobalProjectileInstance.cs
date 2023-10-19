using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common
{
    internal class AvalonGlobalProjectileInstance : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return true;
        }
    }
}
