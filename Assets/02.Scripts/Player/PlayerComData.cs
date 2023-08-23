using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComData : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void AnimatorSetBool(string name , bool value)
    {
        animator.SetBool(name, value);
    }

    public void AnimatorSetTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public Transform GetAvataPos(HumanBodyBones pos)
    {
        return animator.GetBoneTransform(pos);
    }

}
