using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MornTransition
{
    internal sealed class MornTransitionFade : MornTransitionBase
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fillDuration;
        [SerializeField] private float _clearDuration;

        public override async UniTask FillAsync(CancellationToken ct = default)
        {
            var elapsed = 0f;
            while (elapsed < _fillDuration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / _fillDuration);
                SetAlpha(t);
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            SetAlpha(1f);
        }

        public override async UniTask ClearAsync(CancellationToken ct = default)
        {
            var elapsed = 0f;
            while (elapsed < _clearDuration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / _clearDuration);
                SetAlpha(1f - t);
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            SetAlpha(0f);
        }

        public override void FillImmediate()
        {
            SetAlpha(1f);
        }

        public override void ClearImmediate()
        {
            SetAlpha(0f);
        }

        private void SetAlpha(float alpha)
        {
            var color = _image.color;
            color.a = alpha;
            _image.color = color;
        }
    }
}