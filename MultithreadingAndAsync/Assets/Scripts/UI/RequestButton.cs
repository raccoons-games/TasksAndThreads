using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UI
{
    public class RequestButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        private void Start()
        {
            button.onClick.AddListener(async () =>
            {
                UnityWebRequest request = UnityWebRequest.Get("google.com");
                await request.SendWebRequest();
                print(request.responseCode);
            });
        }
    }
}