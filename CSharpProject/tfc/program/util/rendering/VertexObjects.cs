using tfc.program.util.gl;
using System;
using OpenGL;

// https://coffeebeancode.gitbook.io/lwjgl-game-design/tutorials/chapter-1-drawing-your-first-triangle
namespace tfc.program.util.rendering {
	class VertexArrayObject : IDisposable {
		private uint id;
		private int vertices;
		private OpenGLW graphicsLibrary;

		public VertexArrayObject(OpenGLW graphicsLibrary) {
			this.graphicsLibrary = graphicsLibrary;
			id = graphicsLibrary.genVertexArrays();
			GC.SuppressFinalize(this);
		}

		public VertexArrayObject(OpenGLW graphicsLibrary, uint id, int vertices) {
			this.graphicsLibrary = graphicsLibrary;
			this.id = id;
			this.vertices = vertices;
		}

		public uint getID() {
			return id;
		}

		public void bind() {
			graphicsLibrary.bindVertexArray(id);
		}

		public void unbind() {
			graphicsLibrary.bindVertexArray(0);
		}

		public int getVertexCount() {
			return vertices;
		}
		
		~VertexArrayObject() {
			Console.WriteLine("Warning; A VAO was not Disposed!");
		}

		public void Dispose() {
			graphicsLibrary.deleteVAO(id);
			GC.SuppressFinalize(this);
		}

        public void countIndices(VertexBufferObject indices) {
			this.vertices = indices.getSize();
        }
    }

	class VertexBufferObject : IDisposable { 
		private uint id;
		private OpenGLW graphicsLibrary;
		private int size;

		public VertexBufferObject(OpenGLW GL) {
			this.graphicsLibrary = GL;
			id = graphicsLibrary.genBuffers();
		}

		~VertexBufferObject() {
			Console.WriteLine("Warning; A VAO was not Disposed!");
			Dispose(false);
		}

		public virtual void Dispose() {
			GC.SuppressFinalize(this);
			Dispose(true);
		}

		public virtual void Dispose(bool disposing) {
			graphicsLibrary.deleteVAO(id);
			GC.SuppressFinalize(this);
		}

		// TODO: figure out if there's an enum I can replace attribute with
		public void uploadVertices(float[] vertices, int dimensions) {
			graphicsLibrary.bindBuffer(BufferTarget.ArrayBuffer, id);
			graphicsLibrary.bufferData(BufferTarget.ArrayBuffer, vertices, BufferUsage.StaticDraw);
//			graphicsLibrary.bindBuffer(GLEnum.ArrayBuffer, 0);
			size = vertices.Length / dimensions;
		}

		public void uploadIndicies(int[] data) {
			graphicsLibrary.bindBuffer(BufferTarget.ElementArrayBuffer, id);
			graphicsLibrary.bufferData(BufferTarget.ElementArrayBuffer, data, BufferUsage.StaticDraw);
//			graphicsLibrary.bindBuffer(GLEnum.ElementArrayBuffer, 0);
			size = data.Length;
		}

		public int getSize() {
			return size;
        }

        public uint getId() {
			return id;
        }
    }
}