using Assets.Sources.LoadingTree.SharedDataBundle;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Sources.LoadingTree.Operations
{
    public class ReadInfoPlistOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            string apiHost = GetApiHost();

            if (string.IsNullOrEmpty(apiHost))
                throw new Exception("API_HOST not found in Info.plist");

            Debug.Log("API_HOST: " + apiHost);

            bundle.Add(SharedBundleKeys.ApiHost, apiHost);
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetAPIHost();

        private string GetApiHost()
        {
            string apiHost = null;

            try
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    IntPtr apiHostPtr = GetAPIHost();

                    if (apiHostPtr != IntPtr.Zero)
                    {
                        apiHost = Marshal.PtrToStringAuto(apiHostPtr);
                        Marshal.FreeHGlobal(apiHostPtr);
                    }
                }
                else
                {
                    Debug.LogWarning("This script is intended to run on iOS.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to read API_HOST: " + e.Message);
            }

            return apiHost;
        }
    }
}
