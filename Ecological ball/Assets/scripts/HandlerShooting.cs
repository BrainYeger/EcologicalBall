using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class HandlerShooting : MonoBehaviour
{
    private LineRenderer laser = null;
    private int yyg = 5;
    private bool IsShooting = false;
    public GameObject createCreat = null;
    public createCreature cc = null;
    public GameObject showLight = null;
    public GameObject zj = null;

    private SteamVR_Action_Vector2 m_Touch;
    private SteamVR_Action_Vector2 m_Touch_l;
    private SteamVR_Action_Boolean mBG;
    private SteamVR_Action_Boolean mA;
    private SteamVR_Action_Boolean mB;
    private UiManager uiManager = null;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        laser.startColor = Color.red;
        laser.endColor = Color.red;
        laser.positionCount = 2;
        
        cc = createCreat.GetComponent<createCreature>();
        m_Touch = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("rightHandler");
        m_Touch_l = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("leftHandler");
        mBG = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("rightBG");
        mA = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("rightA");
        mB = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("rightB");

        uiManager = showLight.GetComponent<UiManager>();
    }



    // Update is called once per frame
    void Update()
    {
        

        if(m_Touch_l.GetAxis(SteamVR_Input_Sources.Any).x!=0|| m_Touch_l.GetAxis(SteamVR_Input_Sources.Any).y != 0)
        {
            zj.transform.Translate(transform.TransformDirection(new Vector3(m_Touch_l.GetAxis(SteamVR_Input_Sources.Any).y*-0.1f, m_Touch_l.GetAxis(SteamVR_Input_Sources.Any).x * -0.1f, 0)));
        }

        if (m_Touch.GetAxis(SteamVR_Input_Sources.Any).x > 0.5f)
        {
            if (yyg != 0)
            {
                yyg = 0;
                cc.ChangeCreate(true);
            }
        }
        else if(m_Touch.GetAxis(SteamVR_Input_Sources.Any).x < -0.5f)
        {
            if (yyg != 1)
            {
                yyg = 1;
                cc.ChangeCreate(false);
            }
        }
        else if(m_Touch.GetAxis(SteamVR_Input_Sources.Any).y > 0.5f)
        {
            if (yyg != 2)
            {
                yyg = 0;
                cc.ChangeKind(true);
            }
        }
        else if(m_Touch.GetAxis(SteamVR_Input_Sources.Any).y < -0.5f)
        {
            if (yyg != 3)
            {
                yyg = 3;
                cc.ChangeKind(false);
            }
        }
        else
        {
            yyg = 5;
        }
        if (mBG.GetChanged(SteamVR_Input_Sources.Any)&&mBG.GetState(SteamVR_Input_Sources.Any))
        {
            PressedShoot();
        }
        else if (mBG.GetChanged(SteamVR_Input_Sources.Any) && !mBG.GetState(SteamVR_Input_Sources.Any))
        {
            PressedUpShoot();
        }

        if (mB.GetChanged(SteamVR_Input_Sources.Any) && mB.GetState(SteamVR_Input_Sources.Any))
        {
            uiManager.ShowWater();
        }
        else if (mB.GetChanged(SteamVR_Input_Sources.Any) && !mB.GetState(SteamVR_Input_Sources.Any))
        {
            uiManager.UnShowHighLight();
        }

        if (mA.GetChanged(SteamVR_Input_Sources.Any) && mA.GetState(SteamVR_Input_Sources.Any))
        {
            uiManager.ShowRich();
        }
        else if (mA.GetChanged(SteamVR_Input_Sources.Any) && !mA.GetState(SteamVR_Input_Sources.Any))
        {
            uiManager.UnShowHighLight();
        }

        if (IsShooting)
        {
            laser.enabled = true;
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position + transform.forward * 100);
        }
        else
        {
            laser.enabled = false;
        }


    }
    public void PressedShoot()
    {
        IsShooting = true;
    }
    public void PressedUpShoot()
    {
        IsShooting = false;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            cc.AddCreature(hitInfo.point, hitInfo.normal);

        }
    }
}
