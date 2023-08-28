
namespace JobOffers.Models
{
    public class ApiResponse
    {
        // 200 means Success 
        public object Data { get; set; } 
        // 500 means Internal Server Error.
        public object Errors { get; set; }
        public string StatusCode { get; set; }


    }
}
