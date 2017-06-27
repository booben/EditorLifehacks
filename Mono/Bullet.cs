using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName
{
    [AddComponentMenu("CompanyName/Shells/Bullet")]
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        [IntAttribute("EffectsContainer")]
        private int effectId = -1;
    }
}