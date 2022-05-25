using System.Collections;
using UnityEngine;

namespace ColorChanging
{
    public class CoroutineColorChanging : BaseColorChanging
    {
        private void Start()
        {
            StartCoroutine(Changing());
        }
        private IEnumerator Changing()
        {
            foreach (Material material in this)
            {
                yield return SingleChange(material);
            }
        }
        
        private IEnumerator SingleChange(Material material)
        {
            yield return new ActAndWait(()=>SetMaterial(material), 1);
        }
    }
}