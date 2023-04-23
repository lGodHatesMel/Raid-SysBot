﻿using System.Collections.Generic;

namespace SysBot.Pokemon
{
    /// <summary>
    /// Pokémon Scarlet/Violet RAM offsets
    /// </summary>
    public class PokeDataOffsetsSV
    {
        public const string SVGameVersion = "1.3.0";
        public const string ScarletID = "0100A3D008C5C000";
        public const string VioletID = "01008F6008C5E000";
        public IReadOnlyList<long> BoxStartPokemonPointer { get; } = new long[] { 0x44BFBA8, 0x130, 0x9B0, 0x0 };
        public IReadOnlyList<long> LinkTradePartnerPokemonPointer { get; } = new long[] { 0x44B97D8, 0x48, 0x58, 0x40, 0x148 };
        public IReadOnlyList<long> LinkTradePartnerNIDPointer { get; } = new long[] { 0x44DDC10, 0xF8, 0x8 };
        public IReadOnlyList<long> MyStatusPointer { get; } = new long[] { 0x44BFBA8, 0x100, 0x40 };
        public IReadOnlyList<long> Trader1MyStatusPointer { get; } = new long[] { 0x44B97D8, 0x48, 0xB0, 0x0 }; // The trade partner status uses a compact struct that looks like MyStatus.
        public IReadOnlyList<long> Trader2MyStatusPointer { get; } = new long[] { 0x44B97D8, 0x48, 0xE0, 0x0 };
        public IReadOnlyList<long> ConfigPointer { get; } = new long[] { 0x44BFBA8, 0x1B8, 0x40 };
        public IReadOnlyList<long> CurrentBoxPointer { get; } = new long[] { 0x44BFBA8, 0x128, 0x570 };
        public IReadOnlyList<long> PortalBoxStatusPointer { get; } = new long[] { 0x44D9310, 0x18, 0xA0, 0x1B8, 0x70, 0x28 };  // 9-A in portal, 4-6 in box.
        public IReadOnlyList<long> IsConnectedPointer { get; } = new long[] { 0x44E30A0, 0x10 };
        public IReadOnlyList<long> OverworldPointer { get; } = new long[] { 0x44E2FC8, 0x348, 0x10, 0xD8, 0x28 };

        public const int BoxFormatSlotSize = 0x158;
        public const ulong LibAppletWeID = 0x010000000000100a; // One of the process IDs for the news.

        public IReadOnlyList<long> TeraRaidCodePointer { get; } = new long[] { 0x44DDC10, 0x10, 0x78, 0x10, 0x1A9 };
        public IReadOnlyList<long> TeraRaidBlockPointer { get; } = new long[] { 0x44BFBA8, 0x180, 0x40 };

        public ulong TeraLobby { get; } = 0x04174430;
        public ulong LoadedIntoDesiredState { get; } = 0x0453001C;

        public IReadOnlyList<long> CollisionPointer { get; } = new long[] { 0x4504940, 0xAD8, 0x160, 0x60, 0x100 };

        public IReadOnlyList<long> SaveBlockPointer { get; } = new long[] { 0x44B5158, 0xD8, 0x0, 0x0, 0x30, 0x8, 0x00};
    }
}
