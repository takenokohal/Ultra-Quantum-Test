using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.UltraQuantumTest
{
    [Preserve]
    public unsafe class BulletSystem : SystemMainThreadFilter<BulletSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Bullet* Bullet;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            filter.Bullet->LifeTime += f.DeltaTime;
            if (filter.Bullet->LifeTime <= 5)
                return;
            
            f.Destroy(filter.Entity);
        }
    }
}