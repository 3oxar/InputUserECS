using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class InputPlayer : MonoBehaviour
{
    public float2 Move;
    public float Jerk;
}

class InputPlayerBaker : Baker<InputPlayer>
{
    public override void Bake(InputPlayer authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new InputPlayerComponent 
        { 
            Move = authoring.Move,
            Jerk = authoring.Jerk
        });
    }
}
