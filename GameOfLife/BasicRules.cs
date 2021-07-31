using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal static class BasicRules
    {
        private static void DiesOfUnderpopulation(BoardController controller, Vector cell)
        {
            var board = controller.Board;
            if (!board.IsCellAlive(cell))
            {
                return;
            }
            if (board.GetNeighborCount(cell) < 2)
            {
                controller.KillCell(cell);
            }
        }
        private static void DiesOfOverpopulation(BoardController controller, Vector cell)
        {
            var board = controller.Board;
            if (!board.IsCellAlive(cell))
            {
                return;
            }
            if (board.GetNeighborCount(cell) > 3)
            {
                controller.KillCell(cell);
            }
        }
        private static void CreateLife(BoardController controller, Vector cell)
        {
            var board = controller.Board;
            if (board.IsCellAlive(cell))
            {
                return;
            }
            if (board.GetNeighborCount(cell) == 3)
            {
                controller.AddCell(cell);
            }
        }
        public static void RegisterRules(List<Rule> rules)
        {
            rules.Add(DiesOfUnderpopulation);
            // "lives" doesnt need to be implemented, as it already implicitly happens
            rules.Add(DiesOfOverpopulation);
            rules.Add(CreateLife);
        }
    }
}
