using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = Simple_Mundo.Config.LastHit.LastHitMenu;

namespace Simple_Mundo.Modes
{
    public sealed class LastHit : ModeBase
    {
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
                    EntityManager.MinionsAndMonsters.GetLaneMinions()
                        .Where(a =>
                            a.IsValidTarget(Q.Range) && a.Health < Player.Instance.GetSpellDamage(a, SpellSlot.Q)
                            && a.Distance(Player.Instance.ServerPosition) > Player.Instance.GetAutoAttackRange())
                        .OrderByDescending(a => a.MaxHealth)
                        .FirstOrDefault();
                if (minionsQ != null)
                    Q.Cast(minionsQ);
            }
        }
    }
}