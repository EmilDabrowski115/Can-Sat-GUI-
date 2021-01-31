#version 330 core
layout (location = 0) in vec3 aPos;
//layout (location = 1) in vec3 inColor;
out vec4 color;
//uniform mat4 model;
//uniform mat4 view;
//uniform mat4 projection;

void main()
{
   //gl_Position = projection * view * model * vec4(aPos, 1.0f);
   gl_Position = vec4(aPos, 1.0f);
   //color = vec4(inColor, 1.0);
   color = vec4(aPos, 1.0);
}