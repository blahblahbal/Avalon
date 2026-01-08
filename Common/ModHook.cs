using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Avalon.Common;

public abstract class ModHook : ModType
{
    public static readonly Queue<ModHook> RegisteredHooks = new();
    private bool applied;
    protected virtual ModHook[] HookDependencies => Array.Empty<ModHook>();
    public sealed override void SetupContent() => SetStaticDefaults();

    public void ApplyHook()
    {
        if (applied)
        {
            return;
        }

        foreach (ModHook hook in HookDependencies)
        {
            hook.ApplyHook();
        }

        Apply();
        applied = true;
    }

	public void UnApplyHook()
	{
		if (!applied)
		{
			return;
		}

		foreach (ModHook hook in HookDependencies)
		{
			hook.UnApplyHook();
		}

		UnApply();
		applied = false;
	}


	protected sealed override void Register() => RegisteredHooks.Enqueue(this);

    protected abstract void Apply();

	/// <summary>
	/// Used by hooks that are applied manually to methods, this is so that edits can be unapplied appropriately
	/// </summary>
	protected virtual void UnApply() { }
}
