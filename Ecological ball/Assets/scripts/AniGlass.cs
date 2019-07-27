using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniGlass : CreatureAnimation
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

    }
    public override void Idle()
    {

    }
    public override void Death()
    {

    }
    public override void Eating()
    {

    }
    public override void Withered()
    {
        GetComponent<MeshRenderer>().material.SetColor("_ColorTint", Color.black);
    }
}
