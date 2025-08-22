using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get Random in GameObject List", story: "Get [random] in [list]", category: "Action/GameObject", id: "6c59486f643e2f0e55e6c2e4ca8fe585")]
public partial class GetRandomInGameObjectListAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> List;
    [SerializeReference] public BlackboardVariable<GameObject> Random;

    protected override Status OnStart()
    {
        if (List.Value.Count == 0)
        {
            return Status.Failure;
        }
        Random.Value = List.Value[UnityEngine.Random.Range(0, List.Value.Count)];
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

