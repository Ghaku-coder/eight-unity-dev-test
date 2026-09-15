using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject mission;
    [SerializeField] private GameObject congratulation;
    [SerializeField] private GameObject BlastRainbow;

    private bool missionShown = false;
    private bool congratulationShown = false;

    private void Start()
    {
        panel.SetActive(false);
        mission.SetActive(false);
        congratulation.SetActive(false);
        BlastRainbow.SetActive(false);
    }

    private void Update()
    {
        State();
    }

    public void State()
    {
        gameManager.ShowPlayerScore();

        if (gameManager.Score <= 0 && !missionShown)
        {
            missionShown = true;
            StartCoroutine(ShowMission());
        }

        if (gameManager.Score >= 20 && !congratulationShown)
        {
            congratulationShown = true;
            StartCoroutine(ShowCongratulation());
        }
    }

    public IEnumerator ShowMission()
    {
        panel.SetActive(true);
        mission.SetActive(true);

        yield return new WaitForSeconds(5f);

        mission.SetActive(false);
        panel.SetActive(false);
    }

    private IEnumerator ShowCongratulation()
    {
        Vector3 player = gameManager.player.transform.position + Vector3.up * 2f;

        BlastRainbow.SetActive(true);
        BlastRainbow.transform.position = player;

        panel.SetActive(true);
        congratulation.SetActive(true);

        yield return new WaitForSeconds(5f);

        BlastRainbow.SetActive(false);
        congratulation.SetActive(false);
        panel.SetActive(false);
    }
}