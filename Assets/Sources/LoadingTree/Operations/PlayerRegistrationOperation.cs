using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Utils;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Sources.LoadingTree.Operations
{
    public class PlayerRegistrationOperation : IOperation
    {
        private const int RetriesCount = 3;
        private const string BundleId = "com.example.app";
        private const int AttempsDelay = 2;

        public void Run(SharedBundle bundle)
        {
            SendRequest(
                bundle.Get<string>(SharedBundleKeys.ApiHost),
                bundle.Get<string>(SharedBundleKeys.DeviceId))
                .Forget();
        }

        private async UniTaskVoid SendRequest(string apiHost, string deviceId)
        {
            int attempt = 0;
            bool isSuccess = false;

            while (attempt < RetriesCount && isSuccess == false)
            {
                attempt++;

                try
                {
                    string url = $"https://{apiHost}/v1/players";

                    var jsonData = new
                    {
                        bundleId = BundleId,
                        deviceId = deviceId,
                    };

                    string jsonString = jsonData.ToJson();

                    WWWForm formData = new();

                    using (UnityWebRequest request = UnityWebRequest.Post(url, formData))
                    {
                        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
                        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                        request.downloadHandler = new DownloadHandlerBuffer();
                        request.SetRequestHeader("Content-Type", "application/json");

                        await request.SendWebRequest().ToUniTask();

                        if (request.result != UnityWebRequest.Result.Success)
                            throw new Exception($"Request failed: {request.error} (Attempt {attempt})");

                        Debug.Log("Request successful: " + request.downloadHandler.text);
                        isSuccess = true;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Exception during request: {e.Message} (Attempt {attempt})");
                }

                if (isSuccess == false && attempt < RetriesCount)
                    await UniTask.Delay(TimeSpan.FromSeconds(AttempsDelay));
            }
        }
    }
}
