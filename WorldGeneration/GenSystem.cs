using Avalon.WorldGeneration.Passes;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration;

public class GenSystem : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        // Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.

        // The first step is an Ore. Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
        // First, we find out which step "Shinies" is.

        GenPass currentPass;

        int index = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));

        if (index != -1)
        {
            tasks.Insert(index + 1, new AvalonReset("Avalon Reset", 1000f));
        }

        index = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

        if (index != -1)
        {
            tasks.Insert(index + 1, new OreGenPreHardmode("Adding Avalon Ores", 237.4298f));
        }

        int iceWalls = tasks.FindIndex(genPass => genPass.Name == "Cave Walls");
        if (iceWalls != -1)
        {
            currentPass = new Shrines();
            tasks.Insert(iceWalls + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int underworld = tasks.FindIndex(genPass => genPass.Name == "Micro Biomes");
        if (underworld != -1)
        {
            currentPass = new Underworld();
            tasks.Insert(underworld + 1, currentPass);
            totalWeight += currentPass.Weight;

            currentPass = new Ectovines();
            tasks.Insert(underworld + 2, currentPass);
            totalWeight += currentPass.Weight;
        }

        index = tasks.FindIndex(genPass => genPass.Name == "Vines");
        if (index != -1)
        {
            currentPass = new Hooks.DungeonRemoveCrackedBricks();
            tasks.Insert(index + 1, currentPass);
            totalWeight += currentPass.Weight;
        }
    }
}
