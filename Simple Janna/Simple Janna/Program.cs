using System;
using System.Reflection;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Simple_Janna
{
    internal class Program
    {
        private static readonly string version =
            Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static bool bIgnite;

        public static AIHeroClient _Player => ObjectManager.Player;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (_Player.ChampionName != "Janna") return;
            SpellFactory.Initialize();
            Config.Initialize();
            Events.Initialize();
            Modes.Initialize();

            Drawing.OnDraw += Drawing_OnDraw;

            /*
            Utils.SelectSkin(Config._SkinSelector.CurrentValue);

            Config._SkinSelector.OnValueChange += delegate (ValueBase<int> s, ValueBase<int>.ValueChangeArgs aargs)
            {
                Utils.SelectSkin(aargs.NewValue);
            };
            */


            Chat.Print("Simple Janna " + version + " loaded");
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Config.ReturnBoolMenu("Drawings", "DisableAll")) return;

            if (Config.ReturnBoolMenu("Drawings", "DrawQ") && SpellFactory.Q.IsReady())
                Circle.Draw(Color.HotPink, SpellFactory.Q.Range, _Player.Position);
            if (Config.ReturnBoolMenu("Drawings", "DrawW") && SpellFactory.W.IsReady())
                Circle.Draw(Color.Brown, SpellFactory.W.Range, _Player.Position);
            if (Config.ReturnBoolMenu("Drawings", "DrawE") && SpellFactory.E.IsReady())
                Circle.Draw(Color.Aqua, SpellFactory.E.Range, _Player.Position);
            if (Config.ReturnBoolMenu("Drawings", "DrawR") && SpellFactory.R.IsReady())
                Circle.Draw(Color.DarkOrange, SpellFactory.R.Range, _Player.Position);
        }
    }
}