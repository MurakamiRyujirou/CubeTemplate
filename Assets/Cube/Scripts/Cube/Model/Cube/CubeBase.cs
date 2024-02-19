using System;
using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    public abstract class CubeBase : ICube
    {
        public int Size { get; protected set; }

        // �L���[�u���\������Cubie.Rotate����Ɣz��̈ʒu�����ւ���.
        public ICubie[,,] cubies { get; protected set; }

        /// �L���[�u�̉�]����͖{�N���X�ł͂Ȃ��AICubeRotator�C���^�t�F�[�X���p������N���X�Ŏ�������.
        public ICubeRotator rotator { get; protected set; }

        /// ��]�������s��.
        /// �L���[�u�̉�]����ŃL���[�r�[�z��̈ʒu�����ւ���.
        public void Rotate(Operations oper)
        {
            rotator.Rotate(cubies, oper);
        }

        /// �w�肵����]����œ������Ώۂ̃L���[�r�[�̔z����擾����.
        public ICubie[] GetRotateCubies(Operations oper)
        {
            return GetCubies(GetRotatePositions(oper));
        }

        /// �w�肵����]����œ������Ώۂ̃L���[�r�[���W�z����擾����.
        public Position[] GetRotatePositions(Operations oper)
        {
            Vector3Int[] vecList = PositionLogic.GetPositionsFromOpers(oper);
            List<Position> retList = new();
            foreach (Vector3Int vec in vecList)
            {
                retList.Add(new(vec.x, vec.y, vec.z));
            }
            return retList.ToArray();
        }

        /// �w�肵����]����œ������Ώۂ̃L���[�r�[�̏����ʒu���擾����.
        public Position[] GetRotateInitialPositions(Operations oper)
        {
            Vector3Int[] vecList = PositionLogic.GetPositionsFromOpers(oper);
            List<Position> retList = new();
            foreach (Vector3Int vec in vecList)
            {
                Position pos = new(vec.x, vec.y, vec.z);
                ICubie c = GetCubie(pos);
                retList.Add(c.InitialPosition);
            }
            return retList.ToArray();
        }

        /// �w��̍��W���X�g�Ɍ��݈ʒu����L���[�r�[���擾����.
        public ICubie[] GetCubies(Position[] posList)
        {
            List<ICubie> ret = new();
            foreach (Position pos in posList)
            {
                ret.Add(GetCubie(pos));
            }
            return ret.ToArray();
        }

        /// �w��̍��W�Ɍ��݈ʒu����L���[�r�[���擾����.
        public ICubie GetCubie(Position pos)
        {
            return cubies[pos.X, pos.Y, pos.Z];
        }

        /// �w��̍��W�Ɍ��݈ʒu����L���[�r�[�̃p�l�������擾����.
        public PanelTable GetPanelTable(Position pos)
        {
            return GetCubie(pos).CurrentPanels;
        }

        /// �w��̍��W�Ɍ��݈ʒu����L���[�r�[�́A�w��ʂ̃p�l�������擾����.
        public IPanel GetPanel(Position pos, Faces face)
        {
            return GetCubie(pos).CurrentPanels.Get(face);
        }
    }
}
