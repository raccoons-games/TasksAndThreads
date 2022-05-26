using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawn
{
    public class RadiusSpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject spawned;

        [SerializeField]
        private int count = 100;

        [SerializeField]
        private float radius = 20;

        [SerializeField]
        private Transform parent;
        private void Start()
        {
            for (int i = 0; i < count; i++)
            {
                Instantiate(spawned, Random.insideUnitSphere * radius, Quaternion.identity, parent);
            }
        }
    }
}