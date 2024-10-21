using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUtils : MonoBehaviour
{
    [SerializeField]
    Animator _target;

    private void Reset()
    {
        _target = GetComponent<Animator>();
    }

    public void SetFlag(string name) =>
        _target.SetBool(name, true);
    public void ClearFlag(string name) =>
        _target.SetBool(name, false);
}
