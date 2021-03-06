using System;
using System.Collections.Generic;
using System.Text;
using tfc.program.util.rendering;
using tfc.program.util.gl;

namespace tfc.program.util.rendering {
    class ShaderProgram {
        private OpenGLW GL;
        private uint id;
        private uint vertId;
        private uint fragId;
        private Dictionary<string, int> uniformMap = new Dictionary<string, int>();

        public ShaderProgram(Shader vertex, Shader fragment, OpenGLW gl) {
            this.GL = gl;
            id = gl.createProgram();
            gl.attachShader(id, vertex.getId());
            gl.attachShader(id, fragment.getId());
            vertId = vertex.getId();
            fragId = fragment.getId();
        }

        public void start() {
            GL.useProgram(id);
        }

        public void end() {
            GL.useProgram(0);
        }

        public void setUniform(string name, float x, float y) {
            GL.uniform2(uniformMap[name], x, y);
        }

        public void delete() {
            GL.detachShader(id, vertId);
            GL.detachShader(id, fragId);
            GL.deleteProgram(id);
        }

        public void bindAttribute(uint index, string name) {
            GL.bindAttribute(id, index, name);
        }

        public void link() {
            GL.linkProgram(id);
            GL.validateProgram(id);
            string log = GL.getProgramInfoLog(id);
            if (!log.Equals("")) Console.WriteLine(log);
        }

        public int getUniformLocation(String name) {
            int loc = GL.getUniformLocation(id, name);
            uniformMap.Add(name, loc);
            return loc;
        }
    }
}
