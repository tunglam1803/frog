using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Animator amt;

    void Start()
    {
        amt.SetBool("is_out", true);
        StartCoroutine(TransitionToCheckpoint());
    }

    private IEnumerator TransitionToCheckpoint()
    {
        yield return new WaitForSeconds(1f);
        amt.SetBool("is_out", false);
        amt.SetBool("is_checked", true);
    }
}
