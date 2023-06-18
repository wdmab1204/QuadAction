using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaterialManager : MonoBehaviour
{
    public MeshRenderer bodyRenderer;
    public MeshRenderer headRenderer;
    public MeshRenderer leftHandRenderer;
    public MeshRenderer rightHandRenderer;
    public MeshRenderer leftLegRenderer;
    public MeshRenderer rightLegRenderer;


    [Space()]
    public Material bodyMaterial;
    public Material headMaterial;
    public Material leftHandMaterial;
    public Material rightHandMaterial;
    public Material leftLegMaterial;
    public Material rightLegMaterial;

    // Start is called before the first frame update
    void Start()
    {
        bodyRenderer.material = bodyMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
