using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class SoftmaxLinqExtension
{
    public static IEnumerable<float> SoftMax(this IEnumerable<float> source)
    {
        var exp = source.Select(x => Mathf.Exp(x)).ToArray();
        var sum = exp.Sum();
        return exp.Select(x => x / sum);
    }
}
