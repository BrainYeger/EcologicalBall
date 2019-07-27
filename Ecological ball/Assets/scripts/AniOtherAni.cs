using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniOtherAni : CreatureAnimation
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Walk()
    {
        GetComponent<Animator>().SetBool("isEating", false);
        GetComponent<Animator>().SetBool("isWalking", true);
    }
    public override void Idle()
    {
        GetComponent<Animator>().SetBool("isEating", false);
        GetComponent<Animator>().SetBool("isWalking", false);
    }
    public override void Death()
    {
        GetComponent<Animator>().SetBool("isDead", true);
    }
    public override void Eating()
    {
        GetComponent<Animator>().SetBool("isWalking", false);
        GetComponent<Animator>().SetBool("isEating", true);
    }
    public override void Withered()
    {

    }

}
