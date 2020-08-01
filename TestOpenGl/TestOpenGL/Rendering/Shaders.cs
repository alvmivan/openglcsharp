namespace TestOpenGL.Rendering
{
    public class Shaders
    {
        public const string BasicVert = @"
#version 130

in vec3 vertexPosition;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

out vec3 pos;

void main(void)
{
    pos = vertexPosition;
    gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(vertexPosition, 1);
}
";

        public const string BasicFrag = @"
#version 130

out vec4 fragment;
in vec3 pos;
void main(void)
{
    fragment = vec4( pos * 0.5 + 0.5 , 1);
}
";
    }
}
    
