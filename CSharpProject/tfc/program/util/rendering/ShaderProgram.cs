﻿using System;
using System.Collections.Generic;
using System.Text;
using tfc.program.util.rendering;
using tfc.program.util.gl;

namespace tfc.program.util.rendering {
    class ShaderProgram {
        private OpenGLW GL;
        private uint id;

        public ShaderProgram(Shader vertex, Shader fragment, OpenGLW gl) {
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

        public void delete() {
            GL.deleteProgram(id);
        }

        public void bindAttribute(uint index, string name) {
            GL.bindAttribute(id, index, name);
        }
    }
}
