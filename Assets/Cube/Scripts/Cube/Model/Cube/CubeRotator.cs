using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// キューブモデルの回転処理を担当するクラス.
    public class CubeRotator : ICubeRotator
    {
        /// 回転操作を行う.
        public void Rotate(ICubie[,,] cubies, Operations oper)
        {
            if (oper == Operations.NONE) return;

            // 持ち替え操作(x,y,z)と、２層回し操作(Rw,Lw,,,)を標準回転操作に分解.
            // 180度回転も90度回転2回に分解.
            SingleOperations[] singleOpers = OperationLogic.Break(oper);

            foreach (SingleOperations singleOper in singleOpers)
            {
                RotateInner(cubies, singleOper);
            }
        }

        /// 標準回転操作を行う.
        private void RotateInner(ICubie[,,] cubies, SingleOperations oper)
        {
            Axes axis = SingleOperationLogic.GetAxisFromSingleOperation(oper);
            bool isClockwise = SingleOperationLogic.IsClockwise(oper);

            // スライス操作(M, E, S)の場合.
            if (SingleOperationLogic.IsSlice(oper))
            {
                // 3x3x3の場合、回転軸からM,E,Sを区別してエッジとセンターパーツを取得する.
                Vector3Int[] edgePositions   = PositionLogic.GetEdgePositionFromSlice(axis);
                Vector3Int[] centerPositions = PositionLogic.GetCenterPositionsFromSlice(axis);
                Vector3Int[] corePosition    = PositionLogic.GetCorePosition();

                // エッジとセンターを交換して90度回転を表現する.
                SwapCubies(cubies, edgePositions, isClockwise);
                SwapCubies(cubies, centerPositions, isClockwise);

                // エッジとセンターとコアの各キュービーを自転させる.
                RotationCubies(cubies, edgePositions, axis, isClockwise);
                RotationCubies(cubies, centerPositions, axis, isClockwise);
                RotationCubies(cubies, corePosition, axis, isClockwise);
            }
            else
            {
                // 単層回し(R～F)の場合、回転する面を取得し、そこからコーナーとエッジとセンターを取得する.
                Faces face = SingleOperationLogic.GetFaceFromOperation(oper);
                Vector3Int[] cornerPositions = PositionLogic.GetCornerPositionsFromFace(face);
                Vector3Int[] edgePositions   = PositionLogic.GetEdgePositionsFromFace(face);
                Vector3Int[] centerPosition  = PositionLogic.GetCenterPositionFromFace(face);

                // 面に存在するコーナーとエッジを交換して90度回転を表現する.
                SwapCubies(cubies, cornerPositions, isClockwise);
                SwapCubies(cubies, edgePositions, isClockwise);

                // コーナーとエッジとセンターの各キュービーを自転させる.
                RotationCubies(cubies, cornerPositions, axis, isClockwise);
                RotationCubies(cubies, edgePositions, axis, isClockwise);
                RotationCubies(cubies, centerPosition, axis, isClockwise);
            }
        }

        /// 指定した座標に存在するキュービーを回転させる.
        /// 回転は回転軸(axis)と、時計回りか否か(clockwise)で指定する.
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

        /// 指定した座標に存在するキュービーを回転させる.
        /// 回転は回転軸(axis)と、時計回りか否か(clockwise)で指定する.
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