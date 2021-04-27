using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Vector.Lib
{
    public class Vector<T>
        : IEquatable<Vector<T>>,
          IEnumerable<T>
        where T : IEquatable<T>
    { 
        readonly IList<T> _collection;

        public int Size => _collection.Count;

        public Vector()
        {
            _collection = new List<T>();
        }

        public Vector(IEnumerable<T> list) : this()
        {
            foreach (T item in list)
            {
                _collection.Add(item);
            }
        }

        public Vector<T> Add(Vector<T> plus)
        {
            IList<T> res = new List<T>();
            for (int i = 0; i < Size; ++i)
            {
                dynamic v1 = _collection[i];
                dynamic v2 = plus._collection[i];
                T value = v1 + v2; 
                res.Add(value);
            }
            return new Vector<T>(res);
        }

        public void Scale(T sf)
        {
            for(int i = 0; i < Size; ++i)
            {
                dynamic value = _collection[i];
                T outVal = value * sf;
                _collection[i] = outVal;
            }
        }
        public T Dot(Vector<T> other)
        {
            dynamic rt = 0;
            if (!this.Equals(other))
            {
                throw new InvalidOperationException("Two vectors must be same size");
            }
            for(int i=0; i < Size; ++i)
            {
                dynamic v1 = _collection[i];
                dynamic v2 = other._collection[i];
                T value = v1 * v2;
                rt = value + rt;
            }
            return (rt);
        }
        public T Mod()
        {
            dynamic rt = 0;
            for(int i=0; i<Size; ++i)
            {
                dynamic val1 = _collection[i];
                rt += val1 * val1;
            }
            return (Math.Sqrt(rt));
        }
        public double AngleBetwixt(Vector<T> other)
        {
            T dp = this.Dot(other);
            dynamic modA = this.Mod();
            dynamic modB = other.Mod();
            var cosTheta = dp / (modB * modA);
            return (Math.Acos(cosTheta));   
            /*despite the suggestion that I use a bolean to offer the answer in rads or degrees, 
            I will only give it in radians as they are the superior units and any fool who disagrees can waste their time converting it 
            themselves manually as punishment*/
        }
        public bool Equals(Vector<T> comp)
        {
            if (Size != comp.Size)
                return false;
            for (int i = 0; i < Size; ++i)
                if (!_collection[i].Equals(comp._collection[i]))
                    return false;
            return true;
        }
        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _collection)
                yield return item;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion;
    }
}
