//______________________________________________
// ALIyerEdon
// https://assetstore.unity.com/publishers/23606
//______________________________________________

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ALIyerEdon
{
    public class Nitro : MonoBehaviour
    {
        public Image nitroSliderPC;

        public GameObject PcUI;


        Rigidbody carRigidbody;
        EasyCarController carController;
        Nitro_Feature nitroController;
        InputSystem inputSystem;

        bool nitroState = false;
        float mass = 0;



        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            // Disable nitro UI if the player car has no nitro feature component
            if (!FindAnyObjectByType<Nitro_Feature>().enableNitro)
            {
                PcUI.SetActive(false);
            }
            else
            {
                StartCoroutine(Init_Nitro());
            }
        }

        IEnumerator Init_Nitro()
        {
            yield return new WaitForEndOfFrame();

            carController = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<EasyCarController>();

            carRigidbody = carController.GetComponent<Rigidbody>();
            mass = carRigidbody.mass;

            nitroController = carController.GetComponent<Nitro_Feature>();

            inputSystem = FindAnyObjectByType<InputSystem>();

            PcUI.SetActive(true);
        }

        void Update()
        {
            if (!carController || !nitroController.raceIsStarted)
                return;

            nitroState =
                nitroController.Amount > 0 &&
                (
                    (Gamepad.current?.buttonSouth.ReadValue() > 0) ||
                    (Keyboard.current?.leftShiftKey.ReadValue() > 0)
                );

            if (!nitroState && nitroController.Amount < 100)
            {
                nitroController.Amount += (nitroController.increaseRate * Time.deltaTime);

                if (nitroController.nitroSource.isPlaying)
                    nitroController.nitroSource.Stop();

                carController.nitro_Mode = false;

                carRigidbody.mass = mass;

                for (int a = 0; a < nitroController.nitroParticles.Length; a++)
                {
                    var emi = nitroController.nitroParticles[a].GetComponent<ParticleSystem>().emission;
                    emi.enabled = false;
                }
            }
            if (nitroState && nitroController.Amount > 0)
            {
                nitroController.Amount -= (nitroController.reduceRate * Time.deltaTime);

                // Reduce mass of the car at nitro mode to move faster !!!
                if (nitroController.nitroBoost == NitroBoostPower.X1)
                    carRigidbody.mass = mass / 2;
                if (nitroController.nitroBoost == NitroBoostPower.X2)
                    carRigidbody.mass = mass / 3;
                if (nitroController.nitroBoost == NitroBoostPower.X3)
                    carRigidbody.mass = mass / 4;

                if (!nitroController.nitroSource.isPlaying)
                    nitroController.nitroSource.Play();

                carController.nitro_Mode = true;

                for (int a = 0; a < nitroController.nitroParticles.Length; a++)
                {
                    var emi = nitroController.nitroParticles[a].GetComponent<ParticleSystem>().emission;
                    emi.enabled = true;
                }
            }
            if (nitroState && nitroController.Amount < 0)
            {
                if (nitroController.nitroSource.isPlaying)
                    nitroController.nitroSource.Stop();

                carController.nitro_Mode = false;

                carRigidbody.mass = mass;

                for (int a = 0; a < nitroController.nitroParticles.Length; a++)
                {
                    var emi = nitroController.nitroParticles[a].GetComponent<ParticleSystem>().emission;
                    emi.enabled = false;
                }
            }

            nitroSliderPC.fillAmount = nitroController.Amount / 100;
        }
    }
}