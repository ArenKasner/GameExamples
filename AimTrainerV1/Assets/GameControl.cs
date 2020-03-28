using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Texture2D cursorTexture;

   private Vector2 cursorHotspot;

   private Vector2 mousePos;

    [SerializeField]
    private Text getReadyText;

    [SerializeField]
    private GameObject resultsPanel;

    [SerializeField]
    private Text scoreText, targetsHitText, shotsFiredText, accuracyText, updateScore, updateTargets,missed;

    public static int score, targetsHit, checktargetshit,misses;

    private float shotsFired;

    private float accuracy;

    private int targetsAmount;

    private bool start = false;

    private Vector2 targetRandomPosition;


    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        getReadyText.gameObject.SetActive(false);

        targetsAmount = 15;
        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0f;
        misses = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (start == true)
            {
                shotsFired += 1f;
                FindObjectOfType<AudioManager>().Play("gunshot");
                if (Target.hit == false)
                {
                    misses += 1;
                    Debug.Log("Miss");
                }
            }
            
            
        }


    }

    private IEnumerator GetReady()
    {
        for (int i = 3; i >= 1; i--)
        {
            getReadyText.text = "Get Ready!\n" + i.ToString();
            yield return new WaitForSeconds(1f);
        }

        getReadyText.text = "Go!";
        yield return new WaitForSeconds(1f);

        StartCoroutine("SpawnTargets");
    }
    private IEnumerator SpawnTargets()
    {
        getReadyText.gameObject.SetActive(false);
        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0;

        for (int i = targetsAmount; i > 0; i--)
        {
            targetRandomPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            Instantiate(target, targetRandomPosition, Quaternion.identity);
            updateScore.text = score.ToString();
            updateTargets.text = targetsHit.ToString() + '/' + targetsAmount.ToString();
            missed.text = "Misses: " + misses.ToString();
            yield return new WaitForSeconds(1f);
        }

        resultsPanel.SetActive(true);
        scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit:" + targetsHit + "/" + targetsAmount;
        shotsFiredText.text = "Shots Fired: " + shotsFired;
        accuracy = targetsHit / shotsFired * 100f;
        accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + "%";
    }

    public void StartGetReadyCoroutine()
    {
        resultsPanel.SetActive(false);
        start = true;
        getReadyText.gameObject.SetActive(true);
        StartCoroutine("GetReady");
    }

}
