using tfc.program.util.gl;
using System;
using Silk.NET.OpenGL;

// https://coffeebeancode.gitbook.io/lwjgl-game-design/tutorials/chapter-1-drawing-your-first-triangle
namespace tfc.program.util.rendering {
	class Shader {
		private GLEnum shaderType;
		private OpenGL GL;
		private uint id;

		public Shader(GLEnum type, string src, OpenGL gl) {
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