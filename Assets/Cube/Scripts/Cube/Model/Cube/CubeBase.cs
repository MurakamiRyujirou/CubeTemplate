using System;
using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    public abstract class CubeBase : ICube
    {
        public int Size { get; protected set; }

        // キューブを構成するCubie.Rotateすると配列の位置を入れ替える.
        public ICubie[,,] cubies { get; protected set; }

        /// キューブの回転操作は本クラスではなく、ICubeRotatorインタフェースを継承するクラスで実現する.
        public ICubeRotator rotator { get; protected set; }

        /// 回転処理を行う.
        /// キューブの回転操作でキュービー配列の位置を入れ替える.
        public void Rotate(Operations oper)
        {
            rotator.Rotate(cubies, oper);
        }

        /// 指定した回転操作で動かす対象のキュービーの配列を取得する.
        public ICubie[] GetRotateCubies(Operations oper)
        {
            return GetCubies(GetRotatePositions(oper));
        }

        /// 指定した回転操作で動かす対象のキュービー座標配列を取得する.
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

        /// 指定した回転操作で動かす対象のキュービーの初期位置を取得する.
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

        /// 指定の座標リストに現在位置するキュービーを取得する.
        public ICubie[] GetCubies(Position[] posList)
        {
            List<ICubie> ret = new();
            foreach (Position pos in posList)
            {
                ret.Add(GetCubie(pos));
            }
            return ret.ToArray();
        }

        /// 指定の座標に現在位置するキュービーを取得する.
        public ICubie GetCubie(Position pos)
        {
            return cubies[pos.X, pos.Y, pos.Z];
        }

        /// 指定の座標に現在位置するキュービーのパネル情報を取得する.
        public PanelTable GetPanelTable(Position pos)
        {
            return GetCubie(pos).CurrentPanels;
        }

        /// 指定の座標に現在位置するキュービーの、指定面のパネル情報を取得する.
        public IPanel GetPanel(Position pos, Faces face)
        {
            return GetCubie(pos).CurrentPanels.Get(face);
        }
    }
}
