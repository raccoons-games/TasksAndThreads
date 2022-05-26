using System;
using UnityEngine;
using UnityEngine.UI;
using VertexColorSpheres;

namespace VertexSpheres.UI
{
    public class VertexSpheresSavePanel : MonoBehaviour
    {
        [SerializeField]
        private Button saveButton;

        [SerializeField]
        private Slider progressSlider;

        [SerializeField]
        private VertexColorSphereManager manager;


        private void Start()
        {
            saveButton.onClick.AddListener(Save);
        }

        private async void Save()
        {
            progressSlider.gameObject.SetActive(true);
            await manager.Save((progress) => UnityMainThreadDispatcher.Instance().EnqueueAsync(()=>progressSlider.value = progress));
            progressSlider.gameObject.SetActive(false);
        }
    }

}
