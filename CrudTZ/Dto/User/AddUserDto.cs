namespace CrudTZ.Dto.User
{
    public class AddUserDto
    {
        
        public string NickName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
    }
}
