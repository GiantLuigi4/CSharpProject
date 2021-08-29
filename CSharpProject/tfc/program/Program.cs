﻿using System;
using tfc.program.util.window;
using tfc.program.util.gl;
using tfc.program.util;
using Silk.NET.OpenGL;
using tfc.program.util.rendering;

// https://coffeebeancode.gitbook.io/lwjgl-game-design/tutorials/chapter-1-drawing-your-first-triangle
// https://learnopengl.com/Getting-started/Shaders
namespace tfc.program {
    class Program {
        private static float cR = 0;

        static void Main(string[] args) {
            if (!GLFW.init()) throw new Exception("Failed to initalize glfw");
            GenericList<GLWindow> windows = new GenericList<GLWindow>(1);
            Console.WriteLine("Creating main window");
            GLWindow mainWindow = new GLWindow();
            {
                mainWindow.setTitle("Test");
                mainWindow.show();
                windows.add(mainWindow);
            }
            Console.WriteLine("Creating secondary window");
            {
                GLWindow secondWindow = new GLWindow();
                {
                    secondWindow.setTitle("Test1");
                    secondWindow.show();
                    windows.add(secondWindow);
                }
            }
            float[] vertices = {-0.5f,-0.5f,0f,
                0.5f, -0.5f, 0f,
                0f,0.5f,0f};
            int[] indices = { 0, 1, 2 };
            Console.WriteLine("Creating VAO");
            VertexArrayObject vao = new VertexArrayObject(mainWindow.getContext());
            vao.bind();
            Console.WriteLine("Creating VBOs");
            VertexBufferObject vboVertices = new VertexBufferObject(mainWindow.getContext());
            VertexBufferObject vboIndices = new VertexBufferObject(mainWindow.getContext());
            Console.WriteLine("Uploading VBOs");
            vboVertices.uploadVertices(vertices, 0, 3);
            vboIndices.uploadIndicies(indices);
            vao.countIndices(vboIndices);
            vao.unbind();

            // TODO: asset pack type thing
            Console.WriteLine("Creating shaders");
            util.rendering.Shader vertexShader = new util.rendering.Shader(GLEnum.VertexShader,
                "#version 330 core\n" +
                "layout (location = 0) in vec3 pos;\n" +
                "void main() {\n" +
                "   gl_Position = vec4(pos, 1.0);\n" +
                "}\0", 
                mainWindow.getContext()
            );
            util.rendering.Shader fragmentShader = new util.rendering.Shader(GLEnum.FragmentShader,
                "#version 330 core\n" +
                "out vec4 FragColor;\n" +
                "void main() {\n" +
                "    FragColor = vec4(1);\n" +
                "}\0", 
                mainWindow.getContext()
            );

            Console.WriteLine("Creating Shader Program");
            ShaderProgram program = new ShaderProgram(vertexShader, fragmentShader, mainWindow.getContext());

            Console.WriteLine("Game Loop");
            while (mainWindow.isOpen()) {
                cR += 1f / (255f * 2f);
                float b = 0;

                if (cR >= 1) {
                    cR = 0;
                }
                foreach (GLWindow window in windows) {
                    if (!window.isOpen()) window.hide();
                    window.grabGLContext();

                    OpenGL GL = window.getContext();
                    GL.clearColor(cR, 0.0f, b, 0.0f);
                    GL.clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

                    if (b == 0) {
                        b = 1;

                        program.use();
                        vao.bind();
                        GL.enableVertexAttribArray(0);
                        GL.drawElements(GLEnum.Triangles, (uint)vao.getVertexCount(), GLEnum.Float, 0);
                        GL.disableVertexAttribArray(0);
                        vao.unbind();
                        program.end();
                    }

//                    window.getBounds();
                    window.update();
                    GLFW.pollEvents();
                }
            }
            windows.remove(mainWindow);
            Console.WriteLine("Closing lasting windows");
            foreach (GLWindow window in windows) {
                window.close();
            }

            Console.WriteLine("Disposing lasting objects");
            vboVertices.Dispose();
            vboIndices.Dispose();
            vao.Dispose();

            Console.WriteLine("Deleting Shaders");
            vertexShader.delete();
        }
    }
}