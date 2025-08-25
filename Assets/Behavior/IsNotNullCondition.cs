using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Is Not Null", story: "[Target] is not null", category: "Conditions", id: "ad460f4c66ef65350d850cdafe833b43")]
public partial class IsNotNullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public override bool IsTrue()
    {
        return Target != null;
    }
}
