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
			if (gl.getShaderParameter(id, ShaderParameterName.ShaderSourceLength) == 0) {
				Console.WriteLine("Warning: Shader Length is 0, meaning either the shader failed to upload to the GPU or is empty");
			}
			if (!GL.getShaderParameterBool(id, ShaderParameterName.CompileStatus)) {
				string log = gl.getShaderInfoLog(id);
				Console.WriteLine(log);
				if (log.Equals("")) {
					Console.WriteLine();
					Console.WriteLine("Warning; Shader Log is empty");
					Console.WriteLine("Source: ");
                    Console.WriteLine(src);
					Console.WriteLine();
				}
			}
		}

		public void delete() {
			GL.deleteShader(id);
		}

		public uint getId() {
			return id;
		}
	}
}