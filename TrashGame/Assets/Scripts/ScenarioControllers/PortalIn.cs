using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIn : MonoBehaviour
{
    public List<GameObject> trashItemPrefabs; // List of TrashItem prefabs
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTrashItemRoutine());
    }

    /// <summary>
    /// Spawns TrashItems with a one second delay between spawns
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnTrashItemRoutine()
    {
        while (true) // This will keep spawning TrashItems indefinitely
        {
            GameObject randomTrashItemPrefab = trashItemPrefabs[Random.Range(0, trashItemPrefabs.Count)];
            SpawnTrashItem(randomTrashItemPrefab);
            yield return new WaitForSeconds(spawnTime); // Adjust the interval as needed
        }
    }
    /// <summary>
    /// Spawns a TrashItem
    /// </summary>
    /// <param name="trashItemPrefab"></param>
    void SpawnTrashItem(GameObject trashItemPrefab)
    {
        Vector3 spawnPosition = transform.position + new Vector3(-1f, 0f, 0f); // Offset by -10 on the X-axis relative to the PortalIn object
        Instantiate(trashItemPrefab, spawnPosition, Quaternion.identity);
    }
}
