namespace CrudTZ.Dto.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
    }
}
