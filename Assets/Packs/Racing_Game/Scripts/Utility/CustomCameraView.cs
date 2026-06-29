using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ALIyerEdon
{
    public class CustomCameraView : MonoBehaviour
    {
        public float distance;
        public float height;
        public float heightOffset;

        void Start()
        {
            if (FindAnyObjectByType<SmoothFollow>())
            {
                FindAnyObjectByType<SmoothFollow>().distance = distance;
                FindAnyObjectByType<SmoothFollow>().height = height;
                FindAnyObjectByType<SmoothFollow>().offset = 
                    new Vector3(0, heightOffset, 0);
            }
            if (FindAnyObjectByType<SmoothFollow2>())
            {
                FindAnyObjectByType<SmoothFollow2>().distance = distance;
                FindAnyObjectByType<SmoothFollow2>().height = height;
                FindAnyObjectByType<SmoothFollow2>().Angle = heightOffset;
            }
        }
    }
}