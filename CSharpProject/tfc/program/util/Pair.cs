namespace tfc.program.util {
	class Pair<T, V> {
		private T obj0;
		private V obj1;

		public Pair(T first, V second) {
			obj0 = first;
			obj1 = second;
		}

		public T getFirst() {
			return obj0;
		}

		public V getSecond() {
			return obj1;
		}
	}
}