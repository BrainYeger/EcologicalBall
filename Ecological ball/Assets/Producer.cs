using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : Individual
{
    public double CarbonDioxideComsuptionTick;
    public double H2OComsuptionTick;
    public double LuminousIntensityNeedTick;
    public double FertilityNeedTick;
    public double OxygenGenerationTick;
    public double OrganicMatterGenerationTick;

    public int height;

    public static double CarbonDioxideComsuptionToH2OComsuption;
    public static double CarbonDioxideComsuptionToOxygenGeneration;
    public static double CarbonDioxideComsuptionToOrganicMatterGeneration;
    public static double MaxFertilityConversionRate;

    public static double ReproductionConversionRate;

    protected override void Reproduct()
    {
        double ConversionOrganicMatter = ReproductionConversionRate * (MaxOrganicMatter - MinOrganicMatter);
        if (NowOrganicMatter <= ConversionOrganicMatter)
            return;
        //to do
        //Producer* Son = new Producer();
        //Son->NowOrganicMatter = ConversionOrganicMatter;
        NowOrganicMatter -= ConversionOrganicMatter;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
