using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AuraAPI
{
    public class ForceVolumetricLighting : MonoBehaviour
    {
        public MonoBehaviour auraLightScript;

        void Start()
        {
            auraLightScript.enabled = true;
        }
    }
}
