using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RedOrBluePopup : MonoBehaviour
    {
        [SerializeField]
        private Material red;
        [SerializeField]
        private Material blue;

        [SerializeField]
        private Button buttonRed;

        [SerializeField]
        private Button buttonBlue;

        private TaskCompletionSource<Material> taskCompletionSource;
        private void Start()
        {
            buttonRed.onClick.AddListener(SelectRed);
            buttonBlue.onClick.AddListener(SelectBlue);
        }
        void SelectRed()
        {
            taskCompletionSource?.SetResult(red);
            Hide();
        }

        void SelectBlue()
        {
            taskCompletionSource?.SetResult(blue);
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public Task<Material> Show()
        {
            gameObject.SetActive(true);
            taskCompletionSource = new TaskCompletionSource<Material>();
            return taskCompletionSource.Task;
        }
    }
}