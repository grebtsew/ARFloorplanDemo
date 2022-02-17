using System;
using UnityEngine;

namespace MediapipeHandTracking {
    public class HandProcessor {
        private const int SIZE_RESOLUSTION = 256;
        private AndroidJavaObject singleHandMain;

        public HandProcessor() {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentUnityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            singleHandMain = new AndroidJavaObject("com.jackie.mediapipe.HandTracking", currentUnityActivity);
            singleHandMain.Call("setResolution", SIZE_RESOLUSTION);
        }

        public float[] getHandLandmarksData() {
            return singleHandMain.Call<float[]>("getLandmarks");
        }

        public float[] getHandRectData() {
            return singleHandMain.Call<float[]>("getPalmRect");
        }

        public unsafe void addFrameTexture(Texture2D m_Texture) {
            byte[] frameImage = ImageConversion.EncodeToJPG(m_Texture);

            sbyte[] frameImageSigned = Array.ConvertAll(frameImage, b => unchecked((sbyte)b));
            singleHandMain.Call("setFrame", frameImageSigned);
        }

        void OnDestroy() {
            if (singleHandMain != null) {
                singleHandMain.Dispose();
            }
        }

       
    }
}
