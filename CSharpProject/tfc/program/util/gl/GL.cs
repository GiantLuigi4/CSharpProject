// using Silk.NET.OpenGL;
// using Silk.NET.Core.Contexts;
// using Silk.NET.Windowing;
using System;
using System.Text;
using glfw3;
using OpenGL;

namespace tfc.program.util.gl {
    sealed class OpenGLW {
        public OpenGLW(GLFWwindow window) {
        }

        public void clearColor(float r, float g, float b, float a) {
            Gl.ClearColor(r, g, b, a);
        }

        public void clear(ClearBufferMask parameters) {
            Gl.Clear(parameters);
        }

        public uint genVertexArrays() {
            /*uint[] id = new uint[1];
            Gl.GenVertexArrays(id);
            return id[0];*/
            return Gl.GenVertexArray();
        }

        public uint genBuffers() {
            return Gl.GenBuffer();
        }

        public void deleteVAO(uint vaoid) {
            Gl.DeleteVertexArrays(vaoid);
        }

        public void bindBuffer(BufferTarget target, uint id) {
            Gl.BindBuffer(target, id);
        }

        public unsafe void bufferData<T>(BufferTarget target, T[] data, BufferUsage usage) where T : unmanaged {
            fixed (void* v = &data[0]) {
                Gl.BufferData(target, (uint)(data.Length * sizeof(T)), new IntPtr(v), usage);
            }
        }

        public void vertexAttributePointer(uint attribute, int dimensions, VertexAttribType type, bool normalized, int stride, int pointer) {
            Gl.VertexAttribPointer(attribute, dimensions, type, normalized, stride, pointer);
        }

        public void bindVertexArray(uint id) {
            Gl.BindVertexArray(id);
        }

        public void enableVertexAttribArray(uint array) {
            Gl.EnableVertexAttribArray(array);
        }

        public void disableVertexAttribArray(uint array) {
            Gl.EnableVertexAttribArray(array);
        }

        public void drawElements(PrimitiveType mode, int count, DrawElementsType type, in int indices) {
            Gl.DrawElements(mode, count, type, indices);
        }

        public void drawArrays(PrimitiveType mode, int first, int count) {
            Gl.DrawArrays(mode, first, count);
        }

        public uint createShader(ShaderType type) {
            return Gl.CreateShader(type);
        }

        public void shaderSource(uint shader, string src) {
            Gl.ShaderSource(shader, new string[] { src }, new int[] { src.Length });
        }

        public void compileShader(uint shader) {
            Gl.CompileShader(shader);
        }

        public void getShaderInfoLog(uint shader, int maxLength, out int length, StringBuilder text) {
            Gl.GetShaderInfoLog(shader, maxLength, out length, text);
        }

        public string getShaderInfoLog(uint shader) {
            StringBuilder builder = new StringBuilder();
            int len = 0;
            Gl.GetShaderInfoLog(shader, 1000, out len, builder);
            return builder.ToString(0, len);
        }

        public void deleteShader(uint id) {
            Gl.DeleteShader(id);
        }

        public void attachShader(uint program, uint shader) {
            Gl.AttachShader(program, shader);
        }

        public uint createProgram() {
            return Gl.CreateProgram();
        }

        public void getProgramInfoLog(uint program, int maxLength, out int length, StringBuilder text) {
            Gl.GetProgramInfoLog(program, maxLength, out length, text);
        }

        public string getProgramInfoLog(uint program) {
            StringBuilder builder = new StringBuilder();
            int len = 0;
            Gl.GetProgramInfoLog(program, 1000, out len, builder);
            return builder.ToString(0, len);
        }

        public void linkProgram(uint id) {
            Gl.LinkProgram(id);
        }

        public void useProgram(uint id) {
            Gl.UseProgram(id);
        }

        public void deleteProgram(uint id) {
            Gl.DeleteProgram(id);
        }

        public void bindAttribute(uint id, uint index, string name) {
            Gl.BindAttribLocation(id, index, name);
        }

        public void begin(PrimitiveType mode) {
            Gl.Begin(mode);
        }

        public void end() {
            Gl.End();
        }

        public void vertexPosition(double x, double y, double z) {
            Gl.Vertex3(x, y, z);
        }

        public void vertexPosition(double x, double y) {
            Gl.Vertex2(x, y);
        }

        public void validateProgram(uint id) {
            Gl.ValidateProgram(id);
        }
    }
}
