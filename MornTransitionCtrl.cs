using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MornTransition
{
    public sealed class MornTransitionCtrl : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private bool _initialized;
        [SerializeField] [ReadOnly] private List<MornTransition> _allTransitions;
        [SerializeField] [ReadOnly] private List<MornTransition> _activeTransitions;

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            _allTransitions = new List<MornTransition>(GetComponentsInChildren<MornTransition>());
            _activeTransitions = new List<MornTransition>();
        }

        private bool TryGetTransition(MornTransitionType transitionType, out MornTransition transition)
        {
            transition = _allTransitions.Find(t => t.Type == transitionType);
            return transition != null;
        }

        public async UniTask FillAsync(MornTransitionType transitionType, CancellationToken ct = default)
        {
            Initialize();
            if (TryGetTransition(transitionType, out var transition))
            {
                _activeTransitions.Add(transition);
                await transition.FillAsync(ct);
            }
        }

        public async UniTask ClearAsync(CancellationToken ct = default)
        {
            Initialize();
            var taskList = new List<UniTask>();
            foreach (var transition in _activeTransitions)
            {
                taskList.Add(transition.ClearAsync(ct));
            }

            _activeTransitions.Clear();
            await UniTask.WhenAll(taskList);
        }

        public void FillImmediate(MornTransitionType transitionType)
        {
            Initialize();
            if (TryGetTransition(transitionType, out var transition))
            {
                _activeTransitions.Add(transition);
                transition.FillImmediate();
            }
        }

        public void ClearImmediate()
        {
            Initialize();
            foreach (var transition in _activeTransitions)
            {
                transition.ClearImmediate();
            }

            _activeTransitions.Clear();
        }
    }
}