using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    public Animator animator;
    public CharacterMovement characterMovement;
    public CharacterInventory characterInventory;
    public CharacterStatus characterStatus;
    public Transform targetLook;

    // Необходимо узнать трансформ у рук
    public Transform l_Hand;
    public Transform l_Hand_Target;
    public Transform r_Hand;

    public Quaternion lh_rot; 

    // Вес руки
    public float rh_Weight;

    // Transform чтобы можно было смотреть в направлении камеры
    public Transform shoulder;
    public Transform aimPivot;

    void Start()
    {
        //Находим плечо
        shoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).transform;
        //Теперь необходимо создать 3 пустышки
        //Для пивота
        aimPivot = new GameObject().transform;
        aimPivot.name = "aim pivot";
        aimPivot.transform.parent = transform;
        //Для левой и правой руки
        r_Hand = new GameObject().transform;
        r_Hand.name = "right hand";
        r_Hand.transform.parent = aimPivot;

        l_Hand = new GameObject().transform;
        l_Hand.name = "left hand";
        l_Hand.transform.parent = aimPivot;

        //Теперь необходимо указать позицию нашей правой руки (из инвентаря)
        r_Hand.localPosition = characterInventory.firstWeapon.rHandPos;
        //Также необходимо указать поворот правой руки (из инвентаря)
        Quaternion rotRight = Quaternion.Euler(characterInventory.firstWeapon.rHandRot.x, characterInventory.firstWeapon.rHandRot.y, characterInventory.firstWeapon.rHandRot.z);
        r_Hand.localRotation = rotRight;
    }

    
    void Update()
    {
        //Узнаем позицию и поворот для нашей левой руки, брать ее будем из пустышки в оружие
        lh_rot = l_Hand_Target.rotation;
        l_Hand.position = l_Hand_Target.position;
        //l_Hand.rotation = lh_rot;

        //необходимо менять вес для правой руки
        if (characterStatus.isAiming)
        {
            rh_Weight += Time.deltaTime * 2;
        }
        else
        {
            rh_Weight -= Time.deltaTime * 2;
        }
        //Необходимо держать вес в диапазоне от 0 до 1
        rh_Weight = Mathf.Clamp(rh_Weight, 0, 1);
    }

    void OnAnimatorIK()
    {
        aimPivot.position = shoulder.position;

        if (characterStatus.isAiming)
        {
            //чтобы персонаж смотрел вверх и вниз во время прицеливания
            aimPivot.LookAt(targetLook);
            //персонаж следит за таргет луком
            animator.SetLookAtWeight(1f, 0.3f, 1f); //указываем вес тела, общий вес и вес головы
            animator.SetLookAtPosition(targetLook.position);
            //теперь нужно сделать IK для рук
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, l_Hand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, lh_rot);
            //теперь для правой руки
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rh_Weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rh_Weight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, r_Hand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, r_Hand.rotation);
        }
        else
        {
            //персонаж следит за таргет луком
            /*animator.SetLookAtWeight(.3f,.3f,.3f); //указываем вес тела, общий вес и вес головы
            animator.SetLookAtPosition(targetLook.position);*/
            //теперь нужно сделать IK для рук
            /*animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, l_Hand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, lh_rot);*/
            //теперь для правой руки
            /*animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rh_Weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rh_Weight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, r_Hand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, r_Hand.rotation);*/
        }


    }
}