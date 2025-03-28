using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MornTransition
{
    public abstract class MornTransitionBase : MonoBehaviour
    {
        [SerializeField] private MornTransitionType _type;
        public MornTransitionType Type => _type;
        public abstract UniTask FillAsync(CancellationToken ct = default);
        public abstract UniTask ClearAsync(CancellationToken ct = default);
        public abstract void FillImmediate();
        public abstract void ClearImmediate();
    }
}