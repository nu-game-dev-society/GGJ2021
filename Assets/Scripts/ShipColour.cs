using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipColour : MonoBehaviour
{
    [SerializeField]
    private GameObject sail;

    private CrewMate[] crewMates;
    private Material crewMateMaterial;

    private Color shipColour;

    // Start is called before the first frame update
    void Start()
    {
        // try to get an unused colour, otherwise just randomise it
        // NOTE: just need to make sure we set enough colours that it never needs to randomise
        shipColour = Color.clear;
        if (GameManager.instance == null || GameManager.instance.TryGetUnusedColour(out shipColour) == false)
        {
            shipColour = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 0.6f);
        }

        crewMates = GetComponentsInChildren<CrewMate>();
        UpdateColours();
    }

    public Color getShipColour()
    {
        return shipColour;
    }

    public void setShipColour(Color newColour)
    {
        shipColour = newColour;
        UpdateColours();
    }

    private void UpdateColours()
    {
        MeshRenderer renderer = sail.GetComponent<MeshRenderer>();
        renderer.material.SetColor("_SailColor", shipColour);

        foreach (CrewMate crewMate in crewMates)
        {
            if (crewMateMaterial == null)
            {
                crewMateMaterial = new Material(crewMate.GetComponent<MeshRenderer>().material);
            }

            crewMateMaterial.color = shipColour;

            crewMate.GetComponent<MeshRenderer>().material = crewMateMaterial;
        }
    }
}
