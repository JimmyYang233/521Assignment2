using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PerlinNoise
{
    private long seed;
    private float pixelSize;
    private int totalChunk;

    public PerlinNoise(long seed, float pixelSize)
    {
        this.seed = seed;
        this.pixelSize = pixelSize;
    }

    private float random(int x, int range)
    {    
        float ans = ((x*100+seed)^500) % range;
        //Debug.Log(range);
        return ans;
    }

    public int getNoise(int x, int range)
    {
        int chunkSize = 30;

        float noise = 0;

        range /= 2;

        while (chunkSize > 0)
        {
            int chunkIndex = x/chunkSize;
            int lastIndex = ((int)(6f/pixelSize)-1)/chunkSize;
            //Debug.Log("ChunkIndex " + chunkIndex + ", LastIndex " + lastIndex);
            float prog = (x % chunkSize) / (chunkSize * 1f);
            //Debug.Log(chunkIndex);
            float left_random;
            float right_random;
            if (chunkIndex == 0)
            {
                left_random = 0;
            }
            else
            {
                left_random = random(chunkIndex, range);
            }

            if (chunkIndex == lastIndex)
            {
                right_random = 0;
            }
            else
            {
                right_random = random(chunkIndex + 1, range);
            }
            /**
            if (chunkIndex > lastIndex)
            {
                Debug.Log("X : " + x+ ", chunkSize : " + chunkSize);
            }
            **/
            //Debug.Log(left_random + ", " + right_random);
            //noise = (1 - prog) * left_random + prog * right_random;
            noise += (1 - prog) * left_random + prog * right_random;

            chunkSize /= 2;
            range /= 2;
        }
        
        return (int) Mathf.Round(noise);
    }
}
