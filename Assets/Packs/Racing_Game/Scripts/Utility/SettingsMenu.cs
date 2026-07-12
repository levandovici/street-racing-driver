//______________________________________________
// ALIyerEdon
// https://assetstore.unity.com/publishers/23606
//______________________________________________

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ALIyerEdon;

namespace ALIyerEdon
{
    public class SettingsMenu : MonoBehaviour
    {
        // UI items in settings menu window
        public Dropdown targetFPS;
        public Dropdown dynamicCamera;

        public Slider AccelSensibility;
        public Text AccelSensibilityInfo;
        public Slider musicVolume;
        public Text musicVolumeInfo;

        public Toggle positionUI;

        public Button privacyPolicy;



        private void Awake()
        {
            privacyPolicy.onClick.AddListener(() => Application.OpenURL("https://michitai.com/privacy-policy.html"));
        }



        // Start is called before the first frame update
        void Start()
        {
            // Load initilial settings            
            AccelSensibility.value = PlayerPrefs.GetFloat("accelSensibility");
            AccelSensibilityInfo.text = AccelSensibility.value.ToString();

            targetFPS.value = PlayerPrefs.GetInt("targetFPS");
            dynamicCamera.value = PlayerPrefs.GetInt("Dynamic Camera");

            musicVolume.value = PlayerPrefs.GetFloat("Music");

            if (PlayerPrefs.GetInt("ShowPositionUI") == 3)
                positionUI.isOn = true;
            else
                positionUI.isOn = false;
        }

        // Accelerometer  Sensibility
        public void Accelometer_Sensibility()
        {
            PlayerPrefs.SetFloat("accelSensibility", AccelSensibility.value);
            AccelSensibilityInfo.text = AccelSensibility.value.ToString();
        }
        public void Music_Volume()
        {
            PlayerPrefs.SetFloat("Music", musicVolume.value);
            musicVolumeInfo.text = musicVolume.value.ToString();
            FindAnyObjectByType<Load_Settings>().Update_MusicVolume(musicVolume.value);
        }

        public void Set_TargetFPS()
        {
            PlayerPrefs.SetInt("targetFPS", targetFPS.value);

            if (FindAnyObjectByType<Load_Settings>())
            {
                FindAnyObjectByType<Load_Settings>().Set_TargetFPS();
            }
        }

        public void Set_DynamicCamera()
        {
            PlayerPrefs.SetInt("Dynamic Camera", dynamicCamera.value);
        }

        public void Disable_Object(GameObject target)
        {
            target.SetActive(false);
        }
    }
}