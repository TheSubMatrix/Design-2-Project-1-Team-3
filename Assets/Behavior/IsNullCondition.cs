using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsNull", story: "[Object] is null", category: "Conditions", id: "0f1c3f7d0631985d0fa14458965d993d")]
public partial class IsNullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Object;

    public override bool IsTrue()
    {
        return Object.Value == null;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
