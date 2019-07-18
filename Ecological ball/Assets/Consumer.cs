using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : Individual
{
    public double SpeedTick;
    public double eyeSight;

    private double HungryBordenRate;

    protected override void Reproduct()
    {

    }

    public void Prey()
    {
        List<FoodChain> AllPredator = new List<FoodChain>();
        List<FoodChain> AllPrey = new List<FoodChain>();
        GameManager.Instance.foodchain.Query(Special, AllPredator, AllPrey);
        foreach(Consumer t in GameManager.Instance.AllConsumer)
        {
            //bool IsGet = false;
            foreach(FoodChain x in AllPredator)
            {
                if(x.type == t.Special)
                {
                    if(Mathf.Abs(t.transform.position.x - transform.position.x) + Mathf.Abs(t.transform.position.z - transform.position.z) <= eyeSight)
                    {
                        //IsGet = true;
                        Vector3 direction = transform.position - t.transform.position;
                        transform.position += direction.normalized * (float)SpeedTick;
                        return;
                    }
                }
                //if (!IsGet) return;
            }
            if (HungryBordenRate * MaxOrganicMatter < NowOrganicMatter)
                return;
            foreach(FoodChain x in AllPrey)
            {
                if (x.type == t.Special)
                {
                    if (Mathf.Abs(t.transform.position.x - transform.position.x) + Mathf.Abs(t.transform.position.z - transform.position.z) <= eyeSight)
                    {
                        //IsGet = true;
                        Vector3 direction = t.transform.position - transform.position;
                        transform.position += direction.normalized * (float)SpeedTick;
                        return;
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AllConsumer.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
