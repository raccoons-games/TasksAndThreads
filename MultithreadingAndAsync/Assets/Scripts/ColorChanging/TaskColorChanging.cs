using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ColorChanging
{
    public class TaskColorChanging : BaseColorChanging
    {
        private CancellationTokenSource _cancellationTokenSource;
        private void Start()
        {
            Changing();
        }

        public async void Changing()
        {
            foreach (Material material in this)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                try
                {
                    await SetMaterialAsyncAndDelay(material);
                }
                catch (TaskCanceledException ex)
                {
                    break;
                }
            }
        }

        private async Task SetMaterialAsyncAndDelay(Material material)
        {
            SetMaterial(material);
            Task waitingTask = Task.Delay(1000);
            await waitingTask;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel(false);
        }
    }
}