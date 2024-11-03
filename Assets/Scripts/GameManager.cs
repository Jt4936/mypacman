﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static int m_score = 0;
    public static int m_highScore = 0;
    public static float m_duration = 0f;
    public static GameManager Instant;
    public int m_eatedDot = 0;
    public Text m_scoreText;
    public Text m_highScoreText;
    public Text m_lifeText;
    public Text m_self_duration;
    public Text m_countdown;
    private float m_level_start;

    public static int m_maxLevel = 10;

    public static bool m_paused = true;

    public static bool m_muted = false;
    public Transform m_muteButton;
    public Sprite m_muteSprite;
    public Sprite m_audioStartSprite;

    public AudioSource m_backgroundMusic;

    public static bool m_ButtonMode;
    public static bool m_RockerMode;
    public  Toggle m_ButtonToggle;
    public Toggle m_RockerToggle;

    public GameObject m_settingGameObject;
	// Use this for initialization
	void Start () {
        Instant = this;
        m_countdown.text = "";
        m_level_start = Time.realtimeSinceStartup;
        if (GameObject.FindGameObjectWithTag("Restart"))
        {
            Ghost.m_Restart = GameObject.FindGameObjectWithTag("Restart");
            Ghost.m_Restart.SetActive(false);
        }
        if (m_highScoreText != null)
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                m_highScore = PlayerPrefs.GetInt("HighScore");
            }
            else
            {
                m_highScore = 0;
            }
            m_highScoreText.text = "High Score: " + m_highScore.ToString(); 
        }
        if (m_backgroundMusic != null)
        {
            m_backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        }
      


        if (m_ButtonToggle != null && m_RockerToggle != null)
        {
            if (PlayerPrefs.HasKey("Mode"))
            {
                int mode = PlayerPrefs.GetInt("Mode");
                if (mode == 0)
                {
                    m_ButtonMode = true;
                    m_RockerMode = false;
                    m_ButtonToggle.isOn = true;
                    m_RockerToggle.isOn = false;
                }
                else if (mode == 1)
                {
                    m_ButtonMode = false;
                    m_RockerMode = true;
                    m_ButtonToggle.isOn = false;
                    m_RockerToggle.isOn = true;
                }
            }
            else
            {
                m_ButtonMode = true;
                m_RockerMode = false;
                m_ButtonToggle.isOn = true;
                m_RockerToggle.isOn = false;
                PlayerPrefs.SetInt("Mode", 0);
            }
        }
      

        if (m_muted == true)
        {
            m_muteButton.GetComponent<Image>().sprite = m_audioStartSprite;
            AudioSource sound = GameObject.Find("Sound").GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.mute = true;
            }
            if (m_backgroundMusic != null)
            {
                m_backgroundMusic.mute = true;
            }
           
        }   
	}
	
	// Update is called once per frame
	void Update () {
        //m_countdown.text = "";
        if (m_paused)
        {
            float interval = Time.realtimeSinceStartup - m_level_start;
            Debug.Log("start level====>" + interval);
            if (interval > 5)
            {
                m_countdown.text = "";
                Pause();
            }
            else if (interval > 3)
            {
                m_countdown.text = "Go!";
            }
            else if (interval > 2)
            {
                m_countdown.text = "1";
            }
            else if (interval > 1)
            {
                m_countdown.text = "2";
            }
            else
            {
                m_countdown.text = "3";
            }
        }

        if (m_lifeText!=null)
        {
            m_lifeText.text = "X " + PacStudent.m_life;
        }
     

	}
    public void ReStart()
    {
        SceneManager.LoadScene("ChooseLevel");
        PacStudent.m_life = PacStudent.m_maxLife; 
    }
    public void AddScore(int score)
    {
        m_score += score;
        m_scoreText.text = "Score:" + m_score;
        if (m_score > m_highScore)
        {
            m_highScore = m_score;
        }
        m_scoreText.text = "Score:" + m_score;
        m_highScoreText.text = "HighScore:" + m_highScore;
        GameObject.Find("pacStudent").GetComponent<PacStudent>().PlayClip("eat", 1);
    }

    public void GhostRevenge(GameObject ghost)
    {
        StartCoroutine(Revenge(ghost));
    }

    IEnumerator Revenge(GameObject ghost)
    {
        yield return new WaitForSeconds(10);
        ghost.transform.position = Vector2.zero;
        ghost.SetActive(true);
    }

    public void EatDot()
    {
        m_eatedDot++;  
    }

    public void Win()
    {
        string SceneName = PlayerPrefs.GetString("Scene");
        int sceneNum = int.Parse(SceneName);
        sceneNum++;
        if (sceneNum <= m_maxLevel)
        {
            PlayerPrefs.SetString("Scene", sceneNum.ToString().PadLeft(2, '0'));
            SceneManager.LoadScene("Load");
        }
        else
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Pause()
    {
        if (m_paused == false)
        {
            m_paused = true;
        }
        else
        {
            m_paused = false;
        }
    }

    public void Mute()
    {
        if (m_muted == false)
        {
            m_muteButton.GetComponent<Image>().sprite = m_audioStartSprite;
            AudioSource sound = GameObject.Find("Sound").GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.mute = true;
            }
            if (m_backgroundMusic != null)
            {
                m_backgroundMusic.mute = true;
            }
            m_muted = true;
        }
        else
        {
            m_muteButton.GetComponent<Image>().sprite = m_muteSprite;
            AudioSource sound = GameObject.Find("Sound").GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.mute = false;
            }
            if (m_backgroundMusic != null)
            {
                m_backgroundMusic.mute = false;
            }
            m_muted = false;
        }
    }

    public void OnButtonToggleStateChanged()
    {
        if (m_ButtonToggle.isOn == true)
        {
            m_RockerToggle.isOn = false;
            m_RockerMode = false;
            m_ButtonMode = true;
            PlayerPrefs.SetInt("Mode", 0);
        }
        else
        {
            m_RockerToggle.isOn = true;
            m_RockerMode = true;
            m_ButtonMode = false;
            PlayerPrefs.SetInt("Mode", 1);
        }
    }

    public void OnRockerToggleStateChanged()
    {
        if (m_RockerToggle.isOn == true)
        {
            m_ButtonToggle.isOn = false;
            m_RockerMode = true;
            m_ButtonMode = false;
            PlayerPrefs.SetInt("Mode", 1);
        }
        else
        {
            m_ButtonToggle.isOn = true;
            m_RockerMode = false;
            m_ButtonMode = true;
            PlayerPrefs.SetInt("Mode", 0);
        }
    }

    public void OnSetting()
    {
        if (m_settingGameObject.activeSelf == false)
        {
            m_settingGameObject.SetActive(true);
        }
        else
        {
            m_settingGameObject.SetActive(false);
        }
    }

    public void LoadLScene1()
    {
        SceneManager.LoadScene("01");
        PacStudent.m_life = 3;
        m_level_start = Time.realtimeSinceStartup;
    }

    public void LoadLScene2()
    {

        SceneManager.LoadScene("02");
        PacStudent.m_life = 3;
    }

    public void GoIntroductionScene()
    {
        SceneManager.LoadScene("Introduction");
    }
    public void SaveHighScore()
    {
        int oldHigh;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            oldHigh = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            oldHigh = 0;
        }
        if (m_highScore > oldHigh)
        {
            PlayerPrefs.SetInt("HighScore",m_highScore);
        }
    }
    public void GoHome()
    {
        SceneManager.LoadScene("Start");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        print("Game Over....");
    }
}
