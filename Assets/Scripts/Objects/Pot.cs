using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator potAnimator;
    private IEnumerator coroutine;

    private IEnumerator BreakPotCo(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }

    public void BreakPot()
    {
        potAnimator.SetBool("isBroken", true);
        coroutine = BreakPotCo(0.3f);
        StartCoroutine(coroutine);
    }


	// Use this for initialization
	void Start ()
    {
        potAnimator = GetComponent<Animator>();
	}
}
