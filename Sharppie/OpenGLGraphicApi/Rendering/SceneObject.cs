using OpenGL;

namespace TestOpenGL.Rendering
{
    public class SceneObject
    {
        
        
        public ShaderProgram Program;
        public VBO<Vector3> Vertices;
        public VBO<uint> Polygons;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public Matrix4 ModelMatrix
        {
            get
            {
                var translation = Matrix4.CreateTranslation(Position);
                var rotationX = Matrix4.CreateRotationX(Rotation.X);
                var rotationY = Matrix4.CreateRotationY(Rotation.Y);
                var rotationZ = Matrix4.CreateRotationZ(Rotation.Z);
                var rotation = rotationX * rotationY * rotationZ;
                var scale = Matrix4.CreateScaling(Scale);
                return translation * rotation * scale;
            }
        }



        public void Dispose()
        {
            Polygons.Dispose();
            Vertices.Dispose();
        }
        
    }
}