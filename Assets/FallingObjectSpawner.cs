using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;  // List of spawn points at the top of the screen
    public Sprite[] fallingSprites;  // Array of sprites for the falling objects
    public GameObject fallingObjectPrefab;  // Prefab for the falling object (with a SpriteRenderer)

    public float spawnInterval = 1f;  // Time between spawns
    public float fallSpeed = 2f;      // Speed at which the objects fall
    public float objectLifetime = 5f; // Time before the object is destroyed

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Spawn an object every spawnInterval seconds
            yield return new WaitForSeconds(spawnInterval);

            // Randomly select a spawn point and a sprite
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Sprite selectedSprite = fallingSprites[Random.Range(0, fallingSprites.Length)];

            // Instantiate the falling object at the selected spawn point
            GameObject fallingObject = Instantiate(fallingObjectPrefab, spawnPoint.position, Quaternion.identity);

            // Set the object's sprite
            SpriteRenderer spriteRenderer = fallingObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = selectedSprite;
            }

            // Make the object fall
            StartCoroutine(FallDownAndDestroy(fallingObject));
        }
    }

    IEnumerator FallDownAndDestroy(GameObject fallingObject)
    {
        float elapsedTime = 0f;

        // Continuously move the object down
        while (elapsedTime < objectLifetime)
        {
            fallingObject.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Destroy the object after it has fallen for the specified time
        Destroy(fallingObject);
    }
}
