using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = Quantum.Input;

namespace QuantumUser.View.UltraQuantumTest
{
    public class InputProvider : MonoBehaviour
    {
        private void OnEnable()
        {
            QuantumCallback.Subscribe<CallbackPollInput>(this, callback =>
            {
                var input = new Input
                {
                    Right = Keyboard.current.dKey.isPressed,
                    Left = Keyboard.current.aKey.isPressed,
                    Up = Keyboard.current.wKey.isPressed,
                    Down = Keyboard.current.sKey.isPressed,
                    
                    Fire = Keyboard.current.enterKey.isPressed
                };

                callback.SetInput(input, DeterministicInputFlags.Repeatable);
            });
        }
    }
}