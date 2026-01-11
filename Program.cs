//*****************************************************************************
//** 85. Maximal Rectangle                                          leetcode **
//*****************************************************************************
//** Stacked Quietly Using Arrays Rectangles Emerge                          **
//** Heights rise row by row until areas lock into a perfect SQUARE          **
//** Bars fall to a monotone stack, widths stretched by careful release      **
//**From binary grids, the largest truth stands clean, fast, complete        **
//*****************************************************************************

int maximalRectangle(char** matrix, int matrixSize, int* matrixColSize) {
    int rows = matrixSize;
    int cols = matrixColSize[0];
    int max_area = 0;

    int* heights = (int*)calloc(cols, sizeof(int));
    int* stackIdx = (int*)malloc(sizeof(int) * cols);
    int* stackHeight = (int*)malloc(sizeof(int) * cols);

    int r, c;

    for (r = 0; r < rows; r++)
    {
        for (c = 0; c < cols; c++)
        {
            if (matrix[r][c] == '1')
            {
                heights[c] += 1;
            }
            else
            {
                heights[c] = 0;
            }
        }

        /* Monotonic stack for largest rectangle in histogram */
        int top = -1;

        for (c = 0; c < cols; c++)
        {
            int start = c;

            while (top >= 0 && stackHeight[top] > heights[c])
            {
                int height = stackHeight[top];
                int index = stackIdx[top--];

                int area = height * (c - index);
                if (area > max_area)
                {
                    max_area = area;
                }

                start = index;
            }

            stackIdx[++top] = start;
            stackHeight[top] = heights[c];
        }

        /* Flush remaining stack */
        while (top >= 0)
        {
            int height = stackHeight[top];
            int index = stackIdx[top--];

            int area = height * (cols - index);
            if (area > max_area)
            {
                max_area = area;
            }
        }
    }

    free(heights);
    free(stackIdx);
    free(stackHeight);

    return max_area;
}