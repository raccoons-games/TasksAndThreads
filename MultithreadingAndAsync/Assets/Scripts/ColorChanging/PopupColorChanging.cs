using UI;
using UnityEngine;

namespace ColorChanging
{
    public class PopupColorChanging : BaseColorChanging
    {
        [SerializeField]
        private RedOrBluePopup redOrBluePopup;

        public async void SelectColor()
        {
            Material result = await redOrBluePopup.Show();
            SetMaterial(result);
        }

      
    }
}