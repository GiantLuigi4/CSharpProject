using System;
using System.Collections.Generic;
using System.Text;
using tfc.program.util.rendering;
using tfc.program.util.gl;

namespace tfc.program.util.rendering {
    class ShaderProgram {
        private OpenGL GL;
        private uint id;

        public ShaderProgram(Shader vertex, Shader fragment, OpenGL gl) {
            this.GL = gl;
            id = gl.createProgram();
            gl.attachShader(id, vertex.getID());
            gl.attachShader(id, fragment.getID());
            gl.linkProgram(id);
            string log = gl.getProgramInfoLog(id);
            if (!log.Equals("")) Console.WriteLine(log);
        }

        public void use() {
            GL.useProgram(id);
        }

        public void end() {
            GL.useProgram(0);
        }
    }
}
