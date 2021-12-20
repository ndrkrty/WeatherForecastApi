namespace WeatherApi.Infrastructure.ServiceInterfaces
{
    public class ServiceResult<T>
    {
        public ServiceResult(T sonuc, ServiceResultStatusEnum durum, string mesaj = null)
        {
            Status = durum;
            Result = sonuc;
            Message = mesaj;
        }

        public ServiceResultStatusEnum Status { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
