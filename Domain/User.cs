using System;
namespace Domain
{
	public class User
	{
		public int Id { get; private set; }
		public string Email { get; set; }
		public Role Role { get; set; }
		public string Address { get; set; }
		public bool IsDeleted { get; set; }
	}
    public enum Role
    {
        Comprador = 1,
        Administrador = 2
    };
}


