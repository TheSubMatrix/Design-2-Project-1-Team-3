using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Has Members", story: "[List] has Members", category: "Conditions", id: "3d0fa5a000ffe2c7b5f6d99782816c76")]
public partial class HasMembersCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> List;

    public override bool IsTrue()
    {
        return List.Value.Count > 0;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
