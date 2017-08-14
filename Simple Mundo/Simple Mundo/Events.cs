using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Settings = Simple_Mundo.Config.Draw.DrawMenu;

namespace Simple_Mundo
{
    internal class Events
    {
        static Events()
        {
            Gapcloser.OnGapcloser += OnGapCloser;
            Drawing.OnDraw += OnDraw;
        }


        private static void OnDraw(EventArgs args)
        {
            if (Settings.DrawQ && SpellManager.Q.IsLearned)
                Circle.Draw(Color.Green, SpellManager.Q.Range, Player.Instance.Position);


            if (!SpellManager.HasSmite()) return;
            if (Settings.DrawSmite && Config.Smite.SmiteMenu.SmiteToggle
                || Settings.DrawSmite && Config.Smite.SmiteMenu.SmiteCombo
                || Settings.DrawSmite && Config.Smite.SmiteMenu.SmiteEnemies)
                Circle.Draw(Color.Blue, SpellManager.Smite.Range, Player.Instance.Position);
        }

        public static void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly || Config.Misc.MiscMenu.GapcloseQ)
                return;

            if (sender.IsAttackingPlayer || e.End.Distance(Player.Instance) <= 70)
                SpellManager.Q.Cast(sender);
        }


        public static void Initialize()
        {
        }
    }
}