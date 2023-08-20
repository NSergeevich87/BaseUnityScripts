using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterAnimation characterAnimation;
    public CharacterInput characterInput;

    public void FixedUpdate()
    {
        characterMovement.MoveUpdate();
        characterAnimation.AnimationUpdate();
        characterInput.InputUpdate();
    }
}
