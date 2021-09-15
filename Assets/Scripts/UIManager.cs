using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Debug Tools")]
    public Text xText;
    public Text yText;
    public Text zText;

    public GameObject ARCoreRoot;

    [Header("Create Room Menu")]
    public GameObject CreateRoomMenuPanel;
    public InputField RoomNameInput;
    public InputField RoomSizeInput;

    [Header("Start Panel")]
    public GameObject StartMenuPanel;
    public Button CreateRoomButton;
    public Button StartGameButton;

    [Header("Game UI")]
    public Text CurrentRoomText;
    public Button SpawnGhostButton;

    // Start is called before the first frame update
    void Start()
    {
        HideCRM();
    }

    // Update is called once per frame
    void Update()
    {
        xText.text = "X: " + (int)ARCoreRoot.transform.position.x;
        yText.text = "Y: " + (int)ARCoreRoot.transform.position.y;
        zText.text = "Z: " + (int)ARCoreRoot.transform.position.z;

        foreach(GameObject room in GameManager.Rooms)
		{
			if (room.GetComponent<Room>().Name.Equals(GameManager.CurrentRoom))
			{
				switch (room.tag)
				{
                    case "Safe":
                        CurrentRoomText.color = Color.green;
                        break;
                    case "Haunted":
                        CurrentRoomText.color = Color.red;
                        break;
					default:
                        CurrentRoomText.color = Color.white;
                        break;
				}
			}
		}

        CurrentRoomText.text = "Current Room: " + GameManager.CurrentRoom;
    }

    /// <summary>
    /// Hides the Create Room Menu
    /// </summary>
    public void HideCRM()
	{
        RoomNameInput.text = "";
        RoomSizeInput.text = "";
        CreateRoomMenuPanel.SetActive(false);
        ShowSMB();
	}

    /// <summary>
    /// Shows the Create Room Menu
    /// </summary>
    public void ShowCRM()
	{
        CreateRoomMenuPanel.SetActive(true);
        HideSMB();
	}

    /// <summary>
    /// Hides the Start Canvas
    /// </summary>
    public void HideSM()
    {
        StartMenuPanel.SetActive(false);
    }

    /// <summary>
    /// Hides Start Menu Buttons:
    /// - Create Room Button
    /// - Start Game Button
    /// </summary>
    public void HideSMB()
	{
        CreateRoomButton.gameObject.SetActive(false);
        StartGameButton.gameObject.SetActive(false);

    }


    /// <summary>
    /// Hides Start Menu Buttons:
    /// - Create Room Button
    /// - Start Game Button
    /// </summary>
    public void ShowSMB()
    {
        CreateRoomButton.gameObject.SetActive(true);
        StartGameButton.gameObject.SetActive(true);
    }

    public void SetCRTColor(Color _color)
	{
        CurrentRoomText.color = _color;
	}

    public void ShowSGB()
	{
        SpawnGhostButton.gameObject.SetActive(true);
	}
    
    public void HideSGB()
	{
        SpawnGhostButton.gameObject.SetActive(false);
	}
}
