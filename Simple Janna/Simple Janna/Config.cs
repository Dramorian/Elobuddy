using System.Linq;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Simple_Janna
{
    internal static class Config
    {
        public static Menu JannaAutoShieldMenu, JannaAutoRMenu;
        private static readonly Menu JannaComboMenu;
        private static readonly Menu JannaHarrasMenu;
        private static readonly Menu JannaProtectorMenu;
        private static readonly Menu JannaMiscMenu;
        private static readonly Menu JannaDrawingsMenu;

        static Config()
        {
            var jannaMainMenu = MainMenu.AddMenu("Simple Janna", "Simple Janna");

            JannaComboMenu = jannaMainMenu.AddSubMenu("Combo", "Combo");
            JannaComboMenu.AddLabel("[Combo Settings]");
            JannaComboMenu.Add("UseQ", new CheckBox("Use Q"));
            JannaComboMenu.Add("UseW", new CheckBox("Use W"));
            JannaComboMenu.Add("UseIgnite", new CheckBox("Use Ignite"));
            //JannaComboMenu.Add("HealthR", new Slider("Use Ulti(R) when Health is under (%):", 10, 1, 100));
            JannaComboMenu.AddSeparator(15);
            JannaComboMenu.AddLabel("[KillSteal Settings]");
            JannaComboMenu.Add("KillSteal", new CheckBox("Enable KillSteal"));
            JannaComboMenu.Add("KillStealW", new CheckBox("KillSteal with W"));


            JannaHarrasMenu = jannaMainMenu.AddSubMenu("Harras", "Harras");
            JannaHarrasMenu.AddLabel("[Harras Settings]");
            JannaHarrasMenu.Add("HarrasQ", new CheckBox("Use Q"));
            JannaHarrasMenu.Add("HarrasE", new CheckBox("Use W"));
            JannaHarrasMenu.Add("HarrasManaSlider", new Slider("Minimum mana to Harras (%):", 40, 1));

            JannaProtectorMenu = jannaMainMenu.AddSubMenu("Protector", "Protector");
            JannaProtectorMenu.AddLabel("[Protector Settings]");
            JannaProtectorMenu.AddLabel("(This option will prioritize to protect Allies in teamfights)");
            JannaProtectorMenu.AddLabel(
                "(Also it wont cast (E) skill on Enemies in Combo Mode while allies are in range)");
            JannaProtectorMenu.Add("SupportMode", new CheckBox("Enable Janna Support Mode"));
            JannaProtectorMenu.AddLabel("[Gapcloser Settings]");
            JannaProtectorMenu.Add("GapClose", new CheckBox("Prevent GapClosers (Q)"));
            JannaProtectorMenu.Add("GapCloseAllies", new CheckBox("Prevent Gapclosers on Allies (Q)"));
            JannaProtectorMenu.Add("Interrupt", new CheckBox("Auto Interrupt Spells (Q)"));
            JannaProtectorMenu.Add("Poison", new CheckBox("Auto Protect from Poison Spells (E)"));

            JannaAutoShieldMenu = jannaMainMenu.AddSubMenu("Auto Shield", "Auto Shield");
            JannaAutoShieldMenu.AddLabel("[Auto Shield Settings]");
            JannaAutoShieldMenu.Add("AShield", new CheckBox("Enable Auto Shield"));
            JannaAutoShieldMenu.Add("AShieldMana", new Slider("Minimum Mana for Auto (E)", 50, 1));
            JannaAutoShieldMenu.AddLabel("[Protect Against Spells]");
            foreach (var enemy in EntityManager.Heroes.Enemies)
                if (SpellProtectDB.AvoidSpells.ContainsKey(enemy.ChampionName))
                {
                    JannaAutoShieldMenu.AddLabel(enemy.ChampionName);
                    foreach (var xd in SpellProtectDB.AvoidSpells[enemy.ChampionName])
                        JannaAutoShieldMenu.Add(xd, new CheckBox(xd, false));
                }

            JannaAutoRMenu = jannaMainMenu.AddSubMenu("Auto R", "Auto R");
            JannaAutoRMenu.AddLabel("[Auto Ulti(R) Settings]");
            JannaAutoRMenu.Add("AutoRJanna", new CheckBox("Auto cast Ulti for Janna"));
            JannaAutoRMenu.Add("AutoRSliderJanna", new Slider("Minimum HP to cast Ulti(R) ({0}%):", 20, 1));
            foreach (var protectTarget in EntityManager.Heroes.Allies.Where(x => !x.IsMe))
            {
                JannaAutoRMenu.Add(protectTarget.ChampionName + "CB",
                    new CheckBox("Protect " + protectTarget.ChampionName));
                JannaAutoRMenu.Add(protectTarget.ChampionName + "SL",
                    new Slider(protectTarget.ChampionName + " Minimum HP to cast Ulti(R) ({0}%):", 20, 1));
            }


            JannaMiscMenu = jannaMainMenu.AddSubMenu("Misc", "Misc");
            JannaMiscMenu.AddGroupLabel("[Misc Settings]");
            /*
            JannaMiscMenu.AddLabel("[Skin Selector]");
            JannaMiscMenu.Add("SkinSelector", new CheckBox("Enable Skin Selector"));
            JannaMiscMenu.Add("SkinID", new Slider("Skin ID:", 1, 1, 5));
            */
            JannaMiscMenu.AddLabel("[Prediction]");
            JannaMiscMenu.Add("PredictionH", new Slider("Prediction Hitchance:", 3, 1, 3));

            JannaDrawingsMenu = jannaMainMenu.AddSubMenu("Drawings", "Drawings");
            JannaDrawingsMenu.AddGroupLabel("[Drawings Settings");
            JannaDrawingsMenu.AddLabel("[Draw Range Settings]");
            JannaDrawingsMenu.Add("DisableAll", new CheckBox("Disable All Drawings", false));
            JannaDrawingsMenu.Add("DrawQ", new CheckBox("Draw Q"));
            JannaDrawingsMenu.Add("DrawW", new CheckBox("Draw W", false));
            JannaDrawingsMenu.Add("DrawE", new CheckBox("Draw E", false));
            JannaDrawingsMenu.Add("DrawR", new CheckBox("Draw R", false));
        }

        //Skin Selector Config
        /*
        public static Slider _SkinSelector
        {
            get { return JannaMiscMenu["SkinID"].Cast<Slider>(); }
        }
        */

        //E protect
        public static bool _AShield => JannaAutoShieldMenu["AShield"].Cast<CheckBox>().CurrentValue;

        public static int _AShieldMana => JannaAutoShieldMenu["AShieldMana"].Cast<Slider>().CurrentValue;

        //Ulti for Janna
        public static bool _AutoRJanna => JannaAutoRMenu["AutoRJanna"].Cast<CheckBox>().CurrentValue;

        public static int _AutoRJannaHp => JannaAutoRMenu["AutoRSliderJanna"].Cast<Slider>().CurrentValue;

        public static void Initialize()
        {
        }


        //Ulti for allies
        public static bool _AutoR(string champName)
        {
            return JannaAutoRMenu[champName + "CB"].Cast<CheckBox>().CurrentValue;
        }

        public static int _AutoRHp(string champName)
        {
            return JannaAutoRMenu[champName + "SL"].Cast<Slider>().CurrentValue;
        }


        //--------------- Menu Checkboxes -----------------//
        public static bool ReturnBoolMenu(string category, string unqIdentifier)
        {
            //Console.WriteLine("Returned Menu Name: {0} with identifier: {1}",name.DisplayName,unqIdentifier);
            switch (category)
            {
                case "Combo":
                    return JannaComboMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Harras":
                    return JannaHarrasMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Protector":
                    return JannaProtectorMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Drawings":
                    return JannaDrawingsMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Misc":
                    return JannaMiscMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;
            }
            return false;
        }

        //--------------- Menu Slider -----------------//
        public static int ReturnIntMenu(string category, string unqIdentifier)
        {
            switch (category)
            {
                case "Combo":
                    return JannaComboMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Harras":
                    return JannaHarrasMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Protector":
                    return JannaProtectorMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Drawings":
                    return JannaDrawingsMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Misc":
                    return JannaMiscMenu[unqIdentifier].Cast<Slider>().CurrentValue;
            }
            return 0;
        }
    }
}