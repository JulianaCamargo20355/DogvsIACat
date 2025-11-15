using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNet
{
    float[,] W1;   // pesos da entrada → camada escondida
    float[,] W2;   // pesos da camada escondida → saída
    float lr = 0.05f; // taxa de aprendizado (learning rate)

    int inputSize;
    int hiddenSize;
    int outputSize;

    public NeuralNet(int input, int hidden, int output)
    {
        // Tamanhos da rede
        inputSize = input;
        hiddenSize = hidden;
        outputSize = output;

        // Matrizes de pesos
        W1 = new float[input, hidden];
        W2 = new float[hidden, output];

        Randomize(W1);
        Randomize(W2);
    }

    // inicializa pesos com valores aleatórios
    void Randomize(float[,] w)
    {
        for (int i = 0; i < w.GetLength(0); i++)
        {
            for (int j = 0; j < w.GetLength(1); j++)
            {
                w[i, j] = Random.Range(-1f, 1f);
            }
        }
    }

    // Função de ativação 
    float Tanh(float x)
    {
        return (float)System.Math.Tanh(x);
    }

    // cálculo da rede (forward-pass)
    float[] Activate(float[] x)
    {
        float[] h = new float[hiddenSize];
        float[] o = new float[outputSize];

        // Calculo camada escondida
        for (int j = 0; j < hiddenSize; j++)
        {
            float sum = 0f;
            for (int i = 0; i < inputSize; i++)
            {
                sum += x[i] * W1[i, j];
            }
            h[j] = Tanh(sum);
        }

        // Calculo camada de saída
        for (int j = 0; j < outputSize; j++)
        {
            float sum = 0f;
            for (int i = 0; i < hiddenSize; i++)
            {
                sum += h[i] * W2[i, j];
            }
            o[j] = Tanh(sum);
        }

        return o;
    }

    public float[] FeedForward(float[] x)
    {
        return Activate(x);
    }

    // Treinamento simples 
    public void Train(float[] input, float[] target)
    {
        float[] output = FeedForward(input);

        for (int i = 0; i < outputSize; i++)
        {
            float error = target[i] - output[i];

            for (int j = 0; j < hiddenSize; j++)
            {
                W2[j, i] += lr * error;
            }
        }
    }
}
