using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace tfc.program.util {
    abstract class Iterable<T> : IEnumerable {
        public IEnumerator GetEnumerator() {
            return getIterator().enumerator();
        }

        public abstract Iterator<T> getIterator();
    }

    class Enumerator<T> : IEnumerator {
        private Iterator<T> iterable;

        public Enumerator(Iterator<T> iterable) {
            this.iterable = iterable;
        }

        public T currentObj;
        public object Current => currentObj;

        public bool MoveNext() {
            if (iterable.hasNext()) {
                currentObj = iterable.next();
                return true;
            }
            return false;
        }

        public void Reset() {
            if (iterable.supportsReset()) {
                iterable.reset();
            }
        }
    }

    abstract class Iterator<T> {
        public Enumerator<T> enumerator() {
            return new Enumerator<T>(this);
        }

        public abstract bool hasNext();

        public abstract T next();

        public virtual void reset() {
            throw new AccessViolationException("This iterable does not support resetting");
        }

        public virtual bool supportsReset() {
            return false;
        }
    }
}
