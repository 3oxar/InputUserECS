using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

partial struct JerkDogSystem : ISystem
{

    private float _timeJerk;
    private bool _isjerk;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _isjerk = false;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, inputPlayer) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<InputPlayerComponent>>())
        {
            if (inputPlayer.ValueRW.Jerk != 0 && _isjerk == false)
            {
                _isjerk = true;//���������� �����
                _timeJerk = 0.2f;//������������� ����� �����
            }

            switch (_isjerk)
            {
                case true:
                    transform.ValueRW.Position = transform.ValueRW.TransformPoint(new float3(inputPlayer.ValueRW.Move.x * 5f * SystemAPI.Time.DeltaTime, 0, inputPlayer.ValueRW.Move.y * 5f * SystemAPI.Time.DeltaTime) * -1);
                    _timeJerk -= SystemAPI.Time.DeltaTime;
                    if (_timeJerk <= 0)//����� ����� �����������
                    {
                        _isjerk = false;//��������� �����
                    }
                    break;
            }
        }
    }
    
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
