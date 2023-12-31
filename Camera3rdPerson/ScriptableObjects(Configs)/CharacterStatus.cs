// Не забываем создать status в тойже папке Unity (Create -> Character -> status)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/status")]
public class CharacterStatus : ScriptableObject
{
    public bool isAiming;
    public bool isAimingMove;
    public bool isSprint;
    public bool isGround;
    public bool isJump;
}