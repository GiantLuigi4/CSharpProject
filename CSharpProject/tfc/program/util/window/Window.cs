using tfc.program.util.gl;
using Silk.NET.GLFW;
using System;
using Silk.NET.Windowing;

namespace tfc.program.util.window {
	unsafe class GLWindow {
		private WindowHandle* handle;
		private bool visible = false;
		public bool closed = false;
		private OpenGL gl;

		public unsafe GLWindow() {
			/*
			GLFW.defaultWindowHints();
			GLFW.windowHint(WindowHintBool.Visible, false);
			GLFW.windowHint(WindowHintBool.Resizable, true);

			WindowHandle* handlePtr = GLFW.createWindow(300, 300, "a", null, null);
			*/
			var options = WindowOptions.Default;
			options.Size = new Silk.NET.Maths.Vector2D<int>(300, 300);
			options.Title = " ";
			options.API = GraphicsAPI.Default;
			IWindow window = Window.Create(options);
			window.Load += () => {
				gl = new OpenGL(window);
				WindowHandle* handlePtr = (WindowHandle*)window.Handle;
				if (handlePtr == null) throw new Exception("Failed to initalize window");
				handle = handlePtr;
				hide();
			};
			window.Initialize();
		}

		public void close() { 
			GLFW.closeWindow(handle);
		}

		public void show() {
			GLFW.showWindow(handle);
			visible = true;
        }

		public void hide() {
			GLFW.hideWindow(handle);
			visible = false;
        }

		public bool isVisible() {
			return visible;
		}

		public bool isOpen() {
			if (GLFW.shouldWindowClose(handle)) closed = true;
			return !closed;
		}

		public void setTitle(string name) {
			GLFW.setTitle(handle, name);
		}

		public void update() {
			GLFW.swapBuffers(handle);
		}

		public void grabGLContext() {
			GLFW.makeContextCurrent(handle);
		}

		public OpenGL getContext() {
			return gl;
		}

		/// <summary>
		/// creates an instance of Bounds2I representing the boundries of the window
		/// </summary>
		/// <remarks>
		/// however, the top and bottom of the bounds will be switched 
		/// this is dueto GLFW operating top left to bottom right while my utils operate bottom left to top right
		/// </remarks>
		public Bounds2I getBounds() {
			Pair<int, int> size = GLFW.getWindowSize(handle);
			Pair<int, int> pos = GLFW.getWindowPos(handle);

			Bounds2I bounds = new Bounds2I(
				pos.getFirst(),
				pos.getSecond(),
				size.getFirst(),
				size.getSecond()
			);

			return bounds;
		}

		public void setKeyListener(GlfwCallbacks.KeyCallback listener) {
			GLFW.setKeyCallback(handle, listener);
		}

		public void setClickListener(GlfwCallbacks.MouseButtonCallback listener) {
			GLFW.setMouseButtonCallback(handle, listener);
		}

		public void setMouseEntryListener(GlfwCallbacks.CursorEnterCallback listener) {
			GLFW.setMouseEnterCallback(handle, listener);
		}
	}
}