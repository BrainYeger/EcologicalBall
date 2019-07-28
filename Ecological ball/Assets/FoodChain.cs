using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChain
{
    public enum ChainMember
    {
        bear, wolf, cow, horse, giraffe, camphor_tree, pine_tree, cherry_bay, elm_tree, what_tree, tree, ryegrass, bracken, red_spathe_flower, zoysia, grass, nul
    };
    public ChainMember type;
    public List<FoodChain> Predator = new List<FoodChain>();
    public List<FoodChain> Prey = new List<FoodChain>();

    static public List<FoodChain> all = new List<FoodChain>();

    public FoodChain()
    {
        type = ChainMember.nul;
        all.Add(new FoodChain(ChainMember.bear));//0
        all.Add(new FoodChain(ChainMember.wolf));//1
        all.Add(new FoodChain(ChainMember.cow));//2
        all.Add(new FoodChain(ChainMember.horse));//3
        all.Add(new FoodChain(ChainMember.giraffe));//4
        all.Add(new FoodChain(ChainMember.tree));//5
        all.Add(new FoodChain(ChainMember.grass));//6
        //all.Add(new FoodChain(ChainMember.cherry_bay));
        all[0].Predator.Add(all[1]);
        all[0].Prey.Add(all[2]);
        all[0].Prey.Add(all[3]);
        all[0].Prey.Add(all[4]);
        all[1].Prey.Add(all[0]);
        all[1].Prey.Add(all[2]);
        all[1].Prey.Add(all[3]);
        all[1].Prey.Add(all[4]);
        all[1].Prey.Add(all[6]);
        all[2].Predator.Add(all[0]);
        all[2].Predator.Add(all[1]);
        all[2].Prey.Add(all[6]);
        all[3].Predator.Add(all[0]);
        all[3].Predator.Add(all[1]);
        all[3].Prey.Add(all[6]);
        all[4].Predator.Add(all[0]);
        all[4].Predator.Add(all[1]);
        all[4].Prey.Add(all[5]);
        all[5].Predator.Add(all[4]);
        all[6].Predator.Add(all[1]);
        all[6].Predator.Add(all[2]);
        all[6].Predator.Add(all[3]);
        all[6].Predator.Add(all[4]);
    }
    FoodChain(ChainMember m) { type = m; }

    public bool Query(ChainMember t, List<FoodChain> predator, List<FoodChain> prey)
    {
        /*
        if (type == t)
        {
            predator = Predator;
            prey = Prey;
            return true;
        }
        for (int i = 0; i < Predator.Count; i++)
        {
            if (Predator[i].Query(t, predator, prey))
                return true;
        }
        return false;
        */

        foreach(FoodChain m in all)
        {
            if (m.type == t)
            {
                predator = m.Predator;
                prey = m.Prey;
                return true;
            }
        }
        return false;
    }

    public FoodChain Query(ChainMember t)
    {
        foreach (FoodChain m in all)
        {
            if (m.type == t)
            {
                return m;
            }
        }
        return null;
    }
}
