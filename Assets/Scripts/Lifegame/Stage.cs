using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lifegame.Rules;

namespace Lifegame
{    
    public class Stage : MonoBehaviour 
    {
        private Cell[,,] _map;

        public IRule Rule;
        public float FrameDuration;
        public bool IsPause = false;
        public Cell[,,] Map { get { return _map; } }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Length { get; private set; }

        private void Awake() 
        {
            Create(10,10,10,500);
        }

        private void OnTransformChildrenChanged() 
        {
            foreach(Transform tf in transform)
            {
                var cell = tf.gameObject.GetComponent<Cell>();
                tf.localPosition = new Vector3(cell.Pos.x, cell.Pos.y, cell.Pos.z);
            }
        }

        public IEnumerator Play()
        {
            while(true)
            {
                yield return new WaitWhile(() => IsPause);
                foreach(var cell in Map)
                {
                    cell.SetNextAlive(Rule);
                }
                yield return new WaitForSeconds(FrameDuration);
                foreach(var cell in Map)
                {
                    cell.Next();
                }
            }
        }

        public void Create(int width, int height, int length, int alivers)
        {
            Width = width;
            Height = height;
            Length = length;
            Setup();
            RandomAlive(alivers);
            foreach (var cell in _map)
            {
                var pos = cell.Pos;
                for(int i = -1; i < 2; i++)
                {
                    for(int j = -1; j < 2; j++)
                    {
                        for(int k = -1; k < 2; k++)
                        {
                            var x = (int)pos.x + i;
                            var y = (int)pos.y + j;
                            var z = (int)pos.z + k;
                            if((i == 0 && j == 0 && k == 0) ||
                                !(0 <= x && x < Width) ||
                                !(0 <= y && y < Height) || 
                                !(0 <= z && z < Length))
                            {
                                continue;
                            }
                            cell.AddAroundCell(_map[z,y,x]);
                        }
                    }
                }
            }
        }

        public void Setup()
        {
            foreach(Transform tf in transform)
            {
                Destroy(tf.gameObject);
            }
            var factory = new CellFactory();
            _map = new Cell[Width,Height,Length];
            for(int i = 0; i < Width; i++)
            {
                for(int j = 0; j < Height; j++)
                {
                    for(int k = 0; k < Length; k++)
                    {
                        var go = factory.Create();
                        var cell = go.GetComponent<Cell>();
                        cell.Pos = new Vector3(i, j, k);
                        _map[k,j,i] = cell;
                        go.transform.SetParent(transform);
                    }
                }
            }
        }

        public void RandomAlive(int alivers)
        {
            for(int i = 0; i < alivers; i++)
            {
                var x = UnityEngine.Random.Range(0,Width);
                var y = UnityEngine.Random.Range(0,Height);
                var z = UnityEngine.Random.Range(0,Length);
                if(_map[z,y,x].IsAlive)
                {
                    i--;
                    continue;
                }
                _map[z,y,x].IsAlive = true;
            }
        }
    }
}