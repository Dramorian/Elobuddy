using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace Simple_Janna
{
    internal static class Events
    {
        static Events()
        {
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        public static void Initialize()
        {
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (!SpellFactory.Q.IsReady() || sender.IsAlly) return;
            if (Config.ReturnBoolMenu("Protector", "Interrupt") && sender.IsValidTarget(SpellFactory.Q.Range) &&
                !sender.IsZombie)
                SpellFactory.Q.Cast(sender);
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsAlly || !SpellFactory.Q.IsReady()) return;
            if (Config.ReturnBoolMenu("Protector", "GapClose") && sender.IsValidTarget(SpellFactory.Q.Range))
                SpellFactory.Q.Cast(sender);
            else if (Config.ReturnBoolMenu("Protector", "GapCloseAllies"))
                foreach (var ally in EntityManager.Heroes.Allies.Where(
                    x => x.IsAlly && Program._Player.IsInRange(x, SpellFactory.Q.Range)))
                    if (sender.IsValidTarget(SpellFactory.Q.Range))
                        SpellFactory.Q.Cast(sender);
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsAlly ||
                Program._Player.ManaPercent < Config.JannaAutoShieldMenu["AShieldMana"].Cast<Slider>().CurrentValue ||
                !Config.JannaAutoShieldMenu["AShield"].Cast<CheckBox>().CurrentValue)
                return;

            foreach (var ally in EntityManager.Heroes.Allies.Where(
                x => Program._Player.IsInRange(x, SpellFactory.E.Range)))
                if (sender is AIHeroClient && args.End.Distance(ally) <= 200)
                    if (SpellProtectDB.AvoidSpells.ContainsKey(sender.BaseSkinName))
                        if (SpellProtectDB.AvoidSpells[sender.BaseSkinName].Contains(args.SData.Name))
                            if (Config.JannaAutoShieldMenu[args.SData.Name].Cast<CheckBox>().CurrentValue)
                                SpellFactory.E.Cast(ally);
        }
    }
}