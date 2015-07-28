using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GUISkin GameSkin;
    public Camera GameCamera;

    private bool isFirstMenu = true;
    private bool isLoadGameMenu = false;
    private bool isSettingsMenu = false;
    private bool isNewGameMenu = false;
    private bool isAudioOptions = false;
    private bool isGraphicsOptions = false;

    private string gameTitle = "Mr Owls Wild Ride";
    private string playerName = "";
    private string currentLevel = "Start";

    // The volume goes from 0 to 1
    private float gameVolume = 0.6f;
    private float gameFOV = 60.0f;


    void Start()
    {
        //PlayerPrefs.DeleteAll();

        gameVolume = PlayerPrefs.GetFloat("Game Volume", gameVolume);
        gameFOV = PlayerPrefs.GetFloat("Game FOV", gameFOV);

        if (PlayerPrefs.HasKey("Game Volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Game Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Game Volume", gameVolume);
        }

        /*
        if (PlayerPrefs.HasKey("Game FOV"))
        {
            GameCamera.fieldOfView = PlayerPrefs.GetFloat("Game FOV");
        }
        else
        {
            PlayerPrefs.SetFloat("Game FOV", gameFOV);
        }*/
    }

    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = GameSkin;

        GUI.Label(new Rect(30, 75, 300, 25), gameTitle, "Menu Title");

        FirstMenu();
        LoadGameMenu();
        SettingsMenu();
        NewGameOptions();
        AudioOptionsDisplay();
        GraphicOptionsDisplay();

        if (isLoadGameMenu == true || isSettingsMenu == true || isNewGameMenu == true)
        {
            if (GUI.Button(new Rect(10, Screen.height - 35, 150, 25), "Back"))
            {
                isLoadGameMenu = false;
                isSettingsMenu = false;
                isNewGameMenu = false;
                isAudioOptions = false;
                isGraphicsOptions = false;

                isFirstMenu = true;
            }
        }
    }

    void FirstMenu()
    {
        if (isFirstMenu)
        {
            if (GUI.Button(new Rect(10, Screen.height / 2 - 100, 150, 25), "New Game"))
            {
                isFirstMenu = false;
                isNewGameMenu = true;
            }
            if (GUI.Button(new Rect(10, Screen.height / 2 - 65, 150, 25), "Load Game"))
            {
                isFirstMenu = false;
                isLoadGameMenu = true;
            }
            if (GUI.Button(new Rect(10, Screen.height / 2 - 30, 150, 25), "Settings"))
            {
                isFirstMenu = false;
                isSettingsMenu = true;
            }
            if (GUI.Button(new Rect(10, Screen.height / 2 + 5, 150, 25), "Exit Game"))
            {
                // Only gonna work after the project is built
                Application.Quit();
            }

        }
    }

    void NewGameOptions()
    {
        if (isNewGameMenu)
        {
            GUI.Label(new Rect(10, Screen.height / 2 - 200, 200, 50), "New Game", "Sub Menu Title");

            GUI.Label(new Rect(10, Screen.height / 2 - 100, 90, 25), "Player Name:");
            playerName = GUI.TextField(new Rect(100, Screen.height / 2 - 100, 200, 25), playerName);

            if (playerName != "")
            {
                if (GUI.Button(new Rect(10, Screen.height / 2 - 30, 150, 25), "Create Character"))
                {
                    // Save the players name
                    PlayerPrefs.SetString("Player Name", playerName);

                    // Save the players current level
                    PlayerPrefs.SetString("Current Level", currentLevel);

                    // Load the level
                    Application.LoadLevel("Start");
                }
            }
            else
            {
                if (GUI.Button(new Rect(10, Screen.height / 2 - 30, 150, 25), "Generating"))
                {

                }
            }
        }
    }

    void LoadGameMenu()
    {
        if (isLoadGameMenu)
        {
            GUI.Label(new Rect(10, Screen.height / 2 - 200, 200, 50), "Load Game", "Sub Menu Title");
            GUI.Box(new Rect(10, Screen.height / 2 - 100, Screen.width / 2 + 100, Screen.height - 450), "Choose Saved Game");

            if (PlayerPrefs.HasKey("Player Name"))
            {
                GUI.Label(new Rect(20, Screen.height / 2 - 65, 200, 25), "Player Name: " + PlayerPrefs.GetString("Player Name"));

                if (GUI.Button(new Rect(Screen.width / 2 - 220, Screen.height / 2 - 65, 150, 25), "Load Character"))
                {
                    // Load the players current level
                    Application.LoadLevel(PlayerPrefs.GetString("Current Level"));
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 65, 150, 25), "Delete Character"))
                {
                    // Delete everything
                    PlayerPrefs.DeleteAll();
                }
            }
        }
    }

    void SettingsMenu()
    {
        if (isSettingsMenu)
        {
            GUI.Label(new Rect(10, Screen.height / 2 - 200, 200, 50), "Settings", "Sub Menu Title");

            if (isAudioOptions == true || isGraphicsOptions == true)
            {
                GUI.Box(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height), "");
            }


            if (GUI.Button(new Rect(10, Screen.height / 2 - 30, 150, 25), "Audio Settings"))
            {
                isGraphicsOptions = false;
                isAudioOptions = true;
            }

            if (GUI.Button(new Rect(10, Screen.height / 2 - 30, 150, 25), "Graphic Settings"))
            {
                isGraphicsOptions = true;
                isAudioOptions = false;

            }
        }
    }

    public void AudioOptionsDisplay()
    {
        if (isAudioOptions)
        {
            GUI.Label(new Rect(Screen.width / 2 + 10, 30, 200, 25), "Game Volume");
            gameVolume = GUI.HorizontalSlider(new Rect(Screen.width / 2 + 10, 150, Screen.width / 2 - 55, 25), gameVolume, 0.0f, 1.0f);
            GUI.Label(new Rect(Screen.width / 2 - 35, 145, 50, 25), "" + (System.Math.Round(gameVolume, 2)));
            AudioListener.volume = gameVolume;

            if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height - 35, 150, 25), "Apply"))
            {
                PlayerPrefs.SetFloat("Game Volume", gameVolume);
            }
        }
    }

    public void GraphicOptionsDisplay()
    {
        if (isGraphicsOptions)
        {
            GUI.Label(new Rect(Screen.width / 2 + 10, 30, 200, 25), "Game FOV");
            gameFOV = GUI.HorizontalSlider(new Rect(Screen.width / 2 + 10, 150, Screen.width / 2 - 55, 25), gameFOV, 40.0f, 100.0f);
            GUI.Label(new Rect(Screen.width / 2 - 35, 145, 50, 25), "" + (int)gameFOV);
            GameCamera.fieldOfView = gameFOV;

            GUILayout.BeginVertical();

            GUI.Label(new Rect(Screen.width / 2 + 10, 200, 200, 25), "Graphics Quality");
            for (int i = 0; i < QualitySettings.names.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + 30, 235 + i * 35, 150, 25), QualitySettings.names[i]))
                {
                    QualitySettings.SetQualityLevel(i, true);
                }
            }

            GUILayout.EndVertical();


            if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height - 35, 150, 25), "Apply"))
            {
                PlayerPrefs.SetFloat("Game FOV", gameFOV);
            }
        }
    }
}
