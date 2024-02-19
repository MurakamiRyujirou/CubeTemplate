using System;
using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    public class CubeView : MonoBehaviour
    {
        private const int SIZE = 3;

        [Header("回転するときの効果音.")]
        [SerializeField]
        private AudioClip audioClip;

        // キューブ回転終了を通知する処理.
        public delegate void OnCompleteRotate(Operations oper, bool isOperationDone);
        public OnCompleteRotate completeRotate;

        /// キュービービューを格納する配列.
        /// どれだけ回転させても配列のキュービービューは入れ替えは行わない.
        public CubieView[,,] CubieViews { get; private set; }

        public bool IsRotating { get { return rotator.IsRotating; } }
        public void SetRotateSpeed(float value)
        {
            if (rotator == null)
                rotator = GetComponent<CubeViewRotator>();
            rotator.RotateSpeed = value;
        }

        private CubeViewRotator rotator;

        void Start()
        {
            rotator = GetComponent<CubeViewRotator>();
            rotator.completeRotate += RotateDone;
        }

        public void OnUpdate()
        {
            rotator.OnUpdate();
        }

        /// キュービービューをセットする.
        public void SetCubieViews(CubieView[,,] cubieViews)
        {
            // キューブのサイズとキュービーを格納する配列を初期化する.
            CubieViews = new CubieView[SIZE, SIZE, SIZE];

            // 各配列に引数で渡されたキュービービューを格納していく.
            for (int z = 0; z < SIZE; z++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int x = 0; x < SIZE; x++)
                    {
                        CubieViews[x, y, z] = cubieViews[x, y, z];
                        CubieViews[x, y, z].gameObject.transform.parent = this.transform;
                    }
                }
            }
        }

        /// キュービービューを取得する.
        /// 回転操作するときの対象キュービーを取得する際に利用.
        public CubieView[] GetCubieViews(Position[] pos)
        {
            List<CubieView> ret = new();
            foreach (Position p in pos)
            {
                ret.Add(CubieViews[p.X, p.Y, p.Z]);
            }
            return ret.ToArray();
        }

        /// 回転操作を指示する.
        /// 本メソッド内で回転をするというより、回転するための軸や回転量を設定し、
        /// 以降OnUpdateを通じて徐々に回転させることとなる.
        public bool Rotate(CubieView[] cubies, Operations oper)
        {
            List<Transform> transformList = new();
            foreach (CubieView cubie in cubies)
            {
                transformList.Add(cubie.gameObject.transform);
            }
            bool ret = rotator.Rotate(transformList.ToArray(), oper);
            if (ret)
            {
                // 効果音が設定されていれば音を出す.
                AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
                if (audioClip != null)
                {
                    audioSource.PlayOneShot(audioClip);
                }
            }
            return ret;
        }

        /// キューブの回転操作を停止する.
        public void Stop()
        {
            rotator.Stop();
        }

        public void Reset(Cube cube)
        {
            for (int z = 0; z < SIZE; z++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int x = 0; x < SIZE; x++)
                    {
                        Cubie c = cube.GetCubie(new Position(x, y, z));
                        Position p = c.InitialPosition;
                        CubieView cv = CubieViews[p.X, p.Y, p.Z];

                        float d = (SIZE == 1) ? 0f : (SIZE == 2) ? -0.5f : -1.0f;
                        Vector3 position = new(x + d, y + d, z + d);
                        cv.gameObject.transform.localPosition = position;

                        cv.gameObject.transform.localEulerAngles = Vector3.zero;
                        cv.SetPanels(c.CurrentPanels);
                    }
                }
            }
        }

        public void Adjust(Cube cube)
        {
            for (int z = 0; z < SIZE; z++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int x = 0; x < SIZE; x++)
                    {
                        Cubie c = cube.GetCubie(new Position(x, y, z));
                        Position p = c.InitialPosition;
                        CubieView cv = CubieViews[p.X, p.Y, p.Z];
                        Vector3 pos = cv.gameObject.transform.localPosition;
                        Vector3 rot = cv.gameObject.transform.localEulerAngles;
                        Debug.Log("Model(" + p.X + "," + p.Y + "," + p.Z + ")" +
                                  ",View(" + pos.x + "," + pos.y + "," + pos.z + ")" +
                                       "(" + rot.x + "," + rot.y + "," + rot.z + ")");
                    }
                }
            }
        }

        private void RotateDone(Operations oper, bool isOperationDone)
        {
            // 回転終了を通知
            completeRotate?.Invoke(oper, isOperationDone);
        }
    }
}