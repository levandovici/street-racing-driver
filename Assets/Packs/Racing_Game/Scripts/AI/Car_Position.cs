//______________________________________________
// ALIyerEdon
// https://assetstore.unity.com/publishers/23606
//______________________________________________

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ALIyerEdon;
using TMPro;

namespace ALIyerEdon
{
    public class Car_Position : MonoBehaviour
    {
        [HideInInspector] public bool isPlayer;

        // Racer id
        [HideInInspector] public int RacerID = 0;

        [Space(7)]
        [HideInInspector] public string RacerName;
        public string inGarageName = "Ford GT";

        [HideInInspector] public int currentPosition;

        // Current lpa, checkpoint and distance to the next checkpoint
        [HideInInspector] public int currentLap, currentCheckpoint;
        [HideInInspector] public float nextCheckpointDistance;
        [HideInInspector] public bool canPassLap = true;

        // Internal variables
        [HideInInspector] public string totalPoints;
        [HideInInspector] public Transform nextCheckpoint;
        Race_Manager race_Manager;

        // Update function interval
        [Space(5)]
        public float updateInterval = 0.1f;

        void Start()
        {

            race_Manager = GameObject.FindAnyObjectByType<Race_Manager>();
            StartCoroutine(Check_Distance());

        }

        // Update is called once per frame
        void Update()
        {
            // Draw a ray to the next checkpoint with a white color
            Debug.DrawRay(transform.position, nextCheckpoint.position - transform.position, Color.white);
        }

        IEnumerator Check_Distance()
        {
            while (true)
            {
                yield return new WaitForSeconds(updateInterval);
                nextCheckpointDistance = Vector3.Distance(transform.position, nextCheckpoint.position);

                totalPoints = currentLap.ToString("00") + currentCheckpoint.ToString("000") + (100000 - nextCheckpointDistance).ToString();
                race_Manager.Update_Position(RacerID, totalPoints);
            }
        }

        public void CanPass_Lap()
        {
            StartCoroutine(CanPassLap_Delay());
        }
        IEnumerator CanPassLap_Delay()
        {
            yield return new WaitForSeconds(20f);
            canPassLap = true;
        }
    }
}