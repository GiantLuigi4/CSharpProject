using Silk.NET.OpenGL;
using Silk.NET.Core.Contexts;
using Silk.NET.Windowing;
using System;

namespace tfc.program.util.gl {
    sealed class OpenGL {
        GL gl;

        public unsafe OpenGL(IGLContext window) {
            gl = GL.GetApi(window);
        }

        public unsafe OpenGL(IWindow window) {
            gl = GL.GetApi(window);
        }

        public void clearColor(float r, float g, float b, float a) {
            gl.ClearColor(r, g, b, a);
        }

        public void clear(uint parameters) {
            gl.Clear(parameters);
        }

        public uint genVertexArrays() {
            uint[] id = new uint[1];
            gl.GenVertexArrays(1, id);
            return id[0];
        }

        public uint genBuffers() {
            return gl.GenBuffer();
        }

        public void deleteVAO(uint vaoid) {
            gl.DeleteVertexArray(vaoid);
        }

        public void bindBuffer(GLEnum target, uint id) {
            gl.BindBuffer(target, id);
        }

        public unsafe void bufferData<T>(GLEnum target, T[] data, GLEnum usage) where T : unmanaged {
            fixed (void* v = &data[0]) {
                gl.BufferData(target, (nuint)(data.Length * sizeof(T)), v, usage);
            }
        }

        public void vertexAttributePointer(uint attribute, int dimensions, GLEnum type, bool normalized, uint stride, uint pointer) {
            gl.VertexAttribPointer(attribute, dimensions, type, normalized, stride, pointer);
        }

        public void bindVertexArray(uint id) {
            gl.BindVertexArray(id);
        }

        public void enableVertexAttribArray(uint array) {
            gl.EnableVertexAttribArray(array);
        }

        public void disableVertexAttribArray(uint array) {
            gl.EnableVertexAttribArray(array);
        }

        public void drawElements(GLEnum mode, uint count, GLEnum type, in int indices) {
            gl.DrawElements(mode, count, type, indices);
        }

        public uint createShader(GLEnum type) {
            return gl.CreateShader(type);
        }

        public void shaderSource(uint shader, string src) {
            gl.ShaderSource(shader, src);
        }

        public void compileShader(uint shader) {
            gl.CompileShader(shader);
        }

        public string getShaderInfoLog(uint shader) {
            return gl.GetShaderInfoLog(shader);
        }

        public void deleteShader(uint id) {
            gl.DeleteShader(id);
        }

        public void attachShader(uint program, uint shader) {
            gl.AttachShader(program, shader);
        }

        public uint createProgram() {
            return gl.CreateProgram();
        }

        public string getProgramInfoLog(uint shader) {
            return gl.GetProgramInfoLog(shader);
        }

        public void linkProgram(uint id) {
            gl.LinkProgram(id);
        }

        public void useProgram(uint id) {
            gl.UseProgram(id);
        }
    }
}
