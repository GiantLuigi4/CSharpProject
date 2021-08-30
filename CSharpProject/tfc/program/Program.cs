using System;
using tfc.program.util.window;
using tfc.program.util.gl;
using tfc.program.util;
using tfc.program.util.rendering;
using OpenGL;

// https://coffeebeancode.gitbook.io/lwjgl-game-design/tutorials/chapter-1-drawing-your-first-triangle
// https://learnopengl.com/Getting-started/Shaders
namespace tfc.program {
    class Program {
        private static float cR = 0;

        static void Main(string[] args) {
            if (!GLFW.init()) throw new Exception("Failed to initalize glfw");
            GenericList<GLWindow> windows = new GenericList<GLWindow>(1);
            Console.WriteLine("|Creating windows");
            Console.WriteLine("|-|Creating main window");
            GLWindow mainWindow = new GLWindow();
            {
                mainWindow.setTitle("Test");
                mainWindow.show();
                windows.add(mainWindow);
            }
            Console.WriteLine("|-|Creating secondary window");
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
            VertexBufferObject vboVertices = new VertexBufferObject(mainWindow.getContext());
            vboVertices.uploadVertices(vertices, 0, 3);
            mainWindow.getContext().enableVertexAttribArray(0);

            /*Console.WriteLine("|Creating VAO");
            VertexArrayObject vao = new VertexArrayObject(mainWindow.getContext());
            vao.bind();
            Console.WriteLine("|-|Creating VBOs");
            VertexBufferObject vboVertices = new VertexBufferObject(mainWindow.getContext());
            VertexBufferObject vboIndices = new VertexBufferObject(mainWindow.getContext());
            Console.WriteLine("|-|Uploading VBOs");
            vboVertices.uploadVertices(vertices, 0, 3);
            vboIndices.uploadIndicies(indices);
            mainWindow.getContext().enableVertexAttribArray(0);
            vao.countIndices(vboIndices);
            vao.unbind();*/

            Console.WriteLine("|Creating Shader Program");
            Console.WriteLine("|-|Creating shaders");
            Console.WriteLine("|-|-|Vertex Shader");
            // TODO: asset pack type thing
            util.rendering.Shader vertexShader = new util.rendering.Shader(ShaderType.VertexShader,
                "#version 330 core\n" +
                "in vec3 pos;\n" +
                "void main() {\n" +
                "   gl_Position = vec4(pos / 2., 1.0);\n" +
                "}\0",
                mainWindow.getContext()
            );
            Console.WriteLine("|-|-|Fragment Shader");
            util.rendering.Shader fragmentShader = new util.rendering.Shader(ShaderType.FragmentShader,
                "#version 330 core\n" +
                "out vec4 FragColor;\n" +
                "void main() {\n" +
                "    FragColor = vec4(0.5, 0, 1, 1);\n" +
                "}\0", 
                mainWindow.getContext()
            );
            Console.WriteLine("|-|Link Shaders");
            ShaderProgram program = new ShaderProgram(vertexShader, fragmentShader, mainWindow.getContext());
            program.bindAttribute(0, "pos");
            program.link();

            Console.WriteLine("|Game Loop");
            while (mainWindow.isOpen()) {
                cR += 1f / (255f * 2f);
                int b = 0;

                if (cR >= 1) {
                    cR = 0;
                }
                foreach (GLWindow window in windows) {
                    if (!window.isOpen()) window.hide();
                    window.grabGLContext();

                    OpenGLW GL = window.getContext();
                    GL.clearColor(cR, 0.0f, b, 0.0f);
                    GL.clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    program.start();
                    if (b == 0) {
                        b = 1;

                        GL.begin(PrimitiveType.Triangles);
                        GL.vertexPosition(0, 0, 0);
                        GL.vertexPosition(1, 0, 0);
                        GL.vertexPosition(1, 1, 0);
                        GL.end();
                    } else {
                        GL.begin(PrimitiveType.Triangles);
                        GL.vertexPosition(-1, -1, 0);
                        GL.vertexPosition(1, -1, 0);
                        GL.vertexPosition(0, 0, 0);

                        GL.vertexPosition(0, 0, 0);
                        GL.vertexPosition(1, 0, 0);
                        GL.vertexPosition(1, -1, 0);
                        GL.end();
                    }
                    program.end();


                    // window.getBounds();
                    window.update();
                    GLFW.pollEvents();
                }
            }
            windows.remove(mainWindow);
            Console.WriteLine("|Finalize");
            Console.WriteLine("|-|Closing lasting windows");
            foreach (GLWindow window in windows) {
                window.close();
            }

            /*Console.WriteLine("|-|Disposing lasting vertex objects");
            vboVertices.Dispose();
            vboIndices.Dispose();
            vao.Dispose();*/

            Console.WriteLine("|-|Deleting Shaders");
            vertexShader.delete();
            fragmentShader.delete();

            Console.WriteLine("|-|Deleting Shader Program");
            program.delete();
        }
    }
}
