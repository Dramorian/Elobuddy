using EloBuddy;
using EloBuddy.SDK;

namespace Simple_Mundo
{
    public static class Utility
    {
        public static Item HealthPotion;
        public static Item CorruptingPotion;
        public static Item RefillablePotion;
        public static Item HuntersPotion;
        public static Item TotalBiscuit;


        public static readonly string[] MonstersNames =
        {
            "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak",
            "SRU_Red", "SRU_Krug", "SRU_Dragon_Water", "SRU_Dragon_Fire", "SRU_Dragon_Earth", "SRU_Dragon_Air",
            "SRU_Dragon_Elder", "Sru_Crab", "SRU_Baron", "SRU_RiftHerald"
        };

        public static readonly string[] SmiteNames =
        {
            "summonersmite", "s5_summonersmiteplayerganker", "s5_summonersmiteduel"
        };

        public static int[] SmiteRed { get; } = {3715, 1415, 1414, 1413, 1412};
        public static int[] SmiteBlue { get; } = {3706, 1403, 1402, 1401, 1400};

        static Utility()
        {
            HealthPotion = new Item(2003);
            TotalBiscuit = new Item(2010);
            CorruptingPotion = new Item(2033);
            RefillablePotion = new Item(2031);
            HuntersPotion = new Item(2032);
        }

        public static float SmiteDmgMonster(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Smite);
        }

        public static float SmiteDmgHero(AIHeroClient target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.True,
                20.0f + Player.Instance.Level * 8.0f);
        }

        public static void Initialize()
        {
        }
    }
}