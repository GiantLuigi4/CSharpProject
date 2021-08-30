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
            float[] vertices = {-0.5f,-0.5f,0f,
                0.5f, -0.5f, 0f,
                0f,0.5f,0f};
            int[] indices = { 0, 1, 2 };
            {
                VertexBufferObject vboVerticesa = new VertexBufferObject(mainWindow.getContext());
                vboVerticesa.uploadVertices(vertices, 0, 3);
                mainWindow.getContext().enableVertexAttribArray(0);
            }

            // TODO: asset pack type thing
            ShaderHolder shaderHolderM;
            {
                mainWindow.grabGLContext();
                Console.WriteLine("|Setup main window");
                Console.WriteLine("|-|Creating Shader Program");
                Console.WriteLine("|-|-|Creating shaders");
                Console.WriteLine("|-|-|-|Vertex Shader");
                util.rendering.Shader vertexShader = new util.rendering.Shader(ShaderType.VertexShader,
                    "in vec3 pos;\n" +
                    "void main() {\n" +
                    "   gl_Position = vec4(pos / 2., 1.0);\n" +
                    "}",
                    mainWindow.getContext()
                );
                Console.WriteLine("|-|-|-|Fragment Shader");
                util.rendering.Shader fragmentShader = new util.rendering.Shader(ShaderType.FragmentShader,
                    "in vec4 color;\n" +
                    "void main() {\n" +
                    "    gl_FragColor = vec4(gl_FragCoord.xyz / 2., 1);\n" +
                    "}",
                    mainWindow.getContext()
                );
                Console.WriteLine("|-|-|Link Shaders");
                ShaderProgram program = new ShaderProgram(vertexShader, fragmentShader, mainWindow.getContext());
                program.bindAttribute(0, "pos");
                program.bindAttribute(1, "color");
                program.link();

                shaderHolderM = new ShaderHolder(vertexShader, fragmentShader, program);
            }

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

                    shaderHolderM.start();

                    GL.begin(PrimitiveType.Triangles);
                    GL.vertexPosition(0, 0, 0);
                    GL.vertexPosition(1, 0, 0);
                    GL.vertexPosition(1, 1, 0);

                    GL.vertexPosition(-1, -1, 0);
                    GL.vertexPosition(1, -1, 0);
                    GL.vertexPosition(0, 0, 0);

                    GL.vertexPosition(0, 0, 0);
                    GL.vertexPosition(1, 0, 0);
                    GL.vertexPosition(1, -1, 0);

                    GL.end();

                    shaderHolderM.end();

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

            Console.WriteLine("|-|Cleaning up main window");
            mainWindow.grabGLContext();

            Console.WriteLine("|-|-|Deleting Shaders and Shader Programs");
            shaderHolderM.delete();
        }
    }
}
