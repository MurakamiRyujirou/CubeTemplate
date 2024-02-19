using System;
using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    public class CubeView : MonoBehaviour
    {
        private const int SIZE = 3;

        [Header("��]����Ƃ��̌��ʉ�.")]
        [SerializeField]
        private AudioClip audioClip;

        // �L���[�u��]�I����ʒm���鏈��.
        public delegate void OnCompleteRotate(Operations oper, bool isOperationDone);
        public OnCompleteRotate completeRotate;

        /// �L���[�r�[�r���[���i�[����z��.
        /// �ǂꂾ����]�����Ă��z��̃L���[�r�[�r���[�͓���ւ��͍s��Ȃ�.
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

        /// �L���[�r�[�r���[���Z�b�g����.
        public void SetCubieViews(CubieView[,,] cubieViews)
        {
            // �L���[�u�̃T�C�Y�ƃL���[�r�[���i�[����z�������������.
            CubieViews = new CubieView[SIZE, SIZE, SIZE];

            // �e�z��Ɉ����œn���ꂽ�L���[�r�[�r���[���i�[���Ă���.
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

        /// �L���[�r�[�r���[���擾����.
        /// ��]���삷��Ƃ��̑ΏۃL���[�r�[���擾����ۂɗ��p.
        public CubieView[] GetCubieViews(Position[] pos)
        {
            List<CubieView> ret = new();
            foreach (Position p in pos)
            {
                ret.Add(CubieViews[p.X, p.Y, p.Z]);
            }
            return ret.ToArray();
        }

        /// ��]������w������.
        /// �{���\�b�h���ŉ�]������Ƃ������A��]���邽�߂̎����]�ʂ�ݒ肵�A
        /// �ȍ~OnUpdate��ʂ��ď��X�ɉ�]�����邱�ƂƂȂ�.
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
                // ���ʉ����ݒ肳��Ă���Ή����o��.
                AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
                if (audioClip != null)
                {
                    audioSource.PlayOneShot(audioClip);
                }
            }
            return ret;
        }

        /// �L���[�u�̉�]������~����.
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
            // ��]�I����ʒm
            completeRotate?.Invoke(oper, isOperationDone);
        }
    }
}