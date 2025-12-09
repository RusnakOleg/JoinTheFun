using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Models
{
    public class ToxicityPredictor
    {
        private readonly InferenceSession _session;

        public ToxicityPredictor(string modelPath)
        {
            _session = new InferenceSession(modelPath);
        }

        private string CleanText(string text)
        {
            text = text.ToLower();

            // ПЕРЕЛІК УСІХ УКРАЇНСЬКИХ ЛІТЕР
            text = Regex.Replace(text, @"[^a-z0-9абвгґдеєжзиіїйклмнопрстуфхцчшщьюя!?\*@#%'\-\s]", " ");

            text = Regex.Replace(text, @"\s+", " ").Trim();
            return text;
        }


        public int Predict(string text)
        {
            text = CleanText(text);

            var inputTensor = new DenseTensor<string>(new[] { text }, new[] { 1, 1 });

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", inputTensor)
            };

            using var outputs = _session.Run(inputs);
            var prediction = outputs.First().AsEnumerable<long>().First();

            return (int)prediction;
        }
    }
}
