using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Simple_Mundo
{
    public static class SpellManager
    {
        public static AIHeroClient _player;

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 1050, SkillShotType.Linear, 250, 2000, 60)
                {AllowedCollisionCount = 0};
            W = new Spell.Active(SpellSlot.W, 162);
            E = new Spell.Active(SpellSlot.E, 150);
            R = new Spell.Active(SpellSlot.R);

            if (Utility.SmiteNames.ToList().Contains(Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner1).Name
                .ToLower()))
            {
                Smite = new Spell.Targeted(SpellSlot.Summoner1, 570);
                return;
            }
            if (Utility.SmiteNames.ToList().Contains(Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner2).Name
                .ToLower()))
                Smite = new Spell.Targeted(SpellSlot.Summoner2, 570);
        }

        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Skillshot Q { get; }

        public static Spell.Active W { get; }
        public static Spell.Active E { get; }
        public static Spell.Active R { get; }
        public static Spell.Targeted Smite { get; }

        public static float QDamage(Obj_AI_Base target)
        {
            var mindamage = new[] { 80, 130, 180, 230, 280 }[Q.Level - 1];
            var damage = new[] {0.15f, 0.175f, 0.20f, 0.225f, 0.25f}[Q.Level - 1] * target.Health;
            return Player.Instance.CalculateDamageOnUnit(target, Q.DamageType, Math.Min(mindamage,damage));
        }

        public static float QDamageMinions(Obj_AI_Minion minion)
        {
            var mindamage = new[] { 80, 130, 180, 230, 280 }[Q.Level - 1];
            var maxdamage = new[] {300, 350, 400, 450, 500}[Q.Level - 1];
            return Player.Instance.CalculateDamageOnUnit(minion, Q.DamageType, Math.Min(mindamage,maxdamage));
        }


        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        public static bool HasSmite()
        {
            return Smite != null && Smite.IsLearned;
        }
    }
}