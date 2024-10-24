using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ChickenGameController;

public class ChickenGameController : MonoBehaviour
{
    public enum TYPE_BTN
    {
        TYPE_BTN_BACK,
        TYPE_BTN_VOICE,
        TYPE_BTN_TAP,
        TYPE_BTN_CHOOSE_1,
        TYPE_BTN_CHOOSE_2,
        TYPE_BTN_CHOOSE_3,
        TYPE_BTN_NO,
    }
    [SerializeField] Transform transParentMaps;
    int lvMap = 1;
    [SerializeField] Transform transPlayer;

    [System.Serializable]
    public struct UI_Start
    {
        public GameObject goMain;
        public Button btnBack;
        public Button btnVoice;
        public Button btnTap;
        public TextMeshProUGUI txtLv;
    }
    [SerializeField] UI_Start ui_start;
    public bool isTap { get; private set; }

    [System.Serializable]
    public struct UI_ChooseDuck
    {
        public GameObject goMain;
        public Transform transDuckCurrent;
        public Transform transBtns;
        public Button btnNo;
    }
    [SerializeField] UI_ChooseDuck ui_ChooseDuck;


    [SerializeField] GameObject goAudioSoundEffect;
    [SerializeField] List<AudioClip> audioClips;
    bool isPlaySound = false;

    // Start is called before the first frame update
    void Start()
    {
        lvMap = PlayerPrefs.GetInt("LV_MAP", 0);
        transParentMaps.GetChild(lvMap).gameObject.SetActive(true);
        transPlayer.position = transParentMaps.GetChild(lvMap).GetChild(0).position;

        ShowUiStart();
    }

    void ShowUiStart()
    {
        ui_start.txtLv.text = "LEVEL " + lvMap;
        ui_start.goMain.SetActive(true);
        ui_start.btnBack.onClick.AddListener(() =>
        {
            StartCoroutine(ActiveBtn(TYPE_BTN.TYPE_BTN_BACK));
        });
        ui_start.btnVoice.onClick.AddListener(() =>
        {
            StartCoroutine(ActiveBtn(TYPE_BTN.TYPE_BTN_VOICE));
        });
        ui_start.btnTap.onClick.AddListener(() =>
        {
            StartCoroutine(ActiveBtn(TYPE_BTN.TYPE_BTN_TAP));
        });
    }
    void ShowUiChoose()
    {
        ui_start.goMain.SetActive(false);
        ui_ChooseDuck.goMain.SetActive(true);
    }
    IEnumerator ActiveBtn(TYPE_BTN value)
    {
        if (!isPlaySound)
        {
            isPlaySound = true;
            GameObject go = Instantiate(goAudioSoundEffect);
            go.transform.GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
            yield return new WaitForSeconds(audioClips[0].length);
            Destroy(go);
            isPlaySound = false;
            SetActiveBtn(value);
        }
    }
    void SetActiveBtn(TYPE_BTN value)
    {
        switch (value)
        {
            case TYPE_BTN.TYPE_BTN_BACK:
                {
                    Application.Quit();
                    break;
                }
            case TYPE_BTN.TYPE_BTN_VOICE:
                {
                    isTap = false;
                    ShowUiChoose();
                    break;
                }
            case TYPE_BTN.TYPE_BTN_TAP:
                {
                    isTap = true;
                    break;
                }
            case TYPE_BTN.TYPE_BTN_CHOOSE_1:
                {

                    break;
                }
            case TYPE_BTN.TYPE_BTN_CHOOSE_2:
                {

                    break;
                }
            case TYPE_BTN.TYPE_BTN_CHOOSE_3:
                {

                    break;
                }
            case TYPE_BTN.TYPE_BTN_NO:
                {

                    break;
                }
        }
    }
}
