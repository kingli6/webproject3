using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace College_API.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        public Task<IActionResult> RegisterForCourse(int courseId)
        {
            throw new NotImplementedException();
        }
    }
}