using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using UnsignedEvade;

namespace Simple_Janna
{
    internal class ELogic
    {
        public static List<MissileClient> ProjectileList = new List<MissileClient>();
        public static List<SpellInfo> EnemyProjectileInformation = new List<SpellInfo>();

        public static void TryToE()
            //credit to Chaos for this logic if about to be hit!
        {
            if (SpellFactory.E.IsReady() && SpellFactory.E.IsLearned)
                foreach (var missile in ProjectileList)
                foreach (var info in EnemyProjectileInformation)
                foreach (var client in EntityManager.Heroes.Allies)
                    if (ShouldShield(missile, info, client) && CollisionCheck(missile, info, client))
                        if (info.ChannelType == SpellDatabase.ChannelType.None && SpellFactory.E.IsReady() &&
                            Config.JannaAutoShieldMenu[client.ChampionName].Cast<CheckBox>().CurrentValue)
                            SpellFactory.E.Cast(client);
                        else if (info.ChannelType != SpellDatabase.ChannelType.None && SpellFactory.E.IsReady()
                                 && Config.JannaAutoShieldMenu[client.ChampionName].Cast<CheckBox>().CurrentValue)
                            SpellFactory.E.Cast(client);
        }


        public static bool ShouldShield(MissileClient missile, SpellInfo info, AIHeroClient client)
        {
            if (missile.SpellCaster.Name != "Diana")
                if (missile.SData.Name != info.MissileName ||
                    !missile.IsInRange(client, 800))
                    return false;


            if (info.ProjectileType == SpellDatabase.ProjectileType.LockOnProjectile
                && missile.Target != client)
                return false;


            return true;
        }


        public static bool CollisionCheck(MissileClient missile, SpellInfo info, AIHeroClient client)
        {
            var variable = Prediction.Position.Collision.LinearMissileCollision(
                client, missile.StartPosition.To2D(),
                missile.StartPosition.To2D().Extend(missile.EndPosition, info.Range),
                info.MissileSpeed, info.Width, info.Delay);
            return variable;
        }
    }
}