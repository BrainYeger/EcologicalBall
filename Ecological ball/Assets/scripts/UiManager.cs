using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Scrollbar o2Scroll = null;
    public Scrollbar co2Scroll = null;
    public GameObject showHighLight = null;

    private ShowWater sw = null;
    // Start is called before the first frame update
    void Start()
    {
        showHighLight.SetActive(false);
        sw = showHighLight.GetComponent<ShowWater>();
    }

    // Update is called once per frame
    void Update()
    {
        o2Scroll.size = (float)GameManager.Instance.GlobalOxygen/ 20000;
        co2Scroll.size = (float)GameManager.Instance.GlobalCarbonDioxide/ 20000;
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowRich();
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            UnShowHighLight();
        }
    }
    public void ShowWater()
    {
        showHighLight.SetActive(true);
        sw.ShowMyWater();
    }

    public void ShowRich()
    {
        showHighLight.SetActive(true);
        sw.ShowRich();
    }
    public void UnShowHighLight()
    {
        showHighLight.SetActive(false);
    }
}
