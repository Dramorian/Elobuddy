using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Simple_Janna
{
    public static class SpellFactory
    {
        static SpellFactory()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1700, SkillShotType.Linear, 0, 2000, 125)
                {AllowedCollisionCount = int.MaxValue};
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Active(SpellSlot.R, 725);


            var igniteSlot = Program._Player.GetSpellSlotFromName("summonerdot");
            if (igniteSlot == SpellSlot.Unknown) return;
            Console.WriteLine("Ignite Spell found on slot: " + igniteSlot);
            Program.bIgnite = true;
            Ignite = new Spell.Targeted(igniteSlot, 600);
        }

        public static Spell.Skillshot Q { get; }
        public static Spell.Targeted W { get; }
        public static Spell.Targeted E { get; }
        public static Spell.Active R { get; }
        public static Spell.Targeted Ignite { get; }

        public static void Initialize()
        {
        }


        public static void CastE()
        {
            //var allies = EntityManager.Heroes.Allies.Where(x => !x.IsMe && Program._Player.IsInRange(x, SpellFactory.E.Range));
            if (Config.ReturnBoolMenu("Protector", "SupportMode") &&
                Program._Player.CountAllyChampionsInRange(800) >= 2) return;
            var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (target.IsValidTarget(E.Range) && !target.IsZombie && !target.IsInvulnerable)
                E.Cast(target);
        }

        public static void CastR()
        {
            if (Program._Player.HasBuff("Return") || Program._Player.IsInFountainRange()) return;
            if (Config.ReturnBoolMenu("Combo", "UseR") && Program._Player.HealthPercent <= Config._AutoRJannaHp)
                R.Cast(Program._Player);
        }

        public static void UseIgnite()
        {
            if (!Ignite.IsReady()) return;
            var target = TargetSelector.GetTarget(Ignite.Range, DamageType.True);
            if (target != null && !target.IsZombie && !target.IsInvulnerable)
                if (target.Health <= Utils.CalculateMaxDamage(target) +
                    Program._Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite))
                    Ignite.Cast(target);
        }
    }
}