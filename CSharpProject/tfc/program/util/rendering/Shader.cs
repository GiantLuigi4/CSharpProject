using tfc.program.util.gl;
using System;
using OpenGL;

// https://coffeebeancode.gitbook.io/lwjgl-game-design/tutorials/chapter-1-drawing-your-first-triangle
namespace tfc.program.util.rendering {
	class Shader {
		private ShaderType shaderType;
		private OpenGLW GL;
		private uint id;

		public Shader(ShaderType type, string src, OpenGLW gl) {
			this.shaderType = type;
			this.GL = gl;
			id = gl.createShader(type);
			gl.shaderSource(id, src);
			gl.compileShader(id);
			string log = gl.getShaderInfoLog(id);
			if (!log.Equals("")) Console.WriteLine(log);
		}

		public void delete() {
			GL.deleteShader(id);
		}

		public uint getID() {
			return id;
		}
	}
}