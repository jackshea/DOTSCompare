using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTSCompare.ECS
{
    // By default, the Burst Compiler is enabled. Unity's Burst Compiler
    // is built on top of the LLVM compiler, which is optimized for your
    // hardware.  This allows you continue using C# code but closer to the
    // performance of C++.  To the end-user, it's nearly invisible.  


    public partial class MovementSystemJobsBurst : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            float upperBounds = GameManager.Instance.UpperBounds;
            float lowerBounds = GameManager.Instance.BottomBounds;

            Entities.
                ForEach((ref Translation trans, in MoveForward moveForward) =>
                {
                    trans.Value += new float3(0f, 0f, moveForward.speed * deltaTime);
                    if (trans.Value.z >= upperBounds)
                    {
                        trans.Value.z = lowerBounds;
                    }
                }).Schedule();
        }
    }
}
