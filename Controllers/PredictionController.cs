using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using WJDemoService;

namespace WJWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;
        public PredictionController(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        /// <summary>
        /// 获取预测值
        /// </summary>
        /// <param name="input">输入的值</param>
        /// <returns></returns>
        #region
        ///        {
            //    "MACH_XCOR" : -447.216,
            //    "MACH_YCOR" : -561.537,
            //    "MACH_ZCOR" : 1896670,
            //    "ABS_XCOR" : 13.12,           
            //    "ABS_YCOR" : -11.092,
            //    "ABS_ZCOR" : 1896670,
            //    "RELV_XCOR" : -447.204,
            //    "RELV_YCOR" : -561.504,
            //    "RELV_ZCOR": 1896670,
            //    "DIST_XCOR" : 0,
            //    "DIST_YCOR": -2.828,
            //    "DIST_ZCOR": 1896670,
            //    "WORK_ONCE_TIME": 1,
            //    "WORK_ALL_TIME":144675
            //}
        #endregion
        [HttpGet]
        [Route("GetPrediction")]
        public HttpResponseMessage GetPrediction([FromBody]ModelInput input)
        {
            ComWebResponseEntity<string> result = new ComWebResponseEntity<string>();
            ModelOutput prediction = _predictionEnginePool.Predict(modelName: "MLModel", example:input);
            result.Result = true;
            result.Content = prediction.Prediction +"       准确度：" +prediction.Score[0].ToString();
            string jsonResult = result.ToJson();
            return new HttpResponseMessage { Content = new StringContent(jsonResult, Encoding.UTF8, "application/json") };
        }
        [HttpGet]
        [Route("GetPredict")]
        public string GetPredict([FromBody] ModelInput input)
        {
            ComWebResponseEntity<string> result = new ComWebResponseEntity<string>();

            ModelOutput prediction = _predictionEnginePool.Predict(modelName: "MLModel", example: input);
            result.Result = true;
            result.Content = prediction.Prediction + "       准确度：" + prediction.Score[0].ToString();
            string jsonResult = result.ToJson();
            return jsonResult;

        }
    }
}
