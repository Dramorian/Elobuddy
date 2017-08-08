using System.Linq;
using EloBuddy.SDK;
using Settings = Simple_Mundo.Config.JungleClear.JungleClearMenu;

namespace Simple_Mundo.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on jungleclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            #region Q JungleClear

            if (!Settings.UseQ || !Q.IsReady()) return;

            var monsterQ = EntityManager.MinionsAndMonsters.GetJungleMonsters()
                .OrderByDescending(m => m.Health)
                .FirstOrDefault(m => m.IsValidTarget(Q.Range));

            if (monsterQ == null || !monsterQ.IsValidTarget(Q.Range))
                return;
            Q.Cast(monsterQ);

            #endregion

            #region W JungleClear

            if (!Settings.UseW || !W.IsReady()) return;
            {
                Program.WEnable();
            }

            #endregion

            #region E JungleClear

            if (!Settings.UseE || !E.IsReady()) return;
            E.Cast();
            // TODO: Add jungleclear logic here

            #endregion
        }
    }
}