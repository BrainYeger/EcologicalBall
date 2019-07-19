using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBlock : MonoBehaviour
{
    //const int BlockSize = 10;
    //public static EnvironmentBlock [,]AllBlock = new EnvironmentBlock[BlockSize,BlockSize];

    [SerializeField]int indexX;
    [SerializeField]int indexY;

    public double Oxygen;
    public double CarbonDioxide;
    public double LuminousIntensity;
    public double H2O;
    public double Fertility;
    public double OtherVapor;
    public double RottingSpeed;

    public static double RottingRate;
    public static double DiffusionSpeed;

    void Diffuse(EnvironmentBlock t)
    {
        if (Oxygen > t.Oxygen + EnvironmentBlock.DiffusionSpeed)
        {
            Oxygen -= DiffusionSpeed;
            t.Oxygen += DiffusionSpeed;
        }
        else if (Oxygen + DiffusionSpeed < t.Oxygen)
        {
            //Oxygen += DiffusionSpeed;
            //t.Oxygen -= DiffusionSpeed;
        }
        else
        {
            Oxygen = (Oxygen + t.Oxygen) / 2;
            t.Oxygen = Oxygen;
        }

        if (CarbonDioxide > t.CarbonDioxide + DiffusionSpeed)
        {
            CarbonDioxide -= DiffusionSpeed;
            t.CarbonDioxide += DiffusionSpeed;
        }
        else if (CarbonDioxide + DiffusionSpeed < t.CarbonDioxide)
        {
            //CarbonDioxide += DiffusionSpeed;
            //t.CarbonDioxide -= DiffusionSpeed;
        }
        else
        {
            CarbonDioxide = (CarbonDioxide + t.CarbonDioxide) / 2;
            t.CarbonDioxide = CarbonDioxide;
        }

        if (H2O > t.H2O + DiffusionSpeed)
        {
            H2O -= DiffusionSpeed;
            t.H2O += DiffusionSpeed;
        }
        else if (H2O + DiffusionSpeed < t.H2O)
        {
            //H2O += DiffusionSpeed;
            //t.H2O -= DiffusionSpeed;
        }
        else
        {
            H2O = (H2O + t.H2O) / 2;
            t.H2O = H2O;
        }
    }

    public bool EqualTo(EnvironmentBlock t)
    {
        if (indexX == t.indexX && indexY == t.indexY)
            return true;
        return false;
    }

    private void Awake()
    {
        GameManager.Instance.AllBlock[indexX, indexY] = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //indexX = 0;
        //indexY = 0;

        
    }

    // Update is called once per frame
    void Update()
    {/*
        //to up
        if (indexX > 0)
            Diffuse(AllBlock[indexX - 1,indexY]);
        //to down
        if (indexX < BlockSize - 1)
            Diffuse(AllBlock[indexX + 1,indexY]);
        //to left
        if (indexY > 0)
            Diffuse(AllBlock[indexX,indexY - 1]);
        //to right
        if (indexY < BlockSize - 1)
            Diffuse(AllBlock[indexX,indexY + 1]);*/
    }
}
