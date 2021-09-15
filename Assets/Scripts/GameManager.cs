using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> Rooms = new List<GameObject>();

    [Header("Setup")]
    public UIManager UIManager;
    public GameObject GhostObject;
    public GameObject Player;

    [Header("Variables")]
    public float minSpawnTime;
    public float maxSpawnTime;

    public static bool ghostIsSpawned;
    public static bool hauntedRoomFound;
    private float remainingTime;
    private float updateTime = 5f;

    private GameObject safeRoom;
    private GameObject hauntedRoom;

    public static string CurrentRoom;

    private GameObject Ghost;

    private void Awake()
	{
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //StopGhost();
        ghostIsSpawned = false;
	}

    public void StartGame()
    {
        safeRoom = Rooms.ToArray()[0];
        hauntedRoom = GetRandRoom(safeRoom);

        hauntedRoom.GetComponent<Room>().SetHaunted(true);
        safeRoom.GetComponent<Room>().SetSafeZone(true);

        Debug.Log("Haunted Room: " + hauntedRoom.GetComponent<Room>().Name);
        Debug.Log(hauntedRoom.GetComponent<Room>().checkHaunted());

        Debug.Log("Safe Room: " + safeRoom.GetComponent<Room>().Name);
        Debug.Log(safeRoom.GetComponent<Room>().checkSafeZone());

        hauntedRoom.gameObject.tag = "Haunted";
        safeRoom.gameObject.tag = "Safe";

        UIManager.HideSM();

        remainingTime = GetRandTime(minSpawnTime, maxSpawnTime);
        StartCoroutine("StartHunt");
    }

    /// <summary>
    /// Gets a random room from the Rooms list
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandRoom()
    {
        return Rooms[Random.Range(0, Rooms.Count)];
    }

    /// <summary>
    /// Gets a random unused room
    /// </summary>
    /// <param name="_usedRoom">This room will not be picked in this methon</param>
    /// <returns></returns>
    private GameObject GetRandRoom(GameObject _usedRoom)
    {
        GameObject tempRoom;

        do
        {
            tempRoom = GetRandRoom();
        } while (tempRoom.Equals(_usedRoom));

        return tempRoom;
    }

    /// <summary>
    /// Controls the timing of the Ghost Hunting System
    /// </summary>
    /// <returns>Repeates every 5 seconds</returns>
    private IEnumerator StartHunt()
    {
        if (ghostIsSpawned)
        {
            remainingTime = GetRandTime(minSpawnTime, maxSpawnTime);
        }
		else if (remainingTime > 0 && !ghostIsSpawned)
		{
            remainingTime -= updateTime;
		}
		else if (remainingTime <= 0 && !ghostIsSpawned)
		{
            SpawnGhost();
		}

        if(remainingTime < 10 && remainingTime > 0)
		{
            Player.GetComponent<AudioSource>().Play();
		}

        Debug.Log(remainingTime);

        yield return new WaitForSeconds(updateTime);

        StartCoroutine("StartHunt");
    }

    /// <summary>
    /// Returns a random time for hunt
    /// </summary>
    /// <param name="_min">Minimum time for the hunt to start</param>
    /// <param name="_mix">Maximum time for the hunt to start</param>
    /// <returns>Float which represents the time</returns>
    private float GetRandTime(float _min, float _mix)
    {
        return Random.Range(_min, _mix);
    }

    /// <summary>
    /// Spawns the ghost when hunt starts
    /// </summary>
    public void SpawnGhost()
	{
        //ToDo -> Show ghost and set location to haunted room
        Ghost = Instantiate(GhostObject, hauntedRoom.transform.position, Quaternion.identity);
        ghostIsSpawned = true;
	}

    /// <summary>
    /// Desables the ghost on Awake to hide the ghost
    /// </summary>
    public void StopGhost()
	{
        GhostObject.SetActive(false);
        ghostIsSpawned = false;
    }

    public static void FinishGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
