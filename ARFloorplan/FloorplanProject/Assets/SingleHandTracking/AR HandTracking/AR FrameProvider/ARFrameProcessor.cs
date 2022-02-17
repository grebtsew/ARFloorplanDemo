using System;
using System.Collections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using MediapipeHandTracking;
using UnityEngine.Experimental.Rendering;

public class ARFrameProcessor : MonoBehaviour {

    public static float ALPHA = float.NegativeInfinity;
    public static float BETA = float.NegativeInfinity;
    public ARCameraManager cameraManager;
    private float imageRatio = float.NegativeInfinity;
    private HandProcessor handProcessor;

    void Start() {
        BETA = (float)Screen.width / Screen.height;
        handProcessor = new HandProcessor();
        StartCoroutine(process());
    }

    unsafe void convertCPUImage() {
        XRCpuImage image;
        if (!cameraManager.TryAcquireLatestCpuImage(out image)) {
            Debug.Log("Cant get image");
            return;
        }

        if (float.IsNegativeInfinity(ALPHA)) {
            ALPHA = (float)image.height / image.width;
            imageRatio = (float)(BETA / ALPHA);
        }

        var conversionParams = new XRCpuImage.ConversionParams {
            // Get the entire image
            inputRect = new RectInt(0, 0, image.width, image.height),
            // Downsample by 2
            outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
            // Choose RGBA format
            outputFormat = TextureFormat.RGBA32, // HDR
            // Flip across the vertical axis (mirror image)
            transformation = XRCpuImage.Transformation.MirrorY
        };

        int size = image.GetConvertedDataSize(conversionParams);

        var buffer = new NativeArray<byte>(size, Allocator.Temp);
        image.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);
        image.Dispose();

        Texture2D m_Texture = new Texture2D(
            conversionParams.outputDimensions.x,
            conversionParams.outputDimensions.y,
            DefaultFormat.LDR,
            0);

        m_Texture.LoadRawTextureData(buffer);
        m_Texture.Apply();
        buffer.Dispose();
        // pass image for mediapipe
        handProcessor.addFrameTexture(m_Texture);
    }

    public IEnumerator process() {
        while (true) {
            yield return new WaitForEndOfFrame();
            convertCPUImage();
            yield return null;
        }
    }

    public HandProcessor HandProcessor { get => handProcessor;}
    public float ImageRatio { get => imageRatio;}
}