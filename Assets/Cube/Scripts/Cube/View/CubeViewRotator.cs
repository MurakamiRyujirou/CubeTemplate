using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// �L���[�u�̉�]����ɂ�铮����S������N���X.
    /// �ǂ̃p�[�c�������̂��H�Ȃǂ͊O����^�����A
    /// �^����ꂽ�p�[�c�Ɖ�]����ɏ]����90�x��]�����铮�����s��.
    public class CubeViewRotator : MonoBehaviour
    {
        public delegate void OnCompleteRotate(Operations oper, bool isOperationDone);
        public OnCompleteRotate completeRotate;

        /// ���݉�]���Ă���L���[�r�[�̃��X�g.
        /// RotateAround���������ΏۂƂȂ�̂ŉ�]�r���œ��e���ύX����Ȃ��悤��������.
        private List<Transform> rotateCubies = new();

        /// ���ݍs�Ȃ��Ă����]����.
        public Operations CurrentOperation { get; private set; } = Operations.NONE;

        /// ���݉�]���쒆���ۂ��̏�Ԃ�ێ�����u�[���l.
        public bool IsRotating { get; private set; } = false;

        /// ���݉�]���̃L���[�r�[�̉�]��.0f����90f or 180f�ƂȂ�.
        /// ������_dir�ŕێ�����̂ŁA-90f�Ȃǃ}�C�i�X�̒l�͎����Ȃ����ƂƂ���.
        public float Angle { get; private set; } = 0f;

        /// ���ݍs�Ȃ��Ă����]�̉�]�X�s�[�h.
        /// ����̑���ɂ���]�̑����I����L�����Z���������������̂ŁA�r���ŉ�]�X�s�[�h���ύX�����\���͂���.
        public float RotateSpeed = 1f;

        /// ���݂̉�]����ł̉�]�ʂ̍ő�90f or 180f�ƂȂ�.
        private float angleMax = 90f;

        /// ���ݍs�Ȃ��Ă����]�́A��]����(�����̂����ꂩ).
        private float dir = 0f;

        /// ��]������s�����߂̐ݒ������.
        /// <param name="cubies">��]����L���[�r�[�̃��X�g.</param>
        /// <param name="oper">��]����.</param>
        public bool Rotate(CubieView[] cubies, Operations oper)
        {
            // ��]���ɂ͎��̉�]�͍s��Ȃ�.
            if (IsRotating) return false;

            // �V���ɍs����]�p�ɉ�]�Ώۂ�Transform���i�[���郊�X�g��������.
            rotateCubies = new List<Transform>();
            foreach (CubieView cubie in cubies)
            {
                rotateCubies.Add(cubie.gameObject.transform);
            }
            CurrentOperation = oper;   // ���ꂩ��s����]������L�^.
            IsRotating = true;         // ��]���̃X�e�[�^�X�ɕύX.
            Angle = 0f;                // ���݂̉�]�ʂ����Z�b�g.
            angleMax = AngleMax(oper); // ��]�̍ő�l���Z�b�g.
            dir = Direction(oper);     // ��]�������擾.
            return true;
        }

        /// �X�V����.
        public void OnUpdate()
        {
            if (IsRotating)
                RotateAround();
        }

        /// ��]����𒆎~����.
        public void Stop()
        {
            CurrentOperation = Operations.NONE;
            IsRotating = false;
            Angle = 0f;
            angleMax = 90f;
            dir = 0f;
        }

        /// ��]����̃L���[�u��������������.
        private void RotateAround()
        {
            // ����̃t���[���œ��B�����]�p�x.
            float target;

            // ����̃t���[���ŉ�]���I�����邩(90/180�x�������)���f����.
            bool rotateDone = Angle + RotateSpeed >= angleMax;
            if (rotateDone)
            {
                target = angleMax - Angle;
                Angle = 0f;
            }
            else
            {
                target = RotateSpeed;
                Angle += RotateSpeed;
            }

            // �e�L���[�r�[����]������.
            foreach (Transform cubie in rotateCubies)
            {
                // �W���@�\��RotateAround���Ăяo��.
                // GetAxis�ŉ�]�����擾���邪�A�v���X�}�C�i�X�l�����ʓ|�Ȃ̂�X,Y,Z�̂����ꂩ�R��Ƃ���.speed�͉�]��.
                // dir�͉�]�̌����i�����̕����j�ł����]����J�n����Direction�Ōv�Z����.
                // [����]GetAxis�̓L���[�u�S�̂̌�����ύX���铮�����s��ꂽ�Ƃ��Ɏ����v�Z�������K�v������̂œs�x�v�Z�Ƃ���.
                Vector3 axis = GetAxis(CurrentOperation);
                cubie.RotateAround(transform.position, axis, target * dir);
            }

            // ����̃t���[���ŉ�]�I���ł����isRotating�̏�Ԃ�ω�������.
            if (rotateDone)
            {
                IsRotating = false;
                completeRotate(CurrentOperation, true);
            }
        }

        /// ��]���ƂȂ�Vector3�̒l���擾����.Cube���Ɖ�]�����Ă��������擾�ł���悤��transform����擾���Ă���.
        private Vector3 GetAxis(Operations currentOperation)
        {
            if (currentOperation == Operations.R  || currentOperation == Operations.R_  || currentOperation == Operations.R2)  return transform.right;
            if (currentOperation == Operations.L  || currentOperation == Operations.L_  || currentOperation == Operations.L2)  return transform.right;
            if (currentOperation == Operations.M  || currentOperation == Operations.M_  || currentOperation == Operations.M2)  return transform.right;
            if (currentOperation == Operations.x  || currentOperation == Operations.x_  || currentOperation == Operations.x2)  return transform.right;
            if (currentOperation == Operations.Rw || currentOperation == Operations.Rw_ || currentOperation == Operations.Rw2) return transform.right;
            if (currentOperation == Operations.Lw || currentOperation == Operations.Lw_ || currentOperation == Operations.Lw2) return transform.right;
            if (currentOperation == Operations.U  || currentOperation == Operations.U_  || currentOperation == Operations.U2)  return transform.up;
            if (currentOperation == Operations.D  || currentOperation == Operations.D_  || currentOperation == Operations.D2)  return transform.up;
            if (currentOperation == Operations.E  || currentOperation == Operations.E_  || currentOperation == Operations.E2)  return transform.up;
            if (currentOperation == Operations.y  || currentOperation == Operations.y_  || currentOperation == Operations.y2)  return transform.up;
            if (currentOperation == Operations.Uw || currentOperation == Operations.Uw_ || currentOperation == Operations.Uw2) return transform.up;
            if (currentOperation == Operations.Dw || currentOperation == Operations.Dw_ || currentOperation == Operations.Dw2) return transform.up;
            if (currentOperation == Operations.B  || currentOperation == Operations.B_  || currentOperation == Operations.B2)  return transform.forward;
            if (currentOperation == Operations.F  || currentOperation == Operations.F_  || currentOperation == Operations.F2)  return transform.forward;
            if (currentOperation == Operations.S  || currentOperation == Operations.S_  || currentOperation == Operations.S2)  return transform.forward;
            if (currentOperation == Operations.z  || currentOperation == Operations.z_  || currentOperation == Operations.z2)  return transform.forward;
            if (currentOperation == Operations.Bw || currentOperation == Operations.Bw_ || currentOperation == Operations.Bw2) return transform.forward;
            if (currentOperation == Operations.Fw || currentOperation == Operations.Fw_ || currentOperation == Operations.Fw2) return transform.forward;
            return Vector3.zero;
        }

        /// ��]���ɑ΂��Đ����ǂ���̕����ɉ�]�����邩���擾����.
        private float Direction(Operations currentOperation)
        {
            if (currentOperation == Operations.R  || currentOperation == Operations.L_)  return 1f;
            if (currentOperation == Operations.U  || currentOperation == Operations.D_)  return 1f;
            if (currentOperation == Operations.B  || currentOperation == Operations.F_)  return 1f;
            if (currentOperation == Operations.R2 || currentOperation == Operations.x2)  return 1f;
            if (currentOperation == Operations.U2 || currentOperation == Operations.y2)  return 1f;
            if (currentOperation == Operations.B2 || currentOperation == Operations.z2)  return 1f;
            if (currentOperation == Operations.Rw || currentOperation == Operations.Lw_) return 1f;
            if (currentOperation == Operations.Uw || currentOperation == Operations.Dw_) return 1f;
            if (currentOperation == Operations.Bw || currentOperation == Operations.Fw_) return 1f;
            if (currentOperation == Operations.Rw2) return 1f;
            if (currentOperation == Operations.Uw2) return 1f;
            if (currentOperation == Operations.Bw2) return 1f;
            if (currentOperation == Operations.M)   return 1f;
            if (currentOperation == Operations.E)   return 1f;
            if (currentOperation == Operations.S)   return 1f;
            if (currentOperation == Operations.x)   return 1f;
            if (currentOperation == Operations.y)   return 1f;
            if (currentOperation == Operations.z)   return -1f;
            if (currentOperation == Operations.L  || currentOperation == Operations.R_)  return -1f;
            if (currentOperation == Operations.D  || currentOperation == Operations.U_)  return -1f;
            if (currentOperation == Operations.F  || currentOperation == Operations.B_)  return -1f;
            if (currentOperation == Operations.L2 || currentOperation == Operations.M2)  return -1f;
            if (currentOperation == Operations.D2 || currentOperation == Operations.E2)  return -1f;
            if (currentOperation == Operations.F2 || currentOperation == Operations.S2)  return -1f;
            if (currentOperation == Operations.Lw || currentOperation == Operations.Rw_) return -1f;
            if (currentOperation == Operations.Dw || currentOperation == Operations.Uw_) return -1f;
            if (currentOperation == Operations.Fw || currentOperation == Operations.Bw_) return -1f;
            if (currentOperation == Operations.Lw2) return -1f;
            if (currentOperation == Operations.Dw2) return -1f;
            if (currentOperation == Operations.Fw2) return -1f;
            if (currentOperation == Operations.M_)  return -1f;
            if (currentOperation == Operations.E_)  return -1f;
            if (currentOperation == Operations.S_)  return -1f;
            if (currentOperation == Operations.x_)  return -1f;
            if (currentOperation == Operations.y_)  return -1f;
            if (currentOperation == Operations.z_)  return 1f;
            return 1f;
        }

        /// ��]����̉�]�x�����擾����.
        private float AngleMax(Operations currentOperation)
        {
            return OperationLogic.IsDouble(currentOperation) ? 180f : 90f;
        }
    }
}