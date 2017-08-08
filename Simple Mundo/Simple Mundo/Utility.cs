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
            "SRU_Crab",
            "SRU_Krug",
            "SRU_Gromp",
            "SRU_Murkwolf",
            "SRU_Razorbeak",
            "TTNGolem",
            "TTNWolf",
            "TTNWraith",
            "SRU_Red",
            "SRU_Blue",
            "SRU_Dragon_Water",
            "SRU_Dragon_Fire",
            "SRU_Dragon_Earth",
            "SRU_Dragon_Air",
            "SRU_Dragon_Elder",
            "SRU_RiftHerald",
            "SRU_Baron",
            "TT_Spiderboss"
        };

        public static readonly string[] SencefulBuffs =
        {
            "SRU_Red",
            "SRU_Blue",
            "SRU_Dragon_Water",
            "SRU_Dragon_Fire",
            "SRU_Dragon_Earth",
            "SRU_Dragon_Air",
            "SRU_Dragon_Elder",
            "SRU_Baron",
            "SRU_RiftHerald",
            "TT_Spiderboss"
        };

        public static readonly string[] SmiteNames =
        {
            "summonersmite", "s5_summonersmiteplayerganker", "s5_summonersmiteduel"
        };

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
                390 + Player.Instance.Level * 61);
        }

        public static void Initialize()
        {
        }
    }
}