using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum
{
    public partial struct TimeCounter
    {
        public void CountUp(Frame frame)
        {
            var deltaTime = frame.DeltaTime;
            CurrentTime += deltaTime;
        }

        public void Reset()
        {
            CurrentTime = FP._0;
        }
    }
}