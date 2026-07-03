//______________________________________________
// ALIyerEdon
// https://assetstore.unity.com/publishers/23606
//______________________________________________

using UnityEngine;
using System.Collections;
using ALIyerEdon;

namespace ALIyerEdon
{
    public class CameraSwitch : MonoBehaviour
    {
        [SerializeField]
        public CameraView[] cameraView;

        // Hold curent active camera id
        int currentCamera = 0;
        SmoothFollow2 smoothFollow;

        void Start()
        {
            if (FindAnyObjectByType<SmoothFollow2>())
            {
                smoothFollow = FindAnyObjectByType<SmoothFollow2>();

                smoothFollow.smooth = cameraView[currentCamera].Smooth;
                smoothFollow.distance = cameraView[currentCamera].Distance;
                smoothFollow.height = cameraView[currentCamera].Height;
                smoothFollow.Angle = cameraView[currentCamera].Angle;
            }
        }

#if UNITY_EDITOR
        void Update()
        {
            if (cameraView[currentCamera].captureCurrentView)
            {
                cameraView[currentCamera].captureCurrentView = false;

                cameraView[currentCamera].Smooth =
                    FindAnyObjectByType<SmoothFollow2>().smooth;
                cameraView[currentCamera].Distance =
                   FindAnyObjectByType<SmoothFollow2>().distance;
                cameraView[currentCamera].Height =
                   FindAnyObjectByType<SmoothFollow2>().height;
                cameraView[currentCamera].Angle =
                   FindAnyObjectByType<SmoothFollow2>().Angle;
            }

        }
#endif
        // Switch to next camera based total camera counts
        public void NextCamera()
        {
            if (currentCamera < cameraView.Length - 1)
                currentCamera++;
            else
                currentCamera = 0;

            smoothFollow.smooth = cameraView[currentCamera].Smooth;
            smoothFollow.distance = cameraView[currentCamera].Distance;
            smoothFollow.height = cameraView[currentCamera].Height;
            smoothFollow.Angle = cameraView[currentCamera].Angle;

            if (cameraView[currentCamera].DashboardCamera)
            {
                smoothFollow.SwitchTarget(true);
            }
            else
            {
                smoothFollow.SwitchTarget(false);
            }
        }

        [System.Serializable]
        public class CameraView
        {
            public float Smooth;
            public float Distance;
            public float Height;
            public float Angle;
            public bool captureCurrentView;
            public bool DashboardCamera;
        }
    }
}