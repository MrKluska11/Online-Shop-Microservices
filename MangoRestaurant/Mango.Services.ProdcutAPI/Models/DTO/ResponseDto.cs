namespace Mango.Services.ProdcutAPI.Models.DTO
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }  //we don't know returned type, so this is obejct type
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
