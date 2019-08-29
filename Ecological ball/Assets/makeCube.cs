using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class makeCube : ScriptableWizard
{
    public Transform renderPosition;
    public Cubemap cubemap;

    private void OnWizardUpdate()
    {
        helpString = "Select transform to render";
        if (renderPosition && cubemap)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }
    private void OnWizardCreate()
    {
        GameObject cam = new GameObject("CumCube", typeof(Camera));
        cam.transform.position = renderPosition.position;
        cam.transform.rotation = renderPosition.rotation;

        cam.GetComponent<Camera>().RenderToCubemap(cubemap);
        DestroyImmediate(cam);

    }
    [MenuItem("CookBook/render Cubemap")]
    static void RenderCubemap()
    {
        ScriptableWizard.DisplayWizard("render cubemap", typeof(makeCube), "render!");
    }
}
