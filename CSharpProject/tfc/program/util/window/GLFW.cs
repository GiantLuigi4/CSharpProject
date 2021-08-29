using Silk.NET.GLFW;

namespace tfc.program.util.window {
	class GLFW {
		private static Glfw api = Glfw.GetApi();

		public static bool init() {
			return api.Init();
		}

		public static void defaultWindowHints() {
			api.DefaultWindowHints();
		}

		public static void windowHint(WindowHintBool hintName, bool value) {
			api.WindowHint(hintName, value);
        }

		public static unsafe WindowHandle* createWindow(int width, int height, string title, Monitor* monitor, WindowHandle* share) {
			return api.CreateWindow(width, height, title, monitor, share);
        }

		public static unsafe Pair<int, int> getWindowSize(WindowHandle* handle) {
			int width = 0;
			int height = 0;
			api.GetWindowSize(handle, out width, out height);
			return new Pair<int, int>(width, height);
        }

		public static unsafe Pair<int, int> getWindowPos(WindowHandle* handle) {
			int x = 0;
			int y = 0;
			api.GetWindowPos(handle, out x, out y);
			return new Pair<int, int>(x, y);
        }

		public static unsafe void showWindow(WindowHandle* handle) {
			api.ShowWindow(handle);
		}

		public static unsafe void hideWindow(WindowHandle* handle) {
			api.HideWindow(handle);
		}

		public static unsafe void closeWindow(WindowHandle* handle) {
			api.SetWindowShouldClose(handle, true);
		}

		public static unsafe bool shouldWindowClose(WindowHandle* handle) {
			return api.WindowShouldClose(handle);
		}

		public static unsafe void swapBuffers(WindowHandle* handle) {
			api.SwapBuffers(handle);
		}

		public static void pollEvents() {
			api.PollEvents();
		}

		public static unsafe void setTitle(WindowHandle* handle, string name) {
			api.SetWindowTitle(handle, name);
		}

		public static unsafe void makeContextCurrent(WindowHandle* handle) {
			api.MakeContextCurrent(handle);
		}

		public static unsafe void setSwapInterval(int interval) {
			api.SwapInterval(interval);
		}

		public static Glfw getInstance() {
			return api;
		}

		public static unsafe void setKeyCallback(WindowHandle* handle, GlfwCallbacks.KeyCallback callback) {
			api.SetKeyCallback(handle, callback);
		}

		public static unsafe void setMouseButtonCallback(WindowHandle* handle, GlfwCallbacks.MouseButtonCallback callback) {
			api.SetMouseButtonCallback(handle, callback);
		}

		public static unsafe void setMouseMotionCallback(WindowHandle* handle, GlfwCallbacks.CursorPosCallback callback) {
			api.SetCursorPosCallback(handle, callback);
		}

		public static unsafe void setMouseEnterCallback(WindowHandle* handle, GlfwCallbacks.CursorEnterCallback callback) {
			api.SetCursorEnterCallback(handle, callback);
		}
	}
}