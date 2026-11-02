using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace FemaleTroopsAndBandits
{
    public class FemaleTroopsAndBanditsSubModule : MBSubModuleBase
    {
        public const string HarmonyId = "female.troops.and.bandits";

        private readonly Harmony _harmony = new Harmony(HarmonyId);

        protected override void OnSubModuleLoad()
        {
            try
            {
                _harmony.PatchAll(typeof(FemaleTroopsAndBanditsSubModule).Assembly);
                Debug.Print("[FemaleTroopsAndBandits] volunteer + party troop mapping active.");
            }
            catch (System.Exception exception)
            {
                try
                {
                    _harmony.UnpatchAll(HarmonyId);
                }
                catch
                {
                }

                Debug.Print("[FemaleTroopsAndBandits] failed to start: " + exception);
            }
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            // Do not wire upgrades here. Characters are registered but not ready yet
            // (Culture still null). Reading UpgradeTargets that early trips Crash Doctor's
            // null-field fallback, which stamps empty upgrade lists and shows a false
            // "troop files did not fully load" notice. Session launch + daily tick apply.
            CampaignGameStarter starter = gameStarterObject as CampaignGameStarter;
            if (game.GameType is Campaign && starter != null)
            {
                starter.AddBehavior(new FemaleTroopBranchBehavior());
            }
        }
    }
}
