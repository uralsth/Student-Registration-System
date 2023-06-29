using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Application.DataTransferObject
{
	public class StudentProjectEditDto : StudentProjectCreateDto
	{
        public int? Id { get; set; }
        public int ProjectId { get; set; }
    }
}
