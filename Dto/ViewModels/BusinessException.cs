using Newtonsoft.Json;


namespace Dto.ViewModels
{
    public class BusinessException : Exception
    {
        public BusinessException(string val) : base(val)
        {

        }
        public BusinessException(object val) : base(JsonConvert.SerializeObject(val))
        {

        }
    }
}
