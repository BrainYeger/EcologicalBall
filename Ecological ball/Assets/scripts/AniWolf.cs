using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class AniWolf : CreatureAnimation
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
        GetComponent<Animal>().Speed = 1.0f;
        GetComponent<Animator>().SetBool("Attack1", false);
        //GetComponent<Animal>().walkSpeed = new Speeds(0.5f, 0.5f, 0.5f);
        GetComponent<Animal>().Move(transform.forward);
    }
    public override void Idle()
    {
        GetComponent<Animal>().StopAllCoroutines();
    }
    public override void Death()
    {
        GetComponent<Animator>().SetTrigger("Death");
    }
    public override void Eating()
    {
        GetComponent<Animator>().SetFloat("Vertical", 0.0f);
        GetComponent<Animator>().SetBool("Attack1", true);
        GetComponent<Animal>().Move(new Vector3(0, 0, 0));
    }
    public override void Withered()
    {
        base.Withered();
    }
}
