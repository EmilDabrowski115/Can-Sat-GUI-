#define ARR_SIZE(x) (sizeof(x) / sizeof(*(x)))

GLFWwindow *setupWindow();
void framebuffer_size_callback(GLFWwindow* window, int width, int height);
void processInput(GLFWwindow *window);
unsigned int compileVertexShader(const char source[]);
unsigned int compileFragmentShader(const char source[]);
unsigned int createProgramFromShaders(unsigned int *shaders, size_t size);
unsigned int createTriangleVAO(float *vertices, size_t size);
char *fileToString(const char *filename);

int main()
{   
    GLFWwindow *window = setupWindow();

    // compile and link shaders together
    const char *vertexShaderSource = fileToString("./src/shader.vs");
    const char *fragmentShaderSource = fileToString("./src/shader.fs");

    unsigned int shaders[] = {
        compileVertexShader(vertexShaderSource), 
        compileFragmentShader(fragmentShaderSource)
    };
    unsigned int shaderProgram = createProgramFromShaders(shaders, ARR_SIZE(shaders));
    for (int i = 0; i < ARR_SIZE(shaders); i++){
        glDeleteShader(shaders[i]);
    }

    // create VAO for given triangle data
    // float triangleVertices[] = {
    //     // vertices
    //    -0.5f, -0.5f, 0.0f,
    //     0.5f, -0.5f, 0.0f,
    //     0.0f,  0.5f, 0.0f,
    // }; 
    float vertices[] = {
        -0.5f, -0.5f, -0.5f,
        0.5f, -0.5f, -0.5f,
        0.5f,  0.5f, -0.5f,
        0.5f,  0.5f, -0.5f,
        -0.5f,  0.5f, -0.5f,
        -0.5f, -0.5f, -0.5f,

        -0.5f, -0.5f,  0.5f,
        0.5f, -0.5f,  0.5f,
        0.5f,  0.5f,  0.5f,
        0.5f,  0.5f,  0.5f,
        -0.5f,  0.5f,  0.5f,
        -0.5f, -0.5f,  0.5f,

        -0.5f,  0.5f,  0.5f,
        -0.5f,  0.5f, -0.5f,
        -0.5f, -0.5f, -0.5f,
        -0.5f, -0.5f, -0.5f,
        -0.5f, -0.5f,  0.5f,
        -0.5f,  0.5f,  0.5f,

        0.5f,  0.5f,  0.5f,
        0.5f,  0.5f, -0.5f,
        0.5f, -0.5f, -0.5f,
        0.5f, -0.5f, -0.5f,
        0.5f, -0.5f,  0.5f,
        0.5f,  0.5f,  0.5f,

        -0.5f, -0.5f, -0.5f,
        0.5f, -0.5f, -0.5f,
        0.5f, -0.5f,  0.5f,
        0.5f, -0.5f,  0.5f,
        -0.5f, -0.5f,  0.5f,
        -0.5f, -0.5f, -0.5f,

        -0.5f,  0.5f, -0.5f,
        0.5f,  0.5f, -0.5f,
        0.5f,  0.5f,  0.5f,
        0.5f,  0.5f,  0.5f,
        -0.5f,  0.5f,  0.5f,
        -0.5f,  0.5f, -0.5f
    };
    unsigned int VAO = createTriangleVAO(vertices, ARR_SIZE(vertices));

    glEnable(GL_DEPTH_TEST);  


    while(!glfwWindowShouldClose(window))
    {   
        processInput(window);

        // clear background 
        glClearColor(0.7f, 0.2f, 0.1f, 1.0f);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        glUseProgram(shaderProgram);

        // model
        mat4 model = GLM_MAT4_IDENTITY_INIT;
        float rads = glm_rad(-55.0f);
        vec3 axis = {1.0f, 0.0f, 0.0f};
        glm_rotate(model, rads, axis);
        int modelLoc = glGetUniformLocation(shaderProgram, "model");
        glUniformMatrix4fv(modelLoc, 1, GL_FALSE, (const GLfloat*)model);

        // view
        mat4 view = GLM_MAT4_IDENTITY_INIT;
        // note that we're translating the scene in the reverse direction of where we want to move
        vec3 translate_vec = {0.0f, 0.0f, -3.0f};
        glm_translate(view, translate_vec);
        int viewLoc = glGetUniformLocation(shaderProgram, "view");
        glUniformMatrix4fv(viewLoc, 1, GL_FALSE, (const GLfloat*)view);

        // projection
        // mat4 projection = GLM_MAT4_IDENTITY_INIT;
        mat4 projection;
        glm_perspective(glm_rad(45.0f), 800.0f / 600.0f, 0.1f, 100.0f, projection);
        int projectionLoc = glGetUniformLocation(shaderProgram, "projection");
        glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, (const GLfloat*)projection);


        // draw triangles
        glBindVertexArray(VAO);
        glDrawArrays(GL_TRIANGLES, 0, 36);

        glfwSwapBuffers(window);
        glfwPollEvents();
    }

    glfwTerminate();
    return 0;
}

// mat4 * transform()
// {
//         mat4 * mat = malloc(sizeof(mat4));
//         mat4 tmpmat = GLM_MAT4_IDENTITY_INIT; // matrix that will hold result of transformations
//         memcpy(mat, &tmpmat, sizeof(mat4));

//         float time = glfwGetTime();
        
//         // scale
//         vec3 scale = {1.0f, 1.0f, 1.0f};
//         glm_scale(mat, scale);

//         // rotate
//         float speed = 50.0f;
//         float precision = 60.0f;
//         float rads = glm_rad((float)((int)(time*speed*precision) % (int)(360*precision)) / (precision));
//         vec3 axis = {0.0f, 0.0f, 1.0f};
//         glm_rotate(mat, rads, axis);

//         // translate
//         vec3 translate_vec = {0.0f, 0.0f, 0.0f};
//         glm_translate(mat, translate_vec);

//         return mat;
// }


void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
    glViewport(0, 0, width, height);
}


void processInput(GLFWwindow *window)
{
    if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
        glfwSetWindowShouldClose(window, GL_TRUE);
    if (glfwGetKey(window, GLFW_KEY_1) == GLFW_PRESS)
        glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
    if (glfwGetKey(window, GLFW_KEY_2) == GLFW_PRESS)
        glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
}


unsigned int createTriangleVAO(float vertices[], size_t size)
{
    // bind arrays and buffers
    unsigned int VAO; // vertex array object
    unsigned int VBO; // vertex buffer object
    glGenVertexArrays(1, &VAO);  
    glGenBuffers(1, &VBO); 
    
    // use this for object context (VAO) so we can configure it
    glBindVertexArray(VAO); 

    // bind buffer and static vertices data to current VAO
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, size * sizeof(float), vertices, GL_STATIC_DRAW);

    // linking vertex attributes to current VAO
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(0);
    // glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)0);
    // glEnableVertexAttribArray(0);

    // glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)(3 * sizeof(float)));
    // glEnableVertexAttribArray(1);

    // now we can unbind vbo
    glBindBuffer(GL_ARRAY_BUFFER, 0);
    glBindVertexArray(0);

    return VAO;
}