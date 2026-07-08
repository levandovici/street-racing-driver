//______________________________________________
// ALIyerEdon
// https://assetstore.unity.com/publishers/23606
//______________________________________________

using ALIyerEdon;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

namespace ALIyerEdon
{
    public class Load_Settings : MonoBehaviour
    {

        public Color fogColor = Color.white;

        public GameObject[] localFog;

        public Camera[] cinematicCameras;

        AudioSource music;

        IEnumerator Start()
        {

            #region Music
            music = GetComponentInChildren<AudioSource>();
            music.volume = PlayerPrefs.GetFloat("Music");
            #endregion

            yield return new WaitForEndOfFrame();

            #region Fog Smoke
            if (localFog.Length != 0)
            {
                for (int a = 0; a < localFog.Length; a++)
                {
                    var ps = localFog[a].GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.startColor = fogColor;
                }
            }
            // Update wheel smoke effects
            EasyCarAudio[] carAudio = FindObjectsByType<EasyCarAudio>();

            for (int a = 0; a < carAudio.Length; a++)
            {
                for (int b = 0; b < carAudio[a].wheelSmokes.Length; b++)
                {
                    var ps = carAudio[a].wheelSmokes[b].GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.startColor = fogColor;
                }
            }
            #endregion

        }

        public void Update_MusicVolume(float volume)
        {
            music.volume = volume;
        }

        public void Set_TargetFPS()
        {
            if (PlayerPrefs.GetInt("targetFPS") == 0)
            {
                Application.targetFrameRate = 30;
            }
            else
            {
                Application.targetFrameRate = 60;
            }
        }
    }
}