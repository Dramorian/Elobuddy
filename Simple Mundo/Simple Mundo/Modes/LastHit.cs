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
                    EntityManager.MinionsAndMonsters.EnemyMinions
                        .FirstOrDefault(m => m.IsValidTarget(SpellManager.Q.Range) &&
                                             m.Health < SpellManager.QDamageMinions(m));
                if (minionsQ != null)
                {
                    var qpred = Q.GetPrediction(minionsQ);
                    if (qpred.HitChance >= HitChance.Medium)
                        Q.Cast(qpred.CastPosition);
                }
            }
        }
    }
}