using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace VertexColorSpheres
{
    

    public class VertexColorSphere : MonoBehaviour
    {
        [System.Serializable]
        public class VertexColorSphereSettings
        {
            [SerializeField]
            private Vector3[] _vertices;

            [SerializeField]
            private Color[] _colors;

            public Vector3[] Vertices { get => _vertices; private set => _vertices = value; }
            public Color[] Colors { get => _colors; private set => _colors = value; }

            public VertexColorSphereSettings(MeshFilter meshFilter)
            {
                Vertices = meshFilter.mesh.vertices;
                Colors = new Color[Vertices.Length];
            }
        }

        [SerializeField]
        private MeshFilter meshFilter;

        [SerializeField]
        private bool _threading = false;

        private VertexColorSphereSettings settings;
        
        private Thread _thread;

        private float _time;

        private object _settingsLock = new object();
        private void Start()
        {
            
            _time = Time.deltaTime;
            settings = new VertexColorSphereSettings(meshFilter);
            if (_threading)
            {
                _thread = new Thread(() => ProcessTarget());
                _thread.Start();
            }
        }

        private void Update()
        {
            if (_threading) return;
            for (int verticeId = 0; verticeId < settings.Vertices.Length; verticeId++)
            {
                float brightness = Mathf.Sin(verticeId + _time) % 1;
                settings.Colors[verticeId] = new Color(brightness, brightness, brightness);
            }

            meshFilter.mesh.colors = settings.Colors;
            _time = Time.time;
        }

        public async void ProcessTarget()
        {
            while (true)
            {
                lock (_settingsLock)
                {
                    for (int verticeId = 0; verticeId < settings.Vertices.Length; verticeId++)
                    {
                        float brightness = Mathf.Abs(Mathf.Sin(_time + (Vector3.Angle(settings.Vertices[verticeId], Vector3.up) / 90)));
                        settings.Colors[verticeId] = new Color(brightness, brightness, brightness);
                        settings.Vertices[verticeId] = settings.Vertices[verticeId].normalized * (brightness + 0.5f);
                    }
                }
                await UnityMainThreadDispatcher.Instance().EnqueueAsync(() =>
                {
                    UpdateResults();
                });
            }
            
        }

        private void UpdateResults()
        {
            lock (_settingsLock)
            {
                meshFilter.mesh.colors = settings.Colors;
                meshFilter.mesh.vertices = settings.Vertices;
            }
            _time = Time.time;
        }

        public string SerializeSettings()
        {
            lock (_settingsLock)
            {
                return JsonUtility.ToJson(settings);
            }
        }
    }
}