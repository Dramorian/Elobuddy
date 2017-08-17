using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = Simple_Mundo.Config.Harass.HarassMenu;

namespace Simple_Mundo.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on harass mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            // TODO: Add harass logic here
            // See how I used the Settings.UseQ and Settings.Mana here, this is why I love
            // my way of using the menu in the Config class!
            if (Settings.UseQ && Player.Instance.HealthPercent > Settings.Health && Q.IsReady())
            {
                var targetQ = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

                if (targetQ != null )
                {
                    var predQ = Q.GetPrediction(targetQ);
                    if (predQ.HitChance >= HitChance.High)
                        Q.Cast(predQ.CastPosition);
                }
            }
        }
    }
}