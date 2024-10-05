using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

partial struct MoveDogSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, inputPlayer) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<InputPlayerComponent>>())
        {
            transform.ValueRW.Position = transform.ValueRO.TransformPoint(new float3(inputPlayer.ValueRO.Move.x * SystemAPI.Time.DeltaTime, 0, inputPlayer.ValueRO.Move.y * SystemAPI.Time.DeltaTime) * -1);
        }
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }
}
