using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteraction : MonoBehaviour
{
    public Material switchMaterial;
    private bool isOn = true;
    private Light sceneLight;

    void Start()
    {
        sceneLight = FindFirstObjectByType<Light>();
    }

    public void ToggleLight()
    {
        isOn = !isOn;
        if (sceneLight != null)
        {
            sceneLight.enabled = isOn;
        }
        if (switchMaterial != null)
        {
            switchMaterial.color = isOn ? Color.white : Color.gray;
        }
    }
}
