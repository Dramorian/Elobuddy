using System.Linq;
using EloBuddy.SDK;
using Settings = Simple_Mundo.Config.LaneClear.LaneClearMenu;

namespace Simple_Mundo.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            if (!Settings.UseQ || !Q.IsReady()) return;
            var minionsQ = EntityManager.MinionsAndMonsters.GetLaneMinions()
                .OrderByDescending(m => m.Health)
                .FirstOrDefault(m => m.IsValidTarget(Q.Range));

            if (minionsQ != null && minionsQ.IsValidTarget(Q.Range))
                Q.Cast(minionsQ);
            // TODO: Add laneclear logic here
        }
    }
}