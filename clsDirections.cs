using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class clsDirections
    {
        public int RowOffSet { get;  }
        public int ColumnOffSet { get; }
        private clsDirections(int row, int column) 
        { 
        RowOffSet = row;
        ColumnOffSet = column;
        
        }

        public static readonly clsDirections left= new clsDirections(0,-1);
        public static readonly clsDirections Right = new clsDirections(0, 1);
        public static readonly clsDirections UP = new clsDirections(-1, 0);
        public static readonly clsDirections Down = new clsDirections(1, 0);

        public clsDirections Opposite()
        {
            return new clsDirections(-RowOffSet, -ColumnOffSet);
        }

        public override bool Equals(object obj)
        {
            return obj is clsDirections directions &&
                   RowOffSet == directions.RowOffSet &&
                   ColumnOffSet == directions.ColumnOffSet;
        }

        public override int GetHashCode()
        {
            int hashCode = 945188820;
            hashCode = hashCode * -1521134295 + RowOffSet.GetHashCode();
            hashCode = hashCode * -1521134295 + ColumnOffSet.GetHashCode();
            return hashCode;
        }

        public static bool operator == (clsDirections left, clsDirections right)
        {
            return EqualityComparer<clsDirections>.Default.Equals(left, right);
        }

        public static bool operator !=(clsDirections left, clsDirections right)
        {
            return !(left == right);
        }
    }
}
