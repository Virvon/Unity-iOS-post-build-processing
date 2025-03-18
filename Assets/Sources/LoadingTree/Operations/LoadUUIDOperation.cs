using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.SaveLoadData;
using System;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadUUIDOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            ISaveLoadService saveLoadService = bundle.Get<ISaveLoadService>(SharedBundleKeys.SaveLoadService);
            string uuidData = saveLoadService.TryLoad<string>();

            Guid uuid = string.IsNullOrEmpty(uuidData) ? GenerateUUID(saveLoadService) : new Guid(uuidData);

            bundle.Add(SharedBundleKeys.DeviceId, uuid.ToString());
        }

        private Guid GenerateUUID(ISaveLoadService saveLoadService)
        {
            Guid uuid = Guid.NewGuid();

            saveLoadService.Save(uuid.ToString());

            return uuid;
        }
    }
}
