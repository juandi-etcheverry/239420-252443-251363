﻿using System;
namespace Domain
{
    public class SessionToken
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public Purchase? Cart { get; set; }
    }
}