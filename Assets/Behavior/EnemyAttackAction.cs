using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Enemy Attack", story: "Attack with [attack]", category: "Action", id: "c9a024d5fd9a4d451ec1458ebf781a76")]
public partial class EnemyAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAttack> Attack;

    protected override Status OnStart()
    {
        Attack.Value.Attack();
        return Status.Success;
    }

}

