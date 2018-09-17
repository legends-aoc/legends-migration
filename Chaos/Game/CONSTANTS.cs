// ****************************************************************************
// This file belongs to the Chaos-Server project.
// 
// This project is free and open-source, provided that any alterations or
// modifications to any portions of this project adhere to the
// Affero General Public License (Version 3).
// 
// A copy of the AGPLv3 can be found in the project directory.
// You may also find a copy at <https://www.gnu.org/licenses/agpl-3.0.html>
// ****************************************************************************

using System.Collections.Immutable;

namespace Chaos
{
    /// <summary>
    /// Contains all game constant variables.
    /// </summary>
    internal static class CONSTANTS
    {
        //server
        internal const int LOBBY_PORT = 2554;
        internal const int LOGIN_PORT = 2555;
        internal const int WORLD_PORT = 2556;

        //data
        internal static readonly ImmutableArray<ushort> HAIR_SPRITES = ImmutableArray.Create(new ushort[]
{
            0,
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
            41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60,
            161, 253, 254, 255, 263, 264, 265, 266, 313, 314, 321, 324, 325, 326, 327, 333, 342, 343, 344, 345,
            346, 347, 349, 383, 392, 397, 411, 412, 433, 435, 437, 438, 440, 441, 447, 448, 449, 459, 460, 461,
            476, 482, 483
});
        internal static readonly ImmutableArray<ushort> DOOR_SPRITES = ImmutableArray.Create(new ushort[]
        {
            1994, 1997, 2000, 2003, 2163, 2164, 2165, 2196, 2197, 2198, 2227, 2228, 2229, 2260, 2261, 2262, 2291, 2292, 2293, 2328,
            2329, 2330, 2432, 2436, 2461, 2465, 2673, 2674, 2675, 2680, 2681, 2682, 2687, 2688, 2689, 2694, 2695, 2696, 2714, 2715,
            2721, 2722, 2727, 2728, 2734, 2735, 2761, 2762, 2768, 2769, 2776, 2777, 2783, 2784, 2850, 2851, 2852, 2857, 2858, 2859,
            2874, 2875, 2876, 2881, 2882, 2883, 2897, 2898, 2903, 2904, 2923, 2924, 2929, 2930, 2945, 2946, 2951, 2952, 2971, 2972,
            2977, 2978, 2993, 2994, 2999, 3000, 3019, 3020, 3025, 3026, 3058, 3059, 3066, 3067, 3090, 3091, 3098, 3099, 3118, 3119,
            3126, 3127, 3150, 3151, 3158, 3159, 3178, 3179, 3186, 3187, 3210, 3211, 3218, 3219, 4519, 4520, 4521, 4523, 4524, 4525,
            4527, 4528, 4529, 4532, 4533, 4534, 4536, 4537, 4538, 4540, 4541, 4542
        });
        internal static readonly ImmutableArray<EquipmentSlot> PROFILE_EQUIPMENTSLOT_ORDER = ImmutableArray.Create(new EquipmentSlot[]
        {
            EquipmentSlot.Weapon, EquipmentSlot.Armor, EquipmentSlot.Shield,
            EquipmentSlot.Helmet, EquipmentSlot.Earrings, EquipmentSlot.Necklace,
            EquipmentSlot.LeftRing, EquipmentSlot.RightRing, EquipmentSlot.LeftGaunt,
            EquipmentSlot.RightGaunt, EquipmentSlot.Belt, EquipmentSlot.Greaves,
            EquipmentSlot.Accessory1, EquipmentSlot.Boots, EquipmentSlot.Overcoat,
            EquipmentSlot.OverHelm, EquipmentSlot.Accessory2, EquipmentSlot.Accessory3
        });
        internal static readonly ImmutableArray<string> COMMAND_LIST = ImmutableArray.Create(new string[]
        {
            "hair <num>                                      changes hairstyle",
            "haircolor <num>                                changes hair color",
            "skincolor <num>                                changes skin color",
            "face <num>                                     changes face shape",
            "gender <text>                                      changes gender",
        });

        //game
        internal const int ITEM_SPRITE_OFFSET = 32768;
        internal const int MERCHANT_SPRITE_OFFSET = 16384;
        internal const int PICKUP_RANGE = 4;
        internal const int DROP_RANGE = 4;
        internal const int GLOBAL_SKILL_COOLDOWN_MS = 750;
        internal const int GLOBAL_ITEM_COOLDOWN_MS = 500;
        internal const int GLOBAL_SPELL_COOLDOWN_MS = 250;
        internal const int REFRESH_DELAY_MS = 1000;
        internal const string DEVELOPER_GUILD_NAME = "Chaos Team";
        internal static Location STARTING_LOCATION => (8984, 10, 10);
        internal static Location DEATH_LOCATION => (5031, 15, 15);

        //nation locations
        internal static Location NO_NATION_LOCATION = (8984, 10, 10);
        internal static Location SUOMI_LOCATION = (8984, 10, 10);
        internal static Location LOURES_LOCATION = (8984, 10, 10);
        internal static Location MILETH_LOCATION = (8984, 10, 10);
        internal static Location TAGOR_LOCATION = (8984, 10, 10);
        internal static Location RUCESION_LOCATION = (8984, 10, 10);
        internal static Location NOES_LOCATION = (8984, 10, 10);

        //stat to damage / health / etc modifiers will be added here later.
    }
}
