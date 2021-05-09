using System.Collections;
using System.Collections.Generic;
using Lifegame;

namespace Lifegame.Rules
{
    public class Model3Dd : IRule
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
                return (count == 2 ||
                count == 3 ||
                count == 5 ||
                count == 7 ||
                count == 11 ||
                count == 13 ||
                count == 17 ||
                count == 19 ||
                count == 23 );
            }
            else
            {
                return (count==2);
            }
        }
    }
}