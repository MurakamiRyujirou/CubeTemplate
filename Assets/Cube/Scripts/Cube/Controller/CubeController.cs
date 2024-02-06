using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// �L���[�u�̃r���[�ƃ��f�����Ǘ�����N���X.
    public class CubeController
    {
        /// �L���[�u���f��.
        public Cube Cube { get; private set; }

        /// �L���[�u�r���[.
        public CubeView CubeView { get; private set; }

        /// ������]���s�����߂̃N���X.
        private AutoRotator autoRotator;

        public bool IsRotating { get { return CubeView.IsRotating; } }

        /// constructor.
        public CubeController(Cube cube, CubeView cubeView)
        {
            this.Cube = cube;
            this.CubeView = cubeView;

            // �r���[�ŉ�]���������^�C�~���O��RotateDone�����s����悤�ɂ���.
            // ����ɂ��r���[����]���I�������ƁA���f������]����悤�ɘA�������邱�Ƃ��ł���.
            this.CubeView.completeRotate = RotateDone;

            this.autoRotator = new();
        }

        /// �X�V����.��]�r���̏ꍇ�A��]��i�߂�.
        /// ��]���I����Ă��Ď�����]���̏ꍇ�A���̉�]���s��.
        public void OnUpdate()
        {
            // ��]�r���Ȃ��]��i�߂�.
            CubeView.OnUpdate();

            // ������]������]���Ă��Ȃ��ꍇ�A���̉�]�����K�p����.
            if (autoRotator.IsReserved() && !CubeView.IsRotating)
            {
                Rotate(autoRotator.GetOperation());
            }
        }

        /// ��]�X�s�[�h���Z�b�g����.
        public void SetRotateSpeed(float value)
        {
            CubeView.SetRotateSpeed(value);
        }

        /// ��]�������s��.��]�������J�n�ł����ꍇ��TRUE��Ԃ�.
        public bool Rotate(Operations oper)
        {
            // ���݉�]�r���̏ꍇ�A�V������]�����͍s�킸�{�����͏I������.
            if (CubeView.IsRotating) return false;

            // ��]���ׂ��L���[�r�[�́A�L���[�u�r���[��̔z��̓Y����(���W)���擾����.
            Vector3Int[] posList = Cube.GetRotateIndexes(oper);

            // ���W�ɉ������r���[���擾.
            CubieView[] rotateCubieViews = CubeView.GetCubieViews(posList);

            // �r���[�Ɖ�]����������ɉ�]�������s��.
            return CubeView.Rotate(rotateCubieViews, oper);
        }

        /// �L���[�u�Ɏ�����]������w������.
        /// <param name="opers">�����̉�]����.�z��Ɋi�[���ēn��.</param>
        /// <returns>TRUE:���������{����.</returns>
        public bool AutoRotate(Operations[] opers)
        {
            // ������]���ł���ΐV���ȏ����͎󂯕t���Ȃ�.
            if (autoRotator.IsReserved()) return false;

            // �蓮��]�̓r���ł���ꍇ����t�Ȃ�.
            if (CubeView.IsRotating) return false;

            // �I���W�i����]��S������N���X�ɉ�]������Z�b�g����.
            autoRotator.Setup(opers);

            // �ŏ��̉�]�������J�n����.
            return Rotate(autoRotator.GetOperation());
        }

        /// �L���[�u�����Z�b�g����.
        /// ���f�������Z�b�g���A���̓��e���r���[�ɔ��f����.
        public void Reset()
        {
            // ������]�̓��Z�b�g�@�\��݂��Ă��Ȃ��̂ŐV���ȃC���X�^���X���Z�b�g����.
            if (autoRotator.IsReserved()) autoRotator = new AutoRotator();

            // �L���[�u���f�����Đ�������.�����T�C�Y�������p��.
            Cube = new Cube();

            // �L���[�u�r���[�͉�]��~���w������.
            CubeView.Stop();
        }

        /// �r���[�̃L���[�u��]�N���X�����]����̊����ʒm������ƃR�[������郁�\�b�h.
        private void RotateDone(Operations oper, bool isOperationDone)
        {
            // ��]��������{���Ċ����ʒm���͂��Ă����ꍇ�ɂ́A���f��������]����𔽉f����.
            if (isOperationDone)
            {
                // ���f������]������.
                Cube.Rotate(oper);

                // ���f���ƃr���[����v���邱�Ƃ��m�F����.
                // CubeView.Adjust(Cube);
            }
        }
    }
}