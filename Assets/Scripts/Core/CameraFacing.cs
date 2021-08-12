using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        void LateUpdate()
        {
            FaceCamera();
        }

        private void FaceCamera()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
