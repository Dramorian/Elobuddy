using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using Settings = Simple_Mundo.Config.Misc.MiscMenu;
using Settings2 = Simple_Mundo.Config.Combo.ComboMenu;

namespace Simple_Mundo.Modes
{
    public sealed class PermaActive : ModeBase

    {
        public static Obj_AI_Minion Minion;
        public static AIHeroClient _Player => ObjectManager.Player;

        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            #region KS

            if (Settings.EnableKSQ && Q.IsReady())
            {
                var ksQ = EntityManager.Heroes.Enemies
                    .FirstOrDefault(
                        e => e.IsValidTarget() &&
                             !e.IsDead && !e.IsZombie && !e.IsInvulnerable && e.Health < Player.Instance.GetSpellDamage(e, SpellSlot.Q));


                if (ksQ != null)
                {
                    var qpred = Q.GetPrediction(ksQ);
                    if (qpred.HitChance >= HitChance.High)
                    {
                        Q.Cast(qpred.CastPosition);
                        return;
                    }
                }
            }

            #endregion

            #region Potion

            if (Settings.EnablePotion && !Player.Instance.IsInShopRange() &&
                Player.Instance.HealthPercent <= Settings.MinHPPotion && !PotionRunning())
            {
                if (Item.HasItem(Utility.HealthPotion.Id) && Item.CanUseItem(Utility.HealthPotion.Id))
                {
                    Utility.HealthPotion.Cast();
                    return;
                }
                if (Item.HasItem(Utility.HuntersPotion.Id) && Item.CanUseItem(Utility.HuntersPotion.Id))
                {
                    Utility.HuntersPotion.Cast();
                    return;
                }
                if (Item.HasItem(Utility.TotalBiscuit.Id) && Item.CanUseItem(Utility.TotalBiscuit.Id))
                {
                    Utility.TotalBiscuit.Cast();
                    return;
                }
                if (Item.HasItem(Utility.RefillablePotion.Id) && Item.CanUseItem(Utility.RefillablePotion.Id))
                {
                    Utility.RefillablePotion.Cast();
                    return;
                }
                if (Item.HasItem(Utility.CorruptingPotion.Id) && Item.CanUseItem(Utility.CorruptingPotion.Id))
                {
                    Utility.CorruptingPotion.Cast();
                    return;
                }
            }

            #endregion

            #region Smite

            if (HasSmite)
            {
                //Red Smite Combo
                if (Config.Smite.SmiteMenu.SmiteCombo && Smite.Name.Equals("s5_summonersmiteduel") &&
                    Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) && Smite.IsReady())
                    foreach (
                        var smiteTarget in
                        EntityManager.Heroes.Enemies
                            .Where(h => h.IsValidTarget(Smite.Range))
                            .Where(h => h.HealthPercent <= Config.Smite.SmiteMenu.RedSmitePercent)
                            .OrderByDescending(TargetSelector.GetPriority))
                    {
                        Smite.Cast(smiteTarget);
                        return;
                    }

                // Blue Smite KS
                if (Config.Smite.SmiteMenu.SmiteEnemies && Smite.Name.Equals("s5_summonersmiteplayerganker") &&
                    Smite.IsReady())
                {
                    var smiteKs =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            e =>
                                Smite.IsInRange(e) && !e.IsDead && e.Health > 0 && !e.IsInvulnerable &&
                                e.IsVisible &&
                                e.TotalShieldHealth() < Utility.SmiteDmgHero(e));
                    if (smiteKs != null)
                    {
                        Smite.Cast(smiteKs);
                        return;
                    }
                }

                // Smite Monsters
                if (!Config.Smite.SmiteMenu.SmiteToggle || !Smite.IsReady()) return;
                {
                    var monsters2 =
                        EntityManager.MinionsAndMonsters
                            .GetJungleMonsters(Player.Instance.ServerPosition, Smite.Range)
                            .Where(
                                e =>
                                    !e.IsDead && e.Health > 0 && Utility.MonstersNames.Contains(e.BaseSkinName) &&
                                    !e.IsInvulnerable && e.IsVisible && e.Health <= Utility.SmiteDmgMonster(e));
                    foreach (
                        var n in
                        monsters2.Where(
                            n => Config.Smite.SmiteMenu.MainMenu[n.BaseSkinName].Cast<CheckBox>().CurrentValue))
                    {
                        Smite.Cast(n);
                        return;
                    }
                }
            }

            #endregion

            #region W autodisable

            Core.DelayAction(delegate
            {
                //W autodisable thanks to Sunnyline2
                if (Settings.SmartW && Program.WStatus())
                {
                    var monsters =
                        EntityManager.MinionsAndMonsters.CombinedAttackable.Count(
                            monster => monster.IsValidTarget(W.Range * 2));
                    var enemies = EntityManager.Heroes.Enemies.Count(enemy => enemy.IsValidTarget(W.Range * 2));
                    if (monsters == 0 && enemies == 0)
                        Program.WDisable();
                }
            }, Settings.WSDelay);

            #endregion

            #region R usage on low hp

            if (!Player.Instance.IsInFountainRange() && Player.Instance.IsInShopRange() &&
                Player.Instance.HealthPercent <= Settings2.RMinHP)
                R.Cast();

            #endregion
        }
    }
}