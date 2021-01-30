using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowBoatColour : MonoBehaviour
{
    [SerializeField]
    private GameObject crewMate;

    private Material crewMateMaterial;
    private Color shipColour;

    // Start is called before the first frame update
    void Start()
    {
        shipColour = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 0.6f);
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
        if (crewMateMaterial == null)
        {
            crewMateMaterial = new Material(crewMate.GetComponent<MeshRenderer>().material);
        }

        crewMateMaterial.color = shipColour;

        crewMate.GetComponent<MeshRenderer>().material = crewMateMaterial;
    }
}
