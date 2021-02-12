#version 330 core
layout (location = 0) in vec3 aPos;
//layout (location = 1) in vec3 inColor;
out vec4 color;
uniform mat4 model;
//uniform mat4 view;
//uniform mat4 projection;
uniform vec4 materialColor;

void main()
{
   //gl_Position = projection * view * model * vec4(aPos, 1.0f);
   gl_Position = model * vec4(aPos, 1.0f);
   //color = vec4(0.5, 0.5, 0.5 , 1.0);
   //color = vec4(aPos, 1.0);
   color = materialColor;
}