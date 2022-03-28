using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLayer.Models
{
    public class ResponseModel<T>
    {
        public bool IsError { get; set; }
        public int totalRecords { get; set; }
        public IEnumerable<T> resultList { get; set; }
        public T result { get; set; }
        public string message { get; set; }
    }
    public class ResponseMethod<T> where T : class
    {
        ResponseModel<T> _response;
        public ResponseMethod()
        {
            
        }
        public ResponseModel<T> AddResponse(bool IsError,int totalRecords,IEnumerable<T> resultList,T result,string message)
        {
            _response = new ResponseModel<T>();
            _response.IsError = IsError;
            _response.result = result;
            _response.message = message;
            _response.resultList = resultList;
            _response.totalRecords = totalRecords;
            return _response;
        }

    }
}
