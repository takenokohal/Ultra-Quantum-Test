using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.UltraQuantumTest
{
    [Preserve]
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded
    {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
            var data = f.GetPlayerData(player);
            var asset = f.FindAsset(data.PlayerAvatar);
            var entity = f.Create(asset);
            if (!f.Unsafe.TryGetPointer<PlayerLink>(entity, out var playerLink))
                return;
            playerLink->PlayerRef = player;

            var v = f.GetOrAddSingleton<AllPlayersManager>();

            if (!f.TryResolveList(v.AllPlayers, out var list))
                return;
            list.Add(entity);
        }
    }
}