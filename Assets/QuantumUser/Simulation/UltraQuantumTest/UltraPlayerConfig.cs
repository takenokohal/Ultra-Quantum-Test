using Photon.Deterministic;

namespace Quantum
{
    public class UltraPlayerConfig : AssetObject
    {
        public FP playerSpeed;
        public FP shootCoolTime;
        public AssetRef<EntityPrototype> bulletPrototype;
    }
}