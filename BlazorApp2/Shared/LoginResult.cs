﻿namespace BlazorApp2.Shared
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
