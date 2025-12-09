using JoinTheFun.BLL.Models;
using JoinTheFun.BLL.Services.Interfaces.JoinTheFun.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class ToxicityService : IToxicityService
    {
        private readonly ToxicityPredictor _predictor;

        public ToxicityService(string modelPath)
        {
            _predictor = new ToxicityPredictor(modelPath);
        }

        public int CheckComment(string comment)
        {
            return _predictor.Predict(comment);
        }
    }
}
