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

    public static double CarbonDioxideComsuptionToH2OComsuption = 1;
    public static double CarbonDioxideComsuptionToOxygenGeneration = 1;
    public static double CarbonDioxideComsuptionToOrganicMatterGeneration = 15;
    public static double MaxFertilityConversionRate = 2;

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
        NowOrganicMatter = MaxOrganicMatter * MinOrganicMatterRate + 50;
        MinOrganicMatter = MinOrganicMatterRate * MaxOrganicMatter;
        OxygenComsuptionTick = OrganicMatterComsuptionTick * OrganicMatterComsuptionToOxygenComsuptionRate;
        BreatheCarbonDioxideGenerationTick = OxygenComsuptionTick * OxygenComsuptionToCarbonDioxideComsuptionRate;
        BreatheH20GenerationTick = OxygenComsuptionTick * OxygenComsuptionToH2OComsuptionRate;
        H2OComsuptionTick = CarbonDioxideComsuptionTick * CarbonDioxideComsuptionToH2OComsuption;
        OxygenComsuptionTick = CarbonDioxideComsuptionToOxygenGeneration * CarbonDioxideComsuptionTick;
        OrganicMatterComsuptionTick = CarbonDioxideComsuptionToOrganicMatterGeneration * CarbonDioxideComsuptionTick;
        GameManager.Instance.AllProducer.Add(this);
        NowBlock = GameManager.Instance.FindBlock(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
