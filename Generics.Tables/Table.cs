using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Tables
{
    public class TableItem<T, S, M>
    {
        public Dictionary<(T, S), M> ls = new Dictionary<(T, S), M>();
        public List<T> Rows = new List<T>();
        public List<S> Columns = new List<S>();
        public TableItem(Table<T, S, M> table)
        {
            ls = table.ls;
            Rows = table.Rows;
            Columns = table.Columns;
        }
        public M this [T row, S column]
        {
            get
            {
                if (ls.ContainsKey((row, column)))
                {
                    return ls[(row, column)];
                }
                else
                {
                    ls.Add((row, column), default(M));
                    return ls[(row, column)];
                }
            }
            set
            {
                if(!ls.ContainsKey((row, column)))
                {
                    if (!Rows.Contains(row))
                        Rows.Add(row);
                    if (!Columns.Contains(column))
                        Columns.Add(column);
                    ls.Add((row, column), value);
                }
                else
                {
                    ls[(row, column)] = value;
                }
            }
        }
    }
    public class TableExist<T, S, M>
    {
        public Dictionary<(T, S), M> ls = new Dictionary<(T, S), M>();
        public List<T> Rows = new List<T>();
        public List<S> Columns = new List<S>();
        public TableExist(Table<T, S, M> table)
        {
            ls = table.ls;
            Rows = table.Rows;
            Columns = table.Columns;
        }
        public M this[T row, S column]
        {
            get
            {
                if (Rows.Contains(row) && Columns.Contains(column))
                {
                    if (!ls.ContainsKey((row, column)))
                        ls.Add((row, column), default(M));
                    return ls[(row, column)];
                }
                else
                    throw new ArgumentException();
            }
            set
            {
                if (!ls.ContainsKey((row, column)) && Rows.Contains(row) && Columns.Contains(column))
                {
                    ls.Add((row, column), value);
                }
                else
                    throw new ArgumentException();
            }
        }
    }
    public class Table<T, S, M>
    {
        public List<T> Rows = new List<T>();
        public List<S> Columns = new List<S>();
        public Dictionary<(T, S), M> ls = new Dictionary<(T, S), M>();
        public TableItem<T, S, M> Open
        {
            get
            {
                return new TableItem<T, S, M>(this);
            }
        }
        public TableExist<T, S, M> Existed
        {
            get
            {
                return new TableExist<T, S, M>(this);
            }
        }
        public void AddRow(T value)
        {
            if (!Rows.Contains(value))
                Rows.Add(value);
        }
        public void AddColumn(S value)
        {
            if (!Columns.Contains(value))
                Columns.Add(value);
        }
    }
    
}
