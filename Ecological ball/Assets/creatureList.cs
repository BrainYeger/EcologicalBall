using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class Creature
{
    public String Name;
    public FoodChain.ChainMember Special;
    public GameObject creaturePrefab = null;
    public double MaxOrganicMatter;
    public double OrganicMatterComsuptionTick;
    public int MaxLowOxygenTime;

}

[Serializable]
public class AnimalData:Creature
{
    public double SpeedTick;
    public double eyeSlight;
}

[Serializable]
public class PlantData:Creature
{
    public double CarbonDioxideComsuptionTick;
    public double LuminousIntensityNeedTick;
    public double FertilityNeedTick;
    public int height;
}


[CreateAssetMenu(fileName = "creaturesList", menuName = "Create Creatures List")]
public class creatureList : ScriptableObject
{

    [Space(10)]
    [Header("动物")]
    [SerializeField]
    public List<AnimalData> animals = new List<AnimalData>();

    [Space(10)]
    [Header("树木")]
    [SerializeField]
    public List<PlantData> plants = new List<PlantData>();

    [Space(10)]
    [Header("草")]
    [SerializeField]
    public List<PlantData> grass = new List<PlantData>();

    public double MaxOrganicMatterToOrganicMatterComsuptionRate = 0.0001f;
    public double OrganicMatterComsuptionToOxygenComsuptionRate = 0.04f;
    public double OxygenComsuptionToCarbonDioxideComsuptionRate = 1.0f;
    public double OxygenComsuptionToH2OComsuptionRate = 1;
    public double MinOrganicMatterRate = 0.25f;
    public double HungryBordenRate = 0.5f;
    public double CarbonDioxideComsuptionToH2OComsuption = 1;
    public double CarbonDioxideComsuptionToOxygenGeneration = 1.0f;
    public double CarbonDioxideComsuptionToOrganicMatterGeneration = 15;
    public double MaxFertilityConversionRate = 2;
    public double ReproductionConversionRate;

    public double RottingRate;
    public double DiffusionSpeed;

}
