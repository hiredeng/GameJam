using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorExtensions 
{
    public static void SetFlag(this Animator animator, string name) => 
        animator.SetBool(name, true);
    public static void ClearFlag(this Animator animator, string name) =>
        animator.SetBool(name, false);
}
