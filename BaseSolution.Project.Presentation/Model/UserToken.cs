﻿using Microsoft.AspNetCore.Identity;

namespace BaseSolution.Project.Presentation.Model
{
    public class UserToken
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Role { get; set; }
        public string Token { get; set; }
    }
}
