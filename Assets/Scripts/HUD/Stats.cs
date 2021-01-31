using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    private TMP_Text text;
    private Fleet playerFleet;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        playerFleet = GameManager.instance.player.GetComponent<Fleet>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text.text = "Yer Armarda: " + playerFleet.liveShipsCount.ToString("D2");
    }
}
