using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

partial struct JerkDogSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach(var (transform, inputPlayer) in SystemAPI.Query<RefRW<LocalTransform> ,RefRW<InputPlayerComponent>>())
        {
            if(inputPlayer.ValueRW.Jerk != 0)
            {
                transform.ValueRW.Position = transform.ValueRW.TransformPoint(new float3(inputPlayer.ValueRW.Move.x * 7f * SystemAPI.Time.DeltaTime, 0, inputPlayer.ValueRW.Move.y * 7f * SystemAPI.Time.DeltaTime) * -1);
            }
        }
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
