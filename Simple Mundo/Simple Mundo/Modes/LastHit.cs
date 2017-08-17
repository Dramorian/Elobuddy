using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = Simple_Mundo.Config.LastHit.LastHitMenu;

namespace Simple_Mundo.Modes
{
    public sealed class LastHit : ModeBase
    {
        public static AIHeroClient _Player => ObjectManager.Player;
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on lasthit mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            // TODO: Add lasthit logic here
            if (Settings.UseQ && Q.IsReady())
            {
                var minionsQ =
                    EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                            Q.Range)
                        .FirstOrDefault(m =>
                            m.Distance(_Player) <= Q.Range &&
                            m.Health <= _Player.GetSpellDamage(m, SpellSlot.Q) &&
                            m.IsValidTarget());
                if (minionsQ != null)
                {
                    var qPred = Q.GetPrediction(minionsQ);
                    if (qPred.HitChance >= HitChance.High)
                        Q.Cast(qPred.CastPosition);
                }
            }
        }
    }
}