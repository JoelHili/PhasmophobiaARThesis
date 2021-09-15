using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour
{
    [Header("Input")]
    public GameObject Player;
    public GameObject RoomPrefab;

    [Header("Create Room Menu")]
    public InputField RoomNameInput;
    public InputField RoomSizeInput;

    [Header("Other Settings")]
    public UIManager UIManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Creates Room From "AddRoomButton"
    /// </summary>
    public void CreateRoom()
	{
        if(RoomNameInput.text.Length > 0 && RoomSizeInput.text.Length > 0)
		{
            Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            GameObject newRoom = Instantiate(RoomPrefab, pos, Quaternion.identity);
            newRoom.GetComponent<Room>().Name = RoomNameInput.text;
            newRoom.GetComponent<Room>().Size = float.Parse(RoomSizeInput.text)/5;

			try
			{
                GameManager.Rooms.Add(newRoom.gameObject);
            }
            catch(Exception e)
			{
                Debug.Log("Error:" + e);
			}

            UIManager.HideCRM();
        }
	}
}
