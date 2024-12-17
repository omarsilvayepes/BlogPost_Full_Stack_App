namespace CodePulse.API.Models.DTO
{
    public class RegisterResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
