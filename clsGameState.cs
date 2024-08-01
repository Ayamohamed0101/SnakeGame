using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class clsGameState
    {

        public int rows { get; }
        public int cols { get; }
        public clsGridValues[,] grid { get; }
        public int Score { get; set; }
        public bool GameOver { get; private set; }
        public clsDirections dir { get; private set; }


        private readonly LinkedList<clsPosition> _SnakePosition = new LinkedList<clsPosition>();
        private readonly Random _random = new Random();

        public clsGameState(int row, int col)
        {
            this.rows = row;
            this.cols = col;

            grid = new clsGridValues[row, col];
            dir = clsDirections.Right;
            AddSnake();
            AddFood();
        }

        public void AddSnake()
        {
            int r = rows / 2;

            for (int i = 1; i <= 3; i++)
            {

                grid[r, i] = clsGridValues.Snake;
                _SnakePosition.AddFirst(new clsPosition(r, i));
            }
        }


        private IEnumerable<clsPosition> _EmptyPositions()
        {

            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < cols; j++)
                {
                    if (grid[i, j] == clsGridValues.Empty)
                    {
                        yield return new clsPosition(i, j);

                    }

                }


            }
        }


        private void AddFood()
        {
            List<clsPosition> empty = new List<clsPosition>(_EmptyPositions());
            if (empty.Count == 0)
            {
                return;
            }
            clsPosition position = empty[_random.Next(empty.Count)];
            grid[position.row, position.col] = clsGridValues.Food;
        }
        public clsPosition SnakeHeadPostion()
        {
            return _SnakePosition.First.Value;
        }
        public clsPosition SnakeTailPostion()
        {
            return _SnakePosition.Last.Value;
        }

        public IEnumerable<clsPosition> SnakePostions() 
        {
            return _SnakePosition;
        }
        private void AddHead(clsPosition pos)
        {
            _SnakePosition.AddLast(pos);
            grid[pos.row, pos.col] = clsGridValues.Snake;
        }
        private void RemoveTail()
        {clsPosition tail=_SnakePosition.Last.Value;
            grid[tail.row, tail.col]= clsGridValues.Empty;
            _SnakePosition.RemoveLast();

        }

        private void ChangDirection(clsDirections direction)
        {
            dir = direction;

        }
        private bool OutSideGrid(clsPosition pos)
        {
            return pos.row < 0 ||  pos.row >= rows  ||  pos.col < 0   || pos.col >= cols;

        }

        private clsGridValues WillHit(clsPosition newHeadPos)
        {
            if(OutSideGrid(newHeadPos))
            {
                return clsGridValues.Outside;
            }
            if(newHeadPos==SnakeHeadPostion())
            {
                return clsGridValues.Empty;
            }
            return grid[newHeadPos.row, newHeadPos.col];
        }

        public void MoveSnake()
        {
            clsPosition newHeadPosition = SnakeHeadPostion().translate(dir);
            clsGridValues hit=WillHit(newHeadPosition);
            if (hit==clsGridValues.Outside || hit==clsGridValues.Snake)
            {
                GameOver=true;

            }
            else if(hit==clsGridValues.Empty)
            {
                RemoveTail();
                AddHead(newHeadPosition);//forward step
            }
            else if(hit==clsGridValues.Food) 
            {
                Score++;
                AddHead(newHeadPosition);
                AddFood();
            
            
            }
           
        }


    }
}
