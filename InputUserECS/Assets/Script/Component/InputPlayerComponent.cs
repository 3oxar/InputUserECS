using Unity.Entities;
using Unity.Mathematics;

public struct InputPlayerComponent : IComponentData
{
    public float2 Move;
    public float Jerk;
}
