using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MornTransition
{
    internal sealed class MornTransitionMaterial : MornTransitionBase
    {
        [SerializeField] private Image _image;
        [SerializeField] private string _fillRatePropertyName;
        [SerializeField] private float _fillDuration;
        [SerializeField] private float _clearDuration;
        private Material _material;
        private int _rate;
        private CancellationTokenSource _cts;

        private void Awake()
        {
            // 操作用にマテリアルを複製してImageに入れる
            _material = new Material(_image.materialForRendering);
            _image.material = _material;
            _rate = Shader.PropertyToID(_fillRatePropertyName);
        }

        public override async UniTask FillAsync(CancellationToken ct = default)
        {
            _cts?.Cancel();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var start = _material.GetFloat(_rate);
            await ColorTweenTask(start, 1, _fillDuration * (1 - start), _cts.Token);
        }

        public override async UniTask ClearAsync(CancellationToken ct = default)
        {
            _cts?.Cancel();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var start = _material.GetFloat(_rate);
            await ColorTweenTask(start, 0, _clearDuration * start, _cts.Token);
        }

        public override void FillImmediate()
        {
            _cts?.Cancel();
            _cts = null;
            _material.SetFloat(_rate, 1);
        }

        public override void ClearImmediate()
        {
            _cts?.Cancel();
            _cts = null;
            _material.SetFloat(_rate, 0);
        }

        private async UniTask ColorTweenTask(float start, float end, float duration, CancellationToken ct = default)
        {
            if (duration > 0)
            {
                var elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    _material.SetFloat(_rate, Mathf.Lerp(start, end, elapsedTime / duration));
                    await UniTask.Yield(ct);
                }
            }

            _material.SetFloat(_rate, end);
        }
    }
}