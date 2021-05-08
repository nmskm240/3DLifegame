using System.Collections;
using System.Collections.Generic;
using Lifegame;

namespace Lifegame.Rules
{
    public class Model3D : IRule
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
                return (count % 2 == 0);
            }
        }
    }
}