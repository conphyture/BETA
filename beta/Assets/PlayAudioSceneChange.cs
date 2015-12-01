using UnityEngine;
using System.Collections;

public class PlayAudioSceneChange : MonoBehaviour {

    public string next_level;
    public AudioClip clip1;
    public AudioClip clip2;
    private AudioSource source;
    private int part;
    private int numparts;
    private bool isCoroutineExecuting;

    // Use this for initialization
    void Start () {
        part = 0;
        numparts = clip2 ? 1 : 2;
        source = GetComponent<AudioSource>();
        source.clip = clip1;
        source.Play();
        isCoroutineExecuting = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
            Application.LoadLevel(0);
        if (!source.isPlaying)
        {
            if (part < numparts)
            {
                ++part;
                source.clip = clip2;
                source.PlayDelayed(60);
            }
            else if (next_level.Length > 0)
                StartCoroutine(ExecuteLoadLevelAfterTime(numparts/2.0f));
        }
	}

    IEnumerator ExecuteLoadLevelAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        Application.LoadLevel(next_level);

        isCoroutineExecuting = false;
    }
}
