using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public string Name;
	public float Size;

	private Vector3 Position;

	private bool isHaunted;
	private bool isSafeZone;

	public GameObject TrackerPrefab;
	public GameObject GhostPrefab;


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(Position, Size);
	}

	public void SetHaunted(bool _isHaunted)
	{
		isHaunted = _isHaunted;
	}

	public void SetSafeZone(bool _isSafeZone)
	{
		isSafeZone = _isSafeZone;
	}

	private void Start()
	{
		SphereCollider sc = this.gameObject.AddComponent<SphereCollider>();
		sc.radius = Size;
	}

	private void OnCollisionEnter(Collision collision)
	{

	}

	public Room()
	{
		Name = "";
		Size = 0;
		Position = new Vector3();
		isHaunted = false;
		isSafeZone = false;
	}

	public Room(string _name, float _size, Vector3 _position)
	{
		Name = _name;
		Size = _size;
		Position = _position;
		isHaunted = false;
		isSafeZone = false;
	}

	public bool checkHaunted()
	{
		return isHaunted;
	}

	public bool checkSafeZone()
	{
		return isSafeZone;
	}

	private void Update()
	{
		// Check if player is in the room
		if (this.gameObject.GetComponent<Collider>().bounds.Contains(GameObject.FindGameObjectWithTag("Player").transform.position))
		{
			GameManager.CurrentRoom = Name;

			if (isHaunted)
			{
				GameManager.hauntedRoomFound = true;
			}

			if (GameManager.hauntedRoomFound && isSafeZone)
			{
				GameManager.FinishGame();
			}
		}
		else if(GameManager.CurrentRoom.Equals(Name))
		{
			GameManager.CurrentRoom = "";
		}


		// Check if ghost is in the room
		if (GameManager.Rooms.ToArray()[0].GetComponent<Collider>().bounds.Contains(GameObject.FindGameObjectWithTag("Ghost").transform.position))
		{
			Destroy(GameObject.FindGameObjectWithTag("Ghost"));
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
		}
	}
}
