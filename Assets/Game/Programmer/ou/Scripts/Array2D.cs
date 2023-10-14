using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array2D : MonoBehaviour
{
    public int width;
    public int height;
    private int[,] data;

    public Array2D(int w, int h)
    {
        width = w; height = h;
        data = new int[width, height];
    }

    /**
    * X/Z���W�ɂ���l���擾����
    */
    public int Get(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return data[x, z];
        }
        return -1;
    }

    /**
    * X/Z���W�ɒl(v)��ݒ肷��
    */
    public int Set(int x, int z, int v)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            data[x, z] = v;
            return v;
        }
        return -1;
    }
}
