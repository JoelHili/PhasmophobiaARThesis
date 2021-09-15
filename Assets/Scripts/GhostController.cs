using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("Set Up")]
    public static GameObject Player;

    [Header("Adjustable")]
    public float MoveSpeed;
    public float Timeout;

    private float remainingTime;


    void Awake()
	{
        Player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        if (this.gameObject.GetComponent<Collider>().bounds.Contains(GameObject.FindGameObjectWithTag("Player").transform.position))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Counts down the timeout timer every second
    /// </summary>
    /// <returns></returns>
    private IEnumerable TimeoutCountDown()
	{
        remainingTime -= 1f;

        yield return new WaitForSeconds(1f);
	}

    /// <summary>
    /// Starts counting down from the moment hunting starts
    /// </summary>
    public void StartTimeout()
	{
        remainingTime = Timeout;

        StartCoroutine("TimeoutCountDown");
	}

    /// <summary>
    /// Stops Ghost Hunt
    /// </summary>
    private void EndHunt()
	{
        GameManager.ghostIsSpawned = false;
        Player.GetComponent<AudioSource>().Stop();
        Destroy(this.gameObject);
    }
}
