using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KoitanLib
{
    public class KoitanBuildBlurTexture
    {
        public static Texture2D ResizeTexture(Texture2D tex, int skinWidth)
        {
            Texture2D resTex = new Texture2D(tex.width + skinWidth * 2, tex.height + skinWidth * 2);
            Color[] colors = new Color[resTex.width * resTex.height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.clear;
            }
            resTex.SetPixels(colors);
            for (int y = 0; y < tex.height; y++)
            {
                int ry = y + skinWidth;
                for (int x = 0; x < tex.width; x++)
                {
                    int rx = x + skinWidth;
                    resTex.SetPixel(rx, ry, tex.GetPixel(x, y));
                }
            }
            return resTex;
        }

        public static Texture2D BuildTexture(Texture2D tex, float sig)
        {
            Texture2D resTex = ResizeTexture(tex, (int)sig * 3 + 1);
            resTex = CreateBlurTexture(resTex, sig);
            return resTex;
        }

        public static Texture2D GenerateSDF(Texture2D tex, int cnt)
        {
            Color[] colors = tex.GetPixels();
            //Texture2D resTex = new Texture2D(tex.width, tex.height);

            // 初期化
            SDFTextureGenerator sdfTG = new SDFTextureGenerator(tex);
            // 更新
            for (int i = 0; i < cnt; i++)
            {
                sdfTG.Update();
            }
            Texture2D resTex = sdfTG.GenerateTexture();
            //resTex.Apply();
            return resTex;
        }

        /// <summary>
        /// ぼかし画像を生成
        /// https://qiita.com/divideby_zero/items/4c02177a56f7d500d4c0
        /// </summary>
        /// <param name="sig">ぼかし距離</param>
        public static Texture2D CreateBlurTexture(Texture2D tex, float sig)
        {
            sig = Mathf.Max(sig, 0f);
            int W = tex.width;
            int H = tex.height;
            int Wm = (int)(Mathf.Ceil(3.0f * sig) * 2 + 1);
            int Rm = (Wm - 1) / 2;

            //フィルタ
            float[] msk = new float[Wm];

            sig = 2 * sig * sig;
            float div = Mathf.Sqrt(sig * Mathf.PI);

            //フィルタの作成
            for (int x = 0; x < Wm; x++)
            {
                int p = (x - Rm) * (x - Rm);
                msk[x] = Mathf.Exp(-p / sig) / div;
            }

            var src = tex.GetPixels(0).Select(x => x.a).ToArray();
            var tmp = new float[src.Length];
            var dst = new Color[src.Length];

            //垂直方向
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    float sum = 0;
                    for (int i = 0; i < Wm; i++)
                    {
                        int p = y + i - Rm;
                        if (p < 0 || p >= H) continue;
                        sum += msk[i] * src[x + p * W];
                    }
                    tmp[x + y * W] = sum;
                }
            }
            //水平方向
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    float sum = 0;
                    for (int i = 0; i < Wm; i++)
                    {
                        int p = x + i - Rm;
                        if (p < 0 || p >= W) continue;
                        sum += msk[i] * tmp[p + y * W];
                    }
                    dst[x + y * W] = new Color(1, 1, 1, sum);
                }
            }

            var createTexture = new Texture2D(W, H);
            createTexture.SetPixels(dst);
            createTexture.Apply();

            return createTexture;
        }

        class SDFTextureGenerator
        {
            SDFGrid iGrid, eGrid;
            int w, h;
            public SDFTextureGenerator(Texture2D sourceTex)
            {
                w = sourceTex.width;
                h = sourceTex.height;
                iGrid = new SDFGrid(sourceTex, (a) => a < 0.5f);
                eGrid = new SDFGrid(sourceTex, (a) => a >= 0.5f);
            }

            public void Update()
            {
                iGrid.UpdateGrid();
                eGrid.UpdateGrid();
            }

            public Texture2D GenerateTexture()
            {
                float max = iGrid.MaxDistance + eGrid.MaxDistance;
                Texture2D newTex = new Texture2D(w, h);
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color color = Color.white;
                        color.a = (iGrid.GetDistance(x, y) + eGrid.GetDistance(x, y)) / max;
                        newTex.SetPixel(x, y, color);
                    }
                }
                newTex.Apply();
                return newTex;
            }
        }

        class SDFGrid
        {
            int w, h;
            float[,] Distance, newDistance;
            Vector2[,] offset, newOffset;
            public float MaxDistance { get; private set; }
            public SDFGrid(Texture2D sourceTex, Func<float, bool> func)
            {
                w = sourceTex.width;
                h = sourceTex.height;
                Distance = new float[w, h];
                newDistance = new float[w, h];
                offset = new Vector2[w, h];
                newOffset = new Vector2[w, h];
                MaxDistance = 0;
                // 初期化
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color color = sourceTex.GetPixel(x, y);
                        if (func(color.a))
                        {
                            Set(x, y, 0, Vector2.zero);
                        }
                        else
                        {
                            Set(x, y, float.MaxValue, Vector2.one * Mathf.Infinity);
                        }
                    }
                }
                Apply();
            }

            public float GetDistance(int x, int y)
            {
                return Distance[x, y];
            }

            public Vector2 GetOffset(int x, int y)
            {
                return offset[x, y];
            }

            public void Set(int x, int y, float d, Vector2 o)
            {
                newDistance[x, y] = d;
                newOffset[x, y] = o;
            }

            public void Apply()
            {
                Array.Copy(newDistance, Distance, newDistance.Length);
                Array.Copy(newOffset, offset, newOffset.Length);
            }

            public void UpdateCell(int x, int y)
            {
                float minDistance = GetDistance(x, y);
                Vector2 minOffset = GetOffset(x, y);
                for (int j = -1; j <= 1; j++)
                {
                    int ty = y + j;
                    if (ty < 0 || ty >= h) continue;
                    for (int i = -1; i <= 1; i++)
                    {
                        int tx = x + i;
                        if (tx < 0 || tx >= w) continue;
                        if (i == 0 && j == 0) continue;
                        Vector2 tmpOffset = GetOffset(tx, ty) + new Vector2(i, j);
                        float tmpDistance = tmpOffset.magnitude;
                        if (tmpDistance < minDistance)
                        {
                            minDistance = tmpDistance;
                            minOffset = tmpOffset;
                        }
                    }
                }
                Set(x, y, minDistance, minOffset);
                if (minDistance < 1000 && minDistance > MaxDistance) MaxDistance = minDistance;
            }

            public void UpdateGrid()
            {
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        UpdateCell(x, y);
                    }
                }
                Apply();
            }

            public Texture2D GenerateTexture()
            {
                Texture2D newTex = new Texture2D(w, h);
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        //Debug.Log($"GetDistance({x}, {y}) = {GetDistance(x, y)}");
                        //Debug.Log($"({x}, {y}) = {GetDistance(x, y) / maxDistance}");
                        Color color = Color.white;
                        color.a = 1 - (GetDistance(x, y) / MaxDistance);
                        newTex.SetPixel(x, y, color);
                    }
                }
                Debug.Log($"maxDistance = {MaxDistance}");
                newTex.Apply();
                return newTex;
            }
        }
    }
}
