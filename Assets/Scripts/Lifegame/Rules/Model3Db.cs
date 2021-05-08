using System.Collections;
using System.Collections.Generic;
using Lifegame;

namespace Lifegame.Rules
{
    //ルール
    //自分の周りで9つ以上死んでいるとき
    //生きてるとき　->　2つから4つが生きていれば生存
    //死んでるとき　->　5～8個生存していた場合生成
    public class Model3Db : IRule
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
                return (2<=count && count<=4);
            }
            else
            {
                return (4<count && count<9);
            }
        }
    }
}