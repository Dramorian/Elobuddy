using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace Simple_Mundo
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string DrMundo = "ChampionName, DrMundo";

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Verify the champion we made this addon for
            if (Player.Instance.Hero != Champion.DrMundo)
                return;

            // Initialize the classes that we need
            Config.Initialize();
            Events.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            Utility.Initialize();

            // Listen to events we need


            Chat.Print("Simple Mundo loaded", Color.Cyan);
            Chat.Print("Good luck and have fun", Color.Cyan);


        }

        public static bool WStatus()
        {
            if (Player.HasBuff("BurningAgony"))
                return true;
            return false;
        }

        public static void WEnable()
        {
            if (!WStatus())
                SpellManager.W.Cast();
        }

        public static void WDisable()
        {
            if (WStatus())
                SpellManager.W.Cast();
        }
    }
}