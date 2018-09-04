using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChunkPool
{
    public static Direction CurrentDirection = Direction.Forward;
    public static Vector2 CurrentPos = Vector2.zero;
    const int CountChunks = 17;
    public static int CountStart = 1;
    private static List<Chunk> chunksPool = new List<Chunk>();
    private static List<int> _randomList;
    private static List<int> RandomList
    {
        get
        {
            if (_randomList == null)
            {
                _randomList = new List<int>();
                for (int i = 0; i < CountChunks + 1; i++)
                    _randomList.Add(i);
            }
            return _randomList;
        }
    }
    public static int number;
    public static Chunk GetChunk()
    {
        if(CountStart != 0)
        {
          
            GameObject chunk = Resources.Load<GameObject>("Chunks/Cell" + -1);
            var c = chunk.GetComponent<Chunk>();
            Vector2 PosTo, PosToStack;
            Equal(c.CurrentRotation, CurrentDirection, out PosTo, out PosToStack);
            var ResultChunk = GetChunkFromPool(-1, chunk);
            PosToStack = CurrentPos - PosToStack;
            ResultChunk.transform.position = PosToStack;

            CurrentPos = (Vector2)ResultChunk.transform.position + PosTo;
            CurrentDirection = ResultChunk.RotationTo == Direction.Same ? CurrentDirection : ResultChunk.RotationTo;

            ResultChunk.gameObject.name = number.ToString();
            number++;
            CountStart--;
            return ResultChunk;
          
        }
        while (true)
            for (int i = 0; i < CountChunks + 1; i++)
            {
                RandomList.Shuffle();
                int index = RandomList[i];
                GameObject chunk = Resources.Load<GameObject>("Chunks/Cell" + index);
                var c = chunk.GetComponent<Chunk>();
                float rnd = Random.Range(0f, 100f);
                Vector2 PosTo, PosToStack;
                if (Equal(c.CurrentRotation, CurrentDirection, out PosTo, out PosToStack) && c.Chance > rnd)
                {
                    var ResultChunk = GetChunkFromPool(index, chunk);

                    PosToStack = CurrentPos - PosToStack;
                    ResultChunk.transform.position = PosToStack;

                    CurrentPos = (Vector2)ResultChunk.transform.position + PosTo;
                    CurrentDirection = ResultChunk.RotationTo == Direction.Same ? CurrentDirection : ResultChunk.RotationTo;

                    ResultChunk.gameObject.name = number.ToString();
                    number++;
                    return ResultChunk;
                }


            }
    }

    private static Chunk GetChunkFromPool(int index, GameObject goChunk)
    {
        Chunk chunk = null;
        foreach (var item in chunksPool)
            if (item.index == index && item.IsActive == false)
            {
                chunk = item;
                item.gameObject.SetActive(true);
                break;
            }
        if (!chunk)
            chunk = Object.Instantiate(goChunk).GetComponent<Chunk>();
        chunk.IsActive = true;
        chunksPool.Add(chunk);
        return chunk;
    }
    public static void ChunkDisable(this Chunk chunk)
    {
        chunk.IsActive = false;
        chunk.gameObject.SetActive(false);
    }
    private static bool Equal(DirToMove[] x, Direction y, out Vector2 vec, out Vector2 vec2)
    {
        vec = Vector2.zero;
        vec2 = Vector2.zero;
        var chunk = x.ToList().FirstOrDefault(z => z.direction == y);
        if (chunk == null) return false;
        vec = chunk.PosTo;
        vec2 = chunk.PosToStack;
        return true;
    }
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
public enum Direction
{
    Forward,
    Backward,
    Left,
    Right,
    Same

}
