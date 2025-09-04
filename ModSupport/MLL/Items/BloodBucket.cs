using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class BloodBucket : LiquidBucketItemBase
{
	public override int LiquidType() => LiquidLoader.LiquidType<Blood>();
}

