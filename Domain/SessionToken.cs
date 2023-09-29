using System;
namespace Domain
{
    public class SessionToken
    {
        public Guid Id { get; private set; }
        public User? User { get; set; }
    }
}