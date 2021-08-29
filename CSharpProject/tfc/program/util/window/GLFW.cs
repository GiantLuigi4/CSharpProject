// using Silk.NET.GLFW;
using glfw3;

namespace tfc.program.util.window {
	class GLFW {
		public static void defaultWindowHints() {
			Glfw.DefaultWindowHints();
		}

		public static bool init() {
			return Glfw.Init() != 0;
		}

		// TODO: enum this
		public static void windowHint(int hintName, bool value) {
			Glfw.WindowHint(hintName, value ? 1 : 0);
        }

		public static unsafe GLFWwindow createWindow(int width, int height, string title, GLFWmonitor monitor, GLFWwindow share) {
			return Glfw.CreateWindow(width, height, title, monitor, share);
        }

		public static unsafe Pair<int, int> getWindowSize(GLFWwindow handle) {
			int width = 0;
			int height = 0;
			Glfw.GetWindowSize(handle, ref width, ref height);
			return new Pair<int, int>(width, height);
        }

		public static unsafe Pair<int, int> getWindowPos(GLFWwindow handle) {
			int x = 0;
			int y = 0;
			Glfw.GetWindowPos(handle, ref x, ref y);
			return new Pair<int, int>(x, y);
        }

		public static unsafe void showWindow(GLFWwindow handle) {
			Glfw.ShowWindow(handle);
		}

		public static unsafe void hideWindow(GLFWwindow handle) {
			Glfw.HideWindow(handle);
		}

		public static unsafe void closeWindow(GLFWwindow handle) {
			Glfw.SetWindowShouldClose(handle, 1);
		}

		public static unsafe bool shouldWindowClose(GLFWwindow handle) {
			return Glfw.WindowShouldClose(handle) != 0;
		}

		public static unsafe void swapBuffers(GLFWwindow handle) {
			Glfw.SwapBuffers(handle);
		}

		public static void pollEvents() {
			Glfw.PollEvents();
		}

		public static unsafe void setTitle(GLFWwindow handle, string name) {
			Glfw.SetWindowTitle(handle, name);
		}

		public static unsafe void makeContextCurrent(GLFWwindow handle) {
			Glfw.MakeContextCurrent(handle);
		}

		public static unsafe void setSwapInterval(int interval) {
			Glfw.SwapInterval(interval);
		}

		public static unsafe void setKeyCallback(GLFWwindow handle, GLFWkeyfun callback) {
			Glfw.SetKeyCallback(handle, callback);
		}

		public static unsafe void setMouseButtonCallback(GLFWwindow handle, GLFWmousebuttonfun callback) {
			Glfw.SetMouseButtonCallback(handle, callback);
		}

		public static unsafe void setMouseMotionCallback(GLFWwindow handle, GLFWcursorposfun callback) {
			Glfw.SetCursorPosCallback(handle, callback);
		}

		public static unsafe void setMouseEnterCallback(GLFWwindow handle, GLFWcursorenterfun callback) {
			Glfw.SetCursorEnterCallback(handle, callback);
		}
	}
}