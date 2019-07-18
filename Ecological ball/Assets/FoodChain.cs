using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChain
{
    public enum ChainMember
    {
        shrub, algae, grass, rabbit, fish, mouse, eagle, cat, nul
    };
    public ChainMember type;
    public List<FoodChain> Predator = new List<FoodChain>();
    public List<FoodChain> Prey = new List<FoodChain>();

    public FoodChain()
    {
        type = ChainMember.nul;
        List<FoodChain> all = new List<FoodChain>();
        all.Add(new FoodChain(ChainMember.shrub));
        all.Add(new FoodChain(ChainMember.algae));
        all.Add(new FoodChain(ChainMember.grass));
        all.Add(new FoodChain(ChainMember.rabbit));
        all.Add(new FoodChain(ChainMember.fish));
        all.Add(new FoodChain(ChainMember.mouse));
        all.Add(new FoodChain(ChainMember.cat));
        all.Add(new FoodChain(ChainMember.eagle));
        all[0].Predator.Add(all[3]);
        all[1].Predator.Add(all[4]);
        all[2].Predator.Add(all[3]);
        all[2].Predator.Add(all[5]);
        all[3].Prey.Add(all[0]);
        all[3].Prey.Add(all[2]);
        all[3].Predator.Add(all[7]);
        all[4].Prey.Add(all[1]);
        all[4].Predator.Add(all[6]);
        all[5].Prey.Add(all[2]);
        all[5].Predator.Add(all[6]);
        all[5].Predator.Add(all[7]);
        all[6].Prey.Add(all[4]);
        all[6].Prey.Add(all[5]);
        all[6].Predator.Add(all[7]);
        all[7].Prey.Add(all[3]);
        all[7].Prey.Add(all[5]);
        all[7].Prey.Add(all[6]);
        Predator.Add(all[0]);
        Predator.Add(all[1]);
        Predator.Add(all[2]);
    }
    FoodChain(ChainMember m) { type = m; }

    public bool Query(ChainMember t, List<FoodChain> predator, List<FoodChain> prey)
    {
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
    }
}
