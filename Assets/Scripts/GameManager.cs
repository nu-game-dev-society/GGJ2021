using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]public List<Fleet> activeFleets;
    public Transform player;

    public float playSpaceRadius;
    public GameObject turnAroundWarning;
    public GameObject gameOverWarning;
    bool gameOver => gameOverWarning.activeInHierarchy;

    public Volume volume;
    ColorAdjustments colorAdjustments;

    [SerializeField] List<Color> possibleColours = new List<Color>();
    Dictionary<Color, bool> activeColours = new Dictionary<Color, bool>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        activeFleets = new List<Fleet>(); 
        activeFleets.AddRange(FindObjectsOfType<Fleet>());

        ColorAdjustments tmp;
        if (volume.profile.TryGet<ColorAdjustments>(out tmp))
        {
            colorAdjustments = tmp;
        }

        foreach(Color color in possibleColours)
        {
            activeColours.Add(color, false);
        }
        gameOverWarning.SetActive(false);
    }

    public void SetColourAvailable(Color color)
    {
        activeColours[color] = false;
    }

    public void Update()
    {
        float playerDistance = Vector3.Distance(Vector3.zero, player.transform.position);

        if (playerDistance > playSpaceRadius && !gameOver)
        {
            turnAroundWarning.SetActive(true);
        }
        else
        {
            turnAroundWarning.SetActive(false);
        }

        float perc = ((playerDistance - playSpaceRadius) / 60f);

        perc = Mathf.Clamp01(perc);

        if (colorAdjustments)
            colorAdjustments.saturation.value = -100 * perc;
    }

    public Fleet GetNearestFleetToPosition(Vector3 pos, Fleet shipFleet)
    {
        Fleet nearest = null;
        float closestDist = float.MaxValue;

        foreach (Fleet fleet in activeFleets)
        {
            if (fleet != shipFleet)
            {
                float dist = Vector3.Distance(pos, fleet.center);

                if (dist < closestDist)
                {
                    closestDist = dist;
                    nearest = fleet;
                }
            }
        }

        return nearest;
    }

    public void OnDestroy()
    {
        if (colorAdjustments)
            colorAdjustments.saturation.value = 0f;
    }

    public bool TryGetUnusedColour(out Color colour)
    {
        if(activeColours.Any(c => !c.Value))
        {
            colour = activeColours.First(c => !c.Value).Key;
            activeColours[colour] = true;
            return true;
        }
        colour = Color.clear;
        return false;
    }

    public void GameOver()
    {
        player.GetComponent<PlayerSetShipTargetLocation>().controller.enabled = false;
        gameOverWarning.SetActive(true);
        turnAroundWarning.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
