using System;
using System.Linq;
using Photon.Deterministic;
using Quantum.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.UltraQuantumTest
{
    [Preserve]
    public unsafe class PlayerShootSystem : SystemMainThreadFilter<PlayerShootSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef EntityRef;
            public PlayerLink* PlayerLink;
            public UltraPlayer* UltraPlayer;
            public Transform2D* Transform2D;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            var input = f.GetPlayerInput(filter.PlayerLink->PlayerRef);

            var counter = filter.UltraPlayer->BulletCoolTimeTimeCounter;
            var config = f.FindAsset(filter.UltraPlayer->PlayerConfig);


            counter.CountUp(f);

            
            //Fire
            if (input->Fire && counter.CurrentTime >= FP._0_20)
            {
                var bullet = f.Create(config.bulletPrototype);

                if (!f.Unsafe.TryGetPointer<Transform2D>(bullet, out var transform2D))
                    return;
                transform2D->Teleport(f, CalcPos(f, filter));

                if (!f.Unsafe.TryGetPointer<PhysicsBody2D>(bullet, out var physicsBody2D))
                    return;

                physicsBody2D->Velocity = CalcDir(f, filter) * 5;
                counter.Reset();
            }
            filter.UltraPlayer->BulletCoolTimeTimeCounter = counter;

        }

        private static FPVector2 CalcPos(FrameBase f, Filter filter)
        {
            return filter.Transform2D->Position + CalcDir(f, filter);
        }

        private static FPVector2 CalcDir(FrameBase f, Filter filter)
        {
            if (!f.Unsafe.TryGetPointerSingleton<AllPlayersManager>(out var allPlayersManager))
                throw new Exception();

            if (!f.TryResolveList(allPlayersManager->AllPlayers, out var list))
                throw new Exception();

            var player = filter.EntityRef;
            var otherPlayer = list.FirstOrDefault(value => value != player);
            if (otherPlayer == default)
            {
                return FPVector2.Right;
            }

            if (!f.Unsafe.TryGetPointer<Transform2D>(otherPlayer, out var target))
                throw new Exception();

            var dir = target->Position - filter.Transform2D->Position;
            return dir.Normalized;
        }
    }
}