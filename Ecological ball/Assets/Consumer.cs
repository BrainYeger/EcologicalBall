using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : Individual
{
    public double SpeedTick;
    public double eyeSight;

    private double HungryBordenRate = 0.5;

    private int WalkTick;
    private Vector3 WalkDirection;

    private bool isEating;
    protected override void Reproduct()
    {

    }

    private void Move(Vector3 t)
    {
        EnvironmentBlock tb = GameManager.Instance.FindBlock(transform.position + t);
        if (isEating || !tb || (transform.position + t).x > 4.2 || (transform.position + t).z > 4.5 || GameManager.Instance.GetH2O(transform.position + t) > 260)  { isEating = false; GetComponent<CreatureAnimation>().Idle(); return; }
        NowBlock = tb;
        GetComponent<CreatureAnimation>().Walk();
        transform.position += t;
        transform.eulerAngles = new Vector3(0, Quaternion.FromToRotation(Vector3.forward, t).eulerAngles.y, 0);
        //transform.eulerAngles = Quaternion.FromToRotation(Vector3.forward, t).eulerAngles;
    }

    public void Walk()
    {
        if (WalkDirection == Vector3.zero)
        {
            GetComponent<CreatureAnimation>().Idle();
            return;
        }
        Move(WalkDirection * (float)SpeedTick);
    }

    public void Prey()
    {
        //List<FoodChain> AllPredator = new List<FoodChain>();
        //List<FoodChain> AllPrey = new List<FoodChain>();
        FoodChain s = GameManager.Instance.foodchain.Query(Special);
        /*foreach(Consumer t in GameManager.Instance.AllConsumer)
        {
            if (t.SuddenDeath) continue;
            //bool IsGet = false;
            foreach(FoodChain x in s.Predator)
            {
                if(x.type == t.Special)
                {
                    if((t.transform.position - transform.position).sqrMagnitude < 4)
                    {
                    //IsGet = true;
                    Vector3 direction = new Vector3(t.transform.position.x - transform.position.x, 0, t.transform.position.z - transform.position.z);
                    Move(direction.normalized * (float)SpeedTick);
                    return;
                    }
                }
                //if (!IsGet) return;
            }
        }*/

        if (HungryBordenRate * MaxOrganicMatter >= NowOrganicMatter)
        {
            foreach (Producer t in GameManager.Instance.AllProducer)
            {
                if (t.SuddenDeath) continue;
                foreach (FoodChain x in s.Prey)
                {
                    if (x.type == t.Special)
                    {
                        if((t.transform.position - transform.position).sqrMagnitude < 0.25)
                        {
                            //GetComponent<CreatureAnimation>().Idle();
                            GetComponent<CreatureAnimation>().Eating();
                            WalkTick = 0;
                            double temp = t.NowOrganicMatter - t.MinOrganicMatter;
                            if (temp + NowOrganicMatter > MaxOrganicMatter) NowOrganicMatter = MaxOrganicMatter;
                            else
                                NowOrganicMatter = temp + NowOrganicMatter;
                            t.SuddenDeath = true;
                            t.NowOrganicMatter = t.MinOrganicMatter;
                            t.GetComponent<CreatureAnimation>().Idle();
                            t.GetComponent<CreatureAnimation>().Death();
                            isEating = true;
                            return;
                            //isEating = true;
                        }
                    }
                }
            }
            foreach (Consumer t in GameManager.Instance.AllConsumer)
            {
                if (t.SuddenDeath) continue;
                foreach (FoodChain x in s.Prey)
                {
                    if (x.type == t.Special)
                    {
                        if ((t.transform.position - transform.position).sqrMagnitude < 0.25)
                        {
                            GetComponent<CreatureAnimation>().Idle();
                            GetComponent<CreatureAnimation>().Eating();
                            WalkTick = 0;
                            double temp = t.NowOrganicMatter - t.MinOrganicMatter;
                            if (temp + NowOrganicMatter > MaxOrganicMatter) NowOrganicMatter = MaxOrganicMatter;
                            else
                                NowOrganicMatter = temp + NowOrganicMatter;
                            t.SuddenDeath = true;
                            t.NowOrganicMatter = t.MinOrganicMatter;
                            t.GetComponent<CreatureAnimation>().Idle();
                            t.GetComponent<CreatureAnimation>().Death();
                            isEating = true;
                            return;
                            
                        }
                    }
                }
            }

            foreach (Producer t in GameManager.Instance.AllProducer)
            {
                if (t.SuddenDeath) continue;
                foreach (FoodChain x in s.Prey)
                {
                    if (x.type == t.Special)
                    {
                        if ((t.transform.position - transform.position).sqrMagnitude < 4)
                        {
                            //IsGet = true;
                            Vector3 direction = new Vector3(t.transform.position.x - transform.position.x, 0, t.transform.position.z - transform.position.z);
                            WalkDirection = direction.normalized;
                            Move(direction.normalized * (float)SpeedTick);
                            return;
                        }
                    }
                }
            }

            foreach (Consumer t in GameManager.Instance.AllConsumer)
            {
                if (t.SuddenDeath) continue;
                foreach (FoodChain x in s.Prey)
                {
                    if (x.type == t.Special)
                    {
                        if ((t.transform.position - transform.position).sqrMagnitude < 4)
                        {
                            //IsGet = true;
                            Vector3 direction = new Vector3(transform.position.x - t.transform.position.x, 0, transform.position.z - t.transform.position.z);
                            WalkDirection = direction.normalized;
                            Move(direction.normalized * (float)SpeedTick);
                            return;
                        }
                    }
                }
            }
        }

        //go anywhere

        if (WalkTick == 60)
        {
            WalkTick = 0;
            int x1 = Random.Range(-100, 100);
            int z1 = Random.Range(-100, 100);
            Vector3 tt = new Vector3(x1, 0, z1);
            WalkDirection = tt.normalized;
        }
        else
            WalkTick++;

        Walk();

    }

    // Start is called before the first frame update
    void Start()
    {
        NowOrganicMatter = MaxOrganicMatter;// * MinOrganicMatterRate +50;
        MinOrganicMatter = MinOrganicMatterRate * MaxOrganicMatter;
        OxygenComsuptionTick = OrganicMatterComsuptionTick * OrganicMatterComsuptionToOxygenComsuptionRate;
        BreatheCarbonDioxideGenerationTick = OxygenComsuptionTick * OxygenComsuptionToCarbonDioxideComsuptionRate;
        BreatheH20GenerationTick = OxygenComsuptionTick * OxygenComsuptionToH2OComsuptionRate;
        GameManager.Instance.AllConsumer.Add(this);
        NowBlock = GameManager.Instance.FindBlock(transform.position);
        GetComponent<CreatureAnimation>().Idle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
