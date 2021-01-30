using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private IntVariable playerShips;

    [SerializeField]
    private IntVariable playerPlunder;

    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string value = "";
        value += "Yer Armarda: " + playerShips.RuntimeValue.ToString("D2") + "\n";
        value += "Plunder: " + playerPlunder.RuntimeValue.ToString("D2");

        text.text = value;
    }
}
