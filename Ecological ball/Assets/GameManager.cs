using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int BlockSize;
    public EnvironmentBlock[,] AllBlock;
    public FoodChain foodchain;

    public List<Consumer> AllConsumer;
    public List<Producer> AllProducer;

    private double globaloxygen;
    public double GlobalOxygen
    {
        get
        {
            double OxygenSum = 0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    OxygenSum += AllBlock[i, j].Oxygen;
            return OxygenSum;
        }
    }

    private double globalcarbondioxide;
    public double GlobalCarbonDioxide
    {
        get
        {
            double CarbonDioxideSum = 0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    CarbonDioxideSum += AllBlock[i, j].CarbonDioxide;
            return CarbonDioxideSum;
        }
    }

    public double GetH2O(Vector3 pos)
    {
        EnvironmentBlock t = FindBlock(pos);
        int x = t.indexX;
        int y = t.indexY;

        double LerpX, LerpY;
        double length = AllBlock[1, 0].position.x - AllBlock[0, 0].position.x;
        if (x == 0)
            LerpY = (t.H2O - AllBlock[1, y].H2O) * Mathf.Abs(pos.x + 4) + AllBlock[1, y].H2O;
        else if (x == 9)
            LerpY = (t.H2O - AllBlock[8, y].H2O) * Mathf.Abs(pos.x - 4) + AllBlock[8, y].H2O;
        else
            LerpY = (AllBlock[x + 1, y].H2O - AllBlock[x - 1, y].H2O) * Mathf.Abs(pos.x - x + 4) + AllBlock[x - 1, y].H2O;

        if (y == 0)
            LerpX = (t.H2O - AllBlock[x, 1].H2O) * Mathf.Abs(pos.z + 4) + AllBlock[x, 1].H2O;
        else if (y == 9)
            LerpX = (t.H2O - AllBlock[x, 8].H2O) * Mathf.Abs(pos.z - 4) + AllBlock[x, 8].H2O;
        else
            LerpX = (AllBlock[x, y + 1].H2O - AllBlock[x, y - 1].H2O) * Mathf.Abs(pos.z - y + 4) + AllBlock[x, y - 1].H2O;

        return (LerpX + LerpY + t.H2O) / 3;

    }
    public double GetRich(Vector3 pos)
    {
        EnvironmentBlock t = FindBlock(pos);
        int x = t.indexX;
        int y = t.indexY;

        double LerpX, LerpY;
        double length = AllBlock[1, 0].position.x - AllBlock[0, 0].position.x;
        if (x == 0)
            LerpY = (t.Fertility - AllBlock[1, y].Fertility) * Mathf.Abs(pos.x + 4) + AllBlock[1, y].Fertility;
        else if (x == 9)
            LerpY = (t.Fertility - AllBlock[8, y].Fertility) * Mathf.Abs(pos.x - 4) + AllBlock[8, y].Fertility;
        else
            LerpY = (AllBlock[x + 1, y].Fertility - AllBlock[x - 1, y].Fertility) * Mathf.Abs(pos.x - x + 4) + AllBlock[x - 1, y].Fertility;

        if (y == 0)
            LerpX = (t.Fertility - AllBlock[x, 1].Fertility) * Mathf.Abs(pos.z + 4) + AllBlock[x, 1].Fertility;
        else if (y == 9)
            LerpX = (t.Fertility - AllBlock[x, 8].Fertility) * Mathf.Abs(pos.z - 4) + AllBlock[x, 8].Fertility;
        else
            LerpX = (AllBlock[x, y + 1].Fertility - AllBlock[x, y - 1].Fertility) * Mathf.Abs(pos.z - y + 4) + AllBlock[x, y - 1].Fertility;

        return (LerpX + LerpY + t.Fertility) / 3;

    }

    public EnvironmentBlock FindBlock(Vector3 pos)
    {
        int x = (int)(pos.x + 4.5);
        if (x < 0)
            x = 0;
        else if (x > 9)
            x = 9;

        int y = (int)(pos.z + 4.5);
        if (y < 0)
            y = 0;
        else if (y > 9)
            y = 9;

        return AllBlock[x, y];
    }

    void AllBreathe()
    {
        foreach(EnvironmentBlock SelectBlock in AllBlock)
        {
            double AllOxygenNeed = 0;
            List<Consumer> SelectConsumer = new List<Consumer>();
            List<Producer> SelectProducer = new List<Producer>();
            foreach(Consumer t in AllConsumer)
            {
                if (t.NowBlock.EqualTo(SelectBlock))
                {
                    AllOxygenNeed += t.OxygenComsuptionTick;
                    SelectConsumer.Add(t);
                }
            }
            foreach (Producer t in AllProducer)
            {
                if (t.NowBlock.EqualTo(SelectBlock))
                {
                    AllOxygenNeed += t.OxygenComsuptionTick;
                    SelectProducer.Add(t);
                }
            }
            if (AllOxygenNeed <= SelectBlock.Oxygen)
            {
                SelectBlock.Oxygen -= AllOxygenNeed;
                SelectBlock.CarbonDioxide += AllOxygenNeed * Individual.OxygenComsuptionToCarbonDioxideComsuptionRate;
                SelectBlock.H2O += AllOxygenNeed * Individual.OxygenComsuptionToH2OComsuptionRate;
                foreach(Consumer t in SelectConsumer)
                {
                    t.Breathe(t.OrganicMatterComsuptionTick);
                    if (t.NowLowOxygenTime != 0)
                        t.NowLowOxygenTime--;
                }
                foreach (Producer t in SelectProducer)
                {
                    t.Breathe(t.OrganicMatterComsuptionTick);
                    if (t.NowLowOxygenTime != 0)
                        t.NowLowOxygenTime--;
                }
            }
            else
            {
                SelectBlock.CarbonDioxide += SelectBlock.Oxygen * Individual.OxygenComsuptionToCarbonDioxideComsuptionRate;
                SelectBlock.H2O += SelectBlock.Oxygen * Individual.OxygenComsuptionToH2OComsuptionRate;
                double AllocateRate = SelectBlock.Oxygen / AllOxygenNeed;
                foreach (Consumer t in SelectConsumer)
                {
                    t.Breathe(t.OrganicMatterComsuptionTick * AllocateRate);
                    t.NowLowOxygenTime++;
                }
                foreach (Producer t in SelectProducer)
                {
                    t.Breathe(t.OrganicMatterComsuptionTick);
                    t.NowLowOxygenTime++;
                }
                SelectBlock.Oxygen = 0;
            }
        }
    }

    void AllPhotoSynthesis()
    {
        foreach(EnvironmentBlock SelectBlock in AllBlock)
        {
            double AllCO2Need = 0;
            double AllH2ONeed = 0;
            List<Producer> SelectProducer = new List<Producer>();
            foreach(Producer t in AllProducer)
            {
                if (t.NowBlock.EqualTo(SelectBlock))
                {
                    AllCO2Need += t.CarbonDioxideComsuptionTick;
                    AllH2ONeed += t.H2OComsuptionTick;
                    SelectProducer.Add(t);
                }
            }
            for (int m = 0; m < SelectProducer.Count - 1; m++)
            {
                bool isSort = true;
                for (int n = 0; n < SelectProducer.Count - m - 1; n++)
                {
                    if (SelectProducer[n].height < SelectProducer[n + 1].height)
                    {
                        Producer temp = SelectProducer[n];
                        SelectProducer[n] = SelectProducer[n + 1];
                        SelectProducer[n + 1] = temp;
                        isSort = false;
                    }
                }
                if (isSort) break;
            }
            double AllLuminousIntensity = SelectBlock.LuminousIntensity;
            if (AllCO2Need <= SelectBlock.CarbonDioxide && AllH2ONeed <= SelectBlock.H2O)
            {
                SelectBlock.CarbonDioxide -= AllCO2Need;
                SelectBlock.H2O -= AllH2ONeed;
                foreach (Producer t in SelectProducer)
                {
                    double efficiency;
                    if (AllLuminousIntensity <= t.LuminousIntensityNeedTick)
                    {
                        efficiency = (AllLuminousIntensity / t.LuminousIntensityNeedTick) * (SelectBlock.Fertility / t.FertilityNeedTick);
                        AllLuminousIntensity = 0;
                    }
                    else
                    {
                        efficiency = (SelectBlock.Fertility / t.FertilityNeedTick);
                        AllLuminousIntensity -= t.LuminousIntensityNeedTick;
                    }
                    SelectBlock.Oxygen += efficiency * AllCO2Need * Producer.CarbonDioxideComsuptionToOxygenGeneration;
                    t.NowOrganicMatter += efficiency * AllCO2Need * Producer.CarbonDioxideComsuptionToOrganicMatterGeneration;
                    if (t.NowOrganicMatter > t.MaxOrganicMatter)
                        t.NowOrganicMatter = t.MaxOrganicMatter;

                }
            }
            else
            {
                double CO2Rate = SelectBlock.CarbonDioxide / AllCO2Need;
                double H2ORate = SelectBlock.H2O / AllH2ONeed;
                foreach (Producer t in SelectProducer)
                {
                    double CO2 = CO2Rate * t.CarbonDioxideComsuptionTick;
                    double H2O = H2ORate * t.H2OComsuptionTick;
                    if (CO2 * Producer.CarbonDioxideComsuptionToH2OComsuption <= H2O)
                    {
                        H2O = CO2 * Producer.CarbonDioxideComsuptionToH2OComsuption;
                    }
                    else
                        CO2 = H2O / Producer.CarbonDioxideComsuptionToH2OComsuption;
                    SelectBlock.CarbonDioxide -= CO2;
                    SelectBlock.H2O -= H2O;
                    double efficiency;
                    if (AllLuminousIntensity <= t.LuminousIntensityNeedTick)
                    {
                        efficiency = (AllLuminousIntensity / t.LuminousIntensityNeedTick) * (SelectBlock.Fertility / t.FertilityNeedTick);
                        AllLuminousIntensity = 0;
                    }
                    else
                    {
                        efficiency = (SelectBlock.Fertility / t.FertilityNeedTick);
                        AllLuminousIntensity -= t.LuminousIntensityNeedTick;
                    }
                    SelectBlock.Oxygen += efficiency * CO2 * Producer.CarbonDioxideComsuptionToOxygenGeneration;
                    t.NowOrganicMatter += efficiency * CO2 * Producer.CarbonDioxideComsuptionToOrganicMatterGeneration;
                    if (t.NowOrganicMatter > t.MaxOrganicMatter)
                        t.NowOrganicMatter = t.MaxOrganicMatter;
                }
            }
        }
    }

    private void Awake()
    {
        BlockSize = 10;
        AllBlock = new EnvironmentBlock[BlockSize, BlockSize];
        foodchain = new FoodChain();

        AllConsumer = new List<Consumer>();
        AllProducer = new List<Producer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        AllBreathe();
        AllPhotoSynthesis();
        foreach(Consumer t in AllConsumer)
        {
            t.Prey();
        }
    }
}
