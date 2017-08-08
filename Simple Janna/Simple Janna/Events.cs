using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace Simple_Janna
{
    internal static class Events
    {
        static Events()
        {
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDelete += OnDelete;
            Obj_AI_Base.OnUpdatePosition += OnUpdate;
        }

        public static void Initialize()
        {
        }
        public static void OnUpdate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient &&
                logic.Wlogic.ProjectileList.Contains(missile))
            {
                elogic.ProjectileList.Remove(missile);
                elogic.ProjectileList.Add(missile);
            }
        }

        public static void OnCreate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient)
                elogic.ProjectileList.Add(missile);
        }

        public static void OnDelete(GameObject obj, EventArgs args)
        {
            if (obj == null)
                return;

            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient &&
                logic.Wlogic.ProjectileList.Contains(missile))
            {
                elogic.ProjectileList.Remove(missile);
            }
}
        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (!SpellFactory.Q.IsReady() || sender.IsAlly) return;
            if (Config.ReturnBoolMenu("Protector", "Interrupt") && sender.IsValidTarget(SpellFactory.Q.Range) &&
                !sender.IsZombie)
                SpellFactory.Q.Cast(sender);
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsAlly || !SpellFactory.Q.IsReady()) return;
            if (Config.ReturnBoolMenu("Protector", "GapClose") && sender.IsValidTarget(SpellFactory.Q.Range))
                SpellFactory.Q.Cast(sender);
            else if (Config.ReturnBoolMenu("Protector", "GapCloseAllies"))
                foreach (var ally in EntityManager.Heroes.Allies.Where(
                    x => x.IsAlly && Program._Player.IsInRange(x, SpellFactory.Q.Range)))
                    if (sender.IsValidTarget(SpellFactory.Q.Range))
                        SpellFactory.Q.Cast(sender);
        }
    }
}
