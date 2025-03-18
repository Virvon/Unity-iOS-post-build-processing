using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Sources
{
    public class InfoPlistReader : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern IntPtr GetAPIHost();

        void Start()
        {
            string apiHost = ReadAPIHostFromPlist();
            if (!string.IsNullOrEmpty(apiHost))
            {
                Debug.Log("API_HOST: " + apiHost);
            }
            else
            {
                Debug.LogError("API_HOST not found in Info.plist");
            }
        }

        private string ReadAPIHostFromPlist()
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
                Debug.LogError("Failed to read API_HOST: " + e.Message);
            }
            return apiHost;
        }
    }
}
