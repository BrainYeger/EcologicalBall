using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBlock : MonoBehaviour
{
    //const int BlockSize = 10;
    //public static EnvironmentBlock [,]AllBlock = new EnvironmentBlock[BlockSize,BlockSize];

    public int indexX;
    public int indexY;

    public Vector3 position;
    public double Oxygen;
    public double CarbonDioxide;
    public double LuminousIntensity;
    public double H2O;
    public double Fertility;
    public double OtherVapor;
    public double RottingSpeed;

    public static double RottingRate;
    public static double DiffusionSpeed = 0.002;

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
        position = transform.position;
        Oxygen = 100;
        CarbonDioxide = 100;
        LuminousIntensity = 160;
        RottingSpeed = 30;
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
    {
        //to up
        if (indexX > 0)
            Diffuse(GameManager.Instance.AllBlock[indexX - 1,indexY]);
        //to down
        if (indexX < 9)
            Diffuse(GameManager.Instance.AllBlock[indexX + 1,indexY]);
        //to left
        if (indexY > 0)
            Diffuse(GameManager.Instance.AllBlock[indexX,indexY - 1]);
        //to right
        if (indexY < 9)
            Diffuse(GameManager.Instance.AllBlock[indexX,indexY + 1]);
    }
}
