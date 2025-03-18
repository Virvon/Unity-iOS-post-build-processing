using Assets.Sources.LoadingTree;
using Assets.Sources.LoadingTree.Piplines;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.SaveLoadData;
using UnityEngine;

namespace Assets.Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        private void Awake()
        {
            SharedBundle sharedBundle = new();
            sharedBundle.Add(SharedBundleKeys.SaveLoadService, new SaveLoadService());

            GameLoadingPipline gameLoadingPipline = new();
            GameLauncher gameLauncher = new(gameLoadingPipline.GetOperations());

            gameLauncher.Launch(sharedBundle);
        }
    }
}
