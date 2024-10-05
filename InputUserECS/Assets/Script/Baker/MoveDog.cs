using Unity.Entities;
using UnityEngine;


class MoveDog : MonoBehaviour
{
    public float MoveSpeed;

}

class MoveDogBaker : Baker<MoveDog>
{
    public override void Bake(MoveDog authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new MoveDogComponent
        {
            MoveSpeed = authoring.MoveSpeed
        });

      
    }
}
