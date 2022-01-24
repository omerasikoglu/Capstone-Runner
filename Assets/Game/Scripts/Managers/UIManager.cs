using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        //private Transform youWinUI;
        private Transform tapToStartUI;
        private Transform gameOverUI;
        private Transform inGameUI;

        private TextMeshProUGUI textMesh;
        private TextMeshProUGUI textMeshScore;

        private int currentScore = 0;



        protected void Awake()
        {
            Instance = this;

            Init();

            SetActiveTapToStartUI();
            UpdateScore();
        }

        private void Init()
        {
            //youWinUI = transform.Find(StringData.YOUWIN_UI);
            tapToStartUI = transform.Find(StringData.TAPTOSTART_UI);
            gameOverUI = transform.Find(StringData.GAMEOVER_UI);
            inGameUI = transform.Find(StringData.INGAME_UI);
            textMesh = inGameUI.Find(StringData.BACKGROUND).Find(StringData.TEXT).GetComponent<TextMeshProUGUI>();
            textMeshScore = gameOverUI.Find(StringData.BACKGROUND).Find(StringData.TEXT).GetComponent<TextMeshProUGUI>();
        }

        public void UpdateScore(int amount = 0)
        {
            currentScore += amount;
            textMesh.SetText(currentScore.ToString());
        }

        public void SetDeactiveTapToStartUI()
        {
            tapToStartUI.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        public void SetActiveTapToStartUI()
        {
            tapToStartUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        public void SetActiveGameOverUI()
        {
            gameOverUI.gameObject.SetActive(true);
            //Time.timeScale = 0f;
            textMeshScore.SetText("(Total Score:" + currentScore + ")");
        }
        public void SetActiveInGameUI()
        {
            inGameUI.gameObject.SetActive(true);
        }
    }