using System.Collections.Generic;
using UnityEngine;

public class GameBiheviour : MonoBehaviour
{
    public List<Chunk> CurrentChunks;
    public RocketMoving Rocket;
    public Transform Block;
    public float Distance;
    public int CountChunksAtStart;
    public GameObject CrashAnimation;
    public static GameBiheviour singleton;
    public float Timer;
    public float BestScore
    {
        get
        {
            if (!PlayerPrefs.HasKey("Score_Version_0.95")) return 0;
            return PlayerPrefs.GetFloat("Score_Version_0.95");
        }
        set
        {
            PlayerPrefs.SetFloat("Score_Version_0.95", value);
        }
    }
    public float CurrentSpeed;
    public float StartSpeed;
    public bool lockSpeed;
    public void Start()
    {
        singleton = this;
        UIBIheviour.singleton.newGameUI.SetUI(BestScore.ToString("#.##"));
        UIBIheviour.singleton.optionUI.Init();
    }

    void InitChunks()
    {
        for (int i = 0; i < CountChunksAtStart; i++)
        {
            CurrentChunks.Add(ChunkPool.GetChunk());
        }

    }

    void Update()
    {
        CheckLast();
        Timer += Time.deltaTime;
        if (Rocket.enabled)
        {
            UIBIheviour.singleton.scoreUI.SetUI(Timer.ToString("#.#"));
        }
    }
    public Vector2 CurrentDistance;

    void CheckLast()
    {
        if (CurrentChunks.Count <= 1) return;
        CurrentDistance.x = Mathf.Abs(Block.transform.position.x - Rocket.transform.position.x);
        CurrentDistance.y = Mathf.Abs(Block.transform.position.y - Rocket.transform.position.y);
        if (CurrentDistance.x > Distance || CurrentDistance.y > Distance)
        {
            var chunk = CurrentChunks[0];


            Block.position = chunk.transform.position;
            CurrentChunks.Remove(chunk);
            chunk.ChunkDisable();
            CurrentChunks.Add(ChunkPool.GetChunk());
            if (!lockSpeed)
                ChangeSpeed();
        }
    }

    public void ChangeSpeed()
    {
        lockSpeed = false;
        int delta = ChunkPool.number / 10;
        CurrentSpeed = StartSpeed + delta * 10;
        Rocket.FlySpeed = CurrentSpeed;
    }

    public void Boom()
    {
        CrashAnimation.SetActive(true);
        Pause();
    }
    public void EndAnimationBoom()
    {
        if (BestScore < Timer)
            BestScore = Timer;
        UIBIheviour.singleton.crashedUI.SetUI(Timer.ToString("#.##"));
        UIBIheviour.singleton.scoreUI.gameObject.SetActive(false);
        UIBIheviour.singleton.pauseUI.gameObject.SetActive(false);
    }


    void Pause()
    {
        Rocket.enabled = false;
    }


    void UnPause()
    {
        Rocket.ResetTrails();
        Rocket.enabled = true;
    }
    public void Restart()
    {
        ChunkPool.CountStart = 1;
        ChunkPool.CurrentPos = Vector2.zero;
        ChunkPool.CurrentDirection = Direction.Forward;
        CurrentChunks.ForEach(x => x.ChunkDisable());
        CurrentChunks.Clear();
        Rocket.transform.rotation = Quaternion.identity;
        Rocket.transform.position = new Vector3(0, 150, 0);
        Block.transform.position = new Vector3(0, -73, 0);
        InitChunks();
        CrashAnimation.SetActive(false);
        Timer = 0;
        ChunkPool.number = 0;
        UIBIheviour.singleton.scoreUI.gameObject.SetActive(true);
        UIBIheviour.singleton.pauseUI.gameObject.SetActive(true);
        ChangeSpeed();

        UnPause();
    }

}