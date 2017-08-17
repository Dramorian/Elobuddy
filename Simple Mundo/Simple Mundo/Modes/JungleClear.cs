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

            if (Settings.UseQ && Q.IsReady())
            {
                var monsterQ = EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(Q.Range));

                if (monsterQ != null && monsterQ.IsValidTarget(Q.Range))
                    Q.Cast(monsterQ);
            }

            #endregion

            #region W JungleClear

            if (Settings.UseW && W.IsReady())
                Program.WEnable();

            #endregion

            #region E JungleClear

            if (Settings.UseE && E.IsReady())
            {
                var monsterE = EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(E.Range));

                if (monsterE != null && monsterE.IsValidTarget(E.Range))
                    E.Cast();
                // TODO: Add jungleclear logic here
            }

            #endregion
        }
    }
}