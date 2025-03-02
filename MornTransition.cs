using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MornTransition
{
    public sealed class MornTransition : MonoBehaviour
    {
        [SerializeField] private MornTransitionType _type;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _fillClip;
        [SerializeField] private AnimationClip _clearClip;
        public MornTransitionType Type => _type;

        public async UniTask FillAsync(CancellationToken ct = default)
        {
            _animator.CrossFade(_fillClip.name, 0);
            await UniTask.Delay(TimeSpan.FromSeconds(_fillClip.length), cancellationToken: ct);
        }

        public async UniTask ClearAsync(CancellationToken ct = default)
        {
            _animator.CrossFade(_clearClip.name, 0);
            await UniTask.Delay(TimeSpan.FromSeconds(_clearClip.length), cancellationToken: ct);
        }

        public void FillImmediate()
        {
            _animator.Play(_fillClip.name, 0, 1);
        }

        public void ClearImmediate()
        {
            _animator.Play(_clearClip.name, 0, 1);
        }
    }
}