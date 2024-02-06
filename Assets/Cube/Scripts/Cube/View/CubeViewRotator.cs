using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// キューブの回転操作による動きを担当するクラス.
    /// どのパーツが動くのか？などは外から与えられ、
    /// 与えられたパーツと回転操作に従って90度回転させる動きを行う.
    public class CubeViewRotator : MonoBehaviour
    {
        public delegate void OnCompleteRotate(Operations oper, bool isOperationDone);
        public OnCompleteRotate completeRotate;

        /// 現在回転しているキュービーのリスト.
        /// RotateAround操作をする対象となるので回転途中で内容が変更されないよう実装する.
        private List<Transform> rotateCubies = new();

        /// 現在行なっている回転操作.
        public Operations CurrentOperation { get; private set; } = Operations.NONE;

        /// 現在回転操作中か否かの状態を保持するブール値.
        public bool IsRotating { get; private set; } = false;

        /// 現在回転中のキュービーの回転量.0fから90f or 180fとなる.
        /// 向きを_dirで保持するので、-90fなどマイナスの値は持たないこととする.
        public float Angle { get; private set; } = 0f;

        /// 現在行なっている回転の回転スピード.
        /// 特定の操作により回転の即時終了やキャンセルを実現したいので、途中で回転スピードが変更される可能性はある.
        public float RotateSpeed = 1f;

        /// 現在の回転操作での回転量の最大90f or 180fとなる.
        private float angleMax = 90f;

        /// 現在行なっている回転の、回転向き(正負のいずれか).
        private float dir = 0f;

        /// 回転操作を行うための設定をする.
        /// <param name="cubies">回転するキュービーのリスト.</param>
        /// <param name="oper">回転操作.</param>
        public bool Rotate(CubieView[] cubies, Operations oper)
        {
            // 回転中には次の回転は行わない.
            if (IsRotating) return false;

            // 新たに行う回転用に回転対象のTransformを格納するリストを初期化.
            rotateCubies = new List<Transform>();
            foreach (CubieView cubie in cubies)
            {
                rotateCubies.Add(cubie.gameObject.transform);
            }
            CurrentOperation = oper;   // これから行う回転操作を記録.
            IsRotating = true;         // 回転中のステータスに変更.
            Angle = 0f;                // 現在の回転量をリセット.
            angleMax = AngleMax(oper); // 回転の最大値をセット.
            dir = Direction(oper);     // 回転向きを取得.
            return true;
        }

        /// 更新処理.
        public void OnUpdate()
        {
            if (IsRotating)
                RotateAround();
        }

        /// 回転操作を中止する.
        public void Stop()
        {
            CurrentOperation = Operations.NONE;
            IsRotating = false;
            Angle = 0f;
            angleMax = 90f;
            dir = 0f;
        }

        /// 回転操作のキューブが動く処理部分.
        private void RotateAround()
        {
            // 今回のフレームで到達する回転角度.
            float target;

            // 今回のフレームで回転が終了するか(90/180度回ったか)判断する.
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

            // 各キュービーを回転させる.
            foreach (Transform cubie in rotateCubies)
            {
                // 標準機能のRotateAroundを呼び出す.
                // GetAxisで回転軸を取得するが、プラスマイナス考慮が面倒なのでX,Y,Zのいずれか３種とした.speedは回転量.
                // dirは回転の向き（正負の符号）であり回転操作開始時にDirectionで計算する.
                // [注意]GetAxisはキューブ全体の向きを変更する動きが行われたときに軸を計算し直す必要があるので都度計算とする.
                Vector3 axis = GetAxis(CurrentOperation);
                cubie.RotateAround(transform.position, axis, target * dir);
            }

            // 今回のフレームで回転終了であればisRotatingの状態を変化させる.
            if (rotateDone)
            {
                IsRotating = false;
                completeRotate(CurrentOperation, true);
            }
        }

        /// 回転軸となるVector3の値を取得する.Cubeごと回転させても正しく取得できるようにtransformから取得している.
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

        /// 回転軸に対して正負どちらの方向に回転させるかを取得する.
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

        /// 回転操作の回転度数を取得する.
        private float AngleMax(Operations currentOperation)
        {
            return OperationLogic.IsDouble(currentOperation) ? 180f : 90f;
        }
    }
}