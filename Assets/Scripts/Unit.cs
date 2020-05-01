using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Unit : MonoBehaviour
    {
        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}