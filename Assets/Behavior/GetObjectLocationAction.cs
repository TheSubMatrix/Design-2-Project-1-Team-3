using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get Object Location", story: "Get from current [Object] its last [Position]", category: "Action/GameObject", id: "9345a4d36940e47ee4796f1bf3ef58ac")]
public partial class GetObjectLocationAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Object = new();
    [SerializeReference] public BlackboardVariable<Vector3> Position = new();
    protected override Status OnStart()
    {
        Position.Value = Object.Value.transform.position;
        return Status.Success;
    }
}

