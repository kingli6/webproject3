using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace College_API.Interfaces
{
    public interface IRegistrationRepository
    {
        public Task<IActionResult> RegisterForCourse(int courseId);
    }
}