using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoadController : MonoBehaviour
{
    [SerializeField] Transform transPlayer;
    [SerializeField] Transform[] transBoards;
    [SerializeField] float distance;
    [SerializeField] float k;
    int index = 1;
    [SerializeField] Text txtScore;
    [System.Serializable]
    public struct UI_END
    {
        public GameObject goMain;
        public Text txtScore;
        public Button btnRestart;
    }
    [SerializeField] UI_END uiEndGame;
    float score = 0;
    bool isPlay = true;

    public static RoadController instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        uiEndGame.btnRestart.onClick.AddListener(() =>
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        });
        InitBoad(transBoards[0].gameObject, Random.Range(0, 5), Random.Range(0, 1));
        InitBoad(transBoards[1].gameObject, Random.Range(0, 5), Random.Range(1, 3));
        InitBoad(transBoards[2].gameObject, Random.Range(0, 5), Random.Range(1, 3));
    }
    // Update is called once per frame
    void Update()
    {
        if(!isPlay) return;
        if(transPlayer.position.z > transBoards[index].position.z + k)
        {
            int pre = (index + 2) % transBoards.Length;
            int next = (index + 1)%transBoards.Length;
            transBoards[pre].position = new Vector3(transBoards[index].position.x, transBoards[index].position.y, transBoards[next].position.z + distance);
            InitBoad(transBoards[pre].gameObject, Random.Range(0, 5), Random.Range(0, 3));
            index = next;
        }
        score += Time.deltaTime;
        txtScore.text = "Your score: " + ((int)score).ToString();
    }
    void InitBoad(GameObject go, int n, int m)
    {
        for (int i = 0; i < go.transform.GetChild(0).childCount; i++)
        {
            go.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < go.transform.GetChild(1).childCount; i++)
        {
            go.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
            for(int j=0; j< go.transform.GetChild(1).GetChild(i).childCount; j++)
            {
                go.transform.GetChild(1).GetChild(i).GetChild(j).gameObject.SetActive(false);
            }
        }
        List<int> t = new List<int>();
        for(int i=0; i<n; i++)
        {
            while (true)
            {
                int x = Random.Range(0, go.transform.GetChild(0).childCount);
                if(!t.Contains(x))
                {
                    t.Add(x);
                    break;
                }
            }
            go.transform.GetChild(0).GetChild(t[i]).gameObject.SetActive(true);
        }

        t.Clear();
        for (int i = 0; i < m; i++)
        {
            while (true)
            {
                int x = Random.Range(0, go.transform.GetChild(1).childCount);
                if (!t.Contains(x))
                {
                    t.Add(x);
                    break;
                }
            }
            go.transform.GetChild(1).GetChild(t[i]).gameObject.SetActive(true);
            go.transform.GetChild(1).GetChild(t[i]).GetChild(Random.Range(0, go.transform.GetChild(1).GetChild(t[i]).childCount)).gameObject.SetActive(true);
        }
    }
    public void EndGame()
    {
        txtScore.gameObject.SetActive(false);
        isPlay = false;
        uiEndGame.goMain.SetActive(true);
        uiEndGame.txtScore.text = "Your score: " + ((int)score).ToString();
    }
}
