using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTree : CreatureAnimation
{
    public Mesh witheredTree = null;
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
        Transform[] father = GetComponentsInChildren<Transform>();
        if (father.Length > 1)
        {
            Destroy(father[1].gameObject);
        }
        GetComponent<MeshFilter>().mesh = witheredTree;
    }
}
