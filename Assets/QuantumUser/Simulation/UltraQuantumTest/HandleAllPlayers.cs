using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.UltraQuantumTest
{
    [Preserve]
    public unsafe class HandleAllPlayers : SystemSignalsOnly,
        ISignalOnComponentAdded<AllPlayersManager>,
        ISignalOnComponentRemoved<AllPlayersManager>
    {
        public void OnAdded(Frame f, EntityRef entity, AllPlayersManager* component)
        {
            component->AllPlayers = f.AllocateList<EntityRef>();
        }

        public void OnRemoved(Frame f, EntityRef entity, AllPlayersManager* component)
        {
            f.FreeList(component->AllPlayers);
            component->AllPlayers = default;
        }
    }
}