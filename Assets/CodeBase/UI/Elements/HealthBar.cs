using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _imageHeart;

        public void SetValue(float current, float max) =>
            _imageHeart.fillAmount = current / max;
    }
}