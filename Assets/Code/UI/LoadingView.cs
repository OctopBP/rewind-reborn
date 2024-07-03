using UnityEngine;
using PrimeTween;

namespace Code.UI
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Transform gear;
        [SerializeField] private TweenSettings<Vector3> gearRotationSettings;
        [SerializeField] private Canvas canvas;

        private Tween rotationTween;

        private void OnEnable()
        {
            Show();
        }

        public void Show()
        {
            rotationTween = Tween.LocalEulerAngles(gear, gearRotationSettings);
        }
    }
}
