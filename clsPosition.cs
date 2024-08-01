using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public  class clsPosition
    
    {
        public int row { get; }
        public int col { get; }
        public clsPosition(int r,int c)
        {
            row = r;
            col = c;

            
        }

        public override bool Equals(object obj)
        {
            return obj is clsPosition position &&
                   row == position.row &&
                   col == position.col;
        }

        public override int GetHashCode()
        {
            int hashCode = -1720622044;
            hashCode = hashCode * -1521134295 + row.GetHashCode();
            hashCode = hashCode * -1521134295 + col.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(clsPosition left, clsPosition right)
        {
            return EqualityComparer<clsPosition>.Default.Equals(left, right);
        }

        public static bool operator !=(clsPosition left, clsPosition right)
        {
            return !(left == right);
        }
        public clsPosition translate(clsDirections direction)
        {
            return new clsPosition(row+direction.RowOffSet, col+direction.ColumnOffSet);
        }

       

    }
}
