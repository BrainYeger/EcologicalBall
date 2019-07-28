using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : Individual
{
    public double SpeedTick;
    public double eyeSight;

    private double HungryBordenRate = 0.5;

    protected override void Reproduct()
    {

    }

    private void Move(Vector3 t)
    {
        transform.position += t;
        NowBlock = GameManager.Instance.FindBlock(transform.position);
    }

    public void Prey()
    {
        //List<FoodChain> AllPredator = new List<FoodChain>();
        //List<FoodChain> AllPrey = new List<FoodChain>();
        FoodChain s = GameManager.Instance.foodchain.Query(Special);
        foreach(Consumer t in GameManager.Instance.AllConsumer)
        {
            //bool IsGet = false;
            foreach(FoodChain x in s.Predator)
            {
                if(x.type == t.Special)
                {
                    //if(Mathf.Abs(t.transform.position.x - transform.position.x) + Mathf.Abs(t.transform.position.z - transform.position.z) <= eyeSight)
                    //{
                    //IsGet = true;
                    Vector3 direction = new Vector3(t.transform.position.x - transform.position.x, 0, t.transform.position.z - transform.position.z);
                    Move(direction.normalized * (float)SpeedTick);
                    return;
                    //}
                }
                //if (!IsGet) return;
            }
        }

        if (HungryBordenRate * MaxOrganicMatter >= NowOrganicMatter)
        {
            foreach (Producer t in GameManager.Instance.AllProducer)
            {
                foreach (FoodChain x in s.Prey)
                {
                    if (x.type == t.Special)
                    {
                        //if (Mathf.Abs(t.transform.position.x - transform.position.x) + Mathf.Abs(t.transform.position.z - transform.position.z) <= eyeSight)
                        //{
                        //IsGet = true;
                        Vector3 direction = new Vector3(t.transform.position.x - transform.position.x, 0, t.transform.position.z - transform.position.z);
                        Move(direction.normalized * (float)SpeedTick);
                        return;
                        //}
                    }
                }
            }
            foreach (Consumer t in GameManager.Instance.AllConsumer)
            {
                foreach (FoodChain x in s.Prey)
                {
                    if (x.type == t.Special)
                    {
                        //if (Mathf.Abs(t.transform.position.x - transform.position.x) + Mathf.Abs(t.transform.position.z - transform.position.z) <= eyeSight)
                        //{
                        //IsGet = true;
                        Vector3 direction = new Vector3(transform.position.x - t.transform.position.x, 0, transform.position.z - t.transform.position.z);
                        Move(direction.normalized * (float)SpeedTick);
                        return;
                        //}
                    }
                }
            }
        }

        //go anywhere

        /*int x1 = Random.Range(0, 100);
        int z1 = Random.Range(0, 100);
        Vector3 tt = new Vector3(x1, 0, z1);
        Move(tt.normalized * (float)SpeedTick);*/

    }

    // Start is called before the first frame update
    void Start()
    {
        NowOrganicMatter = MaxOrganicMatter;
        MinOrganicMatter = MinOrganicMatterRate * MaxOrganicMatter;
        OxygenComsuptionTick = OrganicMatterComsuptionTick * OrganicMatterComsuptionToOxygenComsuptionRate;
        BreatheCarbonDioxideGenerationTick = OxygenComsuptionTick * OxygenComsuptionToCarbonDioxideComsuptionRate;
        BreatheH20GenerationTick = OxygenComsuptionTick * OxygenComsuptionToH2OComsuptionRate;
        GameManager.Instance.AllConsumer.Add(this);
        NowBlock = GameManager.Instance.FindBlock(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
