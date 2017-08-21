using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Simple_Janna
{
    internal static class Modes
    {
        static Modes()
        {
            Game.OnTick += Game_OnTick;
        }

        public static void Initialize()
        {
        }

        private static void Game_OnTick(EventArgs args)
        {
            ELogic.TryToE();
            if (Program._Player.IsDead || Program._Player.HasBuff("Recall") ||
                Program._Player.IsInFountainRange()) return;

            KillSteal();

            if (SpellFactory.E.IsReady()) ProtectorE();
            if (SpellFactory.R.IsReady()) ProtectorR();

            //Modes
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) Harass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) Flee();
        }

        private static void Combo()
        {
            if (SpellFactory.W.IsReady() && Config.ReturnBoolMenu("Combo", "ComboW"))
            {
                var targetW = TargetSelector.GetTarget(SpellFactory.W.Range, DamageType.Magical);
                if (targetW != null)
                    SpellFactory.W.Cast(targetW);
            }

            if (SpellFactory.Q.IsReady() && Config.ReturnBoolMenu("Combo", "ComboQ"))
            {
                var targetQ = TargetSelector.GetTarget(SpellFactory.Q.Range, DamageType.Magical);
                if (targetQ != null)
                    SpellFactory.Q.Cast(targetQ);
            }

            // if (SpellFactory.R.IsReady()) SpellFactory.CastR();
            if (Program.bIgnite && SpellFactory.Ignite.IsReady() && Config.ReturnBoolMenu("Combo", "UseIgnite"))
                SpellFactory.UseIgnite();
        }

        private static void Harass()
        {
            if (SpellFactory.W.IsReady() && Config.ReturnBoolMenu("Harass", "HarassW") && Program._Player.ManaPercent >= Config.ReturnIntMenu("Harass", "HarassManaSlider"))
            {
                var targetW = TargetSelector.GetTarget(SpellFactory.W.Range, DamageType.Magical);
                if (targetW != null)
                    SpellFactory.W.Cast(targetW);
            }
        }

        private static void Flee()
        {
            var target = TargetSelector.GetTarget(SpellFactory.Q.Range, DamageType.Magical);

            if (SpellFactory.Q.IsReady())
                if (target != null)
                SpellFactory.Q.Cast(target);
        }

        private static void KillSteal()
        {
            if (!Config.ReturnBoolMenu("Combo", "KillSteal")) return;
            foreach (var target in EntityManager.Heroes.Enemies.Where(
                x => !x.IsZombie && !x.IsInvulnerable && x.IsValidTarget(SpellFactory.W.Range)))
                if (target.Health < Program._Player.GetSpellDamage(target, SpellSlot.W))
                    if (Config.ReturnBoolMenu("Combo", "KillStealW") && target.IsValidTarget(SpellFactory.W.Range))
                        SpellFactory.W.Cast(target);
        }


        private static void ProtectorE()
        {
            foreach (var ally in EntityManager.Heroes.Allies
                .Where(x => Program._Player.IsInRange(x, SpellFactory.E.Range)).OrderBy(x => x.HealthPercent))
            {
                if (ally.HasBuffOfType(BuffType.Poison) && Config.ReturnBoolMenu("Protector", "Poison"))
                    SpellFactory.E.Cast(ally);
                if (ally.IsMe)
                {
                    if (ally.HealthPercent <= 50 && ally.CountEnemyChampionsInRange(600) > 0)
                        SpellFactory.E.Cast(Program._Player);
                }
                else if (!ally.IsMe)
                {
                    if (!Config._AShield || !(Config._AShieldMana <= Program._Player.ManaPercent) ||
                        ally.CountEnemyChampionsInRange(600) <= 0) continue;
                    if (ally.HealthPercent <= 85)
                        SpellFactory.E.Cast(ally);
                }
            }
        }

        private static void ProtectorR()
        {
            if (Program._Player.HasBuff("Return")) return;

            foreach (var ally in EntityManager.Heroes.Allies.Where(
                x => Program._Player.IsInRange(x, SpellFactory.R.Range)))
                if (ally.IsMe && Config._AutoRJanna)
                {
                    if (!ally.IsInFountainRange() && Program._Player.HealthPercent <= Config._AutoRJannaHp &&
                        ally.CountEnemyChampionsInRange(800) > 0)
                        SpellFactory.R.Cast(Program._Player);
                }
                else if (!ally.IsMe && Config._AutoR(ally.ChampionName))
                {
                    if (!ally.IsInFountainRange() && ally.HealthPercent <= Config._AutoRHp(ally.ChampionName) &&
                        ally.CountEnemyChampionsInRange(800) > 0)
                        SpellFactory.R.Cast(ally);
                }
        }
    }
}