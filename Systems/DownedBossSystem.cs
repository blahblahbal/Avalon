using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Systems;

public class DownedBossSystem : ModSystem
{
    public bool DownedArmageddon;
    public bool DownedBacteriumPrime;
    public bool DownedDesertBeak;
    public bool DownedDragonLord;
    public bool DownedKingSting;
    public bool DownedMechasting;
    public bool DownedOblivion;
    public bool DownedPhantasm;

    public override void SaveWorldData(TagCompound tag)
    {
        tag["DownedBacteriumPrime"] = DownedBacteriumPrime;
        tag["DownedDesertBeak"] = DownedDesertBeak;
        tag["DownedPhantasm"] = DownedPhantasm;
        tag["DownedDragonLord"] = DownedDragonLord;
        tag["DownedMechasting"] = DownedMechasting;
        tag["DownedOblivion"] = DownedOblivion;
        tag["DownedKingSting"] = DownedKingSting;
        tag["DownedArmageddon"] = DownedArmageddon;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        if (tag.ContainsKey("DownedBacteriumPrime"))
        {
            DownedBacteriumPrime = tag.Get<bool>("DownedBacteriumPrime");
        }

        if (tag.ContainsKey("DownedDesertBeak"))
        {
            DownedDesertBeak = tag.Get<bool>("DownedDesertBeak");
        }

        if (tag.ContainsKey("DownedPhantasm"))
        {
            DownedPhantasm = tag.Get<bool>("DownedPhantasm");
        }

        if (tag.ContainsKey("DownedDragonLord"))
        {
            DownedDragonLord = tag.Get<bool>("DownedDragonLord");
        }

        if (tag.ContainsKey("DownedMechasting"))
        {
            DownedMechasting = tag.Get<bool>("DownedMechasting");
        }

        if (tag.ContainsKey("DownedOblivion"))
        {
            DownedOblivion = tag.Get<bool>("DownedOblivion");
        }

        if (tag.ContainsKey("DownedKingSting"))
        {
            DownedKingSting = tag.Get<bool>("DownedKingSting");
        }

        if (tag.ContainsKey("DownedArmageddon"))
        {
            DownedArmageddon = tag.Get<bool>("DownedArmageddon");
        }
    }

    public override void NetSend(BinaryWriter writer)
    {
        var flags = new BitsByte
        {
            [0] = DownedBacteriumPrime,
            [1] = DownedDesertBeak,
            [2] = DownedPhantasm,
            [3] = DownedDragonLord,
            [4] = DownedMechasting,
            [5] = DownedOblivion,
            [6] = DownedKingSting,
            [7] = DownedArmageddon
        };
        writer.Write(flags);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte flags = reader.ReadByte();
        DownedBacteriumPrime = flags[0];
        DownedDesertBeak = flags[1];
        DownedPhantasm = flags[2];
        DownedDragonLord = flags[3];
        DownedMechasting = flags[4];
        DownedOblivion = flags[5];
        DownedKingSting = flags[6];
        DownedArmageddon = flags[7];
    }
}
