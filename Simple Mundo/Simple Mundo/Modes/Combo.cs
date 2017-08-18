using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
// Using the config like this makes your life easier, trust me
using Settings = Simple_Mundo.Config.Combo.ComboMenu;
using Settings2 = Simple_Mundo.Config.Misc.MiscMenu;

namespace Simple_Mundo.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            // TODO: Add combo logic here

            #region Q combo

            if (Settings.UseQ && Q.IsReady())
            {
                var targetQ = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (targetQ != null && targetQ.IsValid)
                {
                    var qPred = Q.GetPrediction(targetQ);
                    if (qPred.HitChance >= HitChance.High)
                        Q.Cast(qPred.CastPosition);
                }
            }

            #endregion

            #region W combo

            Core.DelayAction(delegate
            {
                if (Settings.UseW && W.IsReady())
                {
                    var targetW = TargetSelector.GetTarget(W.Range, DamageType.Magical);

                    if (targetW != null && targetW.IsValidTarget(W.Range * 2))
                        Program.WEnable();
                }
            }, Settings2.WSDelay);
            

            #endregion

            #region E combo

            if (Settings.UseE && E.IsReady())
            {
                var targetE = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                if (targetE != null && targetE.IsValidTarget(E.Range))
                    E.Cast();
            }

            #endregion
        }
    }
}