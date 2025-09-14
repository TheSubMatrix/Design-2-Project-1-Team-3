using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Nearest Navigation Position", story: "Find closest [position] to [reference] around [location] with maximum [distance]", category: "Action/Navigation", id: "d59a64d0877fa9a261e67aab66b22795")]
public partial class FindNearestNavigationPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Position;
    [SerializeReference] public BlackboardVariable<Vector3> Reference;
    [SerializeReference] public BlackboardVariable<Vector3> Location;
    [SerializeReference] public BlackboardVariable<float> Distance;

    protected override Status OnStart()
    {
        Vector3 closest = FindNearestValidPoint(Location.Value, Reference.Value, Distance.Value);
        if (closest == Vector3.zero) return Status.Failure;
        Position.Value = closest;
        return Status.Success;
    }
    // Call this method to find the nearest valid point around a specific location
    public Vector3 FindNearestValidPoint(Vector3 targetPosition, Vector3 referencePosition, float radius)
    {
        Vector3 closestValidPosition = targetPosition;
        float closestDistance = Mathf.Infinity;

        // Sample points in a circle within the radius
        const int samples = 36; // Number of samples around the circle (can adjust for performance)
        const float angleStep = 360f / samples;

        // Loop through each angle around the circle
        for (float angle = 0; angle < 360f; angle += angleStep)
        {
            // Calculate the offset in the x and z axes using polar coordinates
            float xOffset = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float zOffset = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            // Create the candidate position at the edge of the circle
            Vector3 candidatePoint = new Vector3(targetPosition.x + xOffset, targetPosition.y, targetPosition.z + zOffset);

            // Check if this point is valid on the NavMesh
            if (!NavMesh.SamplePosition(candidatePoint, out NavMeshHit hit, 1f, NavMesh.AllAreas)) continue;
            // Calculate the distance from the reference position to the valid NavMesh position
            float distance = Vector3.Distance(referencePosition, hit.position);
            
            
            
            // If this valid point is closer than the previous one, update
            if (!(distance < closestDistance)) continue;
            closestDistance = distance;
            closestValidPosition = hit.position;
        }

        return closestValidPosition;
    }
}

