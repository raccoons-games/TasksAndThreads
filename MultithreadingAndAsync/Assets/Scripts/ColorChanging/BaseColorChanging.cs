using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorChanging
{

    public class BaseColorChanging : MonoBehaviour, IEnumerable<Material>
    {
        [SerializeField]
        protected Material material1;
        [SerializeField]
        protected Material material2;


        public IEnumerator<Material> GetEnumerator()
        {
            while (true)
            {
                yield return material1;
                yield return material2;
            }
        }

        public void SetMaterial(Material material)
        {
            GetComponent<Renderer>().material = material;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}