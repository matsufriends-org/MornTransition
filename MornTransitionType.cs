using System;
using MornEnum;

namespace MornTransition
{
    [Serializable]
    public sealed class MornTransitionType : MornEnumBase
    {
        protected override string[] Values => MornTransitionGlobal.I.TransitionNames;
    }
}