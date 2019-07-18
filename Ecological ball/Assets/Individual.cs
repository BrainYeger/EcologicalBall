﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Individual : MonoBehaviour
{
    public EnvironmentBlock NowBlock;

    public int LifeTick;

    public FoodChain.ChainMember Special;

    public double MaxOrganicMatter;
    public double MinOrganicMatter;
    public double NowOrganicMatter;

    public static double MinOrganicMatterRate;

    public double OrganicMatterComsuptionTick;
    public double OxygenComsuptionTick;
    public double BreatheCarbonDioxideGenerationTick;
    public double BreatheH20GenerationTick;

    public int MaxLowOxygenTime;
    public int NowLowOxygenTime;

    public static double MaxOrganicMatterToOrganicMatterComsuptionRate;
    public static double OrganicMatterComsuptionToOxygenComsuptionRate;
    public static double OxygenComsuptionToCarbonDioxideComsuptionRate;
    public static double OxygenComsuptionToH2OComsuptionRate;

    public void Breathe(double omc)
    {
        NowOrganicMatter -= omc;
    }

    bool Dead()
    {
        LifeTick--;
        if (NowOrganicMatter > MinOrganicMatter && NowLowOxygenTime <= MaxLowOxygenTime)
            return false;
        if (NowOrganicMatter <= 0 || LifeTick <= 0)
        {
            //to do , delete this
        }
        else
        {
            if (NowOrganicMatter < NowBlock.RottingSpeed)
            {
                NowOrganicMatter = 0;
                NowBlock.Fertility += NowOrganicMatter * EnvironmentBlock.RottingRate;
            }
            else
            {
                NowOrganicMatter -= NowBlock.RottingSpeed;
                NowBlock.Fertility += NowBlock.RottingSpeed * EnvironmentBlock.RottingRate;
            }
        }
        return true;
    }

    protected abstract void Reproduct();
    // Start is called before the first frame update
    void Start()
    {
        //to do
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
