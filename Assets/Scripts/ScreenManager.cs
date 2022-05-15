using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        public static float ScreenAspect  { get; private set; }
        public static float CameraOrtSize { get; private set; }
        public static float CameraWidth   => CameraOrtSize * ScreenAspect * 2;
        public static float CameraHeight  => CameraOrtSize * 2;

        public static float ScreenRadius => Mathf.Sqrt(CameraWidth / 2f * CameraWidth / 2f + CameraOrtSize * CameraOrtSize);

        [SerializeField] private Camera m_MainCam;

        [Button]
        private void SetRefs()
        {
            m_MainCam = Camera.main;
        }

        private void Awake()
        {
            ScreenAspect = (float) Screen.width / Screen.height;
            SetCameraSize();
        }

        private void SetCameraSize()
        {
            //TODO:Auto resize
            CameraOrtSize = m_MainCam.orthographicSize;
        }
    }
}