using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterMovement characterMovement;
    public CharacterStatus characterStatus;
    public void AnimationUpdate()
    {
        animator.SetBool("Sprint", characterStatus.isSprint);
        animator.SetBool("Aiming", characterStatus.isAiming);

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
            float v = characterMovement.vertical;
            float h = characterMovement.horizontal;

            animator.SetFloat("Vertical", v, 0.15f, Time.deltaTime);
            animator.SetFloat("Horizontal", h, 0.15f, Time.deltaTime);
        }
    }
}
