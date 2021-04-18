using System.Linq;
using UnityEngine;
using Unity.Barracuda;

sealed class MnistTest : MonoBehaviour
{
    public NNModel _model;
    public Texture2D _image;

    void Start()
    {
        // Convert the input image to a 1x28x28x1 tensor.
        using var input = new Tensor(1, 28, 28, 1);

        for (var y = 0; y < 28; y++)
        {
            for (var x = 0; x < 28; x++)
            {
                var tx = x * _image.width  / 28;
                var ty = y * _image.height / 28;
                input[0, 27 - y, x, 0] = _image.GetPixel(tx, ty).grayscale;
            }
        }

        // Run the MNIST model.
        using var worker = ModelLoader.Load(_model).CreateWorker();

        worker.Execute(input);

        // Inspect the output tensor.
        var output = worker.PeekOutput();

        var scores = Enumerable.Range(0, 10).
                     Select(i => output[0, 0, 0, i]).SoftMax().ToArray();

        for (var i = 0; i < 10; i++) Debug.Log($"{i}: {scores[i]:0.00}");
    }
}
