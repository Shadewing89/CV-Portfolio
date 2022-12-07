using UnityEngine;

namespace Settings
{
   public class PerformanceSettings : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}