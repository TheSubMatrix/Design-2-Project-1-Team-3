using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Report Found Objects From Vision", story: "Find [target] from [vision]", category: "Action", id: "36dc9395b9208e77bb2827cef7d33595")]
public partial class ReportFoundObjectsWithTagAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<VisionSensor> Vision;

    
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Target.Value = Vision.Value.m_selectedTarget;
        return Status.Success;
    }

    protected override void OnEnd()
    {

    }
}

