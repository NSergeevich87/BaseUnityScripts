using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterMovement characterMovement;
    public CharacterStatus characterStatus;
    public void AnimationUpdate()
    {
        animator.SetBool("AimingMove", characterStatus.isAimingMove);
        animator.SetBool("Sprint", characterStatus.isSprint);
        animator.SetBool("Aiming", characterStatus.isAiming);
        animator.SetBool("Jump", characterStatus.isJump);

        // Если персонаж не целится то АНИМАЦИЯ передвежения может работать
        if (!characterStatus.isAiming)
        {
            AnimationNormal();
        }
        else AnimationAiming();

        void AnimationNormal()
        {
            animator.SetFloat("Vertical", characterMovement.moveAmount, 0.15f, Time.deltaTime);
        }

        void AnimationAiming()
        {
            // когда целимся нам нужно получать два направления vertical, horizontal
            float v = characterMovement.vertical;
            float h = characterMovement.horizontal;

            animator.SetFloat("Vertical", v, 0.15f, Time.deltaTime);
            animator.SetFloat("Horizontal", h, 0.15f, Time.deltaTime);
        }
    }
}