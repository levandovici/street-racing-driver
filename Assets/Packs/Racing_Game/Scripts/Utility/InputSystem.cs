//______________________________________________
// ALIyerEdon
// https://assetstore.unity.com/publishers/23606
//______________________________________________

using UnityEngine;
using System.Collections;
using ALIyerEdon;
using UnityEngine.InputSystem;

namespace ALIyerEdon
{
	public class InputSystem : MonoBehaviour
	{

		[HideInInspector] public bool canControl = false;


		EasyCarController controller;


		float motorInput, steerInput;
		bool handBrake;

		// Accelerometer controlling
		[Header("Accelerometer")]
		public float accelSensibility = 10f;
		public float accelSmooth = 0.5f;
		Vector3 curAc;
		bool accelInput;

		IEnumerator Start()
		{
			if (PlayerPrefs.GetFloat("accelSensibility") == 0)
				PlayerPrefs.SetFloat("accelSensibility", 10f);

			accelInput = true;



			accelSensibility = PlayerPrefs.GetFloat("accelSensibility");

			yield return new WaitForEndOfFrame();

			controller = GameObject.FindGameObjectWithTag("Player")
				.GetComponent<EasyCarController>();

			GameObject.FindGameObjectWithTag("Player")
				.GetComponent<Car_AI>().enabled = false;
		}

		void Update()
		{

			if (!controller || !canControl)
				return;

			if (accelInput)
			{
				// Controll steering (mobile)	
				// 		
				if (Accelerometer.current != null)
				{
					if (Accelerometer.current.acceleration.ReadValue().x > 0.2f || Accelerometer.current.acceleration.ReadValue().x < -0.2f)
					{
						steerInput = Accelerometer.current.acceleration.ReadValue().x * Time.deltaTime * accelSensibility;
					}
					else
					{
						steerInput = 0;
					}
				}
			}

            #region Throttle
            float gamepadThrottle = 0f;
            float keyboardThrottle = 0f;

            if (Gamepad.current != null)
            {
                gamepadThrottle =
                    Gamepad.current.rightTrigger.ReadValue() -
                    Gamepad.current.leftTrigger.ReadValue();
            }

            if (Keyboard.current != null)
            {
                keyboardThrottle =
                    Keyboard.current.wKey.ReadValue() -
                    Keyboard.current.sKey.ReadValue();
            }

            // Use whichever input has the greater magnitude
            motorInput = Mathf.Abs(gamepadThrottle) > Mathf.Abs(keyboardThrottle)
                ? gamepadThrottle
                : keyboardThrottle;
            #endregion

            #region Steer
            float gamepadSteer = 0f;
            float keyboardSteer = 0f;

            if (Gamepad.current != null)
            {
                gamepadSteer = Gamepad.current.leftStick.ReadValue().x;
            }

            if (Keyboard.current != null)
            {
                keyboardSteer =
                    -Keyboard.current.aKey.ReadValue() +
                     Keyboard.current.dKey.ReadValue();
            }

            // Use whichever input has the greater magnitude
            steerInput = Mathf.Abs(gamepadSteer) > Mathf.Abs(keyboardSteer)
                ? gamepadSteer
                : keyboardSteer;
            #endregion

            #region Handbrake
            bool gamepadHandbrake =
                Gamepad.current != null &&
                Gamepad.current.buttonEast.ReadValue() > 0;

            bool keyboardHandbrake =
                Keyboard.current != null &&
                Keyboard.current.spaceKey.ReadValue() > 0;

            handBrake = gamepadHandbrake || keyboardHandbrake;
            #endregion

            #region Camera Switch
            if (Gamepad.current != null)
			{
				if (Gamepad.current.buttonNorth.wasPressedThisFrame)
					FindAnyObjectByType<CameraSwitch>().NextCamera();
			}
			if (Keyboard.current != null)
			{
				if (Keyboard.current.cKey.wasPressedThisFrame)
					FindAnyObjectByType<CameraSwitch>().NextCamera();
			}
			#endregion

			#region Pause
			if (Gamepad.current != null)
			{
				if (Gamepad.current.startButton.wasPressedThisFrame)
					FindAnyObjectByType<Pause_Menu>().Pause();
			}
			if (Keyboard.current != null)
			{
				if (Keyboard.current.escapeKey.wasPressedThisFrame)
					FindAnyObjectByType<Pause_Menu>().Pause();
			}
			#endregion

			controller.Move(motorInput, steerInput, handBrake);
		}

		public void Hand_Brake(bool state)
		{
			handBrake = state;
		}

		public void LoadLevel(string name)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		}

		public void Pause_Game()
		{
			FindAnyObjectByType<Pause_Menu>().Pause();
		}

		public void Switch_Camera()
		{
			FindAnyObjectByType<CameraSwitch>().NextCamera();
		}
	}
}