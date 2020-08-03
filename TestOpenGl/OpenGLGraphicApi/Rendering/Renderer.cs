using System;
using System.Collections.Generic;
using OpenGL;
using OpenGLBinder = Tao.FreeGlut.Glut;


namespace TestOpenGL.Rendering
{
    public class Renderer
    {
        private const int Width = 1280;
        private const int Height = 720;
        private const float Aspect = (float) Width / Height;

        private readonly List<SceneObject> objects = new List<SceneObject>();
        private readonly Camera cam = new Camera(Aspect);
        private ShaderProgram myProgram;
        public void Run()
        {
            // create an OpenGL window
            OpenGLBinder.glutInit();
            OpenGLBinder.glutInitDisplayMode(OpenGLBinder.GLUT_DOUBLE | OpenGLBinder.GLUT_DEPTH);
            OpenGLBinder.glutInitWindowSize(Width, Height);
            OpenGLBinder.glutCreateWindow("Mi ventana");

            // provide the Glut callbacks that are necessary for running this tutorial
            OpenGLBinder.glutIdleFunc(OnRenderFrame);
            OpenGLBinder.glutDisplayFunc(OnDisplay);
            OpenGLBinder.glutCloseFunc(OnClose);
            
            
            
            
            // compilo los shaders
            myProgram = new ShaderProgram(Shaders.BasicVert, Shaders.BasicFrag);
            
            

            
            

            // create a triangle
            var triangleVertices = new VBO<Vector3>(new[] {new Vector3(0, 1, 0), new Vector3(-1, -1, 0), new Vector3(1, -1, 0)});
            var trianglePolygons = new VBO<uint>(new uint[] {0, 1, 2}, BufferTarget.ElementArrayBuffer);

            // create a square
            var squareVertices = new VBO<Vector3>(new Vector3[]
                {new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0)});
            var squarePolygons = new VBO<uint>(new uint[] {0, 1, 2, 0 , 2 , 3}, BufferTarget.ElementArrayBuffer);


            var triangleObject = new SceneObject
            {
                Position = new Vector3(-1.5f, 0, 0),
                Program = myProgram,
                Vertices = triangleVertices,
                Polygons = trianglePolygons,
                
                
            };
            
            var squareObject = new SceneObject
            {
                Position = new Vector3(1.5f, 0, 0),
                Program = myProgram,
                Vertices = squareVertices,
                Polygons = squarePolygons,
                
            };
            
            objects.Add(triangleObject);
            objects.Add(squareObject);


            OpenGLBinder.glutMainLoop();
        }

     
     
     
     

        private void OnClose()
        {
            objects.ForEach(o=>o.Dispose());
            
            myProgram.DisposeChildren = true;
            myProgram.Dispose();
        }

        private void OnDisplay()
        {

        }

        private void OnRenderFrame()
        {
            // set up the OpenGL viewport and clear both the color and depth bits
            Gl.Viewport(0, 0, Width, Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            
            

            foreach (var currentObject in objects)
            {
                
                var program = currentObject.Program;
                
                // bindeo el programa
                program.Use();
                

                // cargo los uniforms
                program["projectionMatrix"].SetValue(cam.ProjectionMatrix);
                program["viewMatrix"].SetValue(cam.ViewMatrix);
                program["modelMatrix"].SetValue(currentObject.ModelMatrix);
                
                
                var vertexPositionIndex = (uint) Gl.GetAttribLocation(program.ProgramID, "vertexPosition");
            
                Gl.EnableVertexAttribArray(vertexPositionIndex);
                Gl.BindBuffer(currentObject.Vertices);
                Gl.VertexAttribPointer(vertexPositionIndex, currentObject.Vertices.Size, currentObject.Vertices.PointerType, true, 12, IntPtr.Zero);
                Gl.BindBuffer(currentObject.Polygons);

                // draw the triangle
                Gl.DrawElements(BeginMode.Triangles, currentObject.Polygons.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            }

            OpenGLBinder.glutSwapBuffers();
        }

    }
}