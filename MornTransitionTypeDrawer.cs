#if UNITY_EDITOR
using MornEnum;
using UnityEditor;

namespace MornTransition
{
    [CustomPropertyDrawer(typeof(MornTransitionType))]
    public class MornTransitionTypeDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornTransitionGlobal.I.TransitionNames;
    }
}
#endif