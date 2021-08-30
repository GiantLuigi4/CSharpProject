using System;
using System.Collections.Generic;
using System.Text;
using tfc.program.util.rendering;

namespace tfc.program.util {
    class ShaderHolder {
        public readonly Shader vert;
        public readonly Shader frag;
        public readonly ShaderProgram program;

        public ShaderHolder(Shader vert, Shader frag, ShaderProgram program) {
            this.vert = vert;
            this.frag = frag;
            this.program = program;
        }

        public void delete() {
            program.delete();
            vert.delete();
            frag.delete();
        }

        public void start() {
            program.start();
        }

        public void end() {
            program.end();
        }
    }
}
