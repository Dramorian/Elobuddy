using EloBuddy;
using EloBuddy.SDK;
using Settings = Simple_Mundo.Config.Combo.ComboMenu;
using Settings2 = Simple_Mundo.Config.Misc.MiscMenu;

namespace Simple_Mundo.Modes
{
    public abstract class ModeBase
    {
        // Change the spell type to whatever type you used in the SpellManager
        // here to have full features of that spells, if you don't need that,
        // just change it to Spell.SpellBase, this way it's dynamic with still
        // the most needed functions
        protected Spell.Skillshot Q => SpellManager.Q;

        protected Spell.Active W => SpellManager.W;

        protected Spell.Active E => SpellManager.E;

        protected Spell.Active R => SpellManager.R;

        protected Spell.Targeted Smite => SpellManager.Smite;

        protected bool HasSmite => SpellManager.HasSmite();

        protected bool PotionRunning()
        {
            return Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemCrystalFlaskJungle") ||
                   Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") ||
                   Player.Instance.HasBuff("ItemDarkCrystalFlask");
        }

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}