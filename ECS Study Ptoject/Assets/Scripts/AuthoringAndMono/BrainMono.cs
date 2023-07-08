
using Unity.Entities;
using UnityEngine;

public class BrainMono : MonoBehaviour
{
    public float BrainHealth;
}
public class BrainBaker : Baker<BrainMono>
{
    public override void Bake(BrainMono authoring)
    {
        var brainEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent<BrainTag>(brainEntity);
        AddComponent(brainEntity, new BrainHealth
        {
            Value = authoring.BrainHealth,
            Max = authoring.BrainHealth
        });
        AddBuffer<BrainDamageBufferElement>(brainEntity);
    }
}
