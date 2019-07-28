using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerShooting : MonoBehaviour
{
    private LineRenderer laser = null;
    private bool IsShooting = true;
    public GameObject createCreat = null;
    public createCreature cc = null;

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
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Oculus_GearVR_RThumbstickX") > 0.1f)
        {
            cc.ChangeCreate(true);
        }
        else if(Input.GetAxis("Oculus_GearVR_RThumbstickX") < -0.1f)
        {
            cc.ChangeCreate(false);
        }
        else if(Input.GetAxis("Oculus_GearVR_RThumbstickY") > 0.1f)
        {
            cc.ChangeKind(true);
        }
        else if(Input.GetAxis("Oculus_GearVR_RThumbstickY") < -0.1f)
        {
            cc.ChangeKind(false);
        }
        if (Input.GetButtonDown("Oculus_GearVR_RIndexTrigger"))
        {
            PressedShoot();
        }
        else if (Input.GetButtonUp("Oculus_GearVR_RIndexTrigger"))
        {
            PressedUpShoot();
        }

        if (IsShooting)
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position + transform.forward * 100);
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
