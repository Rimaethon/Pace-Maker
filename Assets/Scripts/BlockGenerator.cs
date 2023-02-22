using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

    public float blockSpeed = 5f; // Speed at which the blocks move
    public GameObject blockPrefab; // Prefab of the block to be generated
    public Transform[] blockPositions; // Positions at which the blocks can appear
    public float beatTolerance = 0.1f; // Tolerance for detecting beats
    public AudioClip musicClip; // Audio clip of the music to be played
    public float musicOffset = 0.1f; // Offset to synchronize the music and the blocks
    public float timeToLive = 10f; // Time to live for the blocks

    private float beatTimer; // Timer for detecting beats
    private AudioSource musicSource; // Audio source for playing the music
    private List<GameObject> blocks = new List<GameObject>(); // List of generated blocks

    private void Start() {
        // Initialize the audio source
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.playOnAwake = false;
        musicSource.time = musicOffset;

        // Start playing the music
        musicSource.Play();
    }

    private void Update() {
        // Move the character
        transform.Translate(Vector3.right * Time.deltaTime * blockSpeed);

        // Detect beats
        float spectrumValue = AudioSpectrum.spectrumValue;
        if (spectrumValue >= beatTolerance && beatTimer <= 0f) {
            GenerateBlock();
            beatTimer = 1f;
        }
        beatTimer -= Time.deltaTime;

        // Move the blocks
        for (int i = blocks.Count - 1; i >= 0; i--) {
            GameObject block = blocks[i];
            block.transform.Translate(Vector3.back * Time.deltaTime * blockSpeed);
            if (block.transform.position.z < -10f || Time.time - block.GetComponent<Block>().creationTime > timeToLive) {
                Destroy(block);
                blocks.RemoveAt(i);
            }
        }
    }

    private void GenerateBlock() {
        // Get a random block position
        int blockPositionIndex = Random.Range(0, blockPositions.Length);
        Vector3 blockPosition = blockPositions[blockPositionIndex].position;

        // Instantiate the block
        GameObject block = Instantiate(blockPrefab, blockPosition, Quaternion.identity);

        // Add the block to the list
        blocks.Add(block);
    }

}

