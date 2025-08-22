using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Report Found Objects From Vision", story: "Report [objects] from [vision]", category: "Action", id: "36dc9395b9208e77bb2827cef7d33595")]
public partial class ReportFoundObjectsWithTagAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Objects;
    [SerializeReference] public BlackboardVariable<VisionSensor> Vision;
    Status m_countObjectsStatus = Status.Running;
    
    void OnSensorUpdated()
    {
        switch (Vision.Value.VisibleObjects.Count)
        {
            case 0:
                m_countObjectsStatus = Status.Running;
                break;
            default:
                m_countObjectsStatus = Status.Success;
                Objects.Value = Vision.Value.VisibleObjects;
                break;
        }
    }
    
    protected override Status OnStart()
    {
        Vision.Value.OnSightChangeUpdated.AddListener(OnSensorUpdated);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return m_countObjectsStatus;
    }

    protected override void OnEnd()
    {
        Vision.Value.OnSightChangeUpdated.RemoveListener(OnSensorUpdated);
        m_countObjectsStatus = Status.Running;
    }
}

