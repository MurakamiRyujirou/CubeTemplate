using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// �L���[�u���f���̉�]������S������N���X.
    public class CubeRotator : ICubeRotator
    {
        /// ��]������s��.
        public void Rotate(ICubie[,,] cubies, Operations oper)
        {
            if (oper == Operations.NONE) return;

            // �����ւ�����(x,y,z)�ƁA�Q�w�񂵑���(Rw,Lw,,,)��W����]����ɕ���.
            // 180�x��]��90�x��]2��ɕ���.
            SingleOperations[] singleOpers = OperationLogic.Break(oper);

            foreach (SingleOperations singleOper in singleOpers)
            {
                RotateInner(cubies, singleOper);
            }
        }

        /// �W����]������s��.
        private void RotateInner(ICubie[,,] cubies, SingleOperations oper)
        {
            Axes axis = SingleOperationLogic.GetAxisFromSingleOperation(oper);
            bool isClockwise = SingleOperationLogic.IsClockwise(oper);

            // �X���C�X����(M, E, S)�̏ꍇ.
            if (SingleOperationLogic.IsSlice(oper))
            {
                // 3x3x3�̏ꍇ�A��]������M,E,S����ʂ��ăG�b�W�ƃZ���^�[�p�[�c���擾����.
                Vector3Int[] edgePositions   = PositionLogic.GetEdgePositionFromSlice(axis);
                Vector3Int[] centerPositions = PositionLogic.GetCenterPositionsFromSlice(axis);
                Vector3Int[] corePosition    = PositionLogic.GetCorePosition();

                // �G�b�W�ƃZ���^�[����������90�x��]��\������.
                SwapCubies(cubies, edgePositions, isClockwise);
                SwapCubies(cubies, centerPositions, isClockwise);

                // �G�b�W�ƃZ���^�[�ƃR�A�̊e�L���[�r�[�����]������.
                RotationCubies(cubies, edgePositions, axis, isClockwise);
                RotationCubies(cubies, centerPositions, axis, isClockwise);
                RotationCubies(cubies, corePosition, axis, isClockwise);
            }
            else
            {
                // �P�w��(R�`F)�̏ꍇ�A��]����ʂ��擾���A��������R�[�i�[�ƃG�b�W�ƃZ���^�[���擾����.
                Faces face = SingleOperationLogic.GetFaceFromOperation(oper);
                Vector3Int[] cornerPositions = PositionLogic.GetCornerPositionsFromFace(face);
                Vector3Int[] edgePositions   = PositionLogic.GetEdgePositionsFromFace(face);
                Vector3Int[] centerPosition  = PositionLogic.GetCenterPositionFromFace(face);

                // �ʂɑ��݂���R�[�i�[�ƃG�b�W����������90�x��]��\������.
                SwapCubies(cubies, cornerPositions, isClockwise);
                SwapCubies(cubies, edgePositions, isClockwise);

                // �R�[�i�[�ƃG�b�W�ƃZ���^�[�̊e�L���[�r�[�����]������.
                RotationCubies(cubies, cornerPositions, axis, isClockwise);
                RotationCubies(cubies, edgePositions, axis, isClockwise);
                RotationCubies(cubies, centerPosition, axis, isClockwise);
            }
        }

        /// �w�肵�����W�ɑ��݂���L���[�r�[����]������.
        /// ��]�͉�]��(axis)�ƁA���v��肩�ۂ�(clockwise)�Ŏw�肷��.
        private void SwapCubies(ICubie[,,] cubies, Vector3Int[] p, bool clockwise)
        {
            if (clockwise)
            {
                ICubie temp = cubies[p[0].x, p[0].y, p[0].z];
                cubies[p[0].x, p[0].y, p[0].z] = cubies[p[1].x, p[1].y, p[1].z];
                cubies[p[1].x, p[1].y, p[1].z] = cubies[p[2].x, p[2].y, p[2].z];
                cubies[p[2].x, p[2].y, p[2].z] = cubies[p[3].x, p[3].y, p[3].z];
                cubies[p[3].x, p[3].y, p[3].z] = temp;
            }
            else
            {
                ICubie temp = cubies[p[3].x, p[3].y, p[3].z];
                cubies[p[3].x, p[3].y, p[3].z] = cubies[p[2].x, p[2].y, p[2].z];
                cubies[p[2].x, p[2].y, p[2].z] = cubies[p[1].x, p[1].y, p[1].z];
                cubies[p[1].x, p[1].y, p[1].z] = cubies[p[0].x, p[0].y, p[0].z];
                cubies[p[0].x, p[0].y, p[0].z] = temp;
            }
        }

        /// �w�肵�����W�ɑ��݂���L���[�r�[����]������.
        /// ��]�͉�]��(axis)�ƁA���v��肩�ۂ�(clockwise)�Ŏw�肷��.
        private void RotationCubies(ICubie[,,] cubies, Vector3Int[] pList, Axes axis, bool clockwise)
        {
            foreach (Vector3Int p in pList)
            {
                ICubie cubie = cubies[p.x, p.y, p.z];
                System.Type[] types = cubie.GetType().GetInterfaces();
                foreach (System.Type type in types)
                {
                    if (type == typeof(IRotatable))
                    {
                        IRotatable r = (IRotatable)cubie;
                        r.Rotate(axis, clockwise);
                    }
                }
            }
        }
    }
}