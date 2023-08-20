using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public CharacterStatus characterStatus;

    public bool debugAiming;
    public bool isAming;

    public void InputUpdate ()
    {
        if (!debugAiming)
        {
            characterStatus.isAiming = Input.GetMouseButton(1);
        }
        else characterStatus.isAiming = isAming;
    }
}
