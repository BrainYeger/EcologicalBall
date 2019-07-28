using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createCreature : MonoBehaviour
{
    public creatureList creaturesList = null;
    public Text creaturName = null;
    public Camera cam = null;
    //public GameObject createPos = null;
    private int nowKind = 0;
    private int nowCre = 0;
    private GameObject demo = null;
    // Start is called before the first frame update
    void Start()
    {
        ChangeDemo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCreate(true);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            ChangeCreate(false);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeKind(true);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeKind(false);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //从摄像机发出到点击坐标的射线
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                AddCreature(hitInfo.point, hitInfo.normal);
                
            }
        }
    }
    public void ChangeCreate(bool right)
    {
        int max = 0;
        switch (nowKind)
        {
            case 0:
                nowCre += right ? 1 : -1;
                max = creaturesList.animals.Count;
                nowCre = (nowCre + max) % max;
                break;
            case 1:
                nowCre += right ? 1 : -1;
                max = creaturesList.plants.Count;
                nowCre = (nowCre + max) % max;
                break;
            case 2:
                nowCre += right ? 1 : -1;
                max = creaturesList.grass.Count;
                nowCre = (nowCre + max) % max;
                break;

        }
        ChangeDemo();

    }
    public void ChangeKind(bool right)
    {
        if (right)
        {
            nowKind++;
            nowKind = nowKind % 3;
            nowCre = 0;

        }
        else
        {
            nowKind--;
            if (nowKind < 0)
            {
                nowKind = 2;
            }
            nowCre = 0;
        }
        ChangeDemo();
    }

    void ChangeDemo()
    {
        Destroy(demo);
        switch (nowKind)
        {
            case 0:
                demo = GameObject.Instantiate(creaturesList.animals[nowCre].creaturePrefab,transform);
                creaturName.text = creaturesList.animals[nowCre].Name;
                break;
            case 1:
                demo = GameObject.Instantiate(creaturesList.plants[nowCre].creaturePrefab,transform);
                creaturName.text = creaturesList.plants[nowCre].Name;
                break;
            case 2:
                demo = GameObject.Instantiate(creaturesList.grass[nowCre].creaturePrefab,transform);
                creaturName.text = creaturesList.grass[nowCre].Name;
                break;

        }
    }
    public void AddCreature(Vector3 pos,Vector3 rotate)
    {
        GameObject creature = null;
        switch (nowKind)
        {
            case 0:
                creature = GameObject.Instantiate(creaturesList.animals[nowCre].creaturePrefab, pos,Quaternion.Euler(rotate));
                Consumer csm = creature.AddComponent<Consumer>();
                csm.Special = creaturesList.animals[nowCre].Special;
                csm.MaxOrganicMatter = creaturesList.animals[nowCre].MaxOrganicMatter;
                csm.OrganicMatterComsuptionTick = creaturesList.animals[nowCre].OrganicMatterComsuptionTick;
                csm.MaxLowOxygenTime = creaturesList.animals[nowCre].MaxLowOxygenTime;
                csm.SpeedTick = creaturesList.animals[nowCre].SpeedTick;
                csm.eyeSight = creaturesList.animals[nowCre].eyeSlight;
                break;
            case 1:
                creature = GameObject.Instantiate(creaturesList.plants[nowCre].creaturePrefab, pos, Quaternion.Euler(rotate));
                Producer pd = creature.AddComponent<Producer>();
                pd.Special = creaturesList.plants[nowCre].Special;
                pd.MaxOrganicMatter = creaturesList.plants[nowCre].MaxOrganicMatter;
                pd.OrganicMatterComsuptionTick = creaturesList.plants[nowCre].OrganicMatterComsuptionTick;
                pd.MaxLowOxygenTime = creaturesList.plants[nowCre].MaxLowOxygenTime;
                pd.CarbonDioxideComsuptionTick = creaturesList.plants[nowCre].CarbonDioxideComsuptionTick;
                pd.LuminousIntensityNeedTick = creaturesList.plants[nowCre].LuminousIntensityNeedTick;
                pd.FertilityNeedTick = creaturesList.plants[nowCre].FertilityNeedTick;
                pd.height = creaturesList.plants[nowCre].height;
                break;
            case 2:
                creature = GameObject.Instantiate(creaturesList.grass[nowCre].creaturePrefab, pos, Quaternion.Euler(rotate));
                pd = creature.AddComponent<Producer>();
                pd.Special = creaturesList.grass[nowCre].Special;
                pd.MaxOrganicMatter = creaturesList.grass[nowCre].MaxOrganicMatter;
                pd.OrganicMatterComsuptionTick = creaturesList.grass[nowCre].OrganicMatterComsuptionTick;
                pd.MaxLowOxygenTime = creaturesList.grass[nowCre].MaxLowOxygenTime;
                pd.CarbonDioxideComsuptionTick = creaturesList.grass[nowCre].CarbonDioxideComsuptionTick;
                pd.LuminousIntensityNeedTick = creaturesList.grass[nowCre].LuminousIntensityNeedTick;
                pd.FertilityNeedTick = creaturesList.grass[nowCre].FertilityNeedTick;
                pd.height = creaturesList.grass[nowCre].height;
                break;

        }
    }

}
