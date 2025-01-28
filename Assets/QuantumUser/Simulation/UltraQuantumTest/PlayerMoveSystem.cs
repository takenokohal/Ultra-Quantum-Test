using System.Collections.Generic;
using System.Linq;
using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.UltraQuantumTest
{
    [Preserve]
    public unsafe class PlayerMoveSystem : SystemMainThreadFilter<PlayerMoveSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef EntityRef;
            public PlayerLink* PlayerLink;
            public PhysicsBody2D* PhysicsBody2D;
            public UltraPlayer* UltraPlayer;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            var config = f.FindAsset(filter.UltraPlayer->PlayerConfig);
            var input = f.GetPlayerInput(filter.PlayerLink->PlayerRef);

            //Move
            var v = new FPVector2();
            if (input->Right)
                v.X = 1;
            if (input->Left)
                v.X = -1;
            if (input->Up)
                v.Y = 1;
            if (input->Down)
                v.Y = -1;


            filter.PhysicsBody2D->Velocity = v * config.playerSpeed;

        }

    }
}