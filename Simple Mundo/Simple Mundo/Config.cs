using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace Simple_Mundo
{
    public static class Config
    {
        private const string MenuName = "SimpleMundo";

        private static readonly Menu Menu;


        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Simple Mundo");
            Menu.AddLabel("Report any suggestions/problems to the forum thread!");
            Menu.AddGroupLabel("Credits:");
            Menu.AddLabel("Hellsing, Zpitty, Taazuma, hazanpro");


            Combo.Initialize();
            Harass.Initialize();
            JungleClear.Initialize();
            LaneClear.Initialize();
            LastHit.Initialize();
            Misc.Initialize();
            Smite.Initialize();
            Draw.Initialize();
        }

        public static void Initialize()
        {
        }

        #region Combo

        public static class Combo
        {
            private static readonly Menu CMenu;

            static Combo()
            {
                CMenu = Menu.AddSubMenu("Combo");


                ComboMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class ComboMenu
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly Slider _rMinHP;


                static ComboMenu()
                {
                    CMenu.AddGroupLabel("Combo Options");
                    _useQ = CMenu.Add("comboUseQ", new CheckBox("Use [Q]"));
                    _useW = CMenu.Add("comboUseW", new CheckBox("Use [W]"));
                    _useE = CMenu.Add("comboUseE", new CheckBox("Use [E]"));
                    CMenu.AddSeparator();
                    _useR = CMenu.Add("comboUseR", new CheckBox("Use [R]"));
                    _rMinHP = CMenu.Add("comboMinHP", new Slider("Minimum HP for cast({0})", 15));
                }

                public static Menu MainMenu => CMenu;

                public static bool UseQ => _useQ.CurrentValue;

                public static bool UseW => _useW.CurrentValue;

                public static bool UseE => _useE.CurrentValue;

                public static bool UseR => _useR.CurrentValue;

                public static int RMinHP => _rMinHP.CurrentValue;


                public static void Initialize()
                {
                }
            }
        }

        #endregion

        #region Harass

        public static class Harass
        {
            private static readonly Menu HMenu;

            static Harass()
            {
                HMenu = Menu.AddSubMenu("Harass");

                HarassMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class HarassMenu
            {
                private static readonly CheckBox _useQ;
                private static readonly Slider _health;


                static HarassMenu()
                {
                    HMenu.AddGroupLabel("Harass Options");
                    _useQ = HMenu.Add("harassQ", new CheckBox("Use [Q]"));
                    _health = HMenu.Add("harassHealth", new Slider("Maximum health usage in percent({0})", 40));
                }

                public static bool UseQ => _useQ.CurrentValue;
                public static int Health => _health.CurrentValue;


                public static void Initialize()
                {
                }
            }
        }

        #endregion

        #region JungleClear

        public static class JungleClear
        {
            private static readonly Menu JMenu;

            static JungleClear()
            {
                JMenu = Menu.AddSubMenu("JungleClear");

                JungleClearMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class JungleClearMenu
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;


                static JungleClearMenu()
                {
                    JMenu.AddGroupLabel("JungleClear Options");
                    _useQ = JMenu.Add("jungleUseQ", new CheckBox("Use Q"));
                    _useW = JMenu.Add("jungleUseW", new CheckBox("Use W"));
                    _useE = JMenu.Add("jungleUseW", new CheckBox("Use E"));
                }


                public static bool UseQ => _useQ.CurrentValue;

                public static bool UseW => _useW.CurrentValue;
                public static bool UseE => _useE.CurrentValue;

                public static void Initialize()
                {
                }
            }
        }

        #endregion

        #region LaneClear

        public static class LaneClear
        {
            private static readonly Menu LMenu;

            static LaneClear()
            {
                LMenu = Menu.AddSubMenu("LaneClear");

                LaneClearMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class LaneClearMenu
            {
                private static readonly CheckBox _useQ;


                static LaneClearMenu()
                {
                    LMenu.AddGroupLabel("LaneClear Options");
                    _useQ = LMenu.Add("laneUseQ", new CheckBox("Use Q"));
                }


                public static bool UseQ => _useQ.CurrentValue;


                public static void Initialize()
                {
                }
            }
        }

        #endregion

        #region LastHit

        public static class LastHit
        {
            private static readonly Menu LHMenu;

            static LastHit()
            {
                LHMenu = Menu.AddSubMenu("Last Hit");

                LastHitMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class LastHitMenu
            {
                private static readonly CheckBox _useQ;


                static LastHitMenu()
                {
                    LHMenu.AddGroupLabel("LastHit Options");
                    _useQ = LHMenu.Add("lastHitQ", new CheckBox("Use Q"));
                }


                public static bool UseQ => _useQ.CurrentValue;


                public static void Initialize()
                {
                }
            }
        }

        #endregion

        #region Misc

        public static class Misc
        {
            private static readonly Menu MMenu;

            static Misc()
            {
                MMenu = Menu.AddSubMenu("Misc");

                MiscMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class MiscMenu
            {
                private static readonly CheckBox _enablePotion;
                private static readonly CheckBox _enableKSQ;
                private static readonly CheckBox _gapcloseQ;
                private static readonly CheckBox _smartW;
                private static readonly Slider _minHPPotion;
                private static readonly Slider _WSdelay;


                static MiscMenu()
                {
                    MMenu.AddGroupLabel("Misc Options");
                    _enableKSQ = MMenu.Add("KSQ", new CheckBox("Use [Q] on Killable"));
                    _smartW = MMenu.Add("smartW", new CheckBox("Automatic disable [W] (Smart)"));
                    _WSdelay = MMenu.Add("WSdelay", new Slider("Smart [W] Delay (ms)", 0, 0, 500));
                    _gapcloseQ = MMenu.Add("gapcloseQ", new CheckBox("Use [Q] on enemy gapclose"));


                    MMenu.AddGroupLabel("Potion Manager");
                    _enablePotion = MMenu.Add("Potion", new CheckBox("Use Potions"));
                    _minHPPotion = MMenu.Add("minHPPotion", new Slider("Use at % Health", 60));
                }

                public static bool EnablePotion => _enablePotion.CurrentValue;


                public static bool EnableKSQ => _enableKSQ.CurrentValue;
                public static bool GapcloseQ => _gapcloseQ.CurrentValue;

                public static bool SmartW => _smartW.CurrentValue;

                public static int MinHPPotion => _minHPPotion.CurrentValue;


                public static int WSDelay => _WSdelay.CurrentValue;


                public static void Initialize()
                {
                }
            }
        }

        #endregion

        #region Smite

        public static class Smite
        {
            public static readonly Menu SMenu;

            static Smite()
            {
                SMenu = Menu.AddSubMenu("Smite Menu");

                SmiteMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class SmiteMenu
            {
                public static readonly KeyBind _smiteEnemies;
                public static readonly KeyBind _smiteCombo;
                private static readonly KeyBind _smiteToggle;
                private static readonly Slider _redSmitePercent;

                static SmiteMenu()
                {
                    SMenu.AddGroupLabel("Smite Options");
                    SMenu.AddSeparator();
                    _smiteToggle = SMenu.Add("EnableSmite",
                        new KeyBind("Enable Smite Monsters (Toggle)", false, KeyBind.BindTypes.PressToggle, 'M'));
                    _smiteEnemies = SMenu.Add("EnableSmiteEnemies",
                        new KeyBind("Blue Smite KS (Toggle)", false, KeyBind.BindTypes.PressToggle, 'M'));
                    _smiteCombo = SMenu.Add("EnableSmiteCombo",
                        new KeyBind("Red Smite Combo (Toggle)", false, KeyBind.BindTypes.PressToggle, 'M'));
                    _redSmitePercent = SMenu.Add("SmiteRedPercent", new Slider("Red Smite Enemy % HP", 60));
                    SMenu.AddSeparator();
                    SMenu.AddGroupLabel("Smiteable Monsters");
                    SMenu.Add("SRU_Baron", new CheckBox("Baron"));
                    SMenu.Add("SRU_Dragon_Water", new CheckBox("Water Dragon"));
                    SMenu.Add("SRU_Dragon_Fire", new CheckBox("Fire Dragon"));
                    SMenu.Add("SRU_Dragon_Earth", new CheckBox("Earth Dragon"));
                    SMenu.Add("SRU_Dragon_Air", new CheckBox(" Air Dragon"));
                    SMenu.Add("SRU_Dragon_Elder", new CheckBox("Elder Dragon"));
                    SMenu.Add("SRU_Red", new CheckBox("Red"));
                    SMenu.Add("SRU_Blue", new CheckBox("Blue"));
                    SMenu.Add("SRU_Gromp", new CheckBox("Gromp"));
                    SMenu.Add("SRU_Murkwolf", new CheckBox("Murkwolf"));
                    SMenu.Add("SRU_Krug", new CheckBox("Krug"));
                    SMenu.Add("SRU_Razorbeak", new CheckBox("Razorbeak"));
                    SMenu.Add("Sru_Crab", new CheckBox("Crab"));
                    SMenu.Add("SRU_RiftHerald", new CheckBox("Rift Herald", false));
                }

                public static Menu MainMenu => SMenu;


                public static bool SmiteToggle => _smiteToggle.CurrentValue;

                public static bool SmiteEnemies => _smiteEnemies.CurrentValue;

                public static bool SmiteCombo => _smiteCombo.CurrentValue;

                public static int RedSmitePercent => _redSmitePercent.CurrentValue;

                public static void Initialize()
                {
                }
            }
        }

        #endregion

        #region Draw

        public static class Draw
        {
            public static readonly Menu DMenu;

            static Draw()
            {
                DMenu = Menu.AddSubMenu("Draw Menu");

                DrawMenu.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class DrawMenu
            {
                public static readonly CheckBox _drawQ;
                public static readonly CheckBox _drawSmite;


                static DrawMenu()
                {
                    DMenu.AddGroupLabel("Draw Options");
                    DMenu.AddSeparator();
                    _drawQ = DMenu.Add("QDraw", new CheckBox("Draw Q"));
                    _drawSmite = DMenu.Add("SmiteDraw", new CheckBox("Draw Smite"));
                }

                public static Menu MainMenu => DMenu;


                public static bool DrawQ => _drawQ.CurrentValue;

                public static bool DrawSmite => _drawSmite.CurrentValue;


                public static void Initialize()
                {
                }
            }
        }

        #endregion
    }
}