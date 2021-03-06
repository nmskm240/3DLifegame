using System.Collections;
using System.Collections.Generic;
using Lifegame;

namespace Lifegame.Rules
{
    //ルール
    //自分の周りの生きてる数が偶数か奇数かで生死を判断
    public class Model3Da : IRule
    {
        public bool NextAlive(Cell cell)
        {
            var count = 0;
            foreach (var aroundCell in cell.AroundCells)
            {
                count += (aroundCell.IsAlive) ? 1 : 0;
            }
            if(cell.IsAlive)
            {
                return (count % 2 == 0);
            }
            else
            {
                return (count % 2 == 1);
            }
        }
    }
}