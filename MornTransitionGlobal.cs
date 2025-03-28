using MornGlobal;
using UnityEngine;

namespace MornTransition
{
    [CreateAssetMenu(menuName = "Morn/" + nameof(MornTransitionGlobal), fileName = nameof(MornTransitionGlobal))]
    internal sealed class MornTransitionGlobal : MornGlobalBase<MornTransitionGlobal>
    {
        protected override string ModuleName => nameof(MornTransitionAnimation);
        [SerializeField] private string[] _transitionNames;
        public string[] TransitionNames => _transitionNames;
    }
}