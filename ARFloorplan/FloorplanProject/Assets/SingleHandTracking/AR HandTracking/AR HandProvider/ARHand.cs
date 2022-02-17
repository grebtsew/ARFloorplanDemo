using UnityEngine;
using MediapipeHandTracking;

public class ARHand {
    private Vector3[] landmarks, landmarksCP = default;
    public float currentDepth = 0f;
    private Camera cam;

    public ARHand() {
        landmarks = new Vector3[21];
        cam = Camera.main;
    }

    private ARHand(Vector3[] landmarks) {
        this.landmarks = landmarks;
    }

    public void ParseFrom(float[] arr, float c) {
        if (null == arr || arr.Length < 63) return;
        //độ sâu của điểm ở cổ tay

        for (int i = 0; i < 21; i++) {
            float xScreen = Screen.width * ((arr[i * 3 + 1] - 0.5f * (1 - c)) / c);
            float yScreen = Screen.height * (arr[i * 3]);
            this.landmarks[i] = cam.ScreenToWorldPoint(new Vector3(xScreen, yScreen, arr[i * 3 + 2] / 80 + 0.4f));
        }

        if (landmarksCP == default) {
            landmarksCP = new Vector3[21];
            landmarksCP = (Vector3[])landmarks.Clone();
        } else {
            // nễu bị rung giữ nguyên landmark cũ
            if (isVibrate(0.02f)) {
                landmarks = (Vector3[])landmarksCP.Clone();
            } else { // lưu lại landmark khi không bị rung
                landmarksCP = (Vector3[])landmarks.Clone();
            }
        }
    }

    public bool isVibrate(float deltaVibrate) {
        for (int i = 0; i < 21; i++) {
            if (Vector3.Distance(landmarksCP[i], landmarks[i]) > deltaVibrate) return false;
        }
        return true;
    }

    public Vector3 GetLandmark(int index) => this.landmarks[index];
    public Vector3[] GetLandmarks() => this.landmarks;
    public ARHand Clone() {
        return new ARHand((Vector3[])landmarks.Clone());
    }

}