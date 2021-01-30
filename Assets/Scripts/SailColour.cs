using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailColour : MonoBehaviour
{
    [SerializeField]
    private GameObject sail;

    [HideInInspector]
    public Color colour;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer = sail.GetComponent<MeshRenderer>();
        colour = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
        renderer.material.SetColor("_SailColor", colour);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
