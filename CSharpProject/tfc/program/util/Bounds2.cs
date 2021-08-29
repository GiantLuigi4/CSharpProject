namespace tfc.program.util {
	class Bounds2I {
		private int x, y;
		private int width, height;
		
		public Bounds2I(int x, int y, int width, int height) {
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public int getX() {
			return x;
		}

		public int getY() {
			return y;
		}

		public int getW() {
			return width;
		}

		public int getWidth() {
			return width;
		}

		public int getH() {
			return height;
		}

		public int getHeight() {
			return height;
		}

		public int getRight() {
			return x + width;
		}

		public int getTop() {
			return y + height;
		}

		public int getBottom() {
			return y;
		}

		public int getLeft() {
			return x;
		}
	}

	// TODO: float/double/long
}