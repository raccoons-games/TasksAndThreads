using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace VertexColorSpheres
{
    [System.Serializable]
    public class VertexColorSphereData
    {
        public string settings;
    }

    public class VertexColorSpheresData
    {
        public VertexColorSphereData[] sphereDatas;

        public VertexColorSpheresData(int count)
        {
            sphereDatas = new VertexColorSphereData[count];
        }
    }
    public class VertexColorSphereManager : MonoBehaviour
    {
        private float _progress = 0;
        private object _progressLock = new object();

        public float Progress
        {
            get 
            {
                lock (_progressLock)
                {
                    return _progress;
                }
            }
            set
            {
                lock (_progressLock)
                {
                    _progress = value;
                }
            }
        }

        private bool isSaving = false;


        public async Task Save(System.Action<float> progressCallback = null)
        {
            if (isSaving) return;
            isSaving = true;
            await Save(GetComponentsInChildren<VertexColorSphere>(), progressCallback);
            isSaving = false;
        }

        public Task<bool> Save(IEnumerable<VertexColorSphere> spheres, System.Action<float> progressCallback = null)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            Thread thread = new Thread(() =>
            {
                int count = spheres.Count();
                VertexColorSpheresData saveData = new VertexColorSpheresData(count);
                int id = 0;
                foreach (VertexColorSphere sphere in spheres)
                {
                    saveData.sphereDatas[id++] = new VertexColorSphereData() { settings = sphere.SerializeSettings() };
                    progressCallback?.Invoke((float)id / count * 0.9f);
                }
                string saveDataString = JsonUtility.ToJson(saveData);
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/VertexSpheres";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                File.WriteAllText(path + "/SavedData.json", saveDataString);
                progressCallback?.Invoke(1);
                taskCompletionSource.SetResult(true);

            });
            thread.Priority = System.Threading.ThreadPriority.AboveNormal;
            thread.Start();
            return taskCompletionSource.Task;
        }
    }
}