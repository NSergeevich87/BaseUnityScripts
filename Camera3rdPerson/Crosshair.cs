using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public float currnetSpread;
    public float speedSpread;

    public Parts[] parts;
    public CharacterMovement characterMovement;

    float t;
    float curSpread;
    void Update()
    {
        if (characterMovement.moveAmount > 0)
        {
            currnetSpread = 20 * (5 + characterMovement.moveAmount);
        }
        else
        {
            currnetSpread = 20;
        }

        CrosshairUpdate();
    }

    public void CrosshairUpdate()
    {
        //в этом методе буду смещать прицел
        t = 0.005f * speedSpread;
        curSpread = Mathf.Lerp(curSpread, currnetSpread, t);

        for (int i = 0; i < parts.Length; i++)
        {
            Parts p = parts[i];
            p.trans.anchoredPosition = p.pos * curSpread;
        }
    }

    [System.Serializable]
    public class Parts
    {
        public RectTransform trans; //здесь находится каждая часть картинки прицела (4 части)
        public Vector2 pos; //позиция куда будут смещаться
    }
}