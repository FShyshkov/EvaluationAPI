using Microsoft.AspNetCore.Mvc;

namespace EvaluationAPI.Presenters
{
    public sealed class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}
