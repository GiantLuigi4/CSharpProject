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
            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 3 * sizeof(float), 0);
            Gl.VertexAttribPointer(2, 3, VertexAttribType.Float, false, 3 * sizeof(float), 0);
            Gl.VertexAttribPointer(3, 4, VertexAttribType.Float, false, 4 * sizeof(float), 0);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(2);
            Gl.EnableVertexAttribArray(3);
            // TODO: asset pack type thing
            ShaderHolder shaderHolderM;
            {
                mainWindow.grabGLContext();
                Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 3 * sizeof(float), 0);
                Gl.VertexAttribPointer(2, 3, VertexAttribType.Float, false, 3 * sizeof(float), 0);
                Gl.VertexAttribPointer(3, 4, VertexAttribType.Float, false, 4 * sizeof(float), 0);
                Gl.EnableVertexAttribArray(0);
                Gl.EnableVertexAttribArray(2);
                Gl.EnableVertexAttribArray(3);
                Console.WriteLine("|Setup main window");
                Console.WriteLine("|-|Creating Shader Program");
                Console.WriteLine("|-|-|Creating shaders");
                Console.WriteLine("|-|-|-|Vertex Shader");
                // Shader vertexShader = new Shader(ShaderType.VertexShader, "in vec3 position;varying vec2 surfacePosition;uniform vec2 start;uniform vec2 resolution;void main(){gl_Position=vec4(position,1.0);float x=sign(position.x);x-=start.x;float y=sign(position.y);y-=start.y;float maxRes=min(resolution.x,resolution.y)/max(resolution.x,resolution.y);/*TODO:figure out how to remove this branching*/if(resolution.x<resolution.y){surfacePosition=vec2(x,y/maxRes);}else{surfacePosition=vec2(x/maxRes,y);}}",  mainWindow.getContext());
                Shader vertexShader = new Shader(ShaderType.VertexShader,
                    "in vec3 position;\n" +
                    "in vec4 color;\n" +
                    "\n" +
                    "varying vec4 fragColor;\n" +
                    "\n" +
                    "void main() {\n" +
                    "   gl_Position = vec4(position, 1.);\n" +
                    "   fragColor = color;\n" +
                    "}\n"
                    ,
                    mainWindow.getContext()
                );
                Console.WriteLine("|-|-|-|Fragment Shader");
                Shader fragmentShader = new Shader(ShaderType.FragmentShader,
                    "in vec4 fragColor;\n" +
                    "\n" +
                    "void main() {\n" +
                    "   gl_FragColor = fragColor;\n" +
                    "}\n"
                    ,
                    mainWindow.getContext()
                );
                Console.WriteLine("|-|-|Link Shaders");
                ShaderProgram program = new ShaderProgram(vertexShader, fragmentShader, mainWindow.getContext());
                program.bindAttribute(0, "pos");
                program.bindAttribute(2, "normal");
                program.bindAttribute(3, "color");
                program.link();

                shaderHolderM = new ShaderHolder(vertexShader, fragmentShader, program);
            }

            uint buffer;
            {
                mainWindow.grabGLContext();
                OpenGLW GL = mainWindow.getContext();
                buffer = GL.genBuffers();
                GL.bindBuffer(BufferTarget.ArrayBuffer, buffer);
                GL.bufferData(BufferTarget.ArrayBuffer, new float[]{
                    -1.0f, -1.0f,  0.0f,     1.0F, 0.0F, 0.0F, 1.0F,
                     1.0f, -1.0f,  0.0f,     0.0F, 0.0F, 0.0F, 1.0F,
                    -1.0f,  1.0f,  0.0f,     0.0F, 0.0F, 1.0F, 1.0F,
                     1.0f, -1.0f,  0.0f,     0.0F, 1.0F, 0.0F, 1.0F,
                     1.0f,  1.0f,  0.0f,     0.0F, 1.0F, 1.0F, 1.0F,
                    -1.0f,  1.0f,  0.0f,     1.0F, 1.0F, 0.0F, 0.0F
                }, BufferUsage.StaticDraw);

                Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 3 * sizeof(float), 0);
                Gl.VertexAttribPointer(3, 4, VertexAttribType.Float, false, 4 * sizeof(float), 0);
                Gl.EnableVertexAttribArray(0);
                Gl.EnableVertexAttribArray(3);

                GL.bindBuffer(BufferTarget.ArrayBuffer, 0);
            }

            Console.WriteLine("|Game Loop");
            while (mainWindow.isOpen()) {
                cR += 1f / (255f * 2f);

                if (cR >= 1) {
                    cR = 0;
                }
                foreach (GLWindow window in windows) {
                    if (!window.isOpen()) window.hide();
                    window.grabGLContext();

                    OpenGLW GL = window.getContext();
                    GL.clearColor(cR, 0.0f, 0, 0.0f);
                    GL.clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    shaderHolderM.start();
                    int width = window.getWidth();
                    int height = window.getHeight();

                    GL.bindBuffer(BufferTarget.ArrayBuffer, buffer);
                    GL.drawArrays(PrimitiveType.Triangles, 0, 6);
                    GL.bindBuffer(BufferTarget.ArrayBuffer, 0);

                    /*GL.begin(PrimitiveType.Quads);
                    float x = (width / 2) - width / 4;
                    GL.vertexPosition(-1, -1, 0);
                    GL.vertexColor(0, 0, 0, 1);
                    GL.vertexPosition(0.6 - 1, -1, 0);
                    GL.vertexColor(1, 0, 0, 1);
                    GL.vertexPosition(0.6 - 1, 0.3 - 1, 0);
                    GL.vertexColor(1, 1, 0, 1);
                    GL.vertexPosition(-1, 0.3 -1, 0);
                    GL.vertexColor(0, 1, 0, 1);
                    GL.end();*/

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
