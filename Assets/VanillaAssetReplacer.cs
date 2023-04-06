using System;
using System.Collections.Generic;
using ReLogic.Content;
using Terraria;

namespace Avalon.Assets;

public class VanillaAssetReplacer<T> : IReplaceAssets where T : class
{
    private readonly Func<IList<Asset<T>>> storeProvider;
    private readonly Dictionary<int, string> vanillaAssetPath = new();
    public VanillaAssetReplacer(Func<IList<Asset<T>>> storeProvider) => this.storeProvider = storeProvider;

    public void ReplaceAsset(int index, Asset<T> replacement)
    {
        vanillaAssetPath[index] = storeProvider.Invoke()[index].Name;
        storeProvider.Invoke()[index] = replacement;
    }

    public void RestoreAssets()
    {
        foreach (KeyValuePair<int, string> assetPath in vanillaAssetPath)
        {
            storeProvider.Invoke()[assetPath.Key] = Main.Assets.Request<T>(assetPath.Value);
        }
    }
}

public interface IReplaceAssets
{
    public void RestoreAssets();
}
