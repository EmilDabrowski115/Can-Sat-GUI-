#version 330 core
out vec4 FragColor;
in vec4 color;
// uniform vec4 selectedColor;
void main()
{
   FragColor = color;
}